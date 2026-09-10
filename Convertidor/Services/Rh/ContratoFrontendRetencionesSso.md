# Contrato frontend: Retenciones SSO

## Endpoint y solicitud

`POST /api/RhTmpRetencionesSso/GetRetencionesSso`

```json
{
  "tipoNomina": 20,
  "fechaDesde": "01/09/2025",
  "fechaHasta": "30/09/2025"
}
```

Fechas en `dd/MM/yyyy`. Los objetos opcionales del DTO `fechaDesdeObj` y
`fechaHastaObj` no participan en esta operacion. El procedimiento filtra meses
completos; el historico se consulta por tipo de nomina y fechas exactas.

## Respuesta

El controlador devuelve HTTP 200 con `ResultDto<List<RhTmpRetencionesSsoDto>>`.
Ejemplo sintetico de una fila:

```json
{
  "data": [{
    "id": 1,
    "codigoRetencionAporte": 0,
    "secuencia": 0,
    "unidadEjecutora": "UNIDAD DE PRUEBA",
    "cedulaTexto": "V123",
    "nombresApellidos": "PERSONA DE PRUEBA",
    "descripcionCargo": "CARGO DE PRUEBA",
    "fechaIngreso": "2020-01-01T00:00:00",
    "montoSsoTrabajador": 40,
    "montoRpeTrabajador": 5,
    "montoSsoPatrono": 100,
    "montoRpePatrono": 20,
    "montoTotalRetencion": -165,
    "fechaNomina": "202509",
    "siglasTipoNomina": "AN",
    "fechaDesde": "2025-09-01T00:00:00",
    "fechaHasta": "2025-09-30T00:00:00",
    "codigoTipoNomina": 20
  }],
  "isValid": true,
  "message": "",
  "linkData": "/ExcelFiles/RetencionesSSO desde 20250901 Hasta 20250930 Tipo Nomina 20.xlsx",
  "linkDataArlternative": null,
  "page": 0,
  "totalPage": 0,
  "cantidadRegistros": 0,
  "total1": 0,
  "total2": 0,
  "total3": 0,
  "total4": 0
}
```

No existe paginacion en esta operacion. Los conteos y totales del envoltorio no
se calculan; no deben interpretarse como cantidad ni suma de los registros.
Los importes RPE se copian de la fuente temporal o historica, incluidos ceros,
signos y decimales. El total se conserva desde Oracle sin recalcularlo. El procedimiento corregido
redondea el resultado final a dos decimales.
Los identificadores consecutivos del ejemplo corresponden al camino temporal;
el mapeo historico mantiene su comportamiento vigente de `id = 0`.

## Lista y exportacion

En `/apps/rh/retenciones/sso/` se muestran los importes en este orden:
SSO trabajador, RPE trabajador, SSO patrono, RPE patrono y Total.
Cada columna lee su campo `monto...` correspondiente de la respuesta. El DataGrid
permite desplazamiento horizontal cuando los anchos minimos exceden el contenedor.

El boton de exportacion genera `data.xlsx` con todas las propiedades de `data`
mediante `json_to_sheet`, sin recalcular importes de detalle. Las cinco columnas
de monto tienen formato `#,##0.00` y una fila final `TOTAL` con formulas
`ROUND(SUM(...),2)`. La fila incluye todas las filas exportadas y no forma parte
de `data` en la API. El backend genera adicionalmente el Excel expuesto en
`linkData` con el mismo formato y sumatorias. Sin datos no se agrega fila de totales.
La lista React muestra `montoTotalRetencion` con dos decimales.

## Validacion y errores vigentes

No se agregan reglas de validacion en esta correccion. El servicio espera fechas
validas para su conversion. Si no obtiene filas, `data` puede ser `[]` o `null`
y `linkData` queda vacio. El manejo actual de excepciones del servicio devuelve
`data: null`, `isValid: true`, `message: ""` y `linkData: ""`, incluso ante un fallo
al generar el archivo. Por tanto, `isValid` por si solo no prueba que haya datos
ni que la exportacion haya tenido exito. Corregir ese comportamiento queda fuera
del requerimiento 34.

## Instalacion del cambio de precision

Ejecutar `Sql/RH_P_RETENCION_SSO.sql` en Oracle. La definicion de referencia en
`SqlBase/RH.SQL` contiene el mismo cambio. Esto conserva centavos en nuevos
calculos; los registros historicos con total ya redondeado a enteros permanecen
iguales y requieren un proceso separado para recuperar sus importes originales.
Cambiar solo el formato Excel mostraria, por ejemplo, -124.00 en lugar de -124,
pero no recuperaria -124.20 sin instalar el ajuste del procedimiento.
