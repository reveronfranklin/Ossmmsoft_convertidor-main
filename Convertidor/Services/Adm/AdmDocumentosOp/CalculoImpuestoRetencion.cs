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

        if (porcentajeRetencion == 0)
        {
            resultado.MontoImpuesto = 0;
            resultado.MontoRetenido = 0;
            resultado.BaseImponible = 0;
        }
        else
        {
            resultado.BaseImponible = (montoDocumento - montoImpuestoExento) / ((porcentajeIva / 100) + 1);
            resultado.BaseImponible = Math.Round(resultado.BaseImponible, 2);

            resultado.MontoImpuesto = resultado.BaseImponible * (porcentajeIva / 100);
            resultado.MontoImpuesto = Math.Round(resultado.MontoImpuesto, 2);

            resultado.MontoRetenido = resultado.MontoImpuesto * (porcentajeRetencion / 100);
            resultado.MontoRetenido = Math.Round(resultado.MontoRetenido, 2);
        }

        return resultado;
    }
}