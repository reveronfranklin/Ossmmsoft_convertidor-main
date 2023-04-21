using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class PreVSaldosGetDto
	{
	

        public int CodigoSaldo { get; set; }
        public int Ano { get; set; }
        public int FinanciadoId { get; set; }
        public int CodigoFinanciado { get; set; }
        public string DescripcionFinanciado { get; set; } = string.Empty;
        public int CodigoIcp { get; set; }
        public string CodigoSector { get; set; } = string.Empty;
        public string CodigoPrograma { get; set; } = string.Empty;
        public string CodigoSubPrograma { get; set; } = string.Empty;
        public string CodigoProyecto { get; set; } = string.Empty;
        public string CodigoActividad { get; set; } = string.Empty;
        public string CodigoOficina { get; set; } = string.Empty;
        public string CodigoIcpConcat { get; set; } = string.Empty;
        public string DenominacionIcp { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; } = string.Empty;
        public int CodigoPuc { get; set; }
        public string CodigoGrupo { get; set; } = string.Empty;
        public string CodigoPartida { get; set; } = string.Empty;
        public string Codigogenerica { get; set; } = string.Empty;
        public string CodigoEspecifica { get; set; } = string.Empty;
        public string CodigoSubEspecifica { get; set; } = string.Empty;
        public string CodigoNivel5 { get; set; } = string.Empty;
        public string CodigoPucConcat { get; set; } = string.Empty;
        public string DenominacionPuc { get; set; } = string.Empty;
        public decimal Presupuestado { get; set; }
        public decimal Asignacion { get; set; }
        public decimal Bloqueado { get; set; }
        public decimal Modificado { get; set; }
        public decimal Ajustado { get; set; }
        public decimal Vigente { get; set; }
        public decimal Comprometido { get; set; }
        public decimal PorComprometido { get; set; }
        public decimal Disponoble { get; set; }
        public decimal Causado { get; set; }
        public decimal PorCausado { get; set; }
        public decimal Pagado { get; set; }
        public decimal PorPagado { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoPresupuesto { get; set; }
        public DateTime FechaSolicitud { get; set; }
    
	}
}

