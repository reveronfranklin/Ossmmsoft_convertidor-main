using System;
namespace Convertidor.Dtos.Sis
{
	public class LoginDto
	{
        public string Login{ get; set; } = string.Empty;
        public string Password{ get; set; } = string.Empty;
    }
    public class ResultLogin
    {
        
        public string Password { get; set; } = string.Empty;
    }
}

