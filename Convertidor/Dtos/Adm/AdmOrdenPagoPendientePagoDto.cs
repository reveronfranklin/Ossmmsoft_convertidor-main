namespace Convertidor.Dtos.Adm;

public class AdmOrdenPagoPendientePagoDto
{
    public string  NumeroOrdenPago  { get; set; }
    public int  CodigoOrdenPago  { get; set; }
    public DateTime  FechaOrdenPago  { get; set; }   
    public string  TipoOrdenPago  { get; set; }   
    public string  NumeroRecibo  { get; set; }     
    public int  CodigoPeriodoOP  { get; set; }
    public int  CodigoPresupuesto  { get; set; }

    public  List<AdmOrdenPagoPendientePagoBeneficiarioDto>  AdmBeneficiariosPendientesPago { get; set; }
}

public class AdmOrdenPagoPendientePagoBeneficiarioDto
{
    public int  CodigoOrdenPago  { get; set; }
    public int  CodigoPresupuesto  { get; set; }
    public int  CodigoPeriodoOP  { get; set; }
    public int  CodigoProveedor  { get; set; }
    public string  NumeroOrdenPago  { get; set; }
  

    public int  CodigoContactoProveedor  { get; set; }
    public string  PagarALaOrdenDe  { get; set; }    
    public string  NombreProveedor  { get; set; }   
    public decimal  MontoPorPagar  { get; set; }
    public int  CodigoBeneficiarioOp  { get; set; }
    public string  Motivo  { get; set; }   
    public decimal  MontoAPagar  { get; set; }
}




public class AdmOrdenPagoPendientePagoFilterDto
{
    public int  CodigoPresupuesto  { get; set; }
   
}


