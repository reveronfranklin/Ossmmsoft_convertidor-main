namespace Convertidor.Dtos.Sis;

public class AuthPermissionResponseDto
{
    public int Id { get; set; }
 
    public int ContentTypeId { get; set; } 
    public string AppLabel { get; set; }  
    public string Model { get; set; }  
    public string Codename { get; set; }  
    public string Name { get; set; }  
    public string SearchText { get { return $"{AppLabel}-{Model}-{Codename}-{Name}"; } }

}