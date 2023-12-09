using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhDocumentosService: IRhDocumentosService
    {
        
   
        private readonly IRhDocumentosRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;
        private readonly IRhPersonasRepository _personasRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
   

        public RhDocumentosService(IRhDocumentosRepository repository, 
                                        IRhDescriptivasService descriptivaService,
                                        IRhPersonasRepository personasRepository,
                                        ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;
            _personasRepository = personasRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        public async Task<List<RhDocumentosResponseDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {

                var Documentos = await _repository.GetByCodigoPersona(codigoPersona);

                var result = await MapListDocumentosDto(Documentos);


                return (List<RhDocumentosResponseDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        
        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);
    
            return FechaDesdeObj;
        }
       
        public async  Task<RhDocumentosResponseDto> MapDocumentosDto(RH_DOCUMENTOS dtos)
        {


                RhDocumentosResponseDto itemResult = new RhDocumentosResponseDto();
                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.CodigoDocumento = dtos.CODIGO_DOCUMENTO;
                itemResult.TipodocumentoId = dtos.TIPO_DOCUMENTO_ID;
                itemResult.NumeroDocumento = dtos.NUMERO_DOCUMENTO;
                itemResult.FechaVencimiento = dtos.FECHA_VENCIMIENTO;
                itemResult.TipoGradoid = dtos.TIPO_GRADO_ID;
                itemResult.GradoId = dtos.GRADO_ID;
                itemResult.Extra1 = dtos.EXTRA1;
                itemResult.Extra2 = dtos.EXTRA2;
                itemResult.Extra3 = dtos.EXTRA3; 
                itemResult.Usuarioins = dtos.USUARIO_INS;
                itemResult.FechaIns = dtos.FECHA_INS;
                itemResult.UsuarioUpd = dtos.USUARIO_UPD;
                itemResult.FechaUpd = dtos.FECHA_UPD;
             
          
            return itemResult;

        }

        public async Task<List<RhDocumentosResponseDto>> MapListDocumentosDto(List<RH_DOCUMENTOS> dtos)
        {
            List<RhDocumentosResponseDto> result = new List<RhDocumentosResponseDto>();


            foreach (var item in dtos)
            {

                RhDocumentosResponseDto itemResult = new RhDocumentosResponseDto();

                itemResult = await MapDocumentosDto(item);

                result.Add(itemResult);
            }
            return result;



        }


        public async Task<ResultDto<RhDocumentosResponseDto>> Update(RhDocumentosUpdate dto)
        {

            ResultDto<RhDocumentosResponseDto> result = new ResultDto<RhDocumentosResponseDto>(null);
            try
            {
                var persona = await _personasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (persona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona no existe";
                    return result;
                }

                var documento = await _repository.GetByCodigo(dto.CodigoDocumento);
                if (documento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe el Documento";
                    return result;
                }

                var tipoDocumento = await _descriptivaService.GetByTitulo(26);
                if (tipoDocumento==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Documento  Invalido";
                    return result;
                }

                if(dto.NumeroDocumento == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Documento Invalido";
                    return result;
                }

                var tipoGrado = await _descriptivaService.GetByTitulo(30);
                if (tipoGrado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Grado Documento Invalido";
                    return result;
                }

                var Grado = await _descriptivaService.GetByTitulo(31);
                if (Grado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Grado Documento Invalido";
                    return result;
                }



                documento.CODIGO_PERSONA = dto.CodigoPersona;
                documento.CODIGO_DOCUMENTO = dto.CodigoDocumento;
                documento.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;
                documento.NUMERO_DOCUMENTO = dto.NumeroDocumento;
                documento.FECHA_VENCIMIENTO = dto.FechaVencimiento;
                documento.TIPO_GRADO_ID = dto.TipoGradoId;
                documento.GRADO_ID = dto.GradoId;
                documento.USUARIO_INS = dto.UsuarioIns;
                documento.FECHA_INS = dto.FechaIns;
                documento.USUARIO_UPD = dto.UsuarioUpd;
                documento.FECHA_UPD = dto.FechaUpd;
                documento.CODIGO_EMPRESA = dto.CodigoEmpresa;

                var conectado = await _sisUsuarioRepository.GetConectado();
                documento.CODIGO_EMPRESA = conectado.Empresa;
                documento.USUARIO_UPD = conectado.Usuario;


                await _repository.Update(documento);



                var resultDto = await MapDocumentosDto(documento);
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

        public async Task<ResultDto<RhDocumentosResponseDto>> Create(RhDocumentosUpdate dto)
        {

            ResultDto<RhDocumentosResponseDto> result = new ResultDto<RhDocumentosResponseDto>(null);
            try
            {
                var persona = await _personasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (persona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona no existe";
                    return result;
                }

                var tipoDocumento = await _descriptivaService.GetByTitulo(26);
                if (tipoDocumento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Documento  Invalido";
                    return result;
                }

                if (dto.NumeroDocumento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Documento Invalido";
                    return result;
                }

                var tipoGrado = await _descriptivaService.GetByTitulo(30);
                if (tipoGrado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Grado Documento Invalido";
                    return result;
                }

                var Grado = await _descriptivaService.GetByTitulo(31);
                if (Grado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Grado Documento Invalido";
                    return result;
                }


                RH_DOCUMENTOS entity = new RH_DOCUMENTOS();
                entity.CODIGO_DOCUMENTO = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;
                entity.NUMERO_DOCUMENTO = dto.NumeroDocumento;
                entity.FECHA_VENCIMIENTO = dto.FechaVencimiento;
                entity.TIPO_GRADO_ID = dto.TipoGradoId;
                entity.GRADO_ID = dto.GradoId;
                entity.USUARIO_INS = dto.UsuarioIns;
                entity.FECHA_INS = dto.FechaIns;
                entity.USUARIO_UPD = dto.UsuarioUpd;
                entity.FECHA_UPD = dto.FechaUpd;
                entity.CODIGO_EMPRESA = dto.CodigoEmpresa;



                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_UPD = conectado.Usuario;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDocumentosDto(created.Data);
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
 
        public async Task<ResultDto<RhDocumentosDeleteDto>> Delete(RhDocumentosDeleteDto dto)
        {

            ResultDto<RhDocumentosDeleteDto> result = new ResultDto<RhDocumentosDeleteDto>(null);
            try
            {

                var documento = await _repository.GetByCodigo(dto.CodigoDocumento);
                if (documento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Documento no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDocumento);

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

