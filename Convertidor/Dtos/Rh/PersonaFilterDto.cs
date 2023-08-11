using System;
namespace Convertidor.Dtos.Rh
{
	public class PersonaFilterDto
	{
		public int CodigoPersona { get; set; }
		public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
    }
}

