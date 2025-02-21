namespace Convertidor.Data.Entities.Rh;

public class RH_V_TITULAR_BENEFICIARIOS
{

    public string? CEDULA_TITULAR { get; set; } = String.Empty;
    public string? CEDULA_BENEFICIARIO { get; set; }= String.Empty;
    public string? NOMBRES_TITU_BENE { get; set; }= String.Empty;
    public string? APELLIDOS_TITU_BENE { get; set; }  = String.Empty;          
    public DateTime? FECHA_NACIMIENTO_FAMILIAR { get; set; } 
    public string? SEXO { get; set; }= String.Empty;
    public string? ESTADO_CIVIL { get; set; }= String.Empty;
    public string? EDAD { get; set; }= String.Empty;
    public string? CD_LOCALIDAD { get; set; }= String.Empty;
    public string? CD_GRUPO { get; set; }= String.Empty;
    public string? CD_BANCO { get; set; }= String.Empty;
    public string? NU_CUENTA { get; set; }= String.Empty;
    public string? TP_CUENTA { get; set; }= String.Empty;
    public string? DE_EMAIL { get; set; }= String.Empty;
    public string? NRO_AREA { get; set; }= String.Empty;
    public string? NRO_TELEFONO { get; set; } = String.Empty;
    public DateTime? FECHA_EGRESO { get; set; }
    
    public int? CODIGO_ICP { get; set; }
    public string? PARENTESCO { get; set; }= String.Empty;
    
    public string? VINCULO { get; set; }= String.Empty;
    public int? CODIGO_TIPO_NOMINA { get; set; }
    public string? TIPO_NOMINA { get; set; }= String.Empty;
    public DateTime? FECHA_INGRESO { get; set; }
 
    public string? UNIDAD_ADSCRIPCION { get; set; }= String.Empty;
    public string? CARGO_NOMINAL { get; set; }= String.Empty;
    public string? ANTIGUEDAD_CMC { get; set; }= String.Empty;
    public string?   ANTIGUEDAD_OTROS { get; set; }= String.Empty;
    public decimal?   ORDER_BY { get; set; }
    
}