using Convertidor.Data.Entities.Adm;

namespace Convertidor.Services.Adm.AdmDocumentosOp;

// No materializar entidades completas: Oracle puede tener NULL en auditoria y FK legacy.
public static class IvaDocumentoConsultas
{
    public static IQueryable<ADM_DESCRIPTIVAS> Tipos(IQueryable<ADM_DESCRIPTIVAS> origen) =>
        origen.Select(x => new ADM_DESCRIPTIVAS
        {
            DESCRIPCION_ID = x.DESCRIPCION_ID,
            TITULO_ID = (int?)x.TITULO_ID ?? 0,
            CODIGO = x.CODIGO ?? string.Empty,
            DESCRIPCION = x.DESCRIPCION ?? string.Empty,
            EXTRA1 = x.EXTRA1 ?? string.Empty
        });

    public static IQueryable<ADM_RETENCIONES> Conceptos(IQueryable<ADM_RETENCIONES> origen) =>
        origen.Select(x => new ADM_RETENCIONES
        {
            CODIGO_RETENCION = x.CODIGO_RETENCION,
            TIPO_RETENCION_ID = x.TIPO_RETENCION_ID
        });

    public static IQueryable<ADM_DOCUMENTOS_OP> Documentos(IQueryable<ADM_DOCUMENTOS_OP> origen) =>
        origen.Select(x => new ADM_DOCUMENTOS_OP
        {
            CODIGO_DOCUMENTO_OP = x.CODIGO_DOCUMENTO_OP,
            CODIGO_ORDEN_PAGO = x.CODIGO_ORDEN_PAGO,
            TIPO_IMPUESTO_ID = x.TIPO_IMPUESTO_ID,
            ESTATUS_FISCO_ID = x.ESTATUS_FISCO_ID,
            BASE_IMPONIBLE = (decimal?)x.BASE_IMPONIBLE ?? 0m,
            MONTO_RETENIDO = (decimal?)x.MONTO_RETENIDO ?? 0m,
            CODIGO_EMPRESA = (int?)x.CODIGO_EMPRESA ?? 0,
            CODIGO_PRESUPUESTO = x.CODIGO_PRESUPUESTO,
            USUARIO_INS = (int?)x.USUARIO_INS ?? 0
        });
}
