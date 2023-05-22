using System;
using System.Globalization;

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
        public string CodigoGenerica { get; set; } = string.Empty;
        public string CodigoEspecifica { get; set; } = string.Empty;
        public string CodigoSubEspecifica { get; set; } = string.Empty;
        public string CodigoNivel5 { get; set; } = string.Empty;
        public string CodigoPucConcat { get; set; } = string.Empty;
        public string DenominacionPuc { get; set; } = string.Empty;
        public decimal Presupuestado { get; set; }=0;
        public decimal Asignacion { get; set; } = 0;
        public decimal Bloqueado { get; set; } = 0;
        public decimal Modificado { get; set; } = 0;
        public decimal Ajustado { get; set; } = 0;
        public decimal Vigente { get; set; } = 0;
        public decimal Comprometido { get; set; } = 0;
        public decimal PorComprometido { get; set; } = 0;
        public decimal Disponible { get; set; } = 0;
        public decimal Causado { get; set; } = 0;
        public decimal PorCausado { get; set; } = 0;
        public decimal Pagado { get; set; } = 0;
        public decimal PorPagado { get; set; } = 0;
        public int CodigoEmpresa { get; set; }
        public int CodigoPresupuesto { get; set; }
        public string DescripcionPresupuesto { get; set; } = string.Empty;
        public DateTime FechaSolicitud { get; set; }

       
        public string PresupuestadoFormat {
            get {

                return ConvertMoneda(Presupuestado);
            }
        }
        public string DisponibleFormat
        {
            get
            {
                return ConvertMoneda(Disponible);
            }
        }
        public string AsignacionFormat
        {
            get
            {
                return ConvertMoneda(Asignacion);
            }
        }
        public string BloqueadoFormat
        {
            get
            {
                return ConvertMoneda(Bloqueado);
            }
        }
        public string ModificadoFormat
        {
            get
            {
                return ConvertMoneda(Modificado);
            }
        }
        public string AjustadoFormat
        {
            get
            {
                return ConvertMoneda(Ajustado);
            }
        }
        public string VigenteFormat
        {
            get
            {
                return ConvertMoneda(Vigente);
            }
        }
        public string ComprometidoFormat
        {
            get
            {
                return ConvertMoneda(Comprometido);
            }
        }
        public string CausadoFormat
        {
            get
            {
                return ConvertMoneda(Causado);

            }
        }
        public string PagadoFormat
        {
            get
            {
                return ConvertMoneda(Pagado);
            }
        }


        public string ConvertMoneda(decimal value)
        {
            string literal = "";
            if (Pagado == 0)
            {
                literal = "Bs. 0,0";
            }
            else
            {
                literal = "Bs. " + Pagado.ToString("#,#", CultureInfo.InvariantCulture);
            }


            return literal;
        }

        //public string PagadoFormat { get { return Pagado.ToString("C2"); } }

    }
}

