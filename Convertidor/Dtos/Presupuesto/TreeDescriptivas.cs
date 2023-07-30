using System;
namespace Convertidor.Dtos.Presupuesto
{
	public class TreeDescriptivas
	{
        public int Id { get; set; }
        public int ParentId { get; set; }
        public List<string>? hierarchy { get; set; } 
        public string Text { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string>? Patch { get; set; }
        public  List<TreeDescriptivas>? Children { get; set; }
    }
}

