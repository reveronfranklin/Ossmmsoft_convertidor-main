namespace Convertidor.Dtos.Rh
{
	public class ListPeriodoDto
	{
        public int CodigoPeriodo { get; set; }
        public int CodigoTipoNomina{ get; set; }
        public DateTime FechaNomina{ get; set; }
        public int Periodo{ get; set; }
        public string TipoNomina { get; set; } = string.Empty;

        public string Dercripcion { get { return getDescripcion(Periodo,FechaNomina); } }


        private string getDescripcion(int periodo,DateTime fechaNomina)
        {
            string result = string.Empty;
            if (periodo == 1)
            {
                result = "Primera Quincena " + fechaNomina.ToShortDateString();
            }
            else
            {
                result = "Seguna Quincena " + fechaNomina.ToShortDateString(); 
            }
            return result;

        }
    }

    


}

