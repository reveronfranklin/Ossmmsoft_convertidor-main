namespace Convertidor.Dtos;

public class EmailDto
{
    public string To { get; set; } = String.Empty;
    public string Subject { get; set; }= String.Empty;
    public string Content { get; set; }= String.Empty;
    
    public string FilePath  { get; set; }= String.Empty;
    
}