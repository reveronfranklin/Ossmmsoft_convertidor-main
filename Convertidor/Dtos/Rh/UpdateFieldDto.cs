using System;
namespace Convertidor.Dtos.Rh
{
	public class UpdateFieldDto
	{

		public string Field { get; set; } = string.Empty;
        public int Id { get; set; }
		public string Value { get; set; } = string.Empty;
	}
}

