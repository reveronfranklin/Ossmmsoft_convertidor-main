-- Oracle 10g. Configuracion requerida por impuesto 672 / estatus fiscal 664.
-- Instalar antes de volver a cargar la factura con IVA 8% y retencion 100%.
-- Idempotente: reutiliza configuracion unica existente y rechaza duplicados.
-- No modifica el tipo 6%, documentos, OP ni comprobantes.
-- Completar v_usuario si se conoce el usuario aplicativo del operador.
-- Si se deja NULL se conserva el usuario de la plantilla de configuracion.
-- Al terminar revisar el resultado y ejecutar COMMIT o ROLLBACK.
SET SERVEROUTPUT ON;
DECLARE
    v_usuario NUMBER := NULL;
    v_alicuota VARCHAR2(100);
    v_por VARCHAR2(100);
    v_estatus VARCHAR2(100);
    v_titulo NUMBER;
    v_count NUMBER;
    v_tipo NUMBER;
    v_concepto NUMBER;
    v_base NUMBER;
    v_nuevo NUMBER;
BEGIN
    SAVEPOINT cfg_ret_iva;
    SELECT TRIM(EXTRA1) INTO v_alicuota FROM ADM.ADM_DESCRIPTIVAS
     WHERE DESCRIPCION_ID = 672;
    SELECT TRIM(EXTRA1), CODIGO INTO v_por, v_estatus FROM ADM.ADM_DESCRIPTIVAS
     WHERE DESCRIPCION_ID = 664;
    IF NOT REGEXP_LIKE(NVL(v_alicuota, '?'), '^8([.,]0+)?$')
       OR NOT REGEXP_LIKE(NVL(v_por, '?'), '^100([.,]0+)?$')
       OR v_estatus IS NULL THEN
        RAISE_APPLICATION_ERROR(-20001, 'Impuesto 672 o estatus 664 no corresponden a IVA 8% / retencion 100%');
    END IF;

    -- Serializa la asignacion MAX+1 usada por los catalogos existentes.
    LOCK TABLE ADM.ADM_DESCRIPTIVAS IN EXCLUSIVE MODE;
    LOCK TABLE ADM.ADM_RETENCIONES IN EXCLUSIVE MODE;
    SELECT COUNT(DISTINCT D.TITULO_ID), MIN(D.TITULO_ID)
      INTO v_count, v_titulo
      FROM ADM.ADM_RETENCIONES R JOIN ADM.ADM_DESCRIPTIVAS D
        ON D.DESCRIPCION_ID = R.TIPO_RETENCION_ID
     WHERE R.EXTRA1 = v_estatus AND D.CODIGO = 'IVA';
    IF v_count <> 1 THEN
        RAISE_APPLICATION_ERROR(-20002, 'No existe un unico titulo IVA para el concepto del estatus 664');
    END IF;

    SELECT COUNT(*), MIN(DESCRIPCION_ID) INTO v_count, v_tipo
      FROM ADM.ADM_DESCRIPTIVAS
     WHERE CODIGO = 'IVA' AND TITULO_ID = v_titulo
       AND (
         (LENGTH(DESCRIPCION) - NVL(LENGTH(REPLACE(DESCRIPCION, '%', '')), 0) = 1 AND
          REGEXP_LIKE(REGEXP_REPLACE(REGEXP_SUBSTR(DESCRIPCION,
            '[0-9]+([.,][0-9]+)?[[:space:]]*%'), '[[:space:]%]', ''), '^8([.,]0+)?$'))
         OR (INSTR(NVL(DESCRIPCION, ' '), '%') = 0 AND
             REGEXP_LIKE(TRIM(EXTRA1), '^8([.,]0+)?$'))
       );
    IF v_count > 1 THEN
        RAISE_APPLICATION_ERROR(-20003, 'Existen tipos IVA 8% duplicados; revisar antes de instalar');
    END IF;

    IF v_tipo IS NOT NULL THEN
        SELECT COUNT(*), MIN(CODIGO_RETENCION) INTO v_count, v_concepto
          FROM ADM.ADM_RETENCIONES
         WHERE TIPO_RETENCION_ID = v_tipo AND EXTRA1 = v_estatus;
        IF v_count > 1 THEN
            RAISE_APPLICATION_ERROR(-20004, 'Existen conceptos IVA 8% duplicados para estatus 664');
        END IF;
    END IF;

    IF v_tipo IS NULL OR v_concepto IS NULL THEN
        SELECT COUNT(*), MIN(R.CODIGO_RETENCION) INTO v_count, v_base
          FROM ADM.ADM_RETENCIONES R JOIN ADM.ADM_DESCRIPTIVAS D
            ON D.DESCRIPCION_ID = R.TIPO_RETENCION_ID
         WHERE R.EXTRA1 = v_estatus AND D.CODIGO = 'IVA' AND D.TITULO_ID = v_titulo;
        IF v_count <> 1 THEN
            RAISE_APPLICATION_ERROR(-20005, 'No hay una unica plantilla de concepto IVA 100%; requiere revision');
        END IF;
    END IF;

    IF v_tipo IS NULL THEN
        SELECT NVL(MAX(DESCRIPCION_ID), 0) + 1 INTO v_tipo FROM ADM.ADM_DESCRIPTIVAS;
        INSERT INTO ADM.ADM_DESCRIPTIVAS (
            DESCRIPCION_ID, DESCRIPCION_FK_ID, TITULO_ID, DESCRIPCION, CODIGO,
            EXTRA1, EXTRA2, EXTRA3, USUARIO_INS, FECHA_INS,
            USUARIO_UPD, FECHA_UPD, CODIGO_EMPRESA
        )
        SELECT v_tipo, D.DESCRIPCION_FK_ID, D.TITULO_ID, 'I.V.A. (8%)', 'IVA',
               '8', D.EXTRA2, D.EXTRA3, NVL(v_usuario, D.USUARIO_INS), SYSDATE,
               v_usuario, NULL, D.CODIGO_EMPRESA
          FROM ADM.ADM_DESCRIPTIVAS D JOIN ADM.ADM_RETENCIONES R
            ON R.TIPO_RETENCION_ID = D.DESCRIPCION_ID
         WHERE R.CODIGO_RETENCION = v_base;
        IF SQL%ROWCOUNT <> 1 THEN
            RAISE_APPLICATION_ERROR(-20006, 'No se pudo registrar el tipo IVA 8%');
        END IF;
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
               BASE_IMPONIBLE, POR_RETENCION, MONTO_RETENCION, FECHA_INI, FECHA_FIN,
               EXTRA1, EXTRA2, EXTRA3, NVL(v_usuario, USUARIO_INS), SYSDATE,
               v_usuario, NULL, CODIGO_EMPRESA
          FROM ADM.ADM_RETENCIONES WHERE CODIGO_RETENCION = v_base;
        IF SQL%ROWCOUNT <> 1 THEN
            RAISE_APPLICATION_ERROR(-20007, 'No se pudo registrar el concepto IVA 8%');
        END IF;
        v_concepto := v_nuevo;
    END IF;

    DBMS_OUTPUT.PUT_LINE('Tipo IVA 8%: ' || v_tipo || '; concepto 100%: ' || v_concepto);
    DBMS_OUTPUT.PUT_LINE('Configuracion lista. Revisar y ejecutar COMMIT para habilitarla.');
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK TO cfg_ret_iva;
        RAISE;
END;
/

SELECT D.DESCRIPCION_ID, D.TITULO_ID, D.DESCRIPCION, D.CODIGO,
       R.CODIGO_RETENCION, R.CONCEPTO_PAGO, R.POR_RETENCION, R.EXTRA1
  FROM ADM.ADM_DESCRIPTIVAS D JOIN ADM.ADM_RETENCIONES R
    ON R.TIPO_RETENCION_ID = D.DESCRIPCION_ID
 WHERE D.CODIGO = 'IVA'
   AND R.EXTRA1 = (SELECT CODIGO FROM ADM.ADM_DESCRIPTIVAS WHERE DESCRIPCION_ID = 664)
 ORDER BY D.TITULO_ID, D.DESCRIPCION_ID, R.CODIGO_RETENCION;
