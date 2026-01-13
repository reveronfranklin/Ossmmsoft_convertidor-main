namespace Convertidor.Dtos.Adm.Pagos;

public class PagoUpdateDto
{
    //Datos del Pago
    public int CodigoPago { get; set; } 
    public int CodigoBeneficiarioPago { get; set; }
    public string Motivo { get; set; } = string.Empty; //Indicado por el usuario
    public decimal Monto { get; set; } //Ingresado por el usuario 


}