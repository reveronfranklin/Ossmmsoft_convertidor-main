using System;
namespace Convertidor.Dtos.Sis
{
	public class ResultLoginDto
	{

        

        public string Message { get; set; } = string.Empty;
        public string accessToken { get; set; } = string.Empty;
        public string refreshToken { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public UserData? UserData { get; set; }
    }


    public class UserData
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;

    }

}

