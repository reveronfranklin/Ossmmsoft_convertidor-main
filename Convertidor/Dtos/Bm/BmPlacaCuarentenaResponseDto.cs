namespace Convertidor.Dtos.Bm;

public class BmPlacaCuarentenaResponseDto
{
    public int CodigoPlacaCuarentena { get; set; }
    public string NumeroPlaca { get; set; }
    public string Articulo { get; set; }
    
    public string SearchText { get { return $"{NumeroPlaca}-{Articulo}"; } }

    
}