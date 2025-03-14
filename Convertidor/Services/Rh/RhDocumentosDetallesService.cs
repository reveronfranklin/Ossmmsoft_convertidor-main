﻿using Convertidor.Utility;

namespace Convertidor.Data.Repository.Rh
{
	public class RhDocumentosDetallesService: IRhDocumentosDetallesService
    {
        
   
        private readonly IRhDocumentosDetallesRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhDocumentosRepository _rhDocumentosRepository;

        public RhDocumentosDetallesService(IRhDocumentosDetallesRepository repository, 
                                        IRhDescriptivasService descriptivaService, 
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IRhDocumentosRepository rhDocumentosRepository)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhDocumentosRepository = rhDocumentosRepository;
        }
       
       
        
      

        

       
        public async  Task<RhDocumentosDetallesResponseDto> MapDocumentosDetallesDto(RH_DOCUMENTOS_DETALLES dtos)
        {


                RhDocumentosDetallesResponseDto itemResult = new RhDocumentosDetallesResponseDto();
                itemResult.CodigoDocumentoDetalle = dtos.CODIGO_DOCUMENTO_DETALLE;
                itemResult.CodigoDocumento = dtos.CODIGO_DOCUMENTO;
                itemResult.Descripcion = dtos.DESCRIPCION;
            
                itemResult.FechaFinal = dtos.FECHA_FINAL;
                itemResult.FechaFinalString =Fecha.GetFechaString(dtos.FECHA_FINAL);
                FechaDto fechaFinalObj = Fecha.GetFechaDto(dtos.FECHA_FINAL);
                itemResult.FechaFinalObj = (FechaDto)fechaFinalObj;
                itemResult.FechaInicial = dtos.FECHA_INICIAL;
                itemResult.FechaInicialString =Fecha.GetFechaString(dtos.FECHA_INICIAL);
                FechaDto fechaInicialObj = Fecha.GetFechaDto(dtos.FECHA_INICIAL);
                itemResult.FechaInicialObj = (FechaDto)fechaInicialObj;



            return itemResult;

        }

        public async Task<List<RhDocumentosDetallesResponseDto>> MapListDocumentosDetallesDto(List<RH_DOCUMENTOS_DETALLES> dtos)
        {
            List<RhDocumentosDetallesResponseDto> result = new List<RhDocumentosDetallesResponseDto>();


            foreach (var item in dtos)
            {

                RhDocumentosDetallesResponseDto itemResult = new RhDocumentosDetallesResponseDto();

                itemResult = await MapDocumentosDetallesDto(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<List<RhDocumentosDetallesResponseDto>> GetByDocumento(int codigoDocumento)
        {
            try
            {

                var detalleDocumentos = await _repository.GetByCodigoDocumento(codigoDocumento);

                var result = await MapListDocumentosDetallesDto(detalleDocumentos);


                return (List<RhDocumentosDetallesResponseDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public async Task<ResultDto<RhDocumentosDetallesResponseDto>> Update(RhDocumentosDetallesUpdate dto)
        {

            ResultDto<RhDocumentosDetallesResponseDto> result = new ResultDto<RhDocumentosDetallesResponseDto>(null);
            try
            {
                var documentoDetalle = await _repository.GetByCodigo(dto.CodigoDocumentoDetalle);
                if (documentoDetalle == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe el Documento";
                    return result;
                }

                var codigoDocumento = await _rhDocumentosRepository.GetByCodigo(dto.CodigoDocumento);
                if (codigoDocumento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe el Documento";
                    return result;
                }

                
                if (dto.Descripcion is not null && dto.Descripcion.Length>1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Documento  Invalido";
                    return result;
                }

                if (!DateValidate.IsDate(dto.FechaInicial.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial Invalida";
                    result.LinkData = "";
                    return result;
                }
                if (!DateValidate.IsDate(dto.FechaFinal.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Final Invalida";
                    result.LinkData = "";
                    return result;
                }

                documentoDetalle.CODIGO_DOCUMENTO_DETALLE = dto.CodigoDocumentoDetalle;
                documentoDetalle.CODIGO_DOCUMENTO = dto.CodigoDocumento;
                documentoDetalle.DESCRIPCION = dto.Descripcion;
                documentoDetalle.FECHA_FINAL= dto.FechaFinal;
                documentoDetalle.FECHA_INICIAL = dto.FechaInicial;


                var conectado = await _sisUsuarioRepository.GetConectado();
                documentoDetalle.CODIGO_EMPRESA = conectado.Empresa;
                documentoDetalle.USUARIO_UPD = conectado.Usuario;
                documentoDetalle.FECHA_UPD =DateTime.Now;

                await _repository.Update(documentoDetalle);



                var resultDto = await MapDocumentosDetallesDto(documentoDetalle);
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

        public async Task<ResultDto<RhDocumentosDetallesResponseDto>> Create(RhDocumentosDetallesUpdate dto)
        {

            ResultDto<RhDocumentosDetallesResponseDto> result = new ResultDto<RhDocumentosDetallesResponseDto>(null);
            try
            {
                var codigoDocumento = await _rhDocumentosRepository.GetByCodigo(dto.CodigoDocumento);
                if (codigoDocumento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe el Documento";
                    return result;
                }
                if (dto.Descripcion is not null && dto.Descripcion.Length>1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion  Invalida(Obligatoria y Max 1000 Digitos)";
                    return result;
                }

                if (!DateValidate.IsDate(dto.FechaInicial.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial Invalida";
                    result.LinkData = "";
                    return result;
                }
                if (!DateValidate.IsDate(dto.FechaFinal.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Final Invalida";
                    result.LinkData = "";
                    return result;
                }

                RH_DOCUMENTOS_DETALLES entity = new RH_DOCUMENTOS_DETALLES();
                entity.CODIGO_DOCUMENTO_DETALLE = await _repository.GetNextKey();
                entity.CODIGO_DOCUMENTO = dto.CodigoDocumento;
                entity.DESCRIPCION = dto.Descripcion;
                entity.FECHA_FINAL = dto.FechaFinal; 
                entity.FECHA_INICIAL = dto.FechaInicial;




                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_UPD = conectado.Usuario;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDocumentosDetallesDto(created.Data);
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
 
        public async Task<ResultDto<RhDocumentosDetallesDeleteDto>> Delete(RhDocumentosDetallesDeleteDto dto)
        {

            ResultDto<RhDocumentosDetallesDeleteDto> result = new ResultDto<RhDocumentosDetallesDeleteDto>(null);
            try
            {

                var documentoDetalle = await _repository.GetByCodigo(dto.CodigoDocumentoDetalle);
                if (documentoDetalle == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Documento no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDocumentoDetalle);

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

