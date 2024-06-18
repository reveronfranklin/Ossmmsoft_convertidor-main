namespace Convertidor.Dtos.Bm
{
	public class BmTreeDescriptivas
	{
        public int Id { get; set; }
        public int ParentId { get; set; }
        public List<string>? hierarchy { get; set; } 
        public string Text { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string>? Patch { get; set; }
        public  List<BmTreeDescriptivas>? Children { get; set; }
    }
}

