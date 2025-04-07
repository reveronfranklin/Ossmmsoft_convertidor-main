using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmChequesService : IAdmChequesService
    {
        private readonly IAdmChequesRepository _repository;
        private readonly IAdmProveedoresRepository _proveedoresRepository;
        private readonly IAdmContactosProveedorRepository _contactosProveedorRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly ISisCuentaBancoRepository _sisCuentaBancoRepository;
        private readonly ISisBancoRepository _sisBancoRepository;
        private readonly IAdmLotePagoRepository _admLotePagoRepository;
        private readonly IOssConfigRepository _ossConfigRepository;

        public AdmChequesService( IAdmChequesRepository repository,
                                      IAdmProveedoresRepository proveedoresRepository,
                                      IAdmContactosProveedorRepository contactosProveedorRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                      IAdmDescriptivaRepository admDescriptivaRepository,
                                      ISisCuentaBancoRepository sisCuentaBancoRepository,
                                      ISisBancoRepository sisBancoRepository,
                                      IAdmLotePagoRepository admLotePagoRepository,
                                      IOssConfigRepository ossConfigRepository)
        {
            _repository = repository;
            _proveedoresRepository = proveedoresRepository;
            _contactosProveedorRepository = contactosProveedorRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _sisCuentaBancoRepository = sisCuentaBancoRepository;
            _sisBancoRepository = sisBancoRepository;
            _admLotePagoRepository = admLotePagoRepository;
            _ossConfigRepository = ossConfigRepository;
        }

       

        public async Task<AdmChequesResponseDto> MapChequesDto(ADM_CHEQUES dtos)
        {
            AdmChequesResponseDto itemResult = new AdmChequesResponseDto();

            itemResult.CodigoLote = (int)dtos.CODIGO_LOTE_PAGO;
            itemResult.CodigoCheque = dtos.CODIGO_CHEQUE;
            itemResult.Ano = (int)dtos.ANO;
            itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
            var cuenta = await _sisCuentaBancoRepository.GetByCodigo(dtos.CODIGO_CUENTA_BANCO);
            if (cuenta!=null)
            {
                itemResult.NroCuenta = cuenta.NO_CUENTA;
                itemResult.CodigoBanco = cuenta.CODIGO_BANCO;
                itemResult.NombreBanco = "";
                var banco = await _sisBancoRepository.GetByCodigo(cuenta.CODIGO_BANCO);
                if (banco != null)
                {
                    itemResult.NombreBanco = banco.NOMBRE;
                }
            }


            itemResult.NumeroChequera = dtos.NUMERO_CHEQUERA;
            itemResult.NumeroCheque = dtos.NUMERO_CHEQUE;
            itemResult.FechaCheque = dtos.FECHA_CHEQUE;
            itemResult.FechaChequeString = Fecha.GetFechaString(dtos.FECHA_CHEQUE);
            FechaDto fechaChequeObj = Fecha.GetFechaDto(dtos.FECHA_CHEQUE);
            itemResult.FechaChequeObj =(FechaDto) fechaChequeObj;
       
         
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.NombreProveedor = "";
            var proveedor = await _proveedoresRepository.GetByCodigo(dtos.CODIGO_PROVEEDOR);
            if(proveedor!=null)
            {
                itemResult.NombreProveedor = proveedor.NOMBRE_PROVEEDOR;
            }
    
            itemResult.PrintCount = dtos.PRINT_COUNT;
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.Status = dtos.STATUS;
            itemResult.Endoso = dtos.ENDOSO;
            itemResult.CodigoPresupuesto = (int)dtos.CODIGO_PRESUPUESTO;

            itemResult.TipoChequeID = (int)dtos.TIPO_CHEQUE_ID;
            itemResult.DescripcionTipoCheque = "";
            var descripcionTipoCheque = await _admDescriptivaRepository.GetByCodigo(itemResult.TipoChequeID);
            if (descripcionTipoCheque != null)
            {
                itemResult.DescripcionTipoCheque = descripcionTipoCheque.DESCRIPCION;
            }
            
            if (dtos.FECHA_ENTREGA != null)
            {
                itemResult.FechaEntrega =dtos.FECHA_ENTREGA;
                itemResult.FechaEntregaString =Fecha.GetFechaString((DateTime)dtos.FECHA_ENTREGA);
                FechaDto fechaEntregaObj = Fecha.GetFechaDto((DateTime)dtos.FECHA_ENTREGA);
                itemResult.FechaEntregaObj = (FechaDto) fechaEntregaObj;
            }
          
           


            return itemResult;
        }

        public async Task<List<AdmChequesResponseDto>> MapListChequesDto(List<ADM_CHEQUES> dtos)
        {
            List<AdmChequesResponseDto> result = new List<AdmChequesResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapChequesDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmChequesResponseDto>>> GetByLote(AdmChequeFilterDto dto)
        {

            ResultDto<List<AdmChequesResponseDto>> result = new ResultDto<List<AdmChequesResponseDto>>(null);
            try
            {
                await _repository.UpdateSearchText(dto.CodigoLote);
                var cheques = await _repository.GetByLote(dto);
             
                if (cheques.Data != null && cheques.Data.Count() > 0)
                {
                    var listDto = await MapListChequesDto(cheques.Data);

                    result.Data = listDto;
                    result.IsValid = true;
                    result.CantidadRegistros = cheques.CantidadRegistros;
                    result.Page = cheques.Page;
                    result.TotalPage= cheques.TotalPage;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.CantidadRegistros = 0;
                    result.Page = 0;
                    result.TotalPage= 0;
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
        

        public async Task<ResultDto<AdmChequesResponseDto>> Update(AdmChequesUpdateDto dto)
        {
            ResultDto<AdmChequesResponseDto> result = new ResultDto<AdmChequesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var lote = await _admLotePagoRepository.GetByCodigo(dto.CodigoLote);
                if (lote == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote No Existe";
                    return result;
                }
                var cheque = await _repository.GetByCodigoCheque(dto.CodigoCheque);
                if (cheque == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cheque no existe";
                    return result;
                }

                
           

                var cuenta=await _sisCuentaBancoRepository.GetByCodigo(dto.CodigoCuentaBanco);
                if (cuenta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta Banco invalido";
                    return result;
                }
             

               

             
           
                var codigoProveedor = await _proveedoresRepository.GetByCodigo(dto.CodigoProveedor);

                if (codigoProveedor==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }


           
                if (dto.PrintCount < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "PrintCount Invalido";
                    return result;

                }

                if (dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;

                }
                if (dto.Endoso is not null && dto.Endoso.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ensoso Invalido";
                    return result;

                }

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;

                }
              

           


                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto==null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                dto.Ano = presupuesto.ANO;
                var tipoChequeID = await _admDescriptivaRepository.GetByCodigo(dto.TipoChequeID);
                if(tipoChequeID==null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo ChequeId Invalido";
                    return result;
                }
                
                cheque.ANO = dto.Ano;
                cheque.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
              
                cheque.FECHA_CHEQUE = dto.FechaCheque;
              
                cheque.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                cheque.PRINT_COUNT = dto.PrintCount;
                cheque.MOTIVO = dto.Motivo;
                cheque.STATUS = dto.Status;
                cheque.ENDOSO = dto.Endoso;
                cheque.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
           
                cheque.CODIGO_EMPRESA = conectado.Empresa;
                cheque.USUARIO_UPD = conectado.Usuario;
                cheque.FECHA_UPD = DateTime.Now;

                await _repository.Update(cheque);

                var resultDto = await MapChequesDto(cheque);
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

        public async Task<ResultDto<AdmChequesResponseDto>> Create(AdmChequesUpdateDto dto)
        {
            ResultDto<AdmChequesResponseDto> result = new ResultDto<AdmChequesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var lote = await _admLotePagoRepository.GetByCodigo(dto.CodigoLote);
                if (lote == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Lote No Existe";
                    return result;
                }
                var codigoCheque = await _repository.GetByCodigoCheque(dto.CodigoCheque);
                if (codigoCheque != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo cheque Ya existe";
                    return result;
                }

            
                var cuenta=await _sisCuentaBancoRepository.GetByCodigo(dto.CodigoCuentaBanco);
                if (cuenta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta Banco invalido";
                    return result;
                }
              


             
              
                var proveedor = await _proveedoresRepository.GetByCodigo(dto.CodigoProveedor);

                if (proveedor==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }


              
              

              

                if (dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;

                }
                if (dto.Endoso is not null && dto.Endoso.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ensoso Invalido";
                    return result;

                }

              
             


                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto==null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                dto.Ano = presupuesto.ANO;
            

                var tipoChequeID = await _admDescriptivaRepository.GetByCodigo(dto.TipoChequeID);
                if (tipoChequeID==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Pago Invalido";
                    return result;
                }

                var ossConfigChequera = await _ossConfigRepository.GetByClave(dto.TipoChequeID.ToString());
                if (ossConfigChequera != null)
                {
                    dto.NumeroChequera = int.Parse(ossConfigChequera.VALOR);
                }
                dto.NumeroCheque = await _repository.GetNextCheque(dto.NumeroChequera,dto.CodigoPresupuesto);
                
                ADM_CHEQUES entity = new ADM_CHEQUES();
                entity.CODIGO_LOTE_PAGO = dto.CodigoLote;
                entity.CODIGO_CHEQUE = await _repository.GetNextKey();
                entity.ANO = dto.Ano;
                entity.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                entity.NUMERO_CHEQUERA = dto.NumeroChequera;
                entity.NUMERO_CHEQUE = dto.NumeroCheque;
                entity.FECHA_CHEQUE = dto.FechaCheque;
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.PRINT_COUNT = dto.PrintCount;
                entity.MOTIVO = dto.Motivo;
                entity.STATUS = "PE";
                entity.ENDOSO = dto.Endoso;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.TIPO_CHEQUE_ID = dto.TipoChequeID;
      




            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapChequesDto(created.Data);
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

        public async Task<ResultDto<AdmChequesDeleteDto>> Delete(AdmChequesDeleteDto dto) 
        {
            ResultDto<AdmChequesDeleteDto> result = new ResultDto<AdmChequesDeleteDto>(null);
            try
            {

                var codigoCheques = await _repository.GetByCodigoCheque(dto.CodigoCheque);
                if (codigoCheques == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo cheque Contrato no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoCheque);

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

