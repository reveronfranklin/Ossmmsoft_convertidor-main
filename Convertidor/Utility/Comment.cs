using System;
namespace Convertidor.Utility
{
	public class Comment
	{
	
            public int Id { get; set; }
            public int ParentId { get; set; }
            public string hierarchy { get; set; }
            public string Text { get; set; }
            public List<Comment> Children { get; set; }
        
    }
}

