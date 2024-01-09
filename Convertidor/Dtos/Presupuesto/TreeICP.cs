namespace Convertidor.Dtos.Presupuesto
{
	public class TreeICP
	{

        public List<string> Path { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; } = string.Empty;
        public int Id { get; set; }
        
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; } = string.Empty;
    }

    public class TreePUC
    {

        public List<string> Path { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Id { get; set; }

    }

    public class TreeDescriptivaSimple
    {

        public List<string> Path { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Id { get; set; }

    }
}

