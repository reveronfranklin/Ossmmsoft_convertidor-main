﻿namespace Convertidor.Dtos.Rh
{
	public class ListConceptosDto
	{
		public int IdDetalleProceso { get; set; }
        public int CodigoConcepto { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int CodigoTipoNomina { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get { return $"{Codigo.Trim()} {Denominacion.Trim()}"; } }
    }
}

