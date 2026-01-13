namespace Convertidor.Dtos.Sis;

public class AuthUserPermisionResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }  
    public string Login { get; set; }  
    public int PermisionId { get; set; }
    public string DescriptionPermision { get; set; }  
    
}