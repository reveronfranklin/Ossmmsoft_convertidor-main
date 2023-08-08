using System;
namespace Convertidor.Utility
{
	public class Comment
	{
	
			public int Id { get; set; }
			public int ParentId { get; set; }
			public string hierarchy { get; set; } = string.Empty;
			public string Text { get; set; } = string.Empty;
			public string Denominacion { get; set; } = string.Empty;
			public string Descripcion { get; set; } = string.Empty;
			public List<Comment> Children { get; set; }
        
    }
}

