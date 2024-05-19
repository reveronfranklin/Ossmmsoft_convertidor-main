﻿using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmSolicitudesService : IAdmSolicitudesService
    {
        private readonly IAdmSolicitudesRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IPRE_PRESUPUESTOSRepository _presupuestosRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _preIndiceCatPrgRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;

        public AdmSolicitudesService(IAdmSolicitudesRepository repository,
            ISisUsuarioRepository sisUsuarioRepository,
            IAdmDescriptivaRepository admDescriptivaRepository,
            IPRE_PRESUPUESTOSRepository presupuestosRepository,
            IPRE_INDICE_CAT_PRGRepository preIndiceCatPrgRepository,
            IAdmProveedoresRepository admProveedoresRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _presupuestosRepository = presupuestosRepository;
            _preIndiceCatPrgRepository = preIndiceCatPrgRepository;
            _admProveedoresRepository = admProveedoresRepository;
        }

      

        
        public async Task<AdmSolicitudesResponseDto> MapSolicitudesDto(ADM_SOLICITUDES dtos,PRE_PRESUPUESTOS presupuesto)
        {
            AdmSolicitudesResponseDto itemResult = new AdmSolicitudesResponseDto();
            try
            {
                itemResult.CodigoSolicitud = dtos.CODIGO_SOLICITUD;
                itemResult.Ano = presupuesto.ANO;
                itemResult.NumeroSolicitud = dtos.NUMERO_SOLICITUD;
                itemResult.FechaSolicitud = dtos.FECHA_SOLICITUD;
                itemResult.FechaSolicitudString =  Fecha.GetFechaString(dtos.FECHA_SOLICITUD);
                FechaDto fechaSolicitudObj = Fecha.GetFechaDto(dtos.FECHA_SOLICITUD);
                itemResult.FechaSolicitudObj = (FechaDto)fechaSolicitudObj;
                itemResult.CodigoSolicitante = dtos.CODIGO_SOLICITANTE;
                itemResult.DenominacionSolicitante = "";
                var icp = await _preIndiceCatPrgRepository.GetByCodigo(dtos.CODIGO_SOLICITANTE);
                if (icp != null)
                {
                    itemResult.DenominacionSolicitante = icp.DENOMINACION.Trim();
                }
                itemResult.TipoSolicitudId = dtos.TIPO_SOLICITUD_ID;
                itemResult.DescripcionTipoSolicitud = "";
                var descriptiva = await _admDescriptivaRepository.GetByCodigo((int)dtos.TIPO_SOLICITUD_ID);
                if (descriptiva != null)
                {
                    itemResult.DescripcionTipoSolicitud = descriptiva.DESCRIPCION.Trim();
                }
                itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
                itemResult.NombreProveedor = "";
            
                var proveedor = await _admProveedoresRepository.GetByCodigo((int)dtos.CODIGO_PROVEEDOR);
                if ( proveedor!=null)
                {
                    itemResult.NombreProveedor = proveedor.NOMBRE_PROVEEDOR.Trim();
                }

                if (dtos.MOTIVO == null) dtos.MOTIVO = "";
                itemResult.Motivo = dtos.MOTIVO.Trim();
                if (dtos.NOTA == null) dtos.NOTA = "";
                itemResult.Nota = dtos.NOTA.Trim();
                itemResult.Status = dtos.STATUS;
                itemResult.DescripcionStatus = Estatus.GetStatus(dtos.STATUS);
                itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
            
            
            return itemResult;
        }

        public async Task<List<AdmSolicitudesResponseDto>> MapListSolicitudesDto(List<ADM_SOLICITUDES> dtos,PRE_PRESUPUESTOS presupuesto)
        {
            List<AdmSolicitudesResponseDto> result = new List<AdmSolicitudesResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapSolicitudesDto(item,presupuesto);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public string GetDenominacionIcp(List<PRE_INDICE_CAT_PRG> listIcp,int codigoIcp)
        {
            var result = "";
            var icp = listIcp.Where(x => x.CODIGO_ICP == codigoIcp).FirstOrDefault();
            if (icp != null)
            {
                result = icp.DENOMINACION;
            }
            return result;
        }
        public async Task<ResultDto<List<AdmSolicitudesResponseDto>>> GetByPresupuesto(AdmSolicitudesFilterDto filter)
        {
            
            
            ResultDto<List<AdmSolicitudesResponseDto>> result = new ResultDto<List<AdmSolicitudesResponseDto>>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var codigoPresupuesto = await _presupuestosRepository.GetByCodigo(conectado.Empresa, filter.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var icps = await _preIndiceCatPrgRepository.GetAll();
                var listIcp = icps.ToList();
                var solicitudes = await  _repository.GetByPresupuesto(filter);
               
                if (solicitudes.Data.Count > 0)
                {
                    
                    
                    var linqQuery = from sol in solicitudes.Data
                   
                       
                 
                        select new AdmSolicitudesResponseDto() {
                            CodigoSolicitud = sol.CodigoSolicitud,
                            Ano = codigoPresupuesto.ANO ,
                            NumeroSolicitud=sol.NumeroSolicitud,
                            FechaSolicitud=sol.FechaSolicitud,
                            FechaSolicitudString= sol.FechaSolicitudString,
                            FechaSolicitudObj = sol.FechaSolicitudObj,
                            CodigoSolicitante=sol.CodigoSolicitante,
                            DenominacionSolicitante=GetDenominacionIcp(listIcp,sol.CodigoSolicitante),
                            TipoSolicitudId=sol.CodigoSolicitante,
                            DescripcionTipoSolicitud=sol.DescripcionTipoSolicitud,
                            CodigoProveedor=sol.CodigoProveedor,
                            NombreProveedor = sol.NombreProveedor,
                            Motivo=sol.Motivo,
                            Nota = sol.Nota,
                            DescripcionStatus=sol.DescripcionStatus,
                            CodigoPresupuesto=sol.CodigoPresupuesto
                        
                        };
                    
                    var listDto = linqQuery.ToList();

                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = solicitudes.Message;
                    result.Page = solicitudes.Page;
                    result.TotalPage = solicitudes.TotalPage;
                    result.CantidadRegistros = solicitudes.CantidadRegistros;

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

        public async Task<ResultDto<AdmSolicitudesResponseDto>> Update(AdmSolicitudesUpdateDto dto)
        {
            ResultDto<AdmSolicitudesResponseDto> result = new ResultDto<AdmSolicitudesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var solicitud = await _repository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (solicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Solicitud no existe";
                    return result;
                }
                var status = Estatus.GetStatusObj(solicitud.STATUS);
                if (status.Modificable == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Solicitud no puede ser modificada, se encuentra en status: {status.Descripcion}";
                    return result;
                }
                
               

                if (dto.NumeroSolicitud is not null && dto.NumeroSolicitud.Length>20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
                    return result;

                }

                if (dto.FechaSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Solicitud Invalida";
                    return result;
                }
                if (dto.CodigoSolicitante <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }
                var icp = await _preIndiceCatPrgRepository.GetByCodigo(dto.CodigoSolicitante);
                if (icp==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }
                
                var tipoSolicitudTitulo = await _admDescriptivaRepository.GetByTitulo(35);
                var descriptivaSolicitud =
                    tipoSolicitudTitulo.Where(x => x.DESCRIPCION_ID == dto.TipoSolicitudId).FirstOrDefault();
                if (descriptivaSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Solicitud Id no existe";
                    return result;
                }

                if (dto.CodigoProveedor <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                var proveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if ( proveedor==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.Motivo is not null && dto.Motivo.Length>1150)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
                if (dto.Nota is not null && dto.Nota.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nota invalida";
                    return result;
                }

                if (dto.Status is not null && dto.Status.Length>2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }
          

                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
                var codigoPresupuesto = await _presupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                
                solicitud.CODIGO_SOLICITUD = dto.CodigoSolicitud;
                solicitud.ANO = codigoPresupuesto.ANO;
                solicitud.NUMERO_SOLICITUD = dto.NumeroSolicitud;
                solicitud.FECHA_SOLICITUD = dto.FechaSolicitud;
                solicitud.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                solicitud.TIPO_SOLICITUD_ID = dto.TipoSolicitudId;
                solicitud.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                solicitud.MOTIVO = dto.Motivo;
                solicitud.NOTA = dto.Nota;
             
                solicitud.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

                
                solicitud.CODIGO_EMPRESA = conectado.Empresa;
                solicitud.USUARIO_UPD = conectado.Usuario;
                solicitud.FECHA_UPD = DateTime.Now;

                await _repository.Update(solicitud);

                var resultDto = await MapSolicitudesDto(solicitud,codigoPresupuesto);
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

        public async Task<ResultDto<bool>> UpdateStatus(AdmSolicitudesUpdateDto dto)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var solicitud = await _repository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (solicitud == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Solicitud no existe";
                    return result;
                }
                var status = Estatus.GetStatusObj(solicitud.STATUS);
                if (status.Modificable == false)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = $"Solicitud no puede ser modificada, se encuentra en status: {status.Descripcion}";
                    return result;
                }
                
                await _repository.UpdateStatus(solicitud.CODIGO_SOLICITUD,dto.Status);
                
                result.Data = true;
                result.IsValid = true;
                result.Message = "";
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }

        
        public async Task<ResultDto<AdmSolicitudesResponseDto>> Create(AdmSolicitudesUpdateDto dto)
        {
            ResultDto<AdmSolicitudesResponseDto> result = new ResultDto<AdmSolicitudesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoSolicitud = await _repository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (codigoSolicitud != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Solicitud ya existe";
                    return result;
                }

   
                if (dto.NumeroSolicitud is not null && dto.NumeroSolicitud.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
                    return result;

                }

                if (dto.FechaSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Solicitud Invalida";
                    return result;
                }
                if (dto.CodigoSolicitante <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }
                var icp = await _preIndiceCatPrgRepository.GetByCodigo(dto.CodigoSolicitante);
                if (icp==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }

               
                if (dto.TipoSolicitudId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Solicitud Id no existe";
                    return result;

                }
                
                var tipoSolicitudTitulo = await _admDescriptivaRepository.GetByTitulo(35);
                var descriptivaSolicitud =
                    tipoSolicitudTitulo.Where(x => x.DESCRIPCION_ID == dto.TipoSolicitudId).FirstOrDefault();
                if (descriptivaSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Solicitud Id no existe";
                    return result;
                }
                if (dto.CodigoProveedor <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                var proveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if ( proveedor==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (  dto.Motivo is not null && dto.Motivo.Length > 1150)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
                if (dto.Nota is not null && dto.Nota.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nota invalida";
                    return result;
                }
                
             
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
                var codigoPresupuesto = await _presupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
            

                ADM_SOLICITUDES entity = new ADM_SOLICITUDES();
                entity.CODIGO_SOLICITUD = await _repository.GetNextKey();
                entity.ANO = codigoPresupuesto.ANO;
                entity.NUMERO_SOLICITUD=dto.NumeroSolicitud;
                entity.FECHA_SOLICITUD = dto.FechaSolicitud;
                entity.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                entity.TIPO_SOLICITUD_ID = dto.TipoSolicitudId;
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.MOTIVO = dto.Motivo;
                entity.NOTA=dto.Nota;
                entity.STATUS = "PE";
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapSolicitudesDto(created.Data,codigoPresupuesto);
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

        public async Task<ResultDto<AdmSolicitudesDeleteDto>> Delete(AdmSolicitudesDeleteDto dto) 
        {
            ResultDto<AdmSolicitudesDeleteDto> result = new ResultDto<AdmSolicitudesDeleteDto>(null);
            try
            {

                var codigoSolicitud = await _repository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (codigoSolicitud == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud no existe";
                    return result;
                }
                var status = Estatus.GetStatusObj(codigoSolicitud.STATUS);
                if (status.Modificable == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Solicitud no puede ser modificada, se encuentra en status: {status.Descripcion}";
                    return result;
                }

                var deleted = await _repository.Delete(dto.CodigoSolicitud);

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