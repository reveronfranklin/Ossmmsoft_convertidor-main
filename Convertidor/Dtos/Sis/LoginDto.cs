namespace Convertidor.Dtos.Sis
{
	public class LoginDto
	{
        
        public string Email { get; set; } = string.Empty;
        public string Login { get { return ConvertLogin(Email); } }
        public string Password{ get; set; } = string.Empty;

        public string ConvertLogin(string pLogin)
        {

            string login = pLogin.ToUpper();
            if (pLogin.Contains("@"))
            {
                var listStrLineElements = pLogin.Split('@').ToArray();
                login = listStrLineElements[0];
            }
            login = login.ToUpper();
            return login;
        }

    }
    public class ResultLogin
    {
        
        public string Password { get; set; } = string.Empty;
    }


   
}

