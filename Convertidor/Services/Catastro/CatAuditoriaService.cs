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

        public CatAuditoriaService(ICatAuditoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatAuditoriaResponseDto> MapCatAuditoria(CAT_AUDITORIA entity)
        {
            CatAuditoriaResponseDto dto = new CatAuditoriaResponseDto();

            dto.CodigoAuditoria = entity.CODIGO_AUDITORIA;
            dto.TablaId = entity.TABLA_ID;
            dto.SectorId = entity.SECTOR_ID;
            dto.UsuarioIns = entity.USUARIO_INS;
            dto.FechaIns = entity.FECHA_INS;
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
    }
}
