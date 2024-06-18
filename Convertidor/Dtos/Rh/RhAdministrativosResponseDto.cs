namespace Convertidor.Dtos.Rh
{
	public class RhAdministrativosResponseDto
	{
        public int CodigoAdministrativo { get; set; }
        public int CodigoPersona { get; set; }
        public string FechaIngreso { get; set; } = string.Empty;
        
        public FechaDto FechaIngresoObj { get; set; }
        public string TipoPago { get; set; } = string.Empty;
        public string DescripcionTipoPago { get { return GetTipoPago(TipoPago); } }
        public int BancoId { get; set; }
        public string DescripcionBanco { get; set; } = string.Empty;
        public int TipoCuentaId { get; set; }
        public string DescripcionCuenta { get; set; } = string.Empty;
        public string NoCuenta { get; set; } = string.Empty;
        private string GetTipoPago(string status)
        {

            string result = string.Empty;
            List<ListStatus> listStatus = new List<ListStatus>()
            {
                new ListStatus(){Codigo="D",Descripcion="Deposito"},
                new ListStatus(){Codigo="E",Descripcion="Efectivo"},
              
            };


            var statusObject = listStatus.Where(s => s.Codigo == status).FirstOrDefault();
            if (statusObject != null)
            {
                result = statusObject.Descripcion;
            }


            return result;
        }

    }

   

 
}

