-- Requerimiento 34: conservar dos decimales en el total.
-- Instalar en Oracle antes de validar nuevos calculos. No modifica historicos.
CREATE OR REPLACE PROCEDURE RH.RH_P_RETENCION_SSO (P_PROCESO_ID NUMBER, P_CODIGO_TIPO_NOMINA NUMBER, P_FECHA_DESDE VARCHAR2, P_FECHA_HASTA VARCHAR2)
IS
    V_FECHA_DESDE VARCHAR2(10) := TO_CHAR(TO_DATE(P_FECHA_DESDE,'DD/MM/RRRR'),'RRRRMM');
    V_FECHA_HASTA VARCHAR2(10) := TO_CHAR(TO_DATE(P_FECHA_HASTA,'DD/MM/RRRR'),'RRRRMM');

BEGIN
/*	INSERT INTO RH.RH_TMP_VALORES values
                (
                p_proceso_id --p1 number
                ,p_codigo_tipo_nomina --p2 number
                ,p_fecha_desde --p3 varchar2(10)
                ,p_fecha_hasta --p4 varchar2(10)
                );
   COMMIT;
 */
	FOR XX IN 
        (
            SELECT    
                NULL Numero
                ,PVIO.UNIDAD_EJECUTORA
                ,RVPC.NACIONALIDAD||''||RVPC.CEDULA CEDULA
                ,RVPC.nombre||' '||RVPC.apellido NOMBRES_APELLIDOS
                ,RVPC.DESCRIPCION_CARGO
                ,RVPC.FECHA_INGRESO
                ,ROUND(SUM(SSO.MONTO*-1),2) MONTO_SSO_TRABAJADOR
                ,ROUND(SUM(RPE.MONTO*-1),2) MONTO_RPE_TRABAJADOR
                ,ROUND(SUM((SSO.MONTO*10)/4),2)*-1 MONTO_SSO_PATRONO
                ,ROUND(SUM((RPE.MONTO*2)/0.5),2)*-1 MONTO_RPE_PATRONO
                ,ROUND(SUM(SSO.MONTO+RPE.MONTO+ROUND((SSO.MONTO*10)/4,2)+ROUND((RPE.MONTO*2)/0.5,2)),2) MONTO_TOTAL_RETENCION
                ,TO_CHAR(RPE.FECHA_NOMINA,'RRRRMM') FECHA_NOMINA
                ,SIGLAS_TIPO_NOMINA
                FROM 
                 RH_HISTORICO_PERSONAL_CARGO RVPC 
                 ,PRE_INDICE_CAT_PRG PVIO 
                 ,RH_TIPOS_NOMINA RN
                 ,(SELECT RTN.CODIGO_TIPO_NOMINA,RTN.CODIGO_PERSONA, RTN.FECHA_NOMINA, RTN.MONTO
                    FROM RH_HISTORICO_NOMINA RTN  ,RH_CONCEPTOS RC 
                 WHERE  
                  RC.CODIGO_CONCEPTO    = RTN.CODIGO_CONCEPTO 
/*                  AND TO_CHAR(RTN.FECHA_NOMINA,'RRRRMM') >= TO_CHAR(P_FECHA_DESDE,'RRRRMM')
                  AND TO_CHAR(RTN.FECHA_NOMINA,'RRRRMM') <= TO_CHAR(P_FECHA_HASTA,'RRRRMM')  
*/
                  AND TO_CHAR(RTN.FECHA_NOMINA,'RRRRMM') >= V_FECHA_DESDE
                  AND TO_CHAR(RTN.FECHA_NOMINA,'RRRRMM') <= V_FECHA_HASTA  

                  AND (
                   (RTN.CODIGO_TIPO_NOMINA = 21 AND RTN.CODIGO_TIPO_NOMINA = P_CODIGO_TIPO_NOMINA AND RC.CODIGO_CONCEPTO = 1558 ) 
                  OR (RTN.CODIGO_TIPO_NOMINA = 20 AND RTN.CODIGO_TIPO_NOMINA = P_CODIGO_TIPO_NOMINA AND RC.CODIGO_CONCEPTO IN (1678,1665,1673,1656,1667, 1661) ) 
                  OR (RTN.CODIGO_TIPO_NOMINA = 10 AND RTN.CODIGO_TIPO_NOMINA = P_CODIGO_TIPO_NOMINA AND RC.CODIGO_CONCEPTO IN (930, 1532, 1408, 964) ) 
                  OR (RTN.CODIGO_TIPO_NOMINA = 12 AND RTN.CODIGO_TIPO_NOMINA = P_CODIGO_TIPO_NOMINA AND RC.CODIGO_CONCEPTO IN (1375, 1539, 1427, 1589) ) 
                )
                ) RPE
                ,
                 (SELECT RTN.CODIGO_TIPO_NOMINA,RTN.CODIGO_PERSONA, RTN.FECHA_NOMINA, RTN.MONTO
                    FROM RH_HISTORICO_NOMINA RTN  ,RH_CONCEPTOS RC 
                 WHERE  
                  RC.CODIGO_CONCEPTO    = RTN.CODIGO_CONCEPTO 
/*                  AND TO_CHAR(RTN.FECHA_NOMINA,'RRRRMM') >= TO_CHAR(P_FECHA_DESDE,'RRRRMM')
                  AND TO_CHAR(RTN.FECHA_NOMINA,'RRRRMM') <= TO_CHAR(P_FECHA_HASTA,'RRRRMM')  
*/
                  AND TO_CHAR(RTN.FECHA_NOMINA,'RRRRMM') >= V_FECHA_DESDE
                  AND TO_CHAR(RTN.FECHA_NOMINA,'RRRRMM') <= V_FECHA_HASTA  
                  AND (
                   (RTN.CODIGO_TIPO_NOMINA = 21 AND RTN.CODIGO_TIPO_NOMINA = P_CODIGO_TIPO_NOMINA AND RC.CODIGO_CONCEPTO = 1557) 
                  OR (RTN.CODIGO_TIPO_NOMINA = 20 AND RTN.CODIGO_TIPO_NOMINA = P_CODIGO_TIPO_NOMINA AND RC.CODIGO_CONCEPTO IN (1677,1674,1655,1666) ) 
                  OR (RTN.CODIGO_TIPO_NOMINA = 10 AND RTN.CODIGO_TIPO_NOMINA = P_CODIGO_TIPO_NOMINA AND RC.CODIGO_CONCEPTO IN (926,1531,1438,969) ) 
                  OR (RTN.CODIGO_TIPO_NOMINA = 12 AND RTN.CODIGO_TIPO_NOMINA = P_CODIGO_TIPO_NOMINA AND RC.CODIGO_CONCEPTO IN (1374, 1538, 1426,1441, 1427, 1589) ) 
                )
                ) SSO
                WHERE 
                  RVPC.CODIGO_TIPO_NOMINA  = RN.CODIGO_TIPO_NOMINA 
                  AND RVPC.CODIGO_TIPO_NOMINA  = RPE.CODIGO_TIPO_NOMINA 
                  AND RVPC.CODIGO_PERSONA       = RPE.CODIGO_PERSONA 
                  AND RVPC.FECHA_NOMINA        = RPE.FECHA_NOMINA 
                  AND PVIO.CODIGO_ICP          = RVPC.CODIGO_ICP
                  AND RVPC.CODIGO_TIPO_NOMINA  = SSO.CODIGO_TIPO_NOMINA 
                  AND RVPC.CODIGO_PERSONA       = SSO.CODIGO_PERSONA 
                  AND RVPC.FECHA_NOMINA        = SSO.FECHA_NOMINA 
        GROUP BY                 NULL
                    ,PVIO.UNIDAD_EJECUTORA
                    ,RVPC.NACIONALIDAD||''||RVPC.CEDULA 
                    ,RVPC.nombre||' '||RVPC.apellido 
                    ,RVPC.DESCRIPCION_CARGO
                    ,RVPC.FECHA_INGRESO
                    ,TO_CHAR(RPE.FECHA_NOMINA,'RRRRMM') 
                    ,SIGLAS_TIPO_NOMINA
                ORDER BY 13,12,4
        )
            LOOP
	            ---raise_application_error(-21000,'Ciclo');
                -- 
                INSERT INTO RH_TMP_RETENCIONES_SSO VALUES
               (    
               0 --CODIGO_RETENCION_APORTE --NUMBER, 
                ,0 -- NUMBER, 
                ,XX.UNIDAD_EJECUTORA -- VARCHAR2(100), 
                ,XX.CEDULA --VARCHAR2(20), 
                ,XX.NOMBRES_APELLIDOS --VARCHAR2(20), 
                ,XX.DESCRIPCION_CARGO --VARCHAR2(20), 
                ,XX.FECHA_INGRESO --DATE, 
                ,XX.MONTO_SSO_TRABAJADOR --NUMBER, 
                ,XX.MONTO_RPE_TRABAJADOR --NUMBER, 
                ,XX.MONTO_SSO_PATRONO -- NUMBER, 
                ,XX.MONTO_RPE_PATRONO --NUMBER, 
                ,XX.MONTO_TOTAL_RETENCION --NUMBER, 
                ,XX.FECHA_NOMINA  -- DATE, 
                ,XX.SIGLAS_TIPO_NOMINA --VARCHAR2(10), 
                ,P_PROCESO_ID --NUMBER
                ,TO_DATE(P_FECHA_DESDE,'DD/MM/RRRR')
                ,TO_DATE(P_FECHA_HASTA,'DD/MM/RRRR')
                ,P_CODIGO_TIPO_NOMINA
               );
              COMMIT;
                END LOOP;
END RH_P_RETENCION_SSO;
/

SHOW ERRORS;
