namespace Convertidor.Dtos.Presupuesto
{
	public class PreDenominacionPorPartidaDto
	{
        public int Nivel { get; set; }
        public int CodigoPresupuesto { get; set; }
    
        public string CodigoPartida { get; set; } = string.Empty;
        public string CodigoGenerica { get; set; } = string.Empty;
        public string CodigoEspecifica { get; set; } = string.Empty;
        public string CodigoSubEspecifica { get; set; } = string.Empty;
        public string CodigoNivel5 { get; set; } = string.Empty;
        public string CodigoPucConcat { get; set; } = string.Empty;
        public string DenominacionPuc { get; set; } = string.Empty;
      

        public decimal Presupuestado { get; set; }
        public decimal Modificado { get; set; }

        public decimal Comprometido { get; set; }
        public decimal Bloqueado { get; set; }
        public decimal Causado { get; set; }
        public decimal Pagado { get; set; }
        public decimal Deuda { get; set; }
        public decimal Disponibilidad { get; set; }
        public decimal Asignacion { get; set; }
        public decimal DisponibilidadFinan { get; set; }

        public decimal Vigente { get { return Presupuestado + Modificado; } }
        List<PreFinanciadoDto>? PreFinanciadoDto { get; set; }



    }
    public class PreFinanciadoDto
    {


        public int FinanciadoId { get; set; }
        public string DescripcionFinanciado { get; set; } = string.Empty;
    


    }
    public class PreDenominacionPorDepartamentoDto
    {


        public int CodigoPresupuesto { get; set; }
        public string CodigoSector { get; set; } = string.Empty;
        public string CodigoPrograma { get; set; } = string.Empty;
        public string CodigoSubPrograma { get; set; } = string.Empty;
        public string CodigoProyecto { get; set; } = string.Empty;
        public string CodigoActividad { get; set; } = string.Empty;
        public string DenominacionIcp { get; set; } = string.Empty;

        List<PreDenominacionPorPartidaDto>? DetominacionPorPartida { get; set; }


    }

    public class AAPreDenominacionDto
    {
        public int CodigoPresupuesto { get; set; }
        public int CodigoSaldo { get; set; }
        public string CodigoSector { get; set; } = string.Empty;
        public string CodigoPrograma { get; set; } = string.Empty;
        public string CodigoSubPrograma { get; set; } = string.Empty;
        public string CodigoProyecto { get; set; } = string.Empty;
        public string CodigoActividad { get; set; } = string.Empty;
        public string DenominacionIcp { get; set; } = string.Empty;
        public string CodigoPartida { get; set; } = string.Empty;
        public string CodigoGenerica { get; set; } = string.Empty;
        public string CodigoEspecifica { get; set; } = string.Empty;
        public string CodigoSubEspecifica { get; set; } = string.Empty;
        public string CodigoNivel5 { get; set; } = string.Empty;
        public string CodigoPucConcat { get; set; } = string.Empty;
        public string DenominacionPuc { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public int FinanciadoId { get; set; }
        public decimal Presupuestado { get; set; }
        public decimal Asignacion { get; set; }
        public decimal Modificado { get; set; }
        public decimal Comprometido { get; set; }
        public decimal Bloqueado { get; set; }

    }

    public class BBPreDenominacionDto
    {
        public int CodigoPresupuesto { get; set; }
        public int CodigoSaldo { get; set; }
        public string CodigoSector { get; set; } = string.Empty;
        public string CodigoPrograma { get; set; } = string.Empty;
        public string CodigoSubPrograma { get; set; } = string.Empty;
        public string CodigoProyecto { get; set; } = string.Empty;
        public string CodigoActividad { get; set; } = string.Empty;
        public string DenominacionIcp { get; set; } = string.Empty;
        public string CodigoPartida { get; set; } = string.Empty;
        public string CodigoGenerica { get; set; } = string.Empty;
        public string CodigoEspecifica { get; set; } = string.Empty;
        public string CodigoSubEspecifica { get; set; } = string.Empty;
        public string CodigoNivel5 { get; set; } = string.Empty;
        public string CodigoPucConcat { get; set; } = string.Empty;
        public string DenominacionPuc { get; set; } = string.Empty;
        public int FinanciadoId { get; set; }
        public decimal Vigente { get; set; }
        public decimal Asignacion { get; set; }
        public decimal Modificado { get; set; }
        public decimal Comprometido { get; set; }
        public decimal Bloqueado { get; set; }
        public decimal Disponibilidad { get; set; }
        public decimal Causado { get; set; }
        public decimal Pagado { get; set; }

    }

    public class CCPreDenominacionDto
    {
        public int CodigoPresupuesto { get; set; }
        public int CodigoSaldo { get; set; }
        public string CodigoSector { get; set; } = string.Empty;
        public string CodigoPrograma { get; set; } = string.Empty;
        public string CodigoSubPrograma { get; set; } = string.Empty;
        public string CodigoProyecto { get; set; } = string.Empty;
        public string CodigoActividad { get; set; } = string.Empty;
        public string DenominacionIcp { get; set; } = string.Empty;
        public string CodigoPartida { get; set; } = string.Empty;
        public string CodigoGenerica { get; set; } = string.Empty;
        public string CodigoEspecifica { get; set; } = string.Empty;
        public string CodigoSubEspecifica { get; set; } = string.Empty;
        public string CodigoNivel5 { get; set; } = string.Empty;
        public string CodigoPucConcat { get; set; } = string.Empty;
        public string DenominacionPuc { get; set; } = string.Empty;
        public int FinanciadoId { get; set; }
        public decimal Vigente { get; set; }
        public decimal Asignacion { get; set; }
        public decimal Modificado { get; set; }
        public decimal Comprometido { get; set; }
        public decimal Bloqueado { get; set; }
        public decimal Disponibilidad { get; set; }
        public decimal Causado { get; set; }
        public decimal Pagado { get; set; }

    }


    public class FilterPreDenominacionDto
    {
        public int CodigoPresupuesto { get; set; }
        public int FinanciadoId { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string CodigoGrupo { get; set; } = string.Empty;
        public int Nivel { get; set; }


    }

    public class AcumuladosPreDenominacionDto
    {
        public decimal Presupuestado { get; set; }
        public decimal Asignacion { get; set; }
        public decimal Modificado { get; set; }
        public decimal Comprometido { get; set; }
        public decimal Bloqueado { get; set; }

    }






}

