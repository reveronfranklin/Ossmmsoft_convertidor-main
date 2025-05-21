using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
	public class AdmOrdenesPagoPorPagarService: IAdmOrdenesPagoPorPagarService
    {
        private readonly IAdmOrdenesPagoPorPagarRepository _repository;
        private readonly IAdmOrdenesPagoPorPagarBeneficiarioRepository _beneficiarioRepository;


        public AdmOrdenesPagoPorPagarService(IAdmOrdenesPagoPorPagarRepository repository,IAdmOrdenesPagoPorPagarBeneficiarioRepository beneficiarioRepository)
        {
            _repository = repository;
            _beneficiarioRepository = beneficiarioRepository;
        }


        public async Task<ResultDto<List<AdmOrdenPagoPendientePagoDto>>> GetAll(AdmOrdenPagoPendientePagoFilterDto filter)
        {

            ResultDto<List<AdmOrdenPagoPendientePagoDto>> result = new ResultDto<List<AdmOrdenPagoPendientePagoDto>>(null);
            try
            {

                var ordenesPagoPendientes = await _repository.GetAll(filter.CodigoPresupuesto);

               

                if (ordenesPagoPendientes!=null && ordenesPagoPendientes.FirstOrDefault()!=null)
                {
                    List<AdmOrdenPagoPendientePagoDto> listDto = new List<AdmOrdenPagoPendientePagoDto>();

                    foreach (var item in ordenesPagoPendientes)
                    {
                   
                        
                        AdmOrdenPagoPendientePagoDto dto = new AdmOrdenPagoPendientePagoDto();
                        dto.NumeroOrdenPago =item.NUMERO_ORDEN_PAGO;
                        dto.CodigoOrdenPago =item.CODIGO_ORDEN_PAGO;
                        dto.FechaOrdenPago =item.FECHA_ORDEN_PAGO;
                        dto.TipoOrdenPago=item.TIPO_ORDEN_PAGO;
                        dto.NumeroRecibo = item.NUMERO_RECIBO;
                        dto.CodigoPeriodoOP = item.CODIGO_PERIODICO_OP;
                        dto.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
                        List<AdmOrdenPagoPendientePagoBeneficiarioDto> listBeneficiarios =
                            new List<AdmOrdenPagoPendientePagoBeneficiarioDto>();
                        var beneficiarios = await _beneficiarioRepository.GetByOrdenPago(item.CODIGO_ORDEN_PAGO);
                        if (beneficiarios.Count() > 0)
                        {
                            foreach (var itemBeneficiario in beneficiarios)
                            {
                                AdmOrdenPagoPendientePagoBeneficiarioDto beneficiarioDto =
                                    new AdmOrdenPagoPendientePagoBeneficiarioDto();
                                beneficiarioDto.CodigoOrdenPago = itemBeneficiario.CODIGO_ORDEN_PAGO;
                                beneficiarioDto.CodigoPresupuesto = itemBeneficiario.CODIGO_PRESUPUESTO;
                                beneficiarioDto.CodigoPeriodoOP = itemBeneficiario.CODIGO_PERIODICO_OP;
                                beneficiarioDto.CodigoProveedor = itemBeneficiario.CODIGO_PROVEEDOR;
                                beneficiarioDto.NumeroOrdenPago = itemBeneficiario.NUMERO_ORDEN_PAGO;
                                
                               
                                beneficiarioDto.CodigoContactoProveedor = itemBeneficiario.CODIGO_CONTACTO_PROVEEDOR;
                                beneficiarioDto.PagarALaOrdenDe = itemBeneficiario.PAGAR_A_LA_ORDEN_DE;
                                beneficiarioDto.NombreProveedor = itemBeneficiario.NOMBRE_PROVEEDOR;
                                beneficiarioDto.MontoPorPagar = itemBeneficiario.MONTO_A_PAGAR;
                                beneficiarioDto.CodigoBeneficiarioOp = itemBeneficiario.CODIGO_BENEFICIARIO_OP;
                                beneficiarioDto.Motivo = itemBeneficiario.MOTIVO;
                                beneficiarioDto.MontoAPagar = itemBeneficiario.MONTO_A_PAGAR;

                                listBeneficiarios.Add(beneficiarioDto);

                            }
                        }

                        dto.AdmBeneficiariosPendientesPago = listBeneficiarios;
                        listDto.Add(dto);
                    }


                    result.Data = listDto;
                    result.CantidadRegistros = listDto.Count;
                    result.Page = 1;
                    result.TotalPage = 1;
                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }



  


    }
}

