using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class PreCargosGetDto
	{
        public int CodigoCargo { get; set; }
        public int TipoPersonalId { get; set; }
        public string DescripcionTipoPersonal { get; set; } = string.Empty;
        public int TipoCargoId { get; set; }
        public string DescripcionTipoCargo { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Grado { get; set; }
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public int CodigoPresupuesto { get; set; }
    }
}

