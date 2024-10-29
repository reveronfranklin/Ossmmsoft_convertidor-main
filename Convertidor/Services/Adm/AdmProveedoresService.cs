using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;


namespace Convertidor.Services.Adm
{
	public class AdmProveedoresService: IAdmProveedoresService
    {

      
        private readonly IAdmProveedoresRepository _repository;
        private readonly IAdmDescriptivaRepository _repositoryPreDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonaService _personaServices;
        public AdmProveedoresService(IAdmProveedoresRepository repository,
                                      IAdmDescriptivaRepository repositoryPreDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IRhPersonaService personaServices)
		{
            _repository = repository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;
            _personaServices = personaServices;
        }



       
       
        public AdmProveedorResponseDto MapProveedorDto(ADM_PROVEEDORES dtos)
        {
            AdmProveedorResponseDto itemResult = new AdmProveedorResponseDto();
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.NombreProveedor = dtos.NOMBRE_PROVEEDOR;
            itemResult.TipoProveedorId = dtos.TIPO_PROVEEDOR_ID;
            itemResult.Nacionalidad = dtos.NACIONALIDAD;
            itemResult.Cedula = dtos.CEDULA;
            itemResult.Rif = dtos.RIF;
            itemResult.FechaRif = dtos.FECHA_RIF;
            itemResult.FechaRifString =dtos.FECHA_RIF.ToString("u");   
            FechaDto fechaRifObj = FechaObj.GetFechaDto(dtos.FECHA_RIF);
            itemResult.FechaRifObj = fechaRifObj;
            itemResult.Nit = dtos.NIT;
            itemResult.FechaNit = dtos.FECHA_NIT;
            itemResult.FechaNitString =dtos.FECHA_NIT.ToString("u");  
            FechaDto fechaNitObj = FechaObj.GetFechaDto(dtos.FECHA_NIT);
            itemResult.FechaNitObj  =fechaNitObj;
            itemResult.NumeroRegistroContraloria= dtos.NUMERO_REGISTRO_CONTRALORIA;
            itemResult.FechaRegistroContraloria = dtos.FECHA_REGISTRO_CONTRALORIA;
            itemResult.FechaRegistroContraloriaString  =dtos.FECHA_REGISTRO_CONTRALORIA.ToString("u");  
            FechaDto fechaRegistroContraloriaObj = FechaObj.GetFechaDto(dtos.FECHA_REGISTRO_CONTRALORIA);
            itemResult.FechaRegistroContraloriaObj  =fechaRegistroContraloriaObj;
            itemResult.CapitalPagado = dtos.CAPITAL_PAGADO;
            itemResult.CapitalSuscrito = dtos.CAPITAL_SUSCRITO;
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.Status = dtos.STATUS;
            itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
            itemResult.CodigoAuxiliarGastoXPagar = dtos.CODIGO_AUXILIAR_ORDEN_PAGO;
            itemResult.CodigoAuxiliarOrdenPago = dtos.CODIGO_AUXILIAR_ORDEN_PAGO;
            itemResult.EstatusFisicoId= dtos.ESTATUS_FISCO_ID;
            itemResult.NumeroCuenta = dtos.NUMERO_CUENTA;
            return itemResult;
        }

        public List<AdmProveedorResponseDto> MapListProveedorDto(List<ADM_PROVEEDORES> dtos)
        {
            List<AdmProveedorResponseDto> result = new List<AdmProveedorResponseDto>();
           
            
            foreach (var item in dtos)
            {
                

                var itemResult =  MapProveedorDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
        public async Task<ResultDto<AdmProveedorResponseDto?>> Update(AdmProveedorUpdateDto dto)
        {

            ResultDto<AdmProveedorResponseDto?> result = new ResultDto<AdmProveedorResponseDto?>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor no existe";
                    return result;
                }
                if (dto.NombreProveedor.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Invalida";
                    return result;
                }

                
                var tiposProveedor = await _repositoryPreDescriptiva.GetByTitulo(2);
                if (tiposProveedor.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor  Invalido";
                    return result;
                }
                else
                {
                    var tipoProveedor = tiposProveedor.Where(x => x.DESCRIPCION_ID== dto.TipoProveedorId);
                    if (tipoProveedor is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo Proveedor  Invalido";
                        return result;
                    }
                }
               
                var nacionalidad = _personaServices.GetListNacionalidad().Where(x => x== dto.Nacionalidad).FirstOrDefault();
                if (String.IsNullOrEmpty(nacionalidad))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nacionalidad Invalida";
                    return result;
                    
                }
                if (dto.Cedula<= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Invalida";
                    return result;
                }
                if (dto.Rif.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Rif Invalida";
                    return result;
                }

                if (!DateValidate.IsDate(dto.FechaRifString))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Rif Invalida";
                    return result;
                }

