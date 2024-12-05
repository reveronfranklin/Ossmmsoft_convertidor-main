using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmRetencionesService : IAdmRetencionesService
    {
        private readonly IAdmRetencionesRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
   
    
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
       

        public AdmRetencionesService(IAdmRetencionesRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository
                                
                                     )
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
    
        }

      
        public async Task<AdmRetencionesResponseDto> MapRetencionesDto(ADM_RETENCIONES dtos)
        {
            AdmRetencionesResponseDto itemResult = new AdmRetencionesResponseDto();
   
            itemResult.CodigoRetencion = dtos.CODIGO_RETENCION;
            itemResult.TipoRetencionId = dtos.TIPO_RETENCION_ID;
            itemResult.DescripcionTipoRetencion = "";
            var descriptivaTipoRetencion = await _admDescriptivaRepository.GetByCodigo(dtos.TIPO_RETENCION_ID);
            if (descriptivaTipoRetencion != null)
            {
                itemResult.DescripcionTipoRetencion = descriptivaTipoRetencion.DESCRIPCION;
            }
            itemResult.ConceptoPago = dtos.CONCEPTO_PAGO;
            if (dtos.EXTRA1 == null) dtos.EXTRA1 = "";
            itemResult.Codigo = dtos.EXTRA1;
            
         
           
            
            if (dtos.POR_RETENCION == null) dtos.POR_RETENCION = 0;
            itemResult.PorRetencion = dtos.POR_RETENCION;
            
            if(dtos.MONTO_RETENCION==null) dtos.MONTO_RETENCION=0;
            itemResult.MontoRetencion = dtos.MONTO_RETENCION;
            
            if (dtos.BASE_IMPONIBLE == null) dtos.BASE_IMPONIBLE = 0;
            itemResult.BaseImponible = dtos.BASE_IMPONIBLE;
            itemResult.FechaFin = dtos.FECHA_FIN;
            itemResult.FechaIni = dtos.FECHA_INI;
            if (dtos.FECHA_FIN != null)
            {
                itemResult.FechaFinString =FechaObj.GetFechaString( dtos.FECHA_FIN);   
                itemResult.FechaFinObj= FechaObj.GetFechaDto((DateTime)dtos.FECHA_FIN);
            }

            if (dtos.FECHA_INI != null)
            {
                itemResult.FechaIniString =FechaObj.GetFechaString( dtos.FECHA_INI);   
                itemResult.FechaIniObj= FechaObj.GetFechaDto((DateTime)dtos.FECHA_INI);
            }
            
        
            
        
            return itemResult;
            
        }

        public async Task<List<AdmRetencionesResponseDto>> MapListRetencionesDto(List<ADM_RETENCIONES> dtos)
        {
            List<AdmRetencionesResponseDto> result = new List<AdmRetencionesResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapRetencionesDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

     
        
        public async Task<ResultDto<List<AdmRetencionesResponseDto>>> GetAll()
        {

            ResultDto<List<AdmRetencionesResponseDto>> result = new ResultDto<List<AdmRetencionesResponseDto>>(null);
            try
            {
                var retencionesOp = await _repository.GetAll();
                var cant = retencionesOp.Count();
                if (retencionesOp != null && retencionesOp.Count() > 0)
                {
                    var listDto = await MapListRetencionesDto(retencionesOp);

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

        public async Task<ResultDto<AdmRetencionesResponseDto>> Update(AdmRetencionesUpdateDto dto)
        {
            ResultDto<AdmRetencionesResponseDto> result = new ResultDto<AdmRetencionesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoRetencion = await _repository.GetCodigoRetencion(dto.CodigoRetencion);
                if (codigoRetencion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retencion op no existe";
                    return result;
                }
             
                var tipoRetencionId = await _admDescriptivaRepository.GetByCodigo( dto.TipoRetencionId);
                if (tipoRetencionId==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo retencion Id invalido";
                    return result;
                }

           
                if (  string.IsNullOrEmpty(dto.ConceptoPago))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto Es Obligatorio";
                    return result;
                }
                if (dto.ConceptoPago.Length >100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto de pago debe ser Maximo de 100 caracteres";
                    return result;
                }
             
                if (dto.PorRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por retencion invalido";
                    return result;
                }
                if (dto.MontoRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retencion invalido";
                    return result;
                }
               

                if (dto.BaseImponible < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible Invalida";
                    return result;
                }
               
          
                codigoRetencion.TIPO_RETENCION_ID = dto.TipoRetencionId;
                codigoRetencion.CODIGO_RETENCION = dto.CodigoRetencion;
                codigoRetencion.EXTRA1 = dto.Codigo;
                codigoRetencion.POR_RETENCION = dto.PorRetencion;
                codigoRetencion.MONTO_RETENCION = dto.MontoRetencion;
                codigoRetencion.BASE_IMPONIBLE = dto.BaseImponible;
                codigoRetencion.CONCEPTO_PAGO = dto.ConceptoPago;

                codigoRetencion.CODIGO_EMPRESA = conectado.Empresa;
                codigoRetencion.USUARIO_UPD = conectado.Usuario;
                codigoRetencion.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoRetencion);

                var resultDto = await MapRetencionesDto(codigoRetencion);
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

        public async Task<ResultDto<AdmRetencionesResponseDto>> Create(AdmRetencionesUpdateDto dto)
        {
            ResultDto<AdmRetencionesResponseDto> result = new ResultDto<AdmRetencionesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoRetencion = await _repository.GetCodigoRetencion(dto.CodigoRetencion);
                if (codigoRetencion != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retencion  ya existe";
                    return result;
                }
             
                  var tipoRetencionId = await _admDescriptivaRepository.GetByCodigo( dto.TipoRetencionId);
                if (tipoRetencionId==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo retencion Id invalido";
                    return result;
                }

            
                if (  string.IsNullOrEmpty(dto.ConceptoPago))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto Es Obligatorio";
                    return result;
                }
                if (dto.ConceptoPago.Length >100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto de pago debe ser MAximo de 100 caracteres";
                    return result;
                }
                if (dto.PorRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por retencion invalido";
                    return result;
                }
                if (dto.MontoRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retencion invalido";
                    return result;
                }
               
            
                

                if (dto.BaseImponible < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible Invalida";
                    return result;
                }


                ADM_RETENCIONES entity = new ADM_RETENCIONES();
                entity.CODIGO_RETENCION = await _repository.GetNextKey();
                entity.TIPO_RETENCION_ID = dto.TipoRetencionId;
                entity.EXTRA1 = dto.Codigo;
                entity.POR_RETENCION = dto.PorRetencion;
                entity.MONTO_RETENCION = dto.MontoRetencion;
                entity.BASE_IMPONIBLE = dto.BaseImponible;
                entity.CONCEPTO_PAGO = dto.ConceptoPago;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRetencionesDto(created.Data);
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

        public async Task<ResultDto<AdmRetencionesDeleteDto>> Delete(AdmRetencionesDeleteDto dto) 
        {
            ResultDto<AdmRetencionesDeleteDto> result = new ResultDto<AdmRetencionesDeleteDto>(null);
            try
            {

                var codigoRetencion = await _repository.GetCodigoRetencion(dto.CodigoRetencion);
                if (codigoRetencion == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo retencion  no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoRetencion);

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

