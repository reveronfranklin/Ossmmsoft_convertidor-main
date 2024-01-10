namespace Convertidor.Dtos.Presupuesto
{
	public class CreatePRE_PRESUPUESTOSDto
	{
        public int CodigoPresupuesto { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Ano { get; set; }
        public decimal MontoPresupuesto { get; set; }
        public string FechaDesde { get; set; } = string.Empty;
        public string FechaHasta { get; set; } = string.Empty;
        public string FechaAprobacion { get; set; } = string.Empty;
        public string NumeroOrdenanza { get; set; } = string.Empty;
        public string FechaOrdenanza { get; set; } = string.Empty;
        public string Extra1 { get; set; } = string.Empty;
        public string Extra2 { get; set; } = string.Empty;
        public string Extra3 { get; set; } = string.Empty;

        public int UsuarioIns { get; set; }
        public int CodigoEmpresa { get; set; }
    }
}

