using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class PreIndiceCategoriaProgramaticaGetDto
	{
        public int CodigoIcp { get; set; }
        public int Ano { get; set; }
        public int Escenario { get; set; }
        public string CodigoSector { get; set; } = string.Empty;
        public string CodigoPrograma { get; set; } = string.Empty;
        public string CodigoSubPrograma { get; set; } = string.Empty;
        public string CodigoProyecto { get; set; } = string.Empty;      
        public string CodigoActividad { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int CodigoFuncionario { get; set; }
        public string FechaIni { get; set; } = string.Empty;
        public string FechaFin { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;
        public string CodigoOficina { get; set; } = string.Empty;       
        public int CodigoPresupuesto { get; set; }
        public int CodigoIcpPadre { get; set; }
        public DateTime FechaIniDate { get; set; } 
        public DateTime FechaFinDate { get; set; }
        public string CodigoIcpConcat { get { return $"{CodigoSector}-{CodigoPrograma}-{CodigoSubPrograma}-{CodigoProyecto}-{CodigoActividad}-{CodigoOficina}"; } }
       
        public FechaDto FechaIninObj { get { return GetFechaDto(FechaIniDate); } }
        public FechaDto FechaFinObj { get { return GetFechaDto(FechaFinDate); } }



        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(month.Length - 2);

            return FechaDesdeObj;
        }

    }
}

