using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Sis;
using Convertidor.Services.Adm;
using Convertidor.Services.Adm.Pagos;
using Convertidor.Utility;


namespace Convertidor.Services.Adm
{
    
	public class AdmLotePagoService: IAdmLotePagoService
    {
        private readonly IAdmLotePagoRepository _repository;
        private readonly ISisCuentaBancoRepository _sisCuentaBancoRepository;
        private readonly ISisDescriptivaRepository _sisDescriptivaRepository;
        private readonly ISisBancoRepository _bancoRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _presupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmChequesRepository _chequesRepository;
        private readonly IAdmPagoElectronicoService _admPagoElectronicoService;
        private readonly IAdmBeneficiariosOpRepository _admBeneficiariosOpRepository;
        private readonly IAdmBeneficiariosPagosRepository _admBeneficiariosPagosRepository;


        public AdmLotePagoService(IAdmLotePagoRepository repository,
                                        ISisCuentaBancoRepository sisCuentaBancoRepository,
                                        ISisDescriptivaRepository sisDescriptivaRepository,
                                        ISisBancoRepository bancoRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                        IPRE_PRESUPUESTOSRepository presupuestosRepository,
                                        IAdmDescriptivaRepository admDescriptivaRepository,
                                        IAdmChequesRepository chequesRepository,
                                        IAdmPagoElectronicoService admPagoElectronicoService,
                                        IAdmBeneficiariosOpRepository admBeneficiariosOpRepository,
                                        IAdmBeneficiariosPagosRepository admBeneficiariosPagosRepository)
		{
            _repository = repository;
            _repository = repository;
            _sisCuentaBancoRepository = sisCuentaBancoRepository;
            _sisDescriptivaRepository = sisDescriptivaRepository;
            _bancoRepository = bancoRepository;
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _presupuestosRepository = presupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _chequesRepository = chequesRepository;
            _admPagoElectronicoService = admPagoElectronicoService;
            _admBeneficiariosOpRepository = admBeneficiariosOpRepository;
            _admBeneficiariosPagosRepository = admBeneficiariosPagosRepository;
        }

        
       
       
        public  async Task<AdmLotePagoResponseDto> MapAdmLotePagoDto(ADM_LOTE_PAGO dtos)
        {
            AdmLotePagoResponseDto itemResult = new AdmLotePagoResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                
                itemResult.CodigoLotePago = dtos.CODIGO_LOTE_PAGO;
                itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
             
                var cuenta=await _sisCuentaBancoRepository.GetByCodigo(dtos.CODIGO_CUENTA_BANCO);
                if (cuenta != null)
                {
                    itemResult.NumeroCuenta = cuenta.NO_CUENTA;
                    var banco = await _bancoRepository.GetById(cuenta.CODIGO_BANCO);
                    if (banco != null)
                    {
                        itemResult.CodigoBanco = cuenta.CODIGO_BANCO;
                        itemResult.NombreBanco = banco.NOMBRE;
                    }
                }
                itemResult.TipoPagoId = dtos.TIPO_PAGO_ID;
                itemResult.DescripcionTipoPago = "";
                var descriptivaTipoCuenta = await _admDescriptivaRepository.GetByCodigo(itemResult.TipoPagoId);
                if (descriptivaTipoCuenta!=null)
                {
                    itemResult.DescripcionTipoPago = descriptivaTipoCuenta.DESCRIPCION;
                }
                itemResult.FechaPago = dtos.FECHA_PAGO;
                itemResult.FechaPagoString=Fecha.GetFechaString(dtos.FECHA_PAGO);
                itemResult.FechaPagoDto=Fecha.GetFechaDto(dtos.FECHA_PAGO);
                itemResult.CodigoPresupuesto = (int)dtos.CODIGO_PRESUPUESTO;
                itemResult.SearchText=dtos.SEARCH_TEXT;
                itemResult.Status = dtos.STATUS;
                itemResult.Titulo = dtos.TITULO;
                itemResult.FileName=$"/ExcelFile/{dtos.FILE_NAME}";
                

      
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<AdmLotePagoResponseDto>> MapListLotePagoDto(List<ADM_LOTE_PAGO> dtos)
        {
            List<AdmLotePagoResponseDto> result = new List<AdmLotePagoResponseDto>();
            if (dtos.Count > 0)
            {
                foreach (var item in dtos)
                {
                    if (item == null)
                    {
                        var detener = "";
                    }
                    else
                    {
                        var itemResult =  await MapAdmLotePagoDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }

        public async Task<string> GetSearchText(ADM_LOTE_PAGO dto)
        {
            var result = "";
            var cuentaBanco = await   _sisCuentaBancoRepository.GetById(dto.CODIGO_CUENTA_BANCO);
            var banco=await _bancoRepository.GetByCodigo(cuentaBanco.CODIGO_BANCO);
            var descriptivaTipoPago = await _admDescriptivaRepository.GetByCodigo(dto.TIPO_PAGO_ID);

            result = $"{descriptivaTipoPago.DESCRIPCION}-{cuentaBanco.NO_CUENTA}-{banco.NOMBRE}-{dto.STATUS}-{dto.TITULO}";

            return result;
        }
        
        
        public async Task<ResultDto<AdmLotePagoResponseDto>> Aprobar(AdmLotePagoCambioStatusDto dto)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            ResultDto<AdmLotePagoResponseDto> result = new ResultDto<AdmLotePagoResponseDto>(null);
            try
            {
                var lotePago=await _repository.GetByCodigo(dto.CodigoLotePago);
                if (lotePago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote no existe";
                    return result;
                }

                if (lotePago.STATUS != "PE" )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote de pago debe estar Pendiente para poder Aprobarlo";
                    return result;
                }
                
                lotePago.STATUS = dto.Status;
                lotePago.SEARCH_TEXT = await GetSearchText(lotePago);

                lotePago.FECHA_UPD = DateTime.Now;
                lotePago.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(lotePago);
                await _chequesRepository.CambioEstatus(dto.Status,lotePago.CODIGO_LOTE_PAGO,conectado.Usuario,DateTime.Now);
                var pagoElectronicp=await _admPagoElectronicoService.GenerateFilePagoElectronico(lotePago.CODIGO_LOTE_PAGO,conectado.Usuario);
                var resultDto = await  MapAdmLotePagoDto(lotePago);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
                result.LinkData = pagoElectronicp.LinkData;
                result.LinkDataArlternative = pagoElectronicp.LinkDataArlternative;
                return result;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = e.Message;
            }
            return result;
        }

        
        public async Task<ResultDto<AdmLotePagoResponseDto>> Anular(AdmLotePagoCambioStatusDto dto)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            ResultDto<AdmLotePagoResponseDto> result = new ResultDto<AdmLotePagoResponseDto>(null);
            try
            {
                var lotePago=await _repository.GetByCodigo(dto.CodigoLotePago);
                if (lotePago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote no existe";
                    return result;
                }

                lotePago.STATUS = "AP";
                if (lotePago.STATUS != "AP" )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote de pago debe estar Aprobado para poder Anularlo";
                    return result;
                }
                
                lotePago.STATUS = dto.Status;
                lotePago.SEARCH_TEXT = await GetSearchText(lotePago);

                lotePago.FECHA_UPD = DateTime.Now;
                lotePago.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(lotePago);
                await _chequesRepository.CambioEstatus(dto.Status,lotePago.CODIGO_LOTE_PAGO,conectado.Usuario,DateTime.Now);
                await AnularMontoPagadoEnOrdenPago(lotePago.CODIGO_LOTE_PAGO);
                var resultDto = await  MapAdmLotePagoDto(lotePago);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = e.Message;
            }
            return result;
        }

