namespace Convertidor.Dtos.Adm;

public class AdmProductosResponse
{
    public int Codigo  { get; set; }	
    
    public string Descripcion { get; set; }=string.Empty;
    public string CodigoConcat { get; set; }=string.Empty;
    public string DescripcionReal { get; set; }=string.Empty;
}