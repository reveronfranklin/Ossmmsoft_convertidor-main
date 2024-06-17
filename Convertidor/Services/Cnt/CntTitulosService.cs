using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
	public class CntTituloService: ICntTituloService
    {

      
        private readonly ICntTitulosRepository _repository;
        private readonly ICntDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IConfiguration _configuration;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CntTituloService(ICntTitulosRepository repository,
                                      IConfiguration configuration,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      ICntDescriptivaRepository repositoryPreDescriptiva)
		{
            _repository = repository;
            _configuration = configuration;
            _sisUsuarioRepository = sisUsuarioRepository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;


        }


        public async Task<ResultDto<List<CntTitulosResponseDto>>> GetAll()
        {

            ResultDto<List<CntTitulosResponseDto>> result = new ResultDto<List<CntTitulosResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();

               

                if (titulos.Count() > 0)
                {
                    List<CntTitulosResponseDto> listDto = new List<CntTitulosResponseDto>();

                    foreach (var item in titulos)
                    {
                        CntTitulosResponseDto dto = new CntTitulosResponseDto();
                        dto.TituloId = item.TITULO_ID;
                        dto.TituloFkId = item.TITULO_FK_ID;
                        dto.Titulo = item.TITULO;
                        dto.Codigo = item.CODIGO;
                        if (item.EXTRA1 == null) item.EXTRA1 = "";
                        if (item.EXTRA2 == null) item.EXTRA2 = "";
                        if (item.EXTRA3 == null) item.EXTRA3 = "";
                        dto.Extra1 = item.EXTRA1;
                        dto.Extra2 = item.EXTRA2;
                        dto.Extra3 = item.EXTRA3;

                        listDto.Add(dto);
                    }


                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }


        

        public async Task<ResultDto<CntTitulosResponseDto>> Update(CntTitulosUpdateDto dto)
        {

            ResultDto<CntTitulosResponseDto> result = new ResultDto<CntTitulosResponseDto>(null);
            try
            {

                var tituloUpdate = await _repository.GetByCodigo(dto.TituloId);
                if (tituloUpdate == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo no existe";
                    return result;
                }
                if (dto.Titulo.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }

            

                if (dto.TituloFkId > 0)
                {
                    var padre = await _repository.GetByCodigo(dto.TituloFkId);
                    if (padre == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Padre Invalido";
                        return result;
                    }

                }
                if (dto.Codigo.Length > 0)
                {
                    var tituloCodigo = await _repository.GetByCodigoString(dto.Codigo);
                    if (tituloCodigo != null && tituloCodigo.TITULO_ID != dto.TituloId)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = $"Ya existe titulo con ese codigo: {dto.Codigo}";
                        return result;
                    }

                }
                //No se permite modificar ni Extra1 ni Codigo
                //descriptiva.EXTRA1 = dto.Extra1;
                //descriptiva.CODIGO = dto.Codigo;

                tituloUpdate.TITULO = dto.Titulo;
                tituloUpdate.TITULO_FK_ID = dto.TituloFkId;


                tituloUpdate.EXTRA2 = dto.Extra2;
                tituloUpdate.EXTRA3 = dto.Extra3;
                tituloUpdate.FECHA_UPD = DateTime.Now;




                var conectado = await _sisUsuarioRepository.GetConectado();
                tituloUpdate.CODIGO_EMPRESA = conectado.Empresa;
                tituloUpdate.USUARIO_UPD = conectado.Usuario;
                
                await _repository.Update(tituloUpdate);

                var resultDto = MapCntTitulo(tituloUpdate);
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

        public async Task<ResultDto<CntTitulosResponseDto>> Create(CntTitulosUpdateDto dto)
        {

            ResultDto<CntTitulosResponseDto> result = new ResultDto<CntTitulosResponseDto>(null);
            try
            {

                var titulo = await _repository.GetByCodigo(dto.TituloId);
                if (titulo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo existe";
                    return result;
                }
                if (dto.Titulo.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }

            

                if (dto.TituloFkId > 0)
                {
                    var padre = await _repository.GetByCodigo(dto.TituloFkId);
                    if (padre == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Padre Invalido";
                        return result;
                    }

                }
                var descriptivaCodigo = await _repository.GetByCodigoString(dto.Codigo);
                if (descriptivaCodigo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Ya existe Titulo con ese codigo: {dto.Codigo}";
                    return result;
                }

                CNT_TITULOS entity = new CNT_TITULOS();
                entity.TITULO_ID = await _repository.GetNextKey();
                entity.TITULO_FK_ID = dto.TituloFkId;
           
                entity.CODIGO = dto.Codigo;
                entity.TITULO = dto.Titulo;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;


                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                
                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = MapCntTitulo(created.Data);
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
 
        public async Task<ResultDto<CntTitulosDeleteDto>> Delete(CntTitulosDeleteDto dto)
        {

            ResultDto<CntTitulosDeleteDto> result = new ResultDto<CntTitulosDeleteDto>(null);
            try
            {

                var titulo = await _repository.GetByCodigo(dto.TituloId);
                if (titulo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Titulo no existe";
                    return result;
                }

                var descriptiva = await  _repositoryPreDescriptiva.GetByTitulo(dto.TituloId);
                if (descriptiva != null && descriptiva.Count>0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = $"Titulo esta asociado a una  descriptiva";
                    return result;
                }


                var deleted = await _repository.Delete(dto.TituloId);

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

        public CntTitulosResponseDto MapCntTitulo(CNT_TITULOS entity)
        {
            CntTitulosResponseDto dto = new CntTitulosResponseDto();
            dto.TituloId = entity.TITULO_ID;
            dto.TituloFkId = entity.TITULO_FK_ID;
            dto.Titulo = entity.TITULO;
            if (entity.CODIGO == null) entity.CODIGO = "";
            dto.Codigo = entity.CODIGO;
            dto.TituloId = entity.TITULO_ID;
            if (entity.EXTRA1 == null) entity.EXTRA1 = "";
            if (entity.EXTRA2 == null) entity.EXTRA2 = "";
            if (entity.EXTRA3 == null) entity.EXTRA3 = "";
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;

            return dto;

        }



        

   
    }
}

