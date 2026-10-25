# IVA del documento y retencion automatica

Requerimiento 46. Fecha: 2026-10-25.

## Operaciones

| Metodo | Ruta | Efecto |
| --- | --- | --- |
| POST | `/api/AdmDocumentosOp/Create` | Guarda documento y sincroniza retenciones IVA |
| POST | `/api/AdmDocumentosOp/Update` | Recalcula documento y sincroniza sus grupos |
| POST | `/api/AdmDocumentosOp/Delete` | Elimina documento y sincroniza grupos restantes |
| POST | `/api/AdmDocumentosOp/GetByCodigoOrdenPago` | Consulta documentos; body con `codigoOrdenPago` |

Se conserva el contrato legacy de presupuesto; esta correccion no agrega campos
de empresa ni exige que el frontend cree la retencion.

## Ejemplo de Create/Update

```json
{
  "codigoDocumentoOp": 25705,
  "codigoOrdenPago": 18409,
  "fechaComprobante": "2026-10-19",
  "periodoImpositivo": "202610",
  "tipoOperacionId": 655,
  "tipoDocumentoId": 657,
  "fechaDocumento": "2026-10-19",
  "numeroDocumento": "123",
  "numeroControlDocumento": "321",
  "montoDocumento": 234240.00,
  "baseImponible": 216888.89,
  "montoImpuesto": 17351.11,
  "numeroDocumentoAfectado": "",
  "tipoTransaccionId": 669,
  "tipoImpuestoId": 672,
  "montoImpuestoExento": 0,
  "montoRetenido": 17351.11,
  "codigoPresupuesto": 20,
  "numeroExpediente": "",
  "estatusFiscoId": 664
}
```

Para Create usar `codigoDocumentoOp: 0`; el ejemplo identifica el documento de
Update. Los identificadores de catalogo del ejemplo deben existir en el ambiente.
El servidor calcula base, impuesto y retenido desde monto, exento, impuesto y
estatus fiscal, sin confiar en los importes derivados enviados por el cliente.
Delete recibe `{ "codigoDocumentoOp": 25705 }`.

Respuesta: se mantiene `ResultDto<T>` con `data` (documento guardado, o DTO de
Delete), `isValid`, `message`, `cantidadRegistros`, `page` y `totalPage`.
No se agrega paginacion a las mutaciones; el conteo mantiene el comportamiento
existente de documentos de la OP. Un HTTP 200 requiere revisar `isValid`.

## Reglas de integridad

- Factura con IVA 8% genera automaticamente una linea cuya
  `descripcionTipoRetencion` es `I.V.A. (8%)`; `porRetencion` sigue siendo 100
  en el caso reportado. No crear otra retencion desde el frontend.
- Se agrupa por OP, tipo de impuesto, alicuota, concepto y porcentaje retenido.
  La base gravada excluye exentos y el monto es la suma de retenidos del grupo.
- Al cambiar o eliminar documentos se actualizan los grupos, conservando
  identidad y comprobante de las lineas reutilizadas. No se consume una serie.
- Las lineas automaticas no admiten edicion ni eliminacion directa mediante
  `AdmRetencionesOp/Update` o `Delete`; responderan `isValid: false` con
  `La retencion IVA es automatica; modifique el documento que la origina`.
- La configuracion fiscal invalida o concepto ausente/duplicado produce error
  funcional y rollback del documento y la sincronizacion en ADM.
- Retencion cero conserva el impuesto causado, sin generar una linea retenida.
- Despues de guardar correctamente, refrescar consultas de documentos,
  retenciones y beneficiarios de la OP mediante el flujo existente.

Ejemplo de campos de una retencion consultada tras guardar:

```json
{
  "codigoOrdenPago": 18409,
  "descripcionTipoRetencion": "I.V.A. (8%)",
  "porRetencion": 100,
  "baseImponible": 216888.89,
  "montoRetencion": 17351.11,
  "montoRetenido": 17351.11
}
```

