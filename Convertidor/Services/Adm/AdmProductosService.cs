using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
	public class AdmProductosService: IAdmProductosService
    {

      
        private readonly IAdmProductosRepository _repository;

        private readonly IConfiguration _configuration;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public AdmProductosService(IAdmProductosRepository repository,
                                      IConfiguration configuration,
                                      ISisUsuarioRepository sisUsuarioRepository)
		{
            _repository = repository;
            _configuration = configuration;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<AdmProductosResponse>>> GetAllPaginate(AdmProductosFilterDto filter)
        {
            var result = await _repository.GetAllPaginate(filter);
            return result;
        }
        public async Task<ResultDto<List<AdmProductosResponse>>> GetAll()
        {

            ResultDto<List<AdmProductosResponse>> result = new ResultDto<List<AdmProductosResponse>>(null);
            try
            {

                var productos = await _repository.GetAll();

               

                if (productos.Count() > 0)
                {
                    List<AdmProductosResponse> listDto = new List<AdmProductosResponse>();

                    foreach (var item in productos)
                    {
                        AdmProductosResponse dto = new AdmProductosResponse();
                        dto.Codigo = item.CODIGO_PRODUCTO;
                        dto.Descripcion = item.DESCRIPCION;

                        dto.CodigoConcat =
                            $"{item.CODIGO_PRODUCTO1}-{item.CODIGO_PRODUCTO1}-{item.CODIGO_PRODUCTO2}-{item.CODIGO_PRODUCTO3}-{item.CODIGO_PRODUCTO4}";
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


       public async Task<ResultDto<bool>> Update(AdmProductosUpdateDto dto)
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try
            {

                if (!string.IsNullOrEmpty(dto.CodigoReal) && dto.CodigoReal.Length > 15)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "La longitud maxima del Codigo real es de 15 caracteres";
                    return result;
                }
                
                if (!string.IsNullOrEmpty(dto.DescripcionReal) && dto.DescripcionReal.Length > 500)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "La longitud maxima del Descripcion real es de 500 caracteres";
                    return result;
                }
                
                var producto = await _repository.GetByCodigo(dto.CodigoProducto);
                if (producto == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Producto no existe";
                    return result;
                }

                var productoReal = await _repository.GetByCodigoReal(dto.CodigoReal);
                if (productoReal != null && productoReal.CODIGO_PRODUCTO!= producto.CODIGO_PRODUCTO)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = $"Codigo Real {dto.CodigoReal} ya existe en el codigo {productoReal.CODIGO}-{productoReal.DESCRIPCION}";
                    return result;
                }
                
                
                
                producto.CODIGO_REAL = dto.CodigoReal;
                producto.DESCRIPCION_REAL = dto.DescripcionReal;
                producto.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                producto.CODIGO_EMPRESA = conectado.Empresa;
                producto.USUARIO_INS = conectado.Usuario;
                await _repository.Update(producto);
                await _repository.UpdateProductosCache();
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




    }
}

