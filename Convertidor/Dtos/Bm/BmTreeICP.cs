using System;
namespace Convertidor.Dtos.Bm
{
	public class BmTreeICP
	{

        public List<string> Path { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; } = string.Empty;
        public int Id { get; set; }
        
    }

    public class BmItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string UnidadEjecutora { get; set; } = string.Empty;
    }

    public class BmTreePUC
    {

        public List<string> Path { get; set; }
        public string Denominacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Id { get; set; }

    }

    public class BmTreeDescriptivaSimple
    {

        public List<string> Path { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Id { get; set; }

    }
}

