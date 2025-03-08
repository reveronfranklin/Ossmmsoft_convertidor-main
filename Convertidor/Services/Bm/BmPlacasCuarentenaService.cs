using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm
{
    public class BmPlacasCuarentenaService: IBmPlacasCuarentenaService
    {

      
        private readonly IBmPlacasCuarentenaRepository _repository;
        private readonly IBM_V_BM1Repository _bmVBm1Repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public BmPlacasCuarentenaService(IBmPlacasCuarentenaRepository repository,
                                        IBM_V_BM1Repository bmV_BM1Repository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IConfiguration configuration)
		{
            _repository = repository;
            _bmVBm1Repository = bmV_BM1Repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _configuration = configuration;
           

        }

        public async Task<BmPlacaCuarentenaResponseDto> MapBmPlacaCuarentena(BM_PLACAS_CUARENTENA dtos)
        {


            BmPlacaCuarentenaResponseDto itemResult = new BmPlacaCuarentenaResponseDto();
            itemResult.CodigoPlacaCuarentena = dtos.CODIGO_PLACA_CUARENTENA;
            itemResult.NumeroPlaca = dtos.NUMERO_PLACA;

            return itemResult;

        }
        public async Task<List<BmPlacaCuarentenaResponseDto>> MapListArticulosDto(List<BM_PLACAS_CUARENTENA> dtos)
        {
            List<BmPlacaCuarentenaResponseDto> result = new List<BmPlacaCuarentenaResponseDto>();


            foreach (var item in dtos)
            {

                BmPlacaCuarentenaResponseDto itemResult = new BmPlacaCuarentenaResponseDto();

                itemResult = await MapBmPlacaCuarentena(item);

                result.Add(itemResult);
            }
            return result;



        }
        public async Task<ResultDto<List<BmPlacaCuarentenaResponseDto>>> GetAll()
        {

            ResultDto<List<BmPlacaCuarentenaResponseDto>> result = new ResultDto<List<BmPlacaCuarentenaResponseDto>>(null);
            try
            {

                var placas = await _repository.GetAll();



                if (placas.Any())
                {
                    List<BmPlacaCuarentenaResponseDto> listDto = new List<BmPlacaCuarentenaResponseDto>();

                    foreach (var item in placas)
                    {
                        if (item != null)
                        {
                            BmPlacaCuarentenaResponseDto dto = new BmPlacaCuarentenaResponseDto();
                            dto = await MapBmPlacaCuarentena(item);

                            listDto.Add(dto);
                        }
                      
                    }


                    result.Data = listDto;
                    result.CantidadRegistros = listDto.Count;
                    result.TotalPage = 1;
                    result.Page = 1;
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

       

        public async Task<ResultDto<BmPlacaCuarentenaResponseDto>> GetByCodigo(int codigo)
            {

            ResultDto<BmPlacaCuarentenaResponseDto> result = new ResultDto<BmPlacaCuarentenaResponseDto>(null);
            try
            {




                var placa = await _repository.GetByCodigo(codigo);

                if (placa != null)
                {
                    var resultDto =await  MapBmPlacaCuarentena(placa);
                    result.Data = resultDto;

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

        
        


        public async Task<ResultDto<BmPlacaCuarentenaResponseDto>> Create(BmPlacaCuarentenaUpdateDto dto)
        {

            ResultDto<BmPlacaCuarentenaResponseDto> result = new ResultDto<BmPlacaCuarentenaResponseDto>(null);
            try
            {

                if (string.IsNullOrEmpty(dto.NumeroPlaca))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Placa Invalida";
                    return result;
                }

                var bm1 = await _bmVBm1Repository.GetByNroPlaca(dto.NumeroPlaca);
                if (bm1 == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Placa No Existe en BM1";
                    return result;
                }
                
                
                var placa = await _repository.GetByNumeroPlaca(dto.NumeroPlaca);
                if (placa != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Placa ya existe en cuarentena";
                    return result;
                }
              
             


                BM_PLACAS_CUARENTENA entity = new BM_PLACAS_CUARENTENA();        
                entity.CODIGO_PLACA_CUARENTENA = await _repository.GetNextKey();
                entity.NUMERO_PLACA = dto.NumeroPlaca;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var resultDto = await MapBmPlacaCuarentena(created.Data);
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
        
       


        public async Task<ResultDto<BmPlacaCuarentenaDeleteDto>> Delete(BmPlacaCuarentenaDeleteDto dto)
        {

            ResultDto<BmPlacaCuarentenaDeleteDto> result = new ResultDto<BmPlacaCuarentenaDeleteDto>(null);
            try
            {

                var placa = await _repository.GetByCodigo(dto.CodigoPlacaCuarentena);
                if (placa == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Placa no existe";
                    return result;
                }

              
                var deleted = await _repository.Delete(dto.CodigoPlacaCuarentena);

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

