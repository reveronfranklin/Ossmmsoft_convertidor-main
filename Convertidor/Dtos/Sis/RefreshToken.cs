using System;
namespace Convertidor.Dtos.Sis
{
	public class RefreshToken
	{
        public string Token { get; set; } = string.Empty;
        public string Refresh_Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
        public string Login { get; set; }
    }
}

