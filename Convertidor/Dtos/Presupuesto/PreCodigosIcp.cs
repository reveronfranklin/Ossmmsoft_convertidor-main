using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class PreCodigosIcp
	{
        public string CodigoSector { get; set; } = string.Empty;
        public string CodigoPrograma { get; set; } = string.Empty;
        public string CodigoSubPrograma { get; set; } = string.Empty;
        public string CodigoProyecto { get; set; } = string.Empty;
        public string CodigoActividad { get; set; } = string.Empty;
        public string CodigoOficina { get; set; } = string.Empty;
        public string Concat { get { return $"{CodigoSector}-{CodigoPrograma}-{CodigoSubPrograma}-{CodigoProyecto}-{CodigoActividad}-{CodigoOficina}"; } }
    }
}

