namespace Convertidor.Data.Entities.Rh;

public class RH_V_TITULAR_BENEFICIARIOS
{

    public string? CEDULA_TITULAR { get; set; }
    public string? CEDULA_BENEFICIARIO { get; set; }
    public string? NOMBRES_TITU_BENE { get; set; }
    public string? APELLIDOS_TITU_BENE { get; set; }            
    public DateTime? FECHA_NACIMIENTO_FAMILIAR { get; set; } 
    public string? SEXO { get; set; }
    public string? ESTADO_CIVIL { get; set; }
    public string? EDAD { get; set; }
    public string? CD_LOCALIDAD { get; set; }
    public string? CD_GRUPO { get; set; }
    public string? CD_BANCO { get; set; }
    public string? NU_CUENTA { get; set; }
    public string? TP_CUENTA { get; set; }
    public string? DE_EMAIL { get; set; }
    public string? NRO_AREA { get; set; }
    public string? NRO_TELEFONO { get; set; } 
    public DateTime? FECHA_EGRESO { get; set; }
    
    public int? CODIGO_ICP { get; set; }
    public string? PARENTESCO { get; set; }
    public int? CODIGO_TIPO_NOMINA { get; set; }
    public string? TIPO_NOMINA { get; set; }
    public DateTime? FECHA_INGRESO { get; set; }
 
    public string? UNIDAD_ADSCRIPCION { get; set; }
    public string? CARGO_NOMINAL { get; set; }
    public string? ANTIGUEDAD_CMC { get; set; }
    public string?   ANTIGUEDAD_OTROS { get; set; }
    public decimal?   ORDER_BY { get; set; }
    
}