namespace Convertidor.Dtos.Sis
{
	public class ResultLoginDto
	{

        

        public string Message { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public UserData? UserData { get; set; }
    }


    public class UserData
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public List<UserRole>? Roles { get; set; }
        public string username { get; set; } = string.Empty;

    }
    public class UserRole
    {
       
        public string Role { get; set; } = string.Empty;
        

    }
    
    public class AuthResponse
    {
        public string Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public List<UserRole>? Roles { get; set; }
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

    }
    

}

