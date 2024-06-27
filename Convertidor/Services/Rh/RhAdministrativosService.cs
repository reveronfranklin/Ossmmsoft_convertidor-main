using System.Globalization;
using Convertidor.Utility;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Rh
{
	public class RhAdministrativosService: IRhAdministrativosService
    {
        
   
        private readonly IRhAdministrativosRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhHistoricoMovimientoRepository _rhHistoricoMovimientoRepository;


        public RhAdministrativosService(IRhAdministrativosRepository repository, 
                                        IRhDescriptivasService descriptivaService, 
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IRhHistoricoMovimientoRepository rhHistoricoMovimientoRepository)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhHistoricoMovimientoRepository = rhHistoricoMovimientoRepository;
        }
       
        public async Task<List<RhAdministrativosResponseDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                
                var administrativos = await _repository.GetByCodigoPersona(codigoPersona);

                    var result = await MapListAdministrativosDto(administrativos);


                return (List<RhAdministrativosResponseDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public async Task<RH_ADMINISTRATIVOS> GetPrimerMovimientoByCodigoPersona(int codigoPersona)
        {
            try
            {
                var result = await _repository.GetPrimerMovimientoCodigoPersona(codigoPersona);
               


                return (RH_ADMINISTRATIVOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


       
       
        public async  Task<RhAdministrativosResponseDto> MapAdministrativosDto(RH_ADMINISTRATIVOS dtos)
        {
 

                RhAdministrativosResponseDto itemResult = new RhAdministrativosResponseDto();
                itemResult.CodigoAdministrativo = dtos.CODIGO_ADMINISTRATIVO;
                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.FechaIngreso = Fecha.GetFechaString(dtos.FECHA_INGRESO);          
                FechaDto FechaIngresoObj = Fecha.GetFechaDto(dtos.FECHA_INGRESO);
                itemResult.FechaIngresoObj = (FechaDto)FechaIngresoObj;
                
                itemResult.TipoPago = dtos.TIPO_PAGO;
                itemResult.BancoId = dtos.BANCO_ID;
                itemResult.DescripcionBanco = await _descriptivaService.GetDescripcionByCodigoDescriptiva(dtos.BANCO_ID);
                itemResult.TipoCuentaId = dtos.TIPO_CUENTA_ID;
                itemResult.DescripcionCuenta = await _descriptivaService.GetDescripcionByCodigoDescriptiva(dtos.TIPO_CUENTA_ID);
                if (dtos.NO_CUENTA is null) dtos.NO_CUENTA = "";
                itemResult.NoCuenta = dtos.NO_CUENTA;
             
          
            return itemResult;



        }

        public async  Task<List<RhAdministrativosResponseDto>> MapListAdministrativosDto(List<RH_ADMINISTRATIVOS> dtos)
        {
            List<RhAdministrativosResponseDto> result = new List<RhAdministrativosResponseDto>();
           
            
            foreach (var item in dtos)
            {

                RhAdministrativosResponseDto itemResult = new RhAdministrativosResponseDto();

                itemResult = await MapAdministrativosDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
        public async Task<ResultDto<RhAdministrativosResponseDto>> Update(RhAdministrativosUpdate dto)
        {

            ResultDto<RhAdministrativosResponseDto> result = new ResultDto<RhAdministrativosResponseDto>(null);
            try
            {

                var administrativo = await _repository.GetByCodigo(dto.CodigoAdministrativo);
                if (administrativo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Administrativo no existe";
                    return result;
                }
                if (dto.NoCuenta.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cuenta Invalida";
                    return result;
                }

                
                var bancos = await _descriptivaService.GetByTitulo(18);
                if (bancos.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Banco  Invalido";
                    return result;
                }
                else
                {
                    var banco = bancos.Where(x => x.Id == dto.BancoId);
                    if (banco is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Banco  Invalido";
                        return result;
                    }
                }
                var tipoCuentas = await _descriptivaService.GetByTitulo(19);
                if (tipoCuentas.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Cuenta Invalida";
                    return result;
                }
                else
                {
                    var tipoCuenta = tipoCuentas.Where(x => x.Id == dto.BancoId);
                    if (tipoCuenta is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo de Cuenta Invalida";
                        return result;
                    }
                }    
                if (dto.NoCuenta.Length!=20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero de Cuenta Invalida(Min 20 digitos)";
                    return result;
                }

                administrativo.BANCO_ID= dto.BancoId;
                administrativo.TIPO_PAGO = dto.TipoPago;
                administrativo.TIPO_CUENTA_ID = dto.TipoCuentaId;
                administrativo.NO_CUENTA = dto.NoCuenta;
                
                //VALIDAMOS SI TIENE MOVIMIENTO HISTORICO
                //PARA PERMITIR MODIFICAR LA FECHA DE INGRESO
                var historicoNomina = await _rhHistoricoMovimientoRepository.GetPrimerMovimientoByCodigoPersona(administrativo.CODIGO_PERSONA);
                if (historicoNomina == null)
                {
                    var fechaIngreso = Convert.ToDateTime(dto.FechaIngreso, CultureInfo.InvariantCulture);
                    administrativo.FECHA_INGRESO = fechaIngreso;
                }
                else
                {
                    
                    var fechaIngreso = Convert.ToDateTime(dto.FechaIngreso, CultureInfo.InvariantCulture);
                    if (fechaIngreso < historicoNomina.FECHA_NOMINA_MOV)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = $"Fecha de Ingreso no puede ser Menor a la fecha de su primera nomina:{historicoNomina.FECHA_NOMINA_MOV.ToString()}";
                        return result;
                    }
                    administrativo.FECHA_INGRESO = fechaIngreso;
                    
                }
                
                var conectado = await _sisUsuarioRepository.GetConectado();
                administrativo.CODIGO_EMPRESA = conectado.Empresa;
                administrativo.USUARIO_UPD = conectado.Usuario;
                administrativo.FECHA_UPD = DateTime.Now;
                
            


                await _repository.Update(administrativo);

        
                
                var resultDto = await MapAdministrativosDto(administrativo);
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

        public async Task<ResultDto<RhAdministrativosResponseDto>> Create(RhAdministrativosUpdate dto)
        {

            ResultDto<RhAdministrativosResponseDto> result = new ResultDto<RhAdministrativosResponseDto>(null);
            try
            {
                if (dto.NoCuenta.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cuenta Invalida";
                    return result;
                }

                
                var bancos = await _descriptivaService.GetByTitulo(18);
                if (bancos.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Banco  Invalido";
                    return result;
                }
                else
                {
                    var banco = bancos.Where(x => x.Id == dto.BancoId);
                    if (banco is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Banco  Invalido";
                        return result;
                    }
                }
                var tipoCuentas = await _descriptivaService.GetByTitulo(19);
                if (tipoCuentas.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Cuenta Invalida";
                    return result;
                }
                else
                {
                    var tipoCuenta = tipoCuentas.Where(x => x.Id == dto.BancoId);
                    if (tipoCuenta is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo de Cuenta Invalida";
                        return result;
                    }
                }    
                if (dto.NoCuenta.Length!=20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero de Cuenta Invalida(Min 20 digitos)";
                    return result;
                }

                RH_ADMINISTRATIVOS entity = new RH_ADMINISTRATIVOS();
                entity.CODIGO_ADMINISTRATIVO = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.BANCO_ID= dto.BancoId;
                entity.TIPO_PAGO = dto.TipoPago;
                entity.TIPO_CUENTA_ID = dto.TipoCuentaId;
                entity.NO_CUENTA = dto.NoCuenta;
                var fechaIngreso = Convert.ToDateTime(dto.FechaIngreso, CultureInfo.InvariantCulture);
                entity.FECHA_INGRESO = fechaIngreso;
                
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_UPD = null;
                entity.USUARIO_UPD = 0;
                entity.CODIGO_EMPRESA = conectado.Empresa;
                


                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapAdministrativosDto(created.Data);
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
 
        public async Task<ResultDto<RhAdministrativosDeleteDto>> Delete(RhAdministrativosDeleteDto dto)
        {

            ResultDto<RhAdministrativosDeleteDto> result = new ResultDto<RhAdministrativosDeleteDto>(null);
            try
            {

                var administrativo = await _repository.GetByCodigo(dto.CodigoAdministrativo);
                if (administrativo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Administrativo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoAdministrativo);

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

