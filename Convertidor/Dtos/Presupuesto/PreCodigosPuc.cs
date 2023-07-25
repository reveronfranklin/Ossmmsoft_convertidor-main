using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class PreCodigosPuc
	{
       
        public string CodigoGrupo { get; set; } = string.Empty;
        public string CodigoNivel1 { get; set; } = string.Empty;
        public string CodigoNivel2 { get; set; } = string.Empty;
        public string CodigoNivel3 { get; set; } = string.Empty;
        public string CodigoNivel4 { get; set; } = string.Empty;
        public string CodigoNivel5 { get; set; } = string.Empty;
        public string CodigoNivel6 { get; set; } = string.Empty;
        public string Concat { get { return $"{CodigoGrupo}-{CodigoNivel1}-{CodigoNivel2}-{CodigoNivel3}-{CodigoNivel4}-{CodigoNivel5}-{CodigoNivel6}"; } }

    }
}

