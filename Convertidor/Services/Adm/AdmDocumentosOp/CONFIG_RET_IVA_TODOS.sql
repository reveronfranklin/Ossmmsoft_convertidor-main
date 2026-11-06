-- Oracle 10g. Ejecutar con SQL*Plus/SQLcl o como script en SQL Developer.
-- Completa TODAS las alicuotas IVA de la familia del impuesto de referencia.
-- Las combina con TODOS los estatus fiscales que ya tienen plantilla IVA.
-- 672 y 664 solo identifican familias; no limitan el proceso a 8% / 100%.
-- Idempotente. Conserva registros existentes; aborta ante ambiguedad.
-- Omite porcentajes fuera de 0..100 o no numericos y los informa por ID/valor.
-- Nunca aplica ABS ni convierte valores negativos en alicuotas positivas.
-- No modifica documentos, OP, comprobantes ni importes historicos.
-- Revisar salida y consulta final; confirmar con COMMIT o deshacer con ROLLBACK.
-- Los bloqueos de catalogos se mantienen hasta COMMIT/ROLLBACK.
SET SERVEROUTPUT ON;
DECLARE
    v_imp_ref NUMBER := 672;
    v_fis_ref NUMBER := 664;
    v_usuario NUMBER := NULL; -- Opcional; NULL conserva usuario de la plantilla.
    v_tipo_tpl NUMBER := NULL; -- Opcional: tipo IVA que aporta las plantillas.
    v_tit_imp NUMBER;
    v_tit_fis NUMBER;
    v_tit_ret NUMBER;
    v_base_tipo NUMBER;
    v_count NUMBER;
    v_tasa NUMBER;
    v_val NUMBER;
    v_por NUMBER;
    v_tipo NUMBER;
    v_concepto NUMBER;
    v_base NUMBER;
    v_nuevo NUMBER;
    v_texto VARCHAR2(100);
    v_n_tipos NUMBER := 0;
    v_n_conc NUMBER := 0;
    v_n_tasas NUMBER := 0;
    v_n_pares NUMBER := 0;
    v_n_omit NUMBER := 0;
    TYPE tasas_vistas IS TABLE OF BOOLEAN INDEX BY VARCHAR2(100);
    v_vistas tasas_vistas;

    FUNCTION porcentaje(p_texto VARCHAR2) RETURN NUMBER IS
        v_num NUMBER;
    BEGIN
        IF NOT REGEXP_LIKE(NVL(TRIM(p_texto), '?'), '^[+-]?[0-9]+([.,][0-9]+)?$') THEN
            RETURN NULL;
        END IF;
        -- Los valores negativos pueden representar codigos legacy; no son tasas.
        IF SUBSTR(TRIM(p_texto), 1, 1) = '-' THEN
            RETURN NULL;
        END IF;
        v_num := TO_NUMBER(REPLACE(LTRIM(TRIM(p_texto), '+'), ',', '.'),
            '999D999999999999999999999999999', 'NLS_NUMERIC_CHARACTERS=''.,''');
        IF v_num < 0 OR v_num > 100 THEN
            RETURN NULL;
        END IF;
        RETURN v_num;
    EXCEPTION
        WHEN VALUE_ERROR OR INVALID_NUMBER THEN RETURN NULL;
    END;

    FUNCTION tasa_tipo(p_desc VARCHAR2, p_extra VARCHAR2) RETURN NUMBER IS
        v_pct VARCHAR2(100);
        v_num_pct NUMBER;
    BEGIN
        v_num_pct := NVL(LENGTH(p_desc), 0) - NVL(LENGTH(REPLACE(p_desc, '%', '')), 0);
        IF v_num_pct = 1 THEN
            v_pct := REGEXP_REPLACE(REGEXP_SUBSTR(p_desc,
                '[+-]?[0-9]+([.,][0-9]+)?[[:space:]]*%'), '[[:space:]%]', '');
        ELSIF v_num_pct = 0 THEN
            v_pct := TRIM(p_extra);
        ELSE
            RETURN NULL;
        END IF;
        RETURN porcentaje(v_pct);
    EXCEPTION
        WHEN OTHERS THEN RETURN NULL; -- Sin alicuota interpretable: no es candidato.
    END;
BEGIN
    SAVEPOINT cfg_iva_todos;
    SELECT TITULO_ID INTO v_tit_imp FROM ADM.ADM_DESCRIPTIVAS
     WHERE DESCRIPCION_ID = v_imp_ref;
    SELECT TITULO_ID INTO v_tit_fis FROM ADM.ADM_DESCRIPTIVAS
     WHERE DESCRIPCION_ID = v_fis_ref;
    IF v_tit_imp IS NULL OR v_tit_fis IS NULL THEN
        RAISE_APPLICATION_ERROR(-20003, 'Las referencias deben tener titulo de catalogo');
    END IF;

    LOCK TABLE ADM.ADM_DESCRIPTIVAS IN EXCLUSIVE MODE;
    LOCK TABLE ADM.ADM_RETENCIONES IN EXCLUSIVE MODE;
    SELECT COUNT(DISTINCT D.TITULO_ID), MIN(D.TITULO_ID)
      INTO v_count, v_tit_ret
      FROM ADM.ADM_RETENCIONES R
      JOIN ADM.ADM_DESCRIPTIVAS D ON D.DESCRIPCION_ID = R.TIPO_RETENCION_ID
      JOIN ADM.ADM_DESCRIPTIVAS F ON F.CODIGO = R.EXTRA1 AND F.TITULO_ID = v_tit_fis
     WHERE D.CODIGO = 'IVA';
    IF v_count <> 1 THEN
        RAISE_APPLICATION_ERROR(-20004, 'No hay un unico titulo de retenciones IVA para la familia fiscal');
    END IF;
    IF v_tit_ret = v_tit_imp THEN
        RAISE_APPLICATION_ERROR(-20005, 'Las familias de impuesto y retencion no deben coincidir');
    END IF;

    SELECT MIN(DESCRIPCION_ID) INTO v_base_tipo FROM ADM.ADM_DESCRIPTIVAS
     WHERE CODIGO = 'IVA' AND TITULO_ID = v_tit_ret;
    IF v_tipo_tpl IS NOT NULL THEN
        SELECT COUNT(*) INTO v_count FROM ADM.ADM_DESCRIPTIVAS
         WHERE DESCRIPCION_ID = v_tipo_tpl AND CODIGO = 'IVA' AND TITULO_ID = v_tit_ret;
        IF v_count <> 1 THEN
            RAISE_APPLICATION_ERROR(-20006, 'El tipo plantilla no pertenece al titulo de retenciones IVA');
        END IF;
        v_base_tipo := v_tipo_tpl;
    END IF;

    FOR imp IN (
        SELECT DESCRIPCION_ID, DESCRIPCION, EXTRA1 FROM ADM.ADM_DESCRIPTIVAS
         WHERE TITULO_ID = v_tit_imp
           AND (CODIGO = 'IVA' OR REGEXP_LIKE(DESCRIPCION,
                '(^|[^[:alpha:]])I[.]?[[:space:]]*V[.]?[[:space:]]*A[.]?([^[:alpha:]]|$)', 'i'))
         ORDER BY DESCRIPCION_ID
    ) LOOP
        v_tasa := porcentaje(imp.EXTRA1);
        IF v_tasa IS NULL THEN
            v_n_omit := v_n_omit + 1;
            DBMS_OUTPUT.PUT_LINE('OMITIDO impuesto ID ' || imp.DESCRIPCION_ID ||
                '; EXTRA1=[' || NVL(imp.EXTRA1, 'NULL') || ']; no es alicuota valida 0..100.');
        ELSE
            v_texto := TO_CHAR(v_tasa, 'TM9', 'NLS_NUMERIC_CHARACTERS=''.,''');
            IF NOT v_vistas.EXISTS(v_texto) THEN
                v_vistas(v_texto) := TRUE;
                v_n_tasas := v_n_tasas + 1;
                v_tipo := NULL;
                v_count := 0;
                FOR tip IN (
                    SELECT DESCRIPCION_ID, DESCRIPCION, EXTRA1 FROM ADM.ADM_DESCRIPTIVAS
                     WHERE CODIGO = 'IVA' AND TITULO_ID = v_tit_ret
                     ORDER BY DESCRIPCION_ID
                ) LOOP
                    v_val := tasa_tipo(tip.DESCRIPCION, tip.EXTRA1);
                    IF v_val = v_tasa THEN
                        v_count := v_count + 1;
                        v_tipo := tip.DESCRIPCION_ID;
                    END IF;
                END LOOP;
                IF v_count > 1 THEN
                    RAISE_APPLICATION_ERROR(-20007, 'Tipos IVA duplicados para alicuota ' || v_texto || '%');
                END IF;
                IF v_tipo IS NULL THEN
                    SELECT NVL(MAX(DESCRIPCION_ID), 0) + 1 INTO v_tipo FROM ADM.ADM_DESCRIPTIVAS;
                    INSERT INTO ADM.ADM_DESCRIPTIVAS (
                        DESCRIPCION_ID, DESCRIPCION_FK_ID, TITULO_ID, DESCRIPCION, CODIGO,
                        EXTRA1, EXTRA2, EXTRA3, USUARIO_INS, FECHA_INS,
                        USUARIO_UPD, FECHA_UPD, CODIGO_EMPRESA
                    )
                    SELECT v_tipo, DESCRIPCION_FK_ID, TITULO_ID, 'I.V.A. (' || v_texto || '%)', 'IVA',
                           v_texto, EXTRA2, EXTRA3, NVL(v_usuario, USUARIO_INS), SYSDATE,
                           NULL, NULL, CODIGO_EMPRESA
                      FROM ADM.ADM_DESCRIPTIVAS WHERE DESCRIPCION_ID = v_base_tipo;
                    IF SQL%ROWCOUNT <> 1 THEN
                        RAISE_APPLICATION_ERROR(-20008, 'No se pudo crear tipo IVA ' || v_texto || '%');
                    END IF;
                    v_n_tipos := v_n_tipos + 1;
                END IF;

                FOR fis IN (
                    SELECT F.DESCRIPCION_ID, F.CODIGO, F.EXTRA1 FROM ADM.ADM_DESCRIPTIVAS F
                     WHERE F.TITULO_ID = v_tit_fis AND F.CODIGO IS NOT NULL
                       AND EXISTS (
                           SELECT 1 FROM ADM.ADM_RETENCIONES R JOIN ADM.ADM_DESCRIPTIVAS D
                             ON D.DESCRIPCION_ID = R.TIPO_RETENCION_ID
                           WHERE R.EXTRA1 = F.CODIGO AND D.CODIGO = 'IVA' AND D.TITULO_ID = v_tit_ret)
                     ORDER BY F.DESCRIPCION_ID
                ) LOOP
                    v_por := porcentaje(fis.EXTRA1);
                    IF v_por IS NULL THEN
                        DBMS_OUTPUT.PUT_LINE('OMITIDO estatus ID ' || fis.DESCRIPCION_ID ||
                            '; EXTRA1=[' || NVL(fis.EXTRA1, 'NULL') || ']; porcentaje fuera de 0..100 o no numerico.');
                    ELSIF v_por > 0 THEN
                        SELECT COUNT(*), MIN(R.CODIGO_RETENCION) INTO v_count, v_base
                          FROM ADM.ADM_RETENCIONES R JOIN ADM.ADM_DESCRIPTIVAS D
                            ON D.DESCRIPCION_ID = R.TIPO_RETENCION_ID
                         WHERE R.EXTRA1 = fis.CODIGO AND D.CODIGO = 'IVA' AND D.TITULO_ID = v_tit_ret
                           AND (v_tipo_tpl IS NULL OR D.DESCRIPCION_ID = v_tipo_tpl);
                        IF v_count = 0 THEN
                            RAISE_APPLICATION_ERROR(-20009, 'Sin plantilla IVA para estatus ' || fis.DESCRIPCION_ID);
                        END IF;
                        -- MIN solo elige despues de comprobar equivalencia de las plantillas.
                        SELECT COUNT(*) INTO v_count
                          FROM ADM.ADM_RETENCIONES R
                          JOIN ADM.ADM_DESCRIPTIVAS D ON D.DESCRIPCION_ID = R.TIPO_RETENCION_ID
                          JOIN ADM.ADM_RETENCIONES B ON B.CODIGO_RETENCION = v_base
                         WHERE R.EXTRA1 = fis.CODIGO AND D.CODIGO = 'IVA' AND D.TITULO_ID = v_tit_ret
                           AND (v_tipo_tpl IS NULL OR D.DESCRIPCION_ID = v_tipo_tpl
                                OR R.TIPO_RETENCION_ID = v_tipo)
                           AND (DECODE(R.CONCEPTO_PAGO, B.CONCEPTO_PAGO, 0, 1) = 1
                             OR DECODE(R.TIPO_PERSONA_ID, B.TIPO_PERSONA_ID, 0, 1) = 1
                             OR DECODE(R.BASE_IMPONIBLE, B.BASE_IMPONIBLE, 0, 1) = 1
                             OR DECODE(R.MONTO_RETENCION, B.MONTO_RETENCION, 0, 1) = 1
                             OR DECODE(R.FECHA_INI, B.FECHA_INI, 0, 1) = 1
                             OR DECODE(R.FECHA_FIN, B.FECHA_FIN, 0, 1) = 1
                             OR DECODE(R.EXTRA2, B.EXTRA2, 0, 1) = 1
                             OR DECODE(R.EXTRA3, B.EXTRA3, 0, 1) = 1
                             OR DECODE(R.CODIGO_EMPRESA, B.CODIGO_EMPRESA, 0, 1) = 1
                             OR (R.POR_RETENCION IS NOT NULL AND R.POR_RETENCION <> v_por));
                        IF v_count <> 0 THEN
                            RAISE_APPLICATION_ERROR(-20010, 'Plantillas IVA incompatibles para estatus ' || fis.DESCRIPCION_ID);
                        END IF;

                        SELECT COUNT(*), MIN(CODIGO_RETENCION) INTO v_count, v_concepto
                          FROM ADM.ADM_RETENCIONES
                         WHERE TIPO_RETENCION_ID = v_tipo AND EXTRA1 = fis.CODIGO;
                        IF v_count > 1 THEN
                            RAISE_APPLICATION_ERROR(-20011, 'Conceptos duplicados: IVA ' || v_texto || '% / estatus ' || fis.DESCRIPCION_ID);
                        END IF;
                        IF v_concepto IS NULL THEN
                            SELECT NVL(MAX(CODIGO_RETENCION), 0) + 1 INTO v_nuevo FROM ADM.ADM_RETENCIONES;
                            INSERT INTO ADM.ADM_RETENCIONES (
                                CODIGO_RETENCION, TIPO_RETENCION_ID, CONCEPTO_PAGO, TIPO_PERSONA_ID,
                                BASE_IMPONIBLE, POR_RETENCION, MONTO_RETENCION, FECHA_INI, FECHA_FIN,
                                EXTRA1, EXTRA2, EXTRA3, USUARIO_INS, FECHA_INS,
                                USUARIO_UPD, FECHA_UPD, CODIGO_EMPRESA
                            )
                            SELECT v_nuevo, v_tipo, CONCEPTO_PAGO, TIPO_PERSONA_ID,
                                   BASE_IMPONIBLE, v_por, MONTO_RETENCION, FECHA_INI, FECHA_FIN,
                                   EXTRA1, EXTRA2, EXTRA3, NVL(v_usuario, USUARIO_INS), SYSDATE,
                                   NULL, NULL, CODIGO_EMPRESA
                              FROM ADM.ADM_RETENCIONES WHERE CODIGO_RETENCION = v_base;
                            IF SQL%ROWCOUNT <> 1 THEN
                                RAISE_APPLICATION_ERROR(-20012, 'No se pudo crear concepto IVA');
                            END IF;
                            v_n_conc := v_n_conc + 1;
                            v_concepto := v_nuevo;
                        END IF;
                        v_n_pares := v_n_pares + 1;
                        DBMS_OUTPUT.PUT_LINE('IVA ' || v_texto || '%; estatus ' || fis.DESCRIPCION_ID ||
                            '; retencion ' || v_por || '%; tipo ' || v_tipo || '; concepto ' || v_concepto);
                    END IF;
                END LOOP;
            END IF;
        END IF; -- alicuota valida
    END LOOP;
    IF v_n_tasas = 0 OR v_n_pares = 0 THEN
        RAISE_APPLICATION_ERROR(-20013, 'No se encontraron alicuotas IVA o plantillas fiscales aplicables');
    END IF;
    DBMS_OUTPUT.PUT_LINE('Alicuotas procesadas: ' || v_n_tasas || '; combinaciones: ' || v_n_pares);
    DBMS_OUTPUT.PUT_LINE('Impuestos omitidos por EXTRA1 invalido: ' || v_n_omit);
    DBMS_OUTPUT.PUT_LINE('Nuevos tipos: ' || v_n_tipos || '; nuevos conceptos: ' || v_n_conc);
    DBMS_OUTPUT.PUT_LINE('Revisar resultado y ejecutar COMMIT o ROLLBACK.');
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK TO cfg_iva_todos;
        DBMS_OUTPUT.PUT_LINE('Configuracion revertida: ' || SQLERRM);
        RAISE;
END;
/

SELECT D.DESCRIPCION_ID, D.TITULO_ID, D.DESCRIPCION, D.EXTRA1 ALICUOTA,
       R.CODIGO_RETENCION, R.CONCEPTO_PAGO, R.POR_RETENCION, R.EXTRA1 ESTATUS_FISCO
  FROM ADM.ADM_DESCRIPTIVAS D JOIN ADM.ADM_RETENCIONES R
    ON R.TIPO_RETENCION_ID = D.DESCRIPCION_ID
 WHERE D.CODIGO = 'IVA'
 ORDER BY D.TITULO_ID, D.DESCRIPCION_ID, R.EXTRA1, R.CODIGO_RETENCION;
