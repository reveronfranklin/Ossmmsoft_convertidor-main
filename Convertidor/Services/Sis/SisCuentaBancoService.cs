using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;


namespace Convertidor.Services.Sis
{
    
	public class SisCuentaBancoService: ISisCuentaBancoService
    {
        private readonly ISisCuentaBancoRepository _repository;
        private readonly ISisDescriptivaRepository _sisDescriptivaRepository;
        private readonly ISisBancoRepository _bancoRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public SisCuentaBancoService(ISisCuentaBancoRepository repository,
                                        ISisDescriptivaRepository sisDescriptivaRepository,
                                        ISisBancoRepository bancoRepository,
                                      ISisUsuarioRepository sisUsuarioRepository)
		{
            _repository = repository;
            _sisDescriptivaRepository = sisDescriptivaRepository;
            _bancoRepository = bancoRepository;
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
       
       
        public  async Task<SisCuentaBancoResponseDto> MapSisCuentaBancoDto(SIS_CUENTAS_BANCOS dtos)
        {
            SisCuentaBancoResponseDto itemResult = new SisCuentaBancoResponseDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
                itemResult.CodigoBanco = dtos.CODIGO_BANCO;
                itemResult.DescripcionBanco = "";
                var banco = await _bancoRepository.GetById(itemResult.CodigoBanco);
                if (banco != null)
                {
                    itemResult.DescripcionBanco = banco.NOMBRE;
                }
                itemResult.TipoCuentaId = dtos.TIPO_CUENTA_ID;
                itemResult.DescripcionTipoCuenta = "";
                var descriptivaTipoCuenta = await _sisDescriptivaRepository.GetById(itemResult.TipoCuentaId);
                if (descriptivaTipoCuenta!=null)
                {
                    itemResult.DescripcionTipoCuenta = descriptivaTipoCuenta.DESCRIPCION;
                }
        
                itemResult.NoCuenta=dtos.NO_CUENTA;
                itemResult.FormatoMascara=dtos.FORMATO_MASCARA;
                itemResult.DenominacionFuncionalId=dtos.DENOMINACION_FUNCIONAL_ID;
                itemResult.DescripcionDenominacionFuncional="";
                var descriptivaDenominacionFuncional = await _sisDescriptivaRepository.GetById( itemResult.DenominacionFuncionalId);
                if (descriptivaDenominacionFuncional!=null)
                {
                    itemResult.DescripcionDenominacionFuncional = descriptivaDenominacionFuncional.DESCRIPCION;
                }
                itemResult.Codigo=dtos.CODIGO;
                if (dtos.PRINCIPAL==1)
                {
                    itemResult.Principal=true;
                }
                else
                {
                    itemResult.Principal=false;
                }
                if (dtos.RECAUDADORA==1)
                {
                    itemResult.Recaudadora=true;
                }
                else
                {
                    itemResult.Recaudadora=false;
                }
                itemResult.CodigoMayor=dtos.CODIGO_MAYOR;
                itemResult.CodigoAuxiliar=dtos.CODIGO_AUXILIAR;
                itemResult.SearchText=dtos.SEARCH_TEXT;

      
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<SisCuentaBancoResponseDto>> MapListCuentaBancoDto(List<SIS_CUENTAS_BANCOS> dtos)
        {
            List<SisCuentaBancoResponseDto> result = new List<SisCuentaBancoResponseDto>();
            if (dtos.Count > 0)
            {
                foreach (var item in dtos)
                {
                    if (item == null)
                    {
                        var detener = "";
                    }
                    else
                    {
                        var itemResult =  await MapSisCuentaBancoDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
      
        public async Task<ResultDto<SisCuentaBancoResponseDto>> Update(SisCuentaBancoUpdateDto dto)
        {

            ResultDto<SisCuentaBancoResponseDto> result = new ResultDto<SisCuentaBancoResponseDto>(null);
            try
            {
                var cuentaBanco = await _repository.GetById(dto.CodigoCuentaBanco);
                if (cuentaBanco == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cuenta Banco no existe";
                    return result;
                }
                var banco = await _bancoRepository.GetById(dto.CodigoBanco);
                if (banco == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Banco no existe";
                    return result;
                }
                
                var descriptivaTipoCuenta = await _sisDescriptivaRepository.GetById(dto.TipoCuentaId);
                if (descriptivaTipoCuenta==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Cuenta no existe";
                    return result;
                }
                var descriptivaDenominacionFuncional = await _sisDescriptivaRepository.GetById( dto.DenominacionFuncionalId);
                if (descriptivaDenominacionFuncional==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Funcional no existe";
                    return result;
                }
                
                if (String.IsNullOrEmpty(dto.NoCuenta) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nro Cuenta Invalido";
                    return result;
                }
                
                var cuentaEsSoloNumero = StringContainNumbers(dto.NoCuenta);
                if (cuentaEsSoloNumero == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nro Cuenta Invalido,Debe contener solo numeros";
                    return result;
                }
            
                cuentaBanco.CODIGO_BANCO = dto.CodigoBanco;
                cuentaBanco.TIPO_CUENTA_ID = dto.TipoCuentaId;
                cuentaBanco.NO_CUENTA = dto.NoCuenta;
                cuentaBanco.FORMATO_MASCARA = dto.FormatoMascara;
                cuentaBanco.DENOMINACION_FUNCIONAL_ID = dto.DenominacionFuncionalId;
                cuentaBanco.CODIGO = dto.Codigo;
                if (dto.Principal == true)
                {
                    cuentaBanco.PRINCIPAL = 1;
                }
                else
                {
                    cuentaBanco.PRINCIPAL = 0;
                }
                if (dto.Recaudadora == true)
                {
                    cuentaBanco.RECAUDADORA = 1;
                }
                else
                {
                    cuentaBanco.RECAUDADORA = 0;
                }

                cuentaBanco.CODIGO_MAYOR=dto.CodigoMayor;
                cuentaBanco.CODIGO_AUXILIAR=dto.CodigoAuxiliar;
    
                banco.FECHA_UPD = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                banco.CODIGO_EMPRESA = conectado.Empresa;
                banco.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(cuentaBanco);
                var resultDto = await  MapSisCuentaBancoDto(cuentaBanco);
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

        public async Task<ResultDto<SisCuentaBancoResponseDto>> Create(SisCuentaBancoUpdateDto dto)
        {

            ResultDto<SisCuentaBancoResponseDto> result = new ResultDto<SisCuentaBancoResponseDto>(null);
            try
            {
               
              

              
                var cuentaBanco = await _repository.GetById(dto.CodigoCuentaBanco);
                if (cuentaBanco != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cuenta Banco no existe";
                    return result;
                }
                var banco = await _bancoRepository.GetById(dto.CodigoBanco);
                if (banco == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Banco no existe";
                    return result;
                }
                
                var descriptivaTipoCuenta = await _sisDescriptivaRepository.GetById(dto.TipoCuentaId);
                if (descriptivaTipoCuenta==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Cuenta no existe";
                    return result;
                }
                var descriptivaDenominacionFuncional = await _sisDescriptivaRepository.GetById( dto.DenominacionFuncionalId);
                if (descriptivaDenominacionFuncional==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Funcional no existe";
                    return result;
                }
                
                if (String.IsNullOrEmpty(dto.NoCuenta) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nro Cuenta Invalido";
                    return result;
                }

                var cuentaEsSoloNumero = StringContainNumbers(dto.NoCuenta);
                if (cuentaEsSoloNumero == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nro Cuenta Invalido,Debe contener solo numeros";
                    return result;
                }
               
                
                SIS_CUENTAS_BANCOS entity = new SIS_CUENTAS_BANCOS();
                entity.CODIGO_CUENTA_BANCO = await _repository.GetNextKey();
                entity.CODIGO_BANCO = dto.CodigoBanco;
                entity.TIPO_CUENTA_ID = dto.TipoCuentaId;
                entity.NO_CUENTA = dto.NoCuenta;
                entity.FORMATO_MASCARA = dto.FormatoMascara;
                entity.DENOMINACION_FUNCIONAL_ID = dto.DenominacionFuncionalId;
                entity.CODIGO = dto.Codigo;
                if (dto.Principal == true)
                {
                    entity.PRINCIPAL = 1;
                }
                else
                {
                    entity.PRINCIPAL = 0;
                }
                if (dto.Recaudadora == true)
                {
                    entity.RECAUDADORA = 1;
                }
                else
                {
                    entity.RECAUDADORA = 0;
                }

                entity.CODIGO_MAYOR=dto.CodigoMayor;
                entity.CODIGO_AUXILIAR=dto.CodigoAuxiliar;
                
           
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapSisCuentaBancoDto(created.Data);
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
 
        public async Task<ResultDto<SisCuentaBancoDeleteDto>> Delete(SisCuentaBancoDeleteDto dto)
        {

            ResultDto<SisCuentaBancoDeleteDto> result = new ResultDto<SisCuentaBancoDeleteDto>(null);
            try
            {

                var cuenta = await _repository.GetById(dto.CodigoCuentaBanco);
                if (cuenta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cuenta Banco No existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoCuentaBanco);

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

     

     
        public async Task<ResultDto<List<SisCuentaBancoResponseDto>>> GetAll(SisCuentaBancoFilterDto filter)
        {
            ResultDto<List<SisCuentaBancoResponseDto>> result = new ResultDto<List<SisCuentaBancoResponseDto>>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                filter.CodigEmpresa = conectado.Empresa;
                var cuentas = await _repository.GetAll(filter);
                if (cuentas.Data == null)
                {
                    result.Data = null;
                    result.CantidadRegistros=0;
                    result.Page = 0;
                    result.TotalPage = 0;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapListCuentaBancoDto(cuentas.Data);
                result.Data = resultDto;
                result.CantidadRegistros=cuentas.CantidadRegistros;
                result.Page = cuentas.Page;
                result.TotalPage = cuentas.TotalPage;
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
           
        }

        public bool StringContainNumbers(string str)
        {
          

            bool esNumero = true;
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    esNumero = false;
                    break;
                }
            }

            if (esNumero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

