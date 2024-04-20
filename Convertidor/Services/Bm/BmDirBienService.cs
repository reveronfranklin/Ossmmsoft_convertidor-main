using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.Protocols;
using System.Globalization;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Sis;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Adm;
using Convertidor.Services.Bm;
using Convertidor.Services.Sis;
using IronPdf.Editing;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
	public class BmDirBienService: IBmDirBienService
    {
		



   
        private readonly IBmDirBienRepository _repository;
        private readonly IBmDescriptivasService _bmDescriptivaService;
        private readonly IAdmDescriptivasService _admDescriptivasService;
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ISisUbicacionService _sisUbicacionService;


        public BmDirBienService(IBmDirBienRepository repository,
                                    IBmDescriptivasService bmDescriptivaService,
                                    IAdmDescriptivasService admDescriptivasService,
                                    IIndiceCategoriaProgramaService indiceCategoriaProgramaService,
                                    ISisUsuarioRepository sisUsuarioRepository,
                                    ISisUbicacionService sisUbicacionService
                                   )
        {
            _repository = repository;
            _bmDescriptivaService = bmDescriptivaService;
            _admDescriptivasService = admDescriptivasService;
            _indiceCategoriaProgramaService = indiceCategoriaProgramaService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _sisUbicacionService = sisUbicacionService;
     
            
        }
       
        public async Task<BmDirBienResponseDto> GetByCodigoDirBien(int codigoDirBien)
        {
            try
            {
                var DirBien = await _repository.GetByCodigoDirBien(codigoDirBien);
                
                var result = await MapDirBienDto(DirBien);


                return (BmDirBienResponseDto)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

      

        
        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaIObj = new FechaDto();
            FechaIObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaIObj.Month = month.Substring(month.Length - 2);
            FechaIObj.Day = day.Substring(day.Length - 2);

            return FechaIObj;
        }


        public async Task<BmDirBienResponseDto> MapDirBienDto(BM_DIR_BIEN dtos)
        {


            BmDirBienResponseDto itemResult = new BmDirBienResponseDto();
            itemResult.CodigoDirBien = dtos.CODIGO_DIR_BIEN;
            itemResult.CodigoIcp = dtos.CODIGO_ICP;
            itemResult.PaisId = dtos.PAIS_ID;
            itemResult.EstadoId = dtos.ESTADO_ID;
            itemResult.MunicipioId = dtos.MUNICIPIO_ID;
            itemResult.CiudadId = dtos.CIUDAD_ID;
            itemResult.ParroquiaId = dtos.PARROQUIA_ID;
            itemResult.SectorId = dtos.SECTOR_ID;
            itemResult.UrbanizacionId = dtos.URBANIZACION_ID;
            itemResult.ManzanaId = dtos.MANZANA_ID;
            itemResult.ParcelaId = dtos.PARCELA_ID;
            itemResult.VialidadId = dtos.VIALIDAD_ID;
            itemResult.Vialidad = dtos.VIALIDAD;
            itemResult.TipoViviendaId = dtos.TIPO_VIVIENDA_ID;
            itemResult.Vivienda = dtos.VIVIENDA;
            itemResult.TipoNivelId = dtos.TIPO_NIVEL_ID;
            itemResult.Nivel = dtos.NIVEL;
            itemResult.TipoUnidadId = dtos.TIPO_UNIDAD_ID;
            itemResult.NumeroUnidad = dtos.NUMERO_UNIDAD;
            itemResult.ComplementoDir = dtos.COMPLEMENTO_DIR;
            itemResult.TenenciaId = dtos.TENENCIA_ID;
            itemResult.CodigoPostal = dtos.CODIGO_POSTAL;
            itemResult.FechaIni = dtos.FECHA_INI;
            itemResult.FechaIniString = dtos.FECHA_INI.ToString("u");
            FechaDto fechaIniObj = GetFechaDto(dtos.FECHA_INI);
            itemResult.FechaIniObj = (FechaDto)fechaIniObj;
            itemResult.FechaFin= dtos.FECHA_FIN;
            itemResult.FechaFinString = dtos.FECHA_FIN.ToString("u");
            FechaDto fechaFIn = GetFechaDto(dtos.FECHA_FIN);
            itemResult.FechaFinObj = (FechaDto)fechaFIn;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.UnidadTrabajoId = dtos.UNIDAD_TRABAJO_ID;
       

            return itemResult;

        }



        public async  Task<List<BmDirBienResponseDto>> MapListDirBienDto(List<BM_DIR_BIEN> dtos)
        {
            List<BmDirBienResponseDto> result = new List<BmDirBienResponseDto>();

            foreach (var item in dtos)
            {

                BmDirBienResponseDto itemResult = new BmDirBienResponseDto();
                itemResult = await MapDirBienDto(item);

                result.Add(itemResult);


            }
            return result;



        }

        public async Task<ResultDto<BmDirBienResponseDto>> Update(BmDirBienUpdateDto dto)
        {

            ResultDto<BmDirBienResponseDto> result = new ResultDto<BmDirBienResponseDto>(null);
            try
            {

                var codigoDirBien = await _repository.GetByCodigoDirBien(dto.CodigoDirBien);
                if (codigoDirBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Dir Bien no existe";
                    return result;
                }
                var codigoIcp = await _indiceCategoriaProgramaService.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp no existe";
                    return result;
                }
                var pais = await _sisUbicacionService.GetPais(dto.PaisId);

                if (pais is null)
                {
                    pais.Id = dto.PaisId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais  Invalido";
                    return result;
                }

                var estado = await _sisUbicacionService.GetEstado(dto.PaisId, dto.EstadoId);
                if (estado is null)
                {
                    estado.Id = dto.EstadoId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Invalido";
                    return result;
                }

                var municipio = await _sisUbicacionService.GetMunicipio(dto.PaisId, dto.EstadoId, dto.MunicipioId);
                if (municipio is null)
                {
                    municipio.Id = dto.MunicipioId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Municipio Invalido";
                    return result;
                }

                var ciudad = await _sisUbicacionService.GetCiudad(dto.PaisId, dto.EstadoId, dto.MunicipioId, dto.CiudadId);
                if (ciudad is null)
                {
                    ciudad.Id = dto.CiudadId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ciudad Invalida";
                    return result;
                }

                var parroquia = await _sisUbicacionService.GetParroquia(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId);
                if (parroquia is null)
                {
                    parroquia.Id = dto.ParroquiaId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Parroquia Invalida";
                    return result;
                }

                var sector = await _sisUbicacionService.GetSector(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId, dto.SectorId);
                if (sector is null)
                {
                    sector.Id = dto.SectorId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sector Invalido";
                    return result;
                }

                var urbanizacion = await _sisUbicacionService.GetUrbanizacion(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId, dto.SectorId, dto.UrbanizacionId);
                if (urbanizacion is null)
                {
                    urbanizacion.Id = dto.UrbanizacionId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Urbanizacion Invalida";
                    return result;
                }
                if (dto.Vialidad is not null && dto.Vialidad.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad invalida";
                    return result;
                }
                var vialidad = dto.Vialidad;
                if (vialidad.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "vialidad invalida";
                    return result;
                }


                var tipoViviendaId = await _admDescriptivasService.GetByTitulo(6);
                if (tipoViviendaId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo vivienda Id invalido";
                    return result;
                }
                if (dto.Vivienda is not null && dto.Vivienda.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vivienda invalida";
                    return result;
                }
                var vivienda = dto.Vivienda;
                if (vivienda.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vivienda invalida";
                    return result;
                }


                if (dto.TipoNivelId > 0)
                {
                    var tipoNivelId = await _admDescriptivasService.GetByTitulo(7);
                    if (tipoNivelId == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo nivel Id invalido";
                        return result;
                    }
                }
                if (dto.Nivel is not null && dto.Nivel.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nivel invalido";
                    return result;
                }

                if (dto.ComplementoDir is not null && dto.ComplementoDir.Length > 200)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "ComplementoDir invalido";
                    return result;
                }

                if (dto.TenenciaId > 0)
                {
                    var tipoNivelId = await _admDescriptivasService.GetByTitulo(8);
                    if (tipoNivelId == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tenencia Id invalida";
                        return result;
                    }
                }

                if (dto.CodigoPostal == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Postal invalido";
                    return result;
                }

                if (dto.FechaIni == DateTime.Now)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha ini invalida";
                    return result;
                }
                if (dto.FechaFin == DateTime.Today)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Fin invalida";
                    return result;
                }
                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 invalido";
                    return result;
                }

                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 invalido";
                    return result;
                }

                var unidadTrabajoId = await _bmDescriptivaService.GetByIdAndTitulo(6, dto.UnidadTrabajoId);
                if (unidadTrabajoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad trabajo invalido";
                    return result;
                }

                codigoDirBien.CODIGO_DIR_BIEN = dto.CodigoDirBien;
                codigoDirBien.CODIGO_ICP = dto.CodigoIcp;
                codigoDirBien.PAIS_ID = dto.PaisId;
                codigoDirBien.ESTADO_ID = dto.EstadoId;
                codigoDirBien.MUNICIPIO_ID = dto.MunicipioId;
                codigoDirBien.CIUDAD_ID = dto.CiudadId;
                codigoDirBien.PARROQUIA_ID = dto.ParroquiaId;
                codigoDirBien.SECTOR_ID = dto.SectorId;
                codigoDirBien.URBANIZACION_ID = dto.UrbanizacionId;
                codigoDirBien.MANZANA_ID = dto.ManzanaId;
                codigoDirBien.PARCELA_ID = dto.ParcelaId;
                codigoDirBien.VIALIDAD_ID = dto.VialidadId;
                codigoDirBien.VIALIDAD = dto.Vialidad;
                codigoDirBien.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                codigoDirBien.VIVIENDA = dto.Vivienda;
                codigoDirBien.TIPO_NIVEL_ID = dto.TipoNivelId;
                codigoDirBien.NIVEL = dto.Nivel;
                codigoDirBien.TIPO_UNIDAD_ID = dto.TipoUnidadId;
                codigoDirBien.NUMERO_UNIDAD = dto.NumeroUnidad;
                codigoDirBien.COMPLEMENTO_DIR = dto.ComplementoDir;
                codigoDirBien.TENENCIA_ID = dto.TenenciaId;
                codigoDirBien.CODIGO_POSTAL = dto.CodigoPostal;
                codigoDirBien.FECHA_INI=dto.FechaIni;
                codigoDirBien.FECHA_FIN = dto.FechaFin;
                codigoDirBien.EXTRA1=dto.Extra1;
                codigoDirBien.EXTRA2 = dto.Extra2;
                codigoDirBien.EXTRA3 = dto.Extra3;
                codigoDirBien.UNIDAD_TRABAJO_ID = dto.UnidadTrabajoId;
                


                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoDirBien.CODIGO_EMPRESA = conectado.Empresa;
                codigoDirBien.USUARIO_UPD = conectado.Usuario;
                codigoDirBien.FECHA_UPD = DateTime.Now;


                await _repository.Update(codigoDirBien);



                var resultDto = await MapDirBienDto(codigoDirBien);
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

        public async Task<ResultDto<BmDirBienResponseDto>> Create(BmDirBienUpdateDto dto)
        {

            ResultDto<BmDirBienResponseDto> result = new ResultDto<BmDirBienResponseDto>(null);
            try
            {
                var codigoDirBien = await _repository.GetByCodigoDirBien(dto.CodigoDirBien);
                if (codigoDirBien != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Dir Bien no existe";
                    return result;
                }
                var codigoIcp = await _indiceCategoriaProgramaService.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp no existe";
                    return result;
                }
                var pais = await _sisUbicacionService.GetPais(dto.PaisId);
               
                if (pais is null)
                {
                    pais.Id = dto.PaisId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais  Invalido";
                    return result;
                }
               
                var estado = await _sisUbicacionService.GetEstado(dto.PaisId,dto.EstadoId);
                if (estado is null)
                {
                    estado.Id = dto.EstadoId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Invalido";
                    return result;
                }

                var municipio = await _sisUbicacionService.GetMunicipio(dto.PaisId, dto.EstadoId,dto.MunicipioId);
                if (municipio is null)
                {
                    municipio.Id = dto.MunicipioId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Municipio Invalido";
                    return result;
                }

                var ciudad = await _sisUbicacionService.GetCiudad(dto.PaisId, dto.EstadoId,dto.MunicipioId, dto.CiudadId);
                if (ciudad is null)
                {
                    ciudad.Id = dto.CiudadId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ciudad Invalida";
                    return result;
                }

                var parroquia = await _sisUbicacionService.GetParroquia(dto.PaisId, dto.EstadoId,dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId);
                if (parroquia is null)
                {
                    parroquia.Id = dto.ParroquiaId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Parroquia Invalida";
                    return result;
                }

                var sector = await _sisUbicacionService.GetSector(dto.PaisId, dto.EstadoId,dto.MunicipioId,
                    dto.CiudadId,dto.ParroquiaId, dto.SectorId);
                if (sector is null)
                {
                    sector.Id = dto.SectorId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sector Invalido";
                    return result;
                }

                var urbanizacion = await _sisUbicacionService.GetUrbanizacion(dto.PaisId, dto.EstadoId,dto.MunicipioId,
                    dto.CiudadId,dto.ParroquiaId,dto.SectorId, dto.UrbanizacionId);
                if (urbanizacion is null)
                {
                    urbanizacion.Id = dto.UrbanizacionId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Urbanizacion Invalida";
                    return result;
                }
                if (dto.Vialidad is not null && dto.Vialidad.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad invalida";
                    return result;
                }
                var vialidad = dto.Vialidad;
                if (vialidad.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "vialidad invalida";
                    return result;
                }


                var tipoViviendaId = await _admDescriptivasService.GetByTitulo(6);
                if (tipoViviendaId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo vivienda Id invalido";
                    return result;
                }
                if (dto.Vivienda is not null && dto.Vivienda.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vivienda invalida";
                    return result;
                }
                var vivienda = dto.Vivienda;
                if (vivienda.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vivienda invalida";
                    return result;
                }


                if (dto.TipoNivelId > 0)
                {
                    var tipoNivelId = await _admDescriptivasService.GetByTitulo(7);
                    if (tipoNivelId == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo nivel Id invalido";
                        return result;
                    }
                }
                if (dto.Nivel is not null && dto.Nivel.Length>20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nivel invalido";
                    return result;
                }
                
                if (dto.ComplementoDir is not null && dto.ComplementoDir.Length > 200)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "ComplementoDir invalido";
                    return result;
                }
               
                if (dto.TenenciaId > 0)
                {
                    var tipoNivelId = await _admDescriptivasService.GetByTitulo(8);
                    if (tipoNivelId == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tenencia Id invalida";
                        return result;
                    }
                }

                if (dto.CodigoPostal == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Postal invalido";
                    return result;
                }

                if (dto.FechaIni == DateTime.Now)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha ini invalida";
                    return result;
                }
                if (dto.FechaFin == DateTime.Today)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Fin invalida";
                    return result;
                }
                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 invalido";
                    return result;
                }

                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 invalido";
                    return result;
                }

                var unidadTrabajoId = await _bmDescriptivaService.GetByIdAndTitulo(6, dto.UnidadTrabajoId);
                if (unidadTrabajoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad trabajo invalido";
                    return result;
                }

                BM_DIR_BIEN entity = new BM_DIR_BIEN();

                entity.CODIGO_DIR_BIEN = await _repository.GetNextKey();
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.PAIS_ID = dto.PaisId;
                entity.ESTADO_ID = dto.EstadoId;
                entity.MUNICIPIO_ID = dto.MunicipioId;
                entity.CIUDAD_ID = dto.CiudadId;
                entity.PARROQUIA_ID = dto.ParroquiaId;
                entity.SECTOR_ID = dto.SectorId;
                entity.URBANIZACION_ID = dto.UrbanizacionId;
                entity.MANZANA_ID = dto.ManzanaId;
                entity.PARCELA_ID = dto.ParcelaId;
                entity.VIALIDAD_ID = dto.VialidadId;
                entity.VIALIDAD = dto.Vialidad;
                entity.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                entity.VIVIENDA = dto.Vivienda;
                entity.TIPO_NIVEL_ID = dto.TipoNivelId;
                entity.NIVEL = dto.Nivel;
                entity.TIPO_UNIDAD_ID = dto.TipoUnidadId;
                entity.NUMERO_UNIDAD = dto.NumeroUnidad;
                entity.COMPLEMENTO_DIR = dto.ComplementoDir;
                entity.TENENCIA_ID = dto.TenenciaId;
                entity.CODIGO_POSTAL = dto.CodigoPostal;
                entity.FECHA_INI = dto.FechaIni;
                entity.FECHA_FIN = dto.FechaFin;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.UNIDAD_TRABAJO_ID = dto.UnidadTrabajoId;


                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDirBienDto(created.Data);
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

        public async Task<ResultDto<BmDirBienDeleteDto>> Delete(BmDirBienDeleteDto dto)
        {

            ResultDto<BmDirBienDeleteDto> result = new ResultDto<BmDirBienDeleteDto>(null);
            try
            {

                var codigoDirBien = await _repository.GetByCodigoDirBien(dto.CodigoDirBien);
                if (codigoDirBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Dir Bien no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDirBien);

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