                if (dto.Nit is not null && dto.Nit.Length > 0)
                {
                    if (!DateValidate.IsDate(dto.FechaNitString))
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Fecha Nit Invalida";
                        return result;
                    }
                }
                if (dto.NumeroRegistroContraloria is not null && dto.NumeroRegistroContraloria.Length > 0)
                {
                    if (!DateValidate.IsDate(dto.FechaRegistroContraloriaString))
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Fecha Registro Contraloria Invalida";
                        return result;
                    }
                }
                
                if (dto.CapitalPagado<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Capital Pagado Invalido";
                    return result;
                }
                if (dto.CapitalSuscrito<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Capital Suscrito Invalido";
                    return result;
                }
                if (dto.Status.Length<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }
                var impuestos = await _repositoryPreDescriptiva.GetByTitulo(33);
                if (impuestos.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Impuesto  Invalido";
                    return result;
                }
                else
                {
                    var impuesto = impuestos.Where(x => x.DESCRIPCION_ID== dto.TipoImpuestoId);
                    if (impuesto is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo Impuesto  Invalido";
                        return result;
                    }
                }

                proveedor.NOMBRE_PROVEEDOR = dto.NombreProveedor;
                proveedor.TIPO_PROVEEDOR_ID = dto.TipoProveedorId;
                proveedor.NACIONALIDAD = dto.Nacionalidad;
                proveedor.CEDULA = dto.Cedula;
                proveedor.RIF = dto.Rif;
                proveedor.FECHA_RIF = dto.FechaRif;
                proveedor.NIT = dto.Nit;
                proveedor.FECHA_NIT = dto.FechaNit;
                proveedor.NUMERO_REGISTRO_CONTRALORIA = dto.NumeroRegistroContraloria;
                proveedor.FECHA_REGISTRO_CONTRALORIA = dto.FechaRegistroContraloria;
                proveedor.CAPITAL_PAGADO = dto.CapitalPagado;
                proveedor.CAPITAL_SUSCRITO = dto.CapitalSuscrito;
                proveedor.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                proveedor.STATUS = dto.Status;
                proveedor.CODIGO_PERSONA = dto.CodigoPersona;
                proveedor.CODIGO_AUXILIAR_GASTO_X_PAGAR = dto.CodigoAuxiliarGastoXPagar;
                proveedor.CODIGO_AUXILIAR_ORDEN_PAGO = dto.CodigoAuxiliarOrdenPago;
                proveedor.ESTATUS_FISCO_ID = dto.EstatusFisicoId;
                proveedor.CODIGO_AUXILIAR_ORDEN_PAGO = dto.CodigoAuxiliarOrdenPago;
                proveedor.NUMERO_CUENTA = dto.NumeroCuenta;
                proveedor.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                proveedor.CODIGO_EMPRESA = conectado.Empresa;
                proveedor.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(proveedor);
                
                var resultDto =  MapProveedorDto(proveedor);
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

        public async Task<ResultDto<AdmProveedorResponseDto>> Create(AdmProveedorUpdateDto dto)
        {

            ResultDto<AdmProveedorResponseDto> result = new ResultDto<AdmProveedorResponseDto>(null);
            try
            {
               
                if (dto.NombreProveedor.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Invalida";
                    return result;
                }

                
                var tiposProveedor = await _repositoryPreDescriptiva.GetByTitulo(2);
                if (tiposProveedor.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor  Invalido";
                    return result;
                }
                else
                {
                    var tipoProveedor = tiposProveedor.Where(x => x.DESCRIPCION_ID == dto.TipoProveedorId);
                    if (tipoProveedor is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo Proveedor  Invalido";
                        return result;
                    }
                }
               
                var nacionalidad = _personaServices.GetListNacionalidad().Where(x => x== dto.Nacionalidad).FirstOrDefault();
                if (String.IsNullOrEmpty(nacionalidad))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nacionalidad Invalida";
                    return result;
                    
                }
                if (dto.Cedula<= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Invalida";
                    return result;
                }
                if (dto.Rif.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Rif Invalida";
                    return result;
                }

                if (!DateValidate.IsDate(dto.FechaRifString))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Rif Invalida";
                    return result;
                }

                if (dto.Nit is not null && dto.Nit.Length > 0)
                {
                    if (!DateValidate.IsDate(dto.FechaNitString))
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Fecha Nit Invalida";
                        return result;
                    }
                }
                if (dto.NumeroRegistroContraloria is not null && dto.NumeroRegistroContraloria.Length > 0)
                {
                    if (!DateValidate.IsDate(dto.FechaRegistroContraloriaString))
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Fecha Registro Contraloria Invalida";
                        return result;
                    }
                }
                
                if (dto.CapitalPagado<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Capital Pagado Invalido";
                    return result;
                }
                if (dto.CapitalSuscrito<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Capital Suscrito Invalido";
                    return result;
                }
                if (dto.Status.Length<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }
                var impuestos = await _repositoryPreDescriptiva.GetByTitulo(33);
                if (impuestos.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Impuesto  Invalido";
                    return result;
                }
                else
                {
                    var impuesto = impuestos.Where(x => x.DESCRIPCION_ID== dto.TipoImpuestoId);
                    if (impuesto is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo Impuesto  Invalido";
                        return result;
                    }
                }

                ADM_PROVEEDORES proveedor = new ADM_PROVEEDORES();
                proveedor.CODIGO_PROVEEDOR = await _repository.GetNextKey();
                proveedor.NOMBRE_PROVEEDOR = dto.NombreProveedor;
                proveedor.TIPO_PROVEEDOR_ID = dto.TipoProveedorId;
                proveedor.NACIONALIDAD = dto.Nacionalidad;
                proveedor.CEDULA = dto.Cedula;
                proveedor.RIF = dto.Rif;
                proveedor.FECHA_RIF = dto.FechaRif;
                proveedor.NIT = dto.Nit;
                proveedor.FECHA_NIT = dto.FechaNit;
                proveedor.NUMERO_REGISTRO_CONTRALORIA = dto.NumeroRegistroContraloria;
                proveedor.FECHA_REGISTRO_CONTRALORIA = dto.FechaRegistroContraloria;
                proveedor.CAPITAL_PAGADO = dto.CapitalPagado;
                proveedor.CAPITAL_SUSCRITO = dto.CapitalSuscrito;
                proveedor.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                proveedor.STATUS = dto.Status;
                proveedor.CODIGO_PERSONA = dto.CodigoPersona;
                proveedor.CODIGO_AUXILIAR_GASTO_X_PAGAR = dto.CodigoAuxiliarGastoXPagar;
                proveedor.CODIGO_AUXILIAR_ORDEN_PAGO = dto.CodigoAuxiliarOrdenPago;
                proveedor.ESTATUS_FISCO_ID = dto.EstatusFisicoId;
                proveedor.CODIGO_AUXILIAR_ORDEN_PAGO = dto.CodigoAuxiliarOrdenPago;
                proveedor.NUMERO_CUENTA = dto.NumeroCuenta;
                proveedor.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                proveedor.CODIGO_EMPRESA = conectado.Empresa;
                proveedor.FECHA_INS = DateTime.Now;
                proveedor.USUARIO_UPD = conectado.Usuario;
                var created=await _repository.Add(proveedor);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto =  MapProveedorDto(created.Data);
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
 
        public async Task<ResultDto<AdmProveedorDeleteDto>> Delete(AdmProveedorDeleteDto dto)
        {

            ResultDto<AdmProveedorDeleteDto> result = new ResultDto<AdmProveedorDeleteDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoProveedor);

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

        public async Task<ResultDto<AdmProveedorResponseDto>> GetByCodigo(AdmProveedorFilterDto dto)
        { 
            ResultDto<AdmProveedorResponseDto> result = new ResultDto<AdmProveedorResponseDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor no existe";
                    return result;
                }
                
                var resultDto =  MapProveedorDto(proveedor);
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

        public async Task<ResultDto<List<AdmProveedorResponseDto>>> GetAll()
        {
            ResultDto<List<AdmProveedorResponseDto>> result = new ResultDto<List<AdmProveedorResponseDto>>(null);
            try
            {

                var proveedor = await _repository.GetByAll();
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor no existe";
                    return result;
                }
                
                var resultDto =  MapListProveedorDto(proveedor);
                result.Data = resultDto;
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