No cambian las rutas ni los cuerpos de los reportes: el PDF OP se solicita con
`POST /api/ReporteOrdenPago/pdf`, body `{ "codigoOrdenPago": 18409, "usuario": "usuario" }`,
y devuelve PDF para el visor actual. El comprobante IVA y la relacion por periodo
continuan leyendo alicuota e importes de documentos.

## Persistencia y despliegue

Se reservan los campos existentes de `ADM_RETENCIONES_OP` para lineas automaticas:

| Campo | Contenido |
| --- | --- |
| EXTRA1 | Identificador del impuesto del documento, como texto |
| EXTRA2 | Alicuota decimal con punto, independiente de cultura |
| EXTRA3 | Descripcion IVA derivada de esa alicuota |
| EXTRA4 | Marca `IVA_DOCUMENTO` |

`TIPO_RETENCION_ID` identifica el tipo IVA correspondiente a la alicuota del
documento dentro del titulo de retenciones, conservando `CODIGO = IVA` para
aprobacion y series. No se conserva el tipo del 6% para un documento del 8%.
El identificador del impuesto del documento y el del tipo de retencion son
catalogos distintos: no se copia directamente el ID 672 al tipo de retencion.
Se valida una unica opcion de catalogo por alicuota. Si falta IVA 8% en el titulo
de retenciones, debe configurarse antes de generar o reparar la linea.
El mapeo y el reporte OP toman la descripcion persistida cuando existe la marca.
Las filas sin marca conservan su lectura legacy hasta reconciliarlas.
Si una fila legacy ya tiene contenido en EXTRA1..4, se rechaza su conversion
automatica para evitar sobrescribir informacion ajena.

Desplegar el backend legacy y ejecutar
`OssmmasoftVerticalSlice/Features/ReporteOrdenPago/SP_REP_ORD_PAGO_RET_GET.sql`
antes de habilitar el flujo corregido; desplegar los cambios frontend de etiquetas.
No se agregan columnas ni se renombra el catalogo IVA 6%.

Para la OP existente se entrega `FIX_OP_IVA_18409.sql` en la misma carpeta del
reporte. Revisar respaldo y datos antes de ejecutarlo: exige una factura y una
retencion IVA con los importes reportados, preserva comprobantes y no hace COMMIT.
El script no se ejecuto contra Oracle en esta entrega. Para otras OP pendientes,
guardar el documento reconcilia sus grupos; historicos aprobados requieren
diagnostico individual y no deben retornarse solo para corregir esta descripcion.

## Fuente del reporte y correccion del registro existente

La OP PDF consulta exclusivamente las filas de `ADM_RETENCIONES_OP` y su tipo
asociado. No vuelve a consultar facturas para regenerar retenciones al imprimir.
El documento es el origen de la escritura de esa tabla, no una sustitucion de
la tabla en el reporte. La correccion de un registro existente debe persistir
`TIPO_RETENCION_ID`, alicuota y descripcion correctos antes de emitir el PDF.

La resolucion del tipo usa la alicuota explicita en la descripcion del catalogo
(por ejemplo `I.V.A. (8%)`) o EXTRA1 numerico si no contiene porcentaje. Busca
dentro del titulo usado por los conceptos IVA del estatus fiscal; rechaza
configuracion ausente o duplicada. No elige la primera fila de familia IVA.

Ejecutar primero `DIAG_OP_IVA_18409.sql` para comparar registro persistido,
catalogo y version del procedimiento instalado. El script de reparacion tambien
actualiza el identificador del tipo IVA, y aborta si falta el tipo 8% o esta duplicado.
Ninguno de estos scripts se ha ejecutado contra el servidor en esta entrega.

## Lectura de datos legacy con NULL

La sincronizacion carga solo las columnas necesarias de tipos, conceptos y
los documentos. No materializa auditoria ni relaciones opcionales del catalogo
como enteros obligatorios. El payload frontend no requiere cambios por esta
correccion. Para el caso OP 18410, total 500 e IVA 8% con retencion 100%, los
importes esperados son base 462.96 e impuesto/retenido 37.04.
La excepcion completa del alta se registra en el log con el codigo de OP;
la confirmacion del alta en el ambiente Oracle sigue siendo necesaria.
