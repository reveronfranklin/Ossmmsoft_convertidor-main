# Contrato frontend - Recibos de nomina, fase 1 legacy

Requerimiento 49. Fecha: 2026-11-06.

## Ruta y solicitud

POST /api/RhVReciboPago/GeneratePdf mediante ossmmasofApi (backend legacy).
El prefijo /legacy depende del proxy del ambiente. Se requiere Bearer valido.
La ruta no cambia a Vertical Slice en esta fase.

```json
{
  "codigoTipoNomina": 20,
  "codigoPeriodo": 5952,
  "codigoIcp": 2261,
  "codigoPersona": 123,
  "sendEmail": false
}
```

Nomina/periodo deben ser positivos. Persona positiva para individual; omitida,
null o cero corresponde a lote. Oficina omitida/null/cero no restringe oficina;
positiva restringe oficina y se combina con persona cuando ambas estan presentes.
Se rechazan valores negativos. No se recibe empresa: settings:EmpresaConfig es
obligatorio y filtra los datos del servidor antes de componer el documento.

No hay paginacion del PDF. El listado conserva su operacion legacy actual:
POST /api/RhReporteNominaHistorico/GetByPeriodoTipoNominaResumen, con nomina,
periodo y oficina, ResultDto con arreglo en data.

## Respuestas

Exito: HTTP 200, bytes PDF, Content-Type application/pdf,
Content-Disposition inline y Cache-Control no-store. No se devuelve nombre
relativo ni se escribe un archivo en Settings.ExcelFiles.

Error esperado: HTTP 200 con ResultDto<byte[]> y datos null, por ejemplo:

```json
{
  "isValid": false,
  "message": "No hay recibos para los filtros seleccionados.",
  "data": null
}
```

Sin token: 401. JSON incorrecto puede producir 400. Errores inesperados se
registran completos con ILogger y referencias de nomina/periodo/persona; el
cliente recibe mensaje controlado, sin rutas internas ni detalles de excepcion.
Un resultado null/isValid=false de la consulta temporal no se disimula consultando
historico. No se entrega un PDF vacio ni un reporte anterior ante errores.

El cliente usa responseType=blob y verifica Content-Type, tamano y firma %PDF-.
Cuando la respuesta sea JSON de error, lee su mensaje y no abre el visor.
Una API antigua que aun devuelva un nombre de archivo se rechaza como no PDF.
Publicar API y frontend de forma coordinada.

## Seleccion de datos y presentacion

Se conserva la prioridad legacy: temporal de la empresa/periodo/nomina si existe;
en su ausencia, historico. Luego se aplican simultaneamente oficina y persona,
sin volver a asignar la lista original. Si temporal existe pero no contiene la
persona/oficina solicitada, se informa sin datos; no se mezcla con historico.

Se preservan los componentes del recibo, montos y marca: lote temporal provisional;
individual temporal e historico sin marca, conforme a la logica anterior. Cada
recibo agrupa por persona dentro de su oficina para evitar mezclar datos entre
oficinas o personas con cedula coincidente. La generacion no escribe nomina.

Se usa el visor compartido DialogReportInfo/ReportViewAsync con object URL del
Blob. El componente de recibos libera solo las URL que crea, al cerrar, reemplazar,
cambiar filtros o desmontar. No fuerza descarga ni window.open. Cambios de filtros
cancelan/descartan consultas y generacion previas; hay carga, error y reintento.

## Configuracion y despliegue

- Configurar settings:EmpresaConfig con la empresa valida del ambiente.
- Logo: settings:RhReciboLogo, ruta absoluta o relativa a ContentRootPath.
  Si se omite, se usa Path.Combine(settings:BmFiles, "LogoIzquierda.jpeg").
  Verificar existencia y mayusculas/minusculas en Linux. No se requiere logo.png
  en el directorio de trabajo ni un directorio de PDFs con permiso de escritura.
- Se conserva QuestPDF y sus dependencias existentes, sin actualizar paquetes.
  Publicar con el runtime/arquitectura del servidor e incluir sus assets nativos.
  No invocar ShowInPreviewer en el servidor.
- No se modificaron procedimientos ni se requiere SQL para fase 1.
- Verificar logs del servidor para identificar la causa exacta del 500 original;
  la captura no contiene esa excepcion y las pruebas locales no la confirman.

Correo permanece deshabilitado. sendEmail=true devuelve error antes de consultar
o generar; no se usa el destinatario fijo de prueba anterior ni se llama al
servicio de correo. Habilitar correo requiere definir y autorizar destinatarios
productivos en otro alcance.

## Fase 2 y reverso

Migracion a Vertical Slice planificada en Requerimientos/49/PlanTrabajo.md:
portar consulta/render en una feature y comparar contra PDFs aprobados de fase 1,
conservando servicio frontend y visor. No se implementa esa migracion aun.

Reverso de fase 1: restaurar juntos frontend/API anteriores. Esa version vuelve
a tener el contrato de nombre/ruta anterior y sus limitaciones; no se eliminan
datos, tablas ni scripts por este cambio. La aceptacion de ambiente exige publicar
y probar individual/lote, temporal/historico en Linux y Giorgia, sin correo real.
