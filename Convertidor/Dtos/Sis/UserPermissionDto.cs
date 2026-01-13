namespace Convertidor.Dtos.Sis;

public class UserPermissionDto
{
    public int UserId { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public bool IsSuperUser { get; set; }
    public List<Permission> Permissions { get; set; }
}
public class Permission
{
    public string Model { get; set; }
    public List<string> Actions { get; set; }
}
