using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm
{
    public class BmUbicacionesResponsableService: IBmUbicacionesResponsableService
    {
        private readonly IBmUbicacionesResponsableRepository _repository;


        public BmUbicacionesResponsableService(IBmUbicacionesResponsableRepository repository)

        {
            _repository = repository;
          
        }

      
        
        public async Task<ResultDto<List<BmUbicacionesResponsablesResponseDto>>> GetAll()
        {

            ResultDto<List<BmUbicacionesResponsablesResponseDto>> result = new ResultDto<List<BmUbicacionesResponsablesResponseDto>>(null);
            try
            {

                var ubicaciones = await _repository.GetAll();



                if (ubicaciones.Count() > 0)
                {
                    List<BmUbicacionesResponsablesResponseDto> listDto = new List<BmUbicacionesResponsablesResponseDto>();

                    foreach (var item in ubicaciones)
                    {
                        BmUbicacionesResponsablesResponseDto dto = new BmUbicacionesResponsablesResponseDto();
                        
                        dto.CodigoBmConteo = item.CODIGO_BM_CONTEO;
                        dto.Conteo = item.CONTEO;
                        dto.Titulo  = item.TITULO;
                        dto.CodigoDirBien = item.CODIGO_DIR_BIEN;
                        dto.CodigoIcp = item.CODIGO_ICP;
                        dto.CodigoDirBien = item.CODIGO_DIR_BIEN;
                        dto.UnidadTrabajo = item.UNIDAD_TRABAJO;
                        dto.CodigoUsuario = item.CODIGO_USUARIO;
                        dto.CodigoPersona = item.CODIGO_PERSONA;
                        dto.Login = item.LOGIN;
                        dto.Cedula = item.CEDULA;
                        
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

      
        public async Task<ResultDto<List<BmUbicacionesResponsablesResponseDto>>> GetByUsuarioResponsable(string usuario)
        {

            ResultDto<List<BmUbicacionesResponsablesResponseDto>> result = new ResultDto<List<BmUbicacionesResponsablesResponseDto>>(null);
            try
            {

                var ubicaciones = await _repository.GetByUsuarioResponsable(usuario);



                if (ubicaciones.Count() > 0)
                {
                    List<BmUbicacionesResponsablesResponseDto> listDto = new List<BmUbicacionesResponsablesResponseDto>();

                    foreach (var item in ubicaciones)
                    {
                        BmUbicacionesResponsablesResponseDto dto = new BmUbicacionesResponsablesResponseDto();
                        
                        dto.CodigoBmConteo = item.CODIGO_BM_CONTEO;
                        dto.Conteo = item.CONTEO;
                        dto.Titulo  = item.TITULO;
                        dto.CodigoDirBien = item.CODIGO_DIR_BIEN;
                        dto.CodigoIcp = item.CODIGO_ICP;
                        dto.CodigoDirBien = item.CODIGO_DIR_BIEN;
                        dto.UnidadTrabajo = item.UNIDAD_TRABAJO;
                        dto.CodigoUsuario = item.CODIGO_USUARIO;
                        dto.CodigoPersona = item.CODIGO_PERSONA;
                        dto.Login = item.LOGIN;
                        dto.Cedula = item.CEDULA;
                        
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

        

    }
}

