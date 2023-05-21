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

                    string literal = "";
                    if (Presupuestado == 0)
                    {
                        literal = "Bs. 0,0";
                    }
                    else
                    {
                        literal = "Bs. " + Presupuestado.ToString("#,#", CultureInfo.InvariantCulture);
                    }
               
                
                    return literal;
                }
        }
        public string DisponibleFormat
        {
            get
            {

                string literal = "";
                if (Disponible == 0)
                {
                    literal = "Bs. 0,0";
                }
                else
                {
                    literal = "Bs. " + Disponible.ToString("#,#", CultureInfo.InvariantCulture);
                }


                return literal;
            }
        }
        public string AsignacionFormat
        {
            get
            {

                string literal = "";
                if (Asignacion == 0)
                {
                    literal = "Bs. 0,0";
                }
                else
                {
                    literal = "Bs. " + Asignacion.ToString("#,#", CultureInfo.InvariantCulture);
                }


                return literal;
            }
        }
        public string BloqueadoFormat
        {
            get
            {

                string literal = "";
                if (Bloqueado == 0)
                {
                    literal = "Bs. 0,0";
                }
                else
                {
                    literal = "Bs. " + Bloqueado.ToString("#,#", CultureInfo.InvariantCulture);
                }


                return literal;
            }
        }
        public string ModificadoFormat
        {
            get
            {

                string literal = "";
                if (Modificado == 0)
                {
                    literal = "Bs. 0,0";
                }
                else
                {
                    literal = "Bs. " + Modificado.ToString("#,#", CultureInfo.InvariantCulture);
                }


                return literal;
            }
        }
        public string AjustadoFormat
        {
            get
            {

                string literal = "";
                if (Ajustado == 0)
                {
                    literal = "Bs. 0,0";
                }
                else
                {
                    literal = "Bs. " + Ajustado.ToString("#,#", CultureInfo.InvariantCulture);
                }


                return literal;
            }
        }
        public string VigenteFormat
        {
            get
            {

                string literal = "";
                if (Vigente == 0)
                {
                    literal = "Bs. 0,0";
                }
                else
                {
                    literal = "Bs. " + Vigente.ToString("#,#", CultureInfo.InvariantCulture);
                }


                return literal;
            }
        }
        public string ComprometidoFormat
        {
            get
            {

                string literal = "";
                if (Comprometido == 0)
                {
                    literal = "Bs. 0,0";
                }
                else
                {
                    literal = "Bs. " + Comprometido.ToString("#,#", CultureInfo.InvariantCulture);
                }


                return literal;
            }
        }
        public string CausadoFormat
        {
            get
            {

                string literal = "";
                if (Causado == 0)
                {
                    literal = "Bs. 0,0";
                }
                else
                {
                    literal = "Bs. " + Causado.ToString("#,#", CultureInfo.InvariantCulture);
                }


                return literal;
            }
        }
        public string PagadoFormat
        {
            get
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
        }


       

        //public string PagadoFormat { get { return Pagado.ToString("C2"); } }

    }
}

