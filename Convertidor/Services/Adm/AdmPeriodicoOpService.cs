using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmPeriodicoOpService : IAdmPeriodicoOpService
    {
        private readonly IAdmPeriodicoOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
        private readonly IPRE_PRESUPUESTOSRepository _preSUPUESTOSRepository;
        
     

        public AdmPeriodicoOpService(IAdmPeriodicoOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     IPRE_PRESUPUESTOSRepository preSUPUESTOSRepository)
                                     
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _preSUPUESTOSRepository = preSUPUESTOSRepository;
    
            
        }

       
        public async Task<AdmPeriodicoOpResponseDto> MapPeriodicoOpDto(ADM_PERIODICO_OP dtos)
        {
            AdmPeriodicoOpResponseDto itemResult = new AdmPeriodicoOpResponseDto();
            itemResult.CodigoPeriodicoOp = dtos.CODIGO_PERIODICO_OP;
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.CantidadPago=dtos.CANTIDAD_PAGO;
            itemResult.NumeroPago = dtos.NUMERO_PAGO;
            itemResult.FechaPago = dtos.FECHA_PAGO;
            itemResult.FechaPagoString = Fecha.GetFechaString(dtos.FECHA_PAGO);
            FechaDto fechaPagoObj = Fecha.GetFechaDto(dtos.FECHA_PAGO);
            itemResult.FechaPagoObj =(FechaDto)fechaPagoObj;
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.Monto = dtos.MONTO;
            itemResult.MontoAnulado = dtos.MONTO_ANULADO;
            itemResult.MontoPagado = dtos.MONTO_PAGADO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
           
            
            return itemResult;
        }

        public async Task<List<AdmPeriodicoOpResponseDto>> MapListPeriodicoOpDto(List<ADM_PERIODICO_OP> dtos)
        {
            List<AdmPeriodicoOpResponseDto> result = new List<AdmPeriodicoOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    if (item != null)
                    {
                        var itemResult = await MapPeriodicoOpDto(item);

                        result.Add(itemResult);
                    }
                    
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmPeriodicoOpResponseDto>>> GetAll()
        {

            ResultDto<List<AdmPeriodicoOpResponseDto>> result = new ResultDto<List<AdmPeriodicoOpResponseDto>>(null);
            try
            {
                var periodicoOp = await _repository.GetAll();
                var cant = periodicoOp.Count();
                if (periodicoOp != null && periodicoOp.Count() > 0)
                {
                    var listDto = await MapListPeriodicoOpDto(periodicoOp);

                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";
                    

                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No data";

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }

        public async Task<ResultDto<AdmPeriodicoOpResponseDto>> Update(AdmPeriodicoOpUpdateDto dto)
        {
            ResultDto<AdmPeriodicoOpResponseDto> result = new ResultDto<AdmPeriodicoOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPeriodicoOp = await _repository.GetCodigoPeriodicoOp(dto.CodigoPeriodicoOp);
                if (codigoPeriodicoOp == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo periodico op no existe";
                    return result;
                }

                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (dto.CodigoOrdenPago < 0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago no existe";
                    return result;
                }


                
                if (dto.NumeroPago is not null && dto.NumeroPago.Length > 20 )
                {
                    
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero pago invalido";
                    return result;
                }

                var fechaPago = await _repository.GetFechaPago(dto.FechaPago);
                if (dto.FechaPago == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha pago invalida";
                    return result;
                }

                if (dto.Motivo is not null && dto.Motivo.Length > 2000)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Notivo pago invalido";
                    return result;
                }

                if (dto.Monto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto invalido";
                    return result;
                }

                if (dto.MontoPagado < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Pagado invalido";
                    return result;
                }

                if (dto.MontoAnulado < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Anulado invalido";
                    return result;
                }

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

                var codigoPresupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if(dto.CodigoPresupuesto < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo presupuesto invalido";
                    return result;
                }




                codigoPeriodicoOp.CODIGO_PERIODICO_OP = dto.CodigoPeriodicoOp;
                codigoPeriodicoOp.CODIGO_ORDEN_PAGO=dto.CodigoOrdenPago;
                codigoPeriodicoOp.CANTIDAD_PAGO=dto.CodigoOrdenPago;
                codigoPeriodicoOp.NUMERO_PAGO = dto.NumeroPago;
                codigoPeriodicoOp.FECHA_PAGO = dto.FechaPago;
                codigoPeriodicoOp.MOTIVO = dto.Motivo;
                codigoPeriodicoOp.MONTO=dto.Monto;
                codigoPeriodicoOp.MONTO_PAGADO=dto.MontoPagado;
                codigoPeriodicoOp.MONTO_ANULADO = dto.MontoAnulado;
                codigoPeriodicoOp.EXTRA1 = dto.Extra1;
                codigoPeriodicoOp.EXTRA2 = dto.Extra2;
                codigoPeriodicoOp.EXTRA3 = dto.Extra3;
                codigoPeriodicoOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


                codigoPeriodicoOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoPeriodicoOp.USUARIO_UPD = conectado.Usuario;
                codigoPeriodicoOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoPeriodicoOp);

                var resultDto = await MapPeriodicoOpDto(codigoPeriodicoOp);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ResultDto<AdmPeriodicoOpResponseDto>> Create(AdmPeriodicoOpUpdateDto dto)
        {
            ResultDto<AdmPeriodicoOpResponseDto> result = new ResultDto<AdmPeriodicoOpResponseDto>(null);
            try
            {
                

                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPeriodicoOp = await _repository.GetCodigoPeriodicoOp(dto.CodigoPeriodicoOp);
                if (codigoPeriodicoOp != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo periodico ya no existe";
                    return result;
                }

                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (dto.CodigoOrdenPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago no existe";
                    return result;
                }



                if (dto.NumeroPago is not null && dto.NumeroPago.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero pago invalido";
                    return result;
                }

                var fechaPago = await _repository.GetFechaPago(dto.FechaPago);
                if (dto.FechaPago == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha pago invalida";
                    return result;
                }

                if (dto.Motivo is not null && dto.Motivo.Length > 2000)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Notivo pago invalido";
                    return result;
                }

                if (dto.Monto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto invalido";
                    return result;
                }

                if (dto.MontoPagado < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Pagado invalido";
                    return result;
                }

                if (dto.MontoAnulado < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Anulado invalido";
                    return result;
                }

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

                var codigoPresupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo presupuesto invalido";
                    return result;
                }


                ADM_PERIODICO_OP entity = new ADM_PERIODICO_OP();
                entity.CODIGO_PERIODICO_OP = await _repository.GetNextKey();
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.CANTIDAD_PAGO = dto.CantidadPago;
                entity.NUMERO_PAGO = dto.NumeroPago;
                entity.FECHA_PAGO =dto.FechaPago;
                entity.MOTIVO = dto.Motivo;
                entity.MONTO = dto.Monto;
                entity.MONTO_PAGADO = dto.MontoPagado;
                entity.MONTO_ANULADO = dto.MontoAnulado;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;



                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPeriodicoOpDto(created.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {

                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                }

                return result;


            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<AdmPeriodicoOpDeleteDto>> Delete(AdmPeriodicoOpDeleteDto dto) 
        {
            ResultDto<AdmPeriodicoOpDeleteDto> result = new ResultDto<AdmPeriodicoOpDeleteDto>(null);
            try
            {

                var codigoPeriodicoOp = await _repository.GetCodigoPeriodicoOp(dto.CodigoPeriodicoOp);
                if (codigoPeriodicoOp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Beneficiario op no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPeriodicoOp);

                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }
    }
 }