        public async Task AnularMontoPagadoEnOrdenPago(int codigoLotePago)
        {
            AdmChequeFilterDto filter = new AdmChequeFilterDto();
            filter.CodigoLote=codigoLotePago;
            filter.SearchText = "";
            filter.PageNumber = 1;
            filter.PageSize = 1000;
            var pagos=await _chequesRepository.GetByLote(filter);
            if (pagos.Data !=null )
            {
                foreach (var itemPago in pagos.Data)
                {
                    var beneficiariosLotePago=await _admBeneficiariosPagosRepository.GetByPago(itemPago.CODIGO_CHEQUE);
                    if (beneficiariosLotePago!=null && beneficiariosLotePago.CODIGO_BENEFICIARIO_OP!=null)
                    {
                        await _admBeneficiariosOpRepository.UpdateMontoPagado((int)beneficiariosLotePago
                            .CODIGO_BENEFICIARIO_OP,0);
                    }
                }
            }
            
          
        }
        
        public async Task<ResultDto<AdmLotePagoResponseDto>> CambioStatus(AdmLotePagoCambioStatusDto dto)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            ResultDto<AdmLotePagoResponseDto> result = new ResultDto<AdmLotePagoResponseDto>(null);
            try
            {
                var lotePago=await _repository.GetByCodigo(dto.CodigoLotePago);
                if (lotePago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote no existe";
                    return result;
                }

               
                
                lotePago.STATUS = dto.Status;
                lotePago.SEARCH_TEXT = await GetSearchText(lotePago);

                lotePago.FECHA_UPD = DateTime.Now;
                lotePago.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(lotePago);
                await _chequesRepository.CambioEstatus(dto.Status,lotePago.CODIGO_LOTE_PAGO,conectado.Usuario,DateTime.Now);

                var resultDto = await  MapAdmLotePagoDto(lotePago);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = e.Message;
            }
            return result;
        }
        public async Task<ResultDto<AdmLotePagoResponseDto>> Update(AdmLotePagoUpdateDto dto)
        {

            ResultDto<AdmLotePagoResponseDto> result = new ResultDto<AdmLotePagoResponseDto>(null);
            try
            {
                
                if (string.IsNullOrEmpty(dto.Titulo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }
                
                var conectado = await _sisUsuarioRepository.GetConectado();
                var lotePago=await _repository.GetByCodigo(dto.CodigoLotePago);
                if (lotePago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote no existe";
                    return result;
                }
                var cuentaBanco = await   _sisCuentaBancoRepository.GetById(dto.CodigoCuentaBanco);
                if (cuentaBanco == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cuenta Banco no existe";
                    return result;
                }
                var banco=await _bancoRepository.GetByCodigo(cuentaBanco.CODIGO_BANCO);
                
                var descriptivaTipoPago = await _admDescriptivaRepository.GetByCodigo(dto.TipoPagoId);
                if (descriptivaTipoPago==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Pago no existe";
                    return result;
                }
                
                
                var presupuesto=await _presupuestosRepository.GetByCodigo(conectado.Empresa,dto.CodigoPresupuesto);
                if (presupuesto==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }
                
                lotePago.FECHA_PAGO=dto.FechaPago;
                lotePago.CODIGO_CUENTA_BANCO=dto.CodigoCuentaBanco;
                lotePago.TIPO_PAGO_ID=dto.TipoPagoId;
                lotePago.TITULO = dto.Titulo;
                lotePago.SEARCH_TEXT = $"{descriptivaTipoPago.DESCRIPCION}-{cuentaBanco.NO_CUENTA}-{banco.NOMBRE}-{lotePago.STATUS}-{lotePago.TITULO}";
                lotePago.CODIGO_PRESUPUESTO=dto.CodigoPresupuesto;
               
               
             
                lotePago.CODIGO_EMPRESA = conectado.Empresa;
                lotePago.FECHA_UPD = DateTime.Now;
                lotePago.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(lotePago);
                var resultDto = await  MapAdmLotePagoDto(lotePago);
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

        public async Task<ResultDto<AdmLotePagoResponseDto>> Create(AdmLotePagoUpdateDto dto)
        {

            ResultDto<AdmLotePagoResponseDto> result = new ResultDto<AdmLotePagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (string.IsNullOrEmpty(dto.Titulo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }
                var cuentaBanco = await   _sisCuentaBancoRepository.GetById(dto.CodigoCuentaBanco);
                if (cuentaBanco == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cuenta Banco no existe";
                    return result;
                }
              
                
                var descriptivaTipoPago = await _admDescriptivaRepository.GetByCodigo(dto.TipoPagoId);
                if (descriptivaTipoPago==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Pago no existe";
                    return result;
                }
                var banco=await _bancoRepository.GetByCodigo(cuentaBanco.CODIGO_BANCO);
                
                var presupuesto=await _presupuestosRepository.GetByCodigo(conectado.Empresa,dto.CodigoPresupuesto);
                if (presupuesto==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }
                
                
                ADM_LOTE_PAGO entity = new ADM_LOTE_PAGO();
                entity.CODIGO_LOTE_PAGO = await _repository.GetNextKey();
                entity.FECHA_PAGO=dto.FechaPago;
                entity.CODIGO_CUENTA_BANCO=dto.CodigoCuentaBanco;
                entity.TIPO_PAGO_ID=dto.TipoPagoId;
                entity.TITULO = dto.Titulo;
                entity.STATUS = "PE";
                entity.SEARCH_TEXT = $"{descriptivaTipoPago.DESCRIPCION}-{cuentaBanco.NO_CUENTA}-{banco.NOMBRE}-{entity.STATUS}-{entity.TITULO}";

                entity.CODIGO_PRESUPUESTO=dto.CodigoPresupuesto;
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapAdmLotePagoDto(created.Data);
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
 
        public async Task<ResultDto<AdmLotePagoDeleteDto>> Delete(AdmLotePagoDeleteDto dto)
        {

            ResultDto<AdmLotePagoDeleteDto> result = new ResultDto<AdmLotePagoDeleteDto>(null);
            try
            {

                var cuenta = await _repository.GetByCodigo(dto.CodigoLotePago);
                if (cuenta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote No existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoLotePago);

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


        public async Task<ResultDto<List<AdmLotePagoResponseDto>>> GetAll(AdmLotePagoFilterDto filter)
        {
            ResultDto<List<AdmLotePagoResponseDto>> result = new ResultDto<List<AdmLotePagoResponseDto>>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                filter.CodigEmpresa = conectado.Empresa;
                var lotes = await _repository.GetAll(filter);
                if (lotes.Data == null)
                {
                    result.Data = null;
                    result.CantidadRegistros=0;
                    result.Page = 0;
                    result.TotalPage = 0;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListLotePagoDto(lotes.Data);
                result.Data = resultDto;
                result.CantidadRegistros=lotes.CantidadRegistros;
                result.Page = lotes.Page;
                result.TotalPage = lotes.TotalPage;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
           
        }

    
        
    }
}

