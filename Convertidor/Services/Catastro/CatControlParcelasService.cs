using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public class CatControlParcelasService : ICatControlParcelasService
    {
        private readonly ICatControlParcelasRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICAT_UBICACION_NACService _cAT_UBICACION_NACService;

        public CatControlParcelasService(ICatControlParcelasRepository repository,
                                         ISisUsuarioRepository sisUsuarioRepository,
                                         ICAT_UBICACION_NACService cAT_UBICACION_NACService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cAT_UBICACION_NACService = cAT_UBICACION_NACService;
        }

        public async Task<CatControlParcelasResponseDto> MapControlParcelas(CAT_CONTROL_PARCELAS entity)
        {
            CatControlParcelasResponseDto dto = new CatControlParcelasResponseDto();

            dto.CodigoControlParcela = entity.CODIGO_CONTROL_PARCELA;
            dto.CodigoCatastro = entity.CODIGO_CATASTRO;
            dto.CodigoViejoCat = entity.CODIGO_VIEJO_CAT;
            dto.CodigoContribuyente = entity.CODIGO_CONTRIBUYENTE;
            dto.CodigoUbicacionNac = entity.CODIGO_UBICACION_NAC;
            dto.PaisId = entity.PAIS_ID;
            dto.EntidadId = entity.ENTIDAD_ID;
            dto.MunicipioId = entity.MUNICIPIO_ID;
            dto.ParroquiaId = entity.PARROQUIA_ID;
            dto.AmbitoId = entity.AMBITO_ID;
            dto.SectorId = entity.SECTOR_ID;
            dto.SubsectorId = entity.SUB_SECTOR_ID;
            dto.ManzanaId = entity.MANZANA_ID;
            dto.ParcelaId = entity.PARCELA_ID;
            dto.SubsectorId = entity.SUB_SECTOR_ID;
            dto.Observacion = entity.OBSERVACION;
            dto.NumeroControl = entity.NUMERO_CONTROL;
            dto.AreaParcela = entity.AREA_PARCELA;
            dto.FrenteParcela = entity.FRENTE_PARCELA;
            dto.FrenteTipo = entity.FRENTE_TIPO;
            dto.AreaTipo = entity.AREA_TIPO;
            dto.TipoTransaccion = entity.TIPO_TRANSACCION;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.Extra4 = entity.EXTRA4;
            dto.Extra5 = entity.EXTRA5;
            dto.Extra6 = entity.EXTRA6;
            dto.Extra7 = entity.EXTRA7;
            dto.Extra8 = entity.EXTRA8;
            dto.Extra9 = entity.EXTRA9;
            dto.Extra10 = entity.EXTRA10;
            dto.Extra11 = entity.EXTRA11;
            dto.Extra12 = entity.EXTRA12;
            dto.Extra13 = entity.EXTRA13;
            dto.Extra14 = entity.EXTRA14;
            dto.Extra15 = entity.EXTRA15;


            return dto;

        }

        public async Task<List<CatControlParcelasResponseDto>> MapListControlParcelas(List<CAT_CONTROL_PARCELAS> dtos)
        {
            List<CatControlParcelasResponseDto> result = new List<CatControlParcelasResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapControlParcelas(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatControlParcelasResponseDto>>> GetAll()
        {

            ResultDto<List<CatControlParcelasResponseDto>> result = new ResultDto<List<CatControlParcelasResponseDto>>(null);
            try
            {
                var controlParcelas = await _repository.GetAll();
                var cant = controlParcelas.Count();
                if (controlParcelas != null && controlParcelas.Count() > 0)
                {
                    var listDto = await MapListControlParcelas(controlParcelas);

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

        public async Task<ResultDto<CatControlParcelasResponseDto>> Create(CatControlParcelasUpdateDto dto)
        {

            ResultDto<CatControlParcelasResponseDto> result = new ResultDto<CatControlParcelasResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoCatastro <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo catastro Invalido ";
                    return result;
                }

                if (dto.CodigoViejoCat <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo viejo catastro Invalido";
                    return result;

                }

                if (dto.CodigoContribuyente <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo contribuyente Invalido";
                    return result;

                }

                if (dto.CodigoUbicacionNac <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Ubicacion Nacional Invalido";
                    return result;

                }

                var pais = await _cAT_UBICACION_NACService.GetPais(dto.PaisId);

                if (pais is null)
                {
                    pais.Id = dto.PaisId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais  Invalido";
                    return result;
                }

                var estado = await _cAT_UBICACION_NACService.GetEstado(dto.PaisId, dto.EntidadId);
                if (estado is null)
                {
                    estado.Id = dto.EntidadId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Invalido";
                    return result;
                }

                var municipio = await _cAT_UBICACION_NACService.GetMunicipio(dto.PaisId, dto.EntidadId, dto.MunicipioId);
                if (municipio is null)
                {
                    municipio.Id = dto.MunicipioId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Municipio Invalido";
                    return result;
                }

                var ciudad = await _cAT_UBICACION_NACService.GetCiudad(dto.PaisId, dto.EntidadId, dto.MunicipioId, dto.AmbitoId);
                if (ciudad is null)
                {
                    ciudad.Id = dto.AmbitoId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ciudad Invalida";
                    return result;
                }

                var parroquia = await _cAT_UBICACION_NACService.GetParroquia(dto.PaisId, dto.EntidadId, dto.MunicipioId,
                    dto.AmbitoId, dto.ParroquiaId);
                if (parroquia is null)
                {
                    parroquia.Id = dto.ParroquiaId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Parroquia Invalida";
                    return result;
                }

                var sector = await _cAT_UBICACION_NACService.GetSector(dto.PaisId, dto.EntidadId, dto.MunicipioId,
                    dto.AmbitoId, dto.ParroquiaId, dto.SectorId);
                if (sector is null)
                {
                    sector.Id = dto.SectorId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sector Invalido";
                    return result;
                }

                if (dto.Observacion.Length > 100) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observacion Invalida";
                    return result;

                }

                if (dto.NumeroControl.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Control Invalido";
                    return result;

                }

                if (dto.AreaParcela < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Parcela Invalida";
                    return result;

                }

                if (dto.FrenteParcela < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frente Parcela Invalida";
                    return result;

                }

                if (dto.FrenteTipo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frente Tipo Invalida";
                    return result;

                }

                if (dto.AreaTipo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Tipo Invalida";
                    return result;

                }

                if (dto.TipoTransaccion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Invalida";
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

                if (dto.Extra4 is not null && dto.Extra4.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra4 Invalido";
                    return result;
                }
                if (dto.Extra5 is not null && dto.Extra5.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra5 Invalido";
                    return result;
                }

                if (dto.Extra6 is not null && dto.Extra6.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra6 Invalido";
                    return result;
                }

                if (dto.Extra7 is not null && dto.Extra7.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra7 Invalido";
                    return result;
                }
                if (dto.Extra8 is not null && dto.Extra8.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra8 Invalido";
                    return result;
                }

                if (dto.Extra9 is not null && dto.Extra9.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra9 Invalido";
                    return result;
                }

                if (dto.Extra10 is not null && dto.Extra10.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra10 Invalido";
                    return result;
                }
                if (dto.Extra11 is not null && dto.Extra11.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra11 Invalido";
                    return result;
                }

                if (dto.Extra12 is not null && dto.Extra12.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra12 Invalido";
                    return result;
                }

                if (dto.Extra13 is not null && dto.Extra13.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra13 Invalido";
                    return result;
                }
                if (dto.Extra14 is not null && dto.Extra14.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra14 Invalido";
                    return result;
                }

                if (dto.Extra15 is not null && dto.Extra15.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra15 Invalido";
                    return result;
                }



                CAT_CONTROL_PARCELAS entity = new CAT_CONTROL_PARCELAS();
                entity.CODIGO_CONTROL_PARCELA = await _repository.GetNextKey();
                entity.CODIGO_CATASTRO = dto.CodigoCatastro;
                entity.CODIGO_VIEJO_CAT = dto.CodigoViejoCat;
                entity.CODIGO_CONTRIBUYENTE = dto.CodigoContribuyente;
                entity.CODIGO_UBICACION_NAC = dto.CodigoUbicacionNac;
                entity.PAIS_ID = dto.PaisId;
                entity.ENTIDAD_ID = dto.EntidadId;
                entity.MUNICIPIO_ID = dto.MunicipioId;
                entity.PARROQUIA_ID = dto.ParroquiaId;
                entity.AMBITO_ID = dto.AmbitoId;
                entity.SECTOR_ID = dto.SectorId;
                entity.SUB_SECTOR_ID = dto.SubsectorId;
                entity.MANZANA_ID = dto.ManzanaId;
                entity.PARCELA_ID = dto.ParcelaId;
                entity.SUB_PARCELA_ID = dto.SubParcelaId;
                entity.OBSERVACION = dto.Observacion;
                entity.NUMERO_CONTROL = dto.NumeroControl;
                entity.AREA_PARCELA = dto.AreaParcela;
                entity.FRENTE_PARCELA = dto.FrenteParcela;
                entity.FRENTE_TIPO = dto.FrenteTipo;
                entity.AREA_TIPO = dto.AreaTipo;
                entity.TIPO_TRANSACCION = dto.TipoTransaccion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.EXTRA4 = dto.Extra4;
                entity.EXTRA5 = dto.Extra5;
                entity.EXTRA6 = dto.Extra6;
                entity.EXTRA7 = dto.Extra7;
                entity.EXTRA8 = dto.Extra8;
                entity.EXTRA9 = dto.Extra9;
                entity.EXTRA10 = dto.Extra10;
                entity.EXTRA11 = dto.Extra11;
                entity.EXTRA12 = dto.Extra12;
                entity.EXTRA13 = dto.Extra13;
                entity.EXTRA14 = dto.Extra14;
                entity.EXTRA15 = dto.Extra15;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapControlParcelas(created.Data);
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


    }
}
