using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Data.Repository.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatAuditoriaService : ICatAuditoriaService
    {
        private readonly ICatAuditoriaRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CatAuditoriaService(ICatAuditoriaRepository repository,
                                   ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CatAuditoriaResponseDto> MapCatAuditoria(CAT_AUDITORIA entity)
        {
            CatAuditoriaResponseDto dto = new CatAuditoriaResponseDto();

            dto.CodigoAuditoria = entity.CODIGO_AUDITORIA;
            dto.TablaId = entity.TABLA_ID;
            dto.SectorId = entity.SECTOR_ID;
            dto.UsuarioIns = entity.USUARIO_INS;
            dto.FechaIns = entity.FECHA_INS;
            dto.FechaInsString = entity.FECHA_INS.ToString("u");
            FechaDto fechaInsObj = FechaObj.GetFechaDto(entity.FECHA_INS);
            dto.FechaInsObj = (FechaDto)fechaInsObj;
            dto.CodigoEmpresa = entity.CODIGO_EMPRESA;
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

        public async Task<List<CatAuditoriaResponseDto>> MapListCatAuditoria(List<CAT_AUDITORIA> dtos)
        {
            List<CatAuditoriaResponseDto> result = new List<CatAuditoriaResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCatAuditoria(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatAuditoriaResponseDto>>> GetAll()
        {

            ResultDto<List<CatAuditoriaResponseDto>> result = new ResultDto<List<CatAuditoriaResponseDto>>(null);
            try
            {
                var auditoria = await _repository.GetAll();
                var cant = auditoria.Count();
                if (auditoria != null && auditoria.Count() > 0)
                {
                    var listDto = await MapListCatAuditoria(auditoria);

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

        public async Task<ResultDto<CatAuditoriaResponseDto>> Create(CatAuditoriaUpdateDto dto)
        {

            ResultDto<CatAuditoriaResponseDto> result = new ResultDto<CatAuditoriaResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.TablaId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tabla Id Invalido ";
                    return result;
                }

                if (dto.SectorId < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sector Id Invalida";
                    return result;

                }

                if (dto.UsuarioIns < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Ins Invalida";
                    return result;

                }

                if (dto.FechaIns == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Ins Invalida";
                    return result;

                }

                if (dto.CodigoEmpresa < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Empresa Invalido";
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

                

                CAT_AUDITORIA entity = new CAT_AUDITORIA();
                entity.CODIGO_AUDITORIA = await _repository.GetNextKey();
                entity.TABLA_ID = dto.TablaId;
                entity.SECTOR_ID = dto.SectorId;
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
                    var resultDto = await MapCatAuditoria(created.Data);
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

        public async Task<ResultDto<CatAuditoriaResponseDto>> Update(CatAuditoriaUpdateDto dto)
        {

            ResultDto<CatAuditoriaResponseDto> result = new ResultDto<CatAuditoriaResponseDto>(null);
            try
            {


                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoAuditoria = await _repository.GetByCodigo(dto.CodigoAuditoria);
                if (codigoAuditoria == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Auditoria Invalido";
                    return result;

                }


                if (dto.TablaId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tabla Id Invalido ";
                    return result;
                }

                if (dto.SectorId < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sector Id Invalida";
                    return result;

                }

                if (dto.UsuarioIns < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Ins Invalida";
                    return result;

                }

                if (dto.FechaIns == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Ins Invalida";
                    return result;

                }

                if (dto.CodigoEmpresa < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Empresa Invalido";
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


                codigoAuditoria.CODIGO_AUDITORIA = dto.CodigoAuditoria;
                codigoAuditoria.TABLA_ID = dto.TablaId;
                codigoAuditoria.SECTOR_ID = dto.SectorId;
                codigoAuditoria.USUARIO_INS = dto.UsuarioIns;
                codigoAuditoria.FECHA_INS = dto.FechaIns;
                codigoAuditoria.CODIGO_EMPRESA = dto.CodigoEmpresa;
                codigoAuditoria.EXTRA1 = dto.Extra1;
                codigoAuditoria.EXTRA2 = dto.Extra2;
                codigoAuditoria.EXTRA3 = dto.Extra3;
                codigoAuditoria.EXTRA4 = dto.Extra4;
                codigoAuditoria.EXTRA5 = dto.Extra5;
                codigoAuditoria.EXTRA6 = dto.Extra6;
                codigoAuditoria.EXTRA7 = dto.Extra7;
                codigoAuditoria.EXTRA8 = dto.Extra8;
                codigoAuditoria.EXTRA9 = dto.Extra9;
                codigoAuditoria.EXTRA11 = dto.Extra11;
                codigoAuditoria.EXTRA12 = dto.Extra12;
                codigoAuditoria.EXTRA13 = dto.Extra13;
                codigoAuditoria.EXTRA14 = dto.Extra14;
                codigoAuditoria.EXTRA15 = dto.Extra15;


                codigoAuditoria.CODIGO_EMPRESA = conectado.Empresa;
                codigoAuditoria.USUARIO_INS = conectado.Usuario;
                codigoAuditoria.FECHA_INS = DateTime.Now;

                await _repository.Update(codigoAuditoria);
                var resultDto = await MapCatAuditoria(codigoAuditoria);
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


    }
}
