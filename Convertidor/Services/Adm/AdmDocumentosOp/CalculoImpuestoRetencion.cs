namespace Convertidor.Services.Adm.AdmDocumentosOp;

using System;

public class CalculoImpuestoRetencion
{
    public class ResultadoCalculo
    {
        public decimal MontoDocumento { get; set; }
        public decimal MontoImpuestoExento { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal MontoImpuesto { get; set; }
        public decimal MontoRetenido { get; set; }
    }

    public static ResultadoCalculo Calcular(decimal montoDocumento, decimal montoImpuestoExento, decimal porcentajeRetencion, decimal porcentajeIva)
    {
        var resultado = new ResultadoCalculo
        {
            MontoDocumento = montoDocumento,
            MontoImpuestoExento = montoImpuestoExento
        };

        resultado.BaseImponible = Math.Round(
            (montoDocumento - montoImpuestoExento) / ((porcentajeIva / 100) + 1), 2);
        resultado.MontoImpuesto = Math.Round(resultado.BaseImponible * porcentajeIva / 100, 2);
        resultado.MontoRetenido = Math.Round(resultado.MontoImpuesto * porcentajeRetencion / 100, 2);

        return resultado;
    }
}