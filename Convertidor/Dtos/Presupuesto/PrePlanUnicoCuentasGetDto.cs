namespace Convertidor.Dtos.Presupuesto
{
	public class PrePlanUnicoCuentasGetDto
	{
        public int CodigoPuc { get; set; }
        public string CodigoGrupo { get; set; } = string.Empty;
        public string CodigoNivel1 { get; set; } = string.Empty;
        public string CodigoNivel2 { get; set; } = string.Empty;
        public string CodigoNivel3 { get; set; } = string.Empty;
        public string CodigoNivel4 { get; set; } = string.Empty;
        public string CodigoNivel5 { get; set; } = string.Empty;
        public string CodigoNivel6 { get; set; } = string.Empty;
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int? CodigoPresupuesto { get; set; }
        public int? CodigoPucPadre { get; set; }
        public string CodigoPucConcat { get { return $"{CodigoGrupo}.{CodigoNivel1}.{CodigoNivel2}.{CodigoNivel3}.{CodigoNivel4}.{CodigoNivel5}.{CodigoNivel6}"; } }

    }
}

