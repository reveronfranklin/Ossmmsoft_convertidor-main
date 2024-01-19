using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.Protocols;
using System.Globalization;
using AutoMapper;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Data.Repository.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Adm;
using Convertidor.Services.Bm;
using Convertidor.Services.Presupuesto;
using Convertidor.Services.Rh;
using Convertidor.Services.Sis;
using IronPdf.Editing;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
	public class BmDirHBienService: IBmDirHBienService
    {
		
        private readonly DataContext _context;



   
        private readonly IBmDirHBienRepository _repository;
        private readonly IBmDescriptivasService _bmDescriptivaService;
        private readonly IAdmDescriptivasService _admDescriptivasService;
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ISisUbicacionService _sisUbicacionService;
        private readonly IMapper _mapper;

        public BmDirHBienService(IBmDirHBienRepository repository,
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
       
        public async Task<BmDirHBienResponseDto> GetByCodigoHDirBien(int codigoHDirBien)
        {
            try
            {
                var DirBien = await _repository.GetByCodigoHDirBien(codigoHDirBien);
                
                var result = await MapDirHBienDto(DirBien);


                return (BmDirHBienResponseDto)result;
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


        public async Task<BmDirHBienResponseDto> MapDirHBienDto(BM_DIR_H_BIEN dtos)
        {


            BmDirHBienResponseDto itemResult = new BmDirHBienResponseDto();
            itemResult.CodigoHDirBien = dtos.CODIGO_H_DIR_BIEN;
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
            itemResult.FechaFin= dtos.FECHA_FIN;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.UnidadTrabajoId = dtos.UNIDAD_TRABAJO_ID;

            return itemResult;

        }



        public async  Task<List<BmDirHBienResponseDto>> MapListDirHBienDto(List<BM_DIR_H_BIEN> dtos)
        {
            List<BmDirHBienResponseDto> result = new List<BmDirHBienResponseDto>();

            foreach (var item in dtos)
            {

                BmDirHBienResponseDto itemResult = new BmDirHBienResponseDto();
                itemResult = await MapDirHBienDto(item);
                
                result.Add(itemResult);


            }
            return result;



        }

        public async Task<ResultDto<BmDirHBienResponseDto>> Update(BmDirHBienUpdateDto dto)
        {

            ResultDto<BmDirHBienResponseDto> result = new ResultDto<BmDirHBienResponseDto>(null);
            try
            {
                var codigoHDirBien = await _repository.GetByCodigoHDirBien(dto.CodigoHDirBien);
                if (codigoHDirBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo HDir Bien no existe";
                    return result;
                }
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
                if (pais == null)
                {
                    pais.Id = dto.PaisId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais invalido";
                    return result;
                }

                var estado = await _sisUbicacionService.GetEstado(dto.PaisId, dto.EstadoId);
                if (estado == null)
                {
                    estado.Id = dto.EstadoId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado invalido";
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

                if(dto.Vialidad == string.Empty) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Vialidad invalida";
                    return result;
                }

                var tipoViviendaId = await _admDescriptivasService.GetByTitulo(6);
                if(tipoViviendaId==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo vivienda Id invalido";
                    return result;
                }
                if (dto.Vivienda == string.Empty)
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
                if(dto.Nivel==string.Empty) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nivel invalido";
                    return result;
                }

                if (dto.ComplementoDir == string.Empty) 
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

                if(dto.FechaIni==DateTime.MinValue) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha ini invalida";
                    return result;
                }
                if (dto.FechaFin == DateTime.MaxValue)
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

                codigoHDirBien.CODIGO_H_DIR_BIEN = dto.CodigoHDirBien;
                codigoHDirBien.CODIGO_DIR_BIEN = dto.CodigoDirBien;
                codigoHDirBien.CODIGO_ICP = dto.CodigoIcp;
                codigoHDirBien.PAIS_ID = dto.PaisId;
                codigoHDirBien.ESTADO_ID = dto.EstadoId;
                codigoHDirBien.MUNICIPIO_ID = dto.MunicipioId;
                codigoHDirBien.CIUDAD_ID = dto.CiudadId;
                codigoHDirBien.PARROQUIA_ID = dto.ParroquiaId;
                codigoHDirBien.SECTOR_ID = dto.SectorId;
                codigoHDirBien.URBANIZACION_ID = dto.UrbanizacionId;
                codigoHDirBien.MANZANA_ID = dto.ManzanaId;
                codigoHDirBien.PARCELA_ID = dto.ParcelaId;
                codigoHDirBien.VIALIDAD_ID = dto.VialidadId;
                codigoHDirBien.VIALIDAD = dto.Vialidad;
                codigoHDirBien.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                codigoHDirBien.VIVIENDA = dto.Vivienda;
                codigoHDirBien.TIPO_NIVEL_ID = dto.TipoNivelId;
                codigoHDirBien.NIVEL = dto.Nivel;
                codigoHDirBien.TIPO_UNIDAD_ID = dto.TipoUnidadId;
                codigoHDirBien.NUMERO_UNIDAD = dto.NumeroUnidad;
                codigoHDirBien.COMPLEMENTO_DIR = dto.ComplementoDir;
                codigoHDirBien.TENENCIA_ID = dto.TenenciaId;
                codigoHDirBien.CODIGO_POSTAL = dto.CodigoPostal;
                codigoHDirBien.FECHA_INI=dto.FechaIni;
                codigoHDirBien.FECHA_FIN = dto.FechaFin;
                codigoHDirBien.EXTRA1 = dto.Extra1;
                codigoHDirBien.EXTRA2 = dto.Extra2; 
                codigoHDirBien.EXTRA3 = dto.Extra3;
                codigoHDirBien.UNIDAD_TRABAJO_ID=dto.UnidadTrabajoId;
                codigoHDirBien.USUARIO_H_INS = dto.UsuarioHIns;
                codigoHDirBien.FECHA_H_INS=dto.FechaHIns;
                


                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoHDirBien.CODIGO_EMPRESA = conectado.Empresa;
                codigoHDirBien.USUARIO_UPD = conectado.Usuario;
                codigoHDirBien.FECHA_INS = DateTime.Now;


                await _repository.Update(codigoHDirBien);



                var resultDto = await MapDirHBienDto(codigoHDirBien);
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

        public async Task<ResultDto<BmDirHBienResponseDto>> Create(BmDirHBienUpdateDto dto)
        {

            ResultDto<BmDirHBienResponseDto> result = new ResultDto<BmDirHBienResponseDto>(null);
            try
            {
                var codigoHDirBien = await _repository.GetByCodigoHDirBien(dto.CodigoHDirBien);
                if (codigoHDirBien != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo HDir Bien no existe";
                    return result;
                }
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
                if (dto.Vialidad == string.Empty)
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
                if (dto.Vivienda == string.Empty)
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
                if (dto.Nivel == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nivel invalido";
                    return result;
                }
                var nivel = dto.Nivel;
                if (nivel.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nivel invalido";
                    return result;
                }


                if (dto.ComplementoDir == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "ComplementoDir invalido";
                    return result;
                }
                var complementoDir = dto.ComplementoDir;
                if (complementoDir.Length > 200)
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


                BM_DIR_H_BIEN entity = new BM_DIR_H_BIEN();

                entity.CODIGO_H_DIR_BIEN = await _repository.GetNextKey();
                entity.CODIGO_DIR_BIEN = dto.CodigoDirBien;
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
                entity.USUARIO_H_INS = dto.UsuarioHIns;
                entity.FECHA_H_INS = dto.FechaHIns;
                


                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDirHBienDto(created.Data);
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

        public async Task<ResultDto<BmDirHBienDeleteDto>> Delete(BmDirHBienDeleteDto dto)
        {

            ResultDto<BmDirHBienDeleteDto> result = new ResultDto<BmDirHBienDeleteDto>(null);
            try
            {

                var codigoHDirBien = await _repository.GetByCodigoHDirBien(dto.CodigoHDirBien);
                if (codigoHDirBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo HDir Bien no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoHDirBien);

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

