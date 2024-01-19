using AutoMapper;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhPeriodoService: IRhPeriodoService
    {

   
        private readonly IRhPeriodoRepository _repository;
        private readonly IRhTipoNominaRepository _rhTipoNominaRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IMapper _mapper;

        public RhPeriodoService(IRhPeriodoRepository repository,IRhTipoNominaRepository rhTipoNominaRepository,ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _rhTipoNominaRepository = rhTipoNominaRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        public async Task<List<RH_PERIODOS>> GetAll(PeriodoFilterDto filter)
        {
            try
            {
                var result = await _repository.GetAll(filter);
                return (List<RH_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

       

        public async Task<List<RH_PERIODOS>> GetByTipoNomina(int tipoNomina)
           {
            try
            {

                var result = await _repository.GetByTipoNomina(tipoNomina);
                return (List<RH_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RhPeriodosResponseDto>> GetByYear(int ano)
        {
            try
            {

                var fecha = await _repository.GetByYear(ano);
                var resultDto = await MapListPeriodoDto(fecha);
                return resultDto;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

       

        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);

            return FechaDesdeObj;
        }

        public async Task<RhPeriodosResponseDto> MapPeriodosDto(RH_PERIODOS dtos)
        {


            RhPeriodosResponseDto itemResult = new RhPeriodosResponseDto();
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.CodigoTipoNomina = dtos.CODIGO_TIPO_NOMINA;
            itemResult.FechaNomina = dtos.FECHA_NOMINA;
            itemResult.Periodo = dtos.PERIODO;
            itemResult.TipoNomina = dtos.TIPO_NOMINA;
            itemResult.EXTRA1 = dtos.EXTRA1;
            itemResult.EXTRA2 = dtos.EXTRA2;
            itemResult.EXTRA3 = dtos.EXTRA3;
            itemResult.UsuarioPreCierre = dtos.USUARIO_PRECIERRE;
            itemResult.FechaPreCierre = dtos.FECHA_PRECIERRE;
            itemResult.UsuarioCierre = dtos.USUARIO_CIERRE;
            itemResult.FechaCierre = dtos.FECHA_CIERRE;
            itemResult.CodigoCuentaEmpresa = dtos.CODIGO_CUENTA_EMPRESA;
            itemResult.UsuarioPreNomina = dtos.USUARIO_PRENOMINA;
            itemResult.FechaPrenomina = dtos.FECHA_PRENOMINA;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.Descripcion = dtos.DESCRIPCION;




            return itemResult;



        }
       
        public async Task<List<RhPeriodosResponseDto>> MapListPeriodoDto(List<RH_PERIODOS> dtos)
        {
            List<RhPeriodosResponseDto> result = new List<RhPeriodosResponseDto>();

            foreach (var item in dtos)
            {

                RhPeriodosResponseDto itemResult = new RhPeriodosResponseDto();
                itemResult = await MapPeriodosDto(item);
                
                result.Add(itemResult);


            }
            return result;



        }

        public List<ListTipoNomina> GetListTipoNomina()
        {
            List<ListTipoNomina> result = new List<ListTipoNomina>();
            ListTipoNomina normal = new ListTipoNomina();
            normal.Codigo = "N";
            normal.Decripcion = "Normal";
            ListTipoNomina especial = new ListTipoNomina();
            especial.Codigo = "E";
            especial.Decripcion = "Especial";
            result.Add(normal);
            result.Add(especial);
          
            return result;
        }
        public List<ListPeriodo> GetListPeriodo()
        {
            List<ListPeriodo> result = new List<ListPeriodo>();
            ListPeriodo normal = new ListPeriodo();
            normal.Codigo = 1;
            normal.Decripcion = "1ra. Quincena";
            ListPeriodo especial = new ListPeriodo();
            especial.Codigo = 2;
            especial.Decripcion = "2da. Quincena";
            result.Add(normal);
            result.Add(especial);
          
            return result;
        }

        public async Task<ResultDto<RhPeriodosResponseDto>> Update(RhPeriodosUpdate dto)
        {

            ResultDto<RhPeriodosResponseDto> result = new ResultDto<RhPeriodosResponseDto>(null);
            try
            {

                var periodos = await _repository.GetByCodigo(dto.CodigoPeriodo);
                if (periodos == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo no existe";
                    return result;
                }


                var codigoTipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.CodigoTipoNomina);
                if (codigoTipoNomina==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tipo Nomina  Invalido";
                    return result;
                }
               
                FechaDto fechanomina = GetFechaDto(dto.FechaNomina);
                if (fechanomina==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Invalida";
                    return result;
                }

                var periodo = GetListPeriodo().Where(x => x.Codigo == dto.Periodo).FirstOrDefault();
                
               if (periodo==null)
                  {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo Invalido";
                    return result;
                  }

               var tipoNomina = GetListTipoNomina().Where(x => x.Codigo == dto.TipoNomina).FirstOrDefault();
                if (tipoNomina==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Nomina Invalido";
                    return result;
                }
                
               
                periodos.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                periodos.FECHA_NOMINA = dto.FechaNomina;
                periodos.PERIODO = dto.Periodo;
                periodos.TIPO_NOMINA = dto.TipoNomina;
                periodos.CODIGO_CUENTA_EMPRESA = dto.CodigoCuentaEmpresa;
                periodos.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                periodos.DESCRIPCION = dto.Descripcion;


                var conectado = await _sisUsuarioRepository.GetConectado();
                periodos.CODIGO_EMPRESA = conectado.Empresa;
                periodos.USUARIO_UPD = conectado.Usuario;
                periodos.FECHA_UPD = DateTime.Now;


                await _repository.Update(periodos);


               

                var resultDto = await MapPeriodosDto(periodos);
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


        public async Task<ResultDto<RhPeriodosResponseDto>> UpdateDatosCierre(RhPeriodosUsuarioFechaUpdate dto)
        {

            ResultDto<RhPeriodosResponseDto> result = new ResultDto<RhPeriodosResponseDto>(null);
            try
            {

                var periodos = await _repository.GetByCodigo(dto.CodigoPeriodo);
                if (periodos == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo no existe";
                    return result;
                }



                FechaDto fechaCierre = GetFechaDto(dto.Fecha);
                if (fechaCierre==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Cierre Invalida";
                    return result;
                }
                periodos.USUARIO_CIERRE = dto.Usuario;
                periodos.FECHA_CIERRE = dto.Fecha;
      



                var conectado = await _sisUsuarioRepository.GetConectado();
                periodos.CODIGO_EMPRESA = conectado.Empresa;
                periodos.USUARIO_UPD = conectado.Usuario;
                periodos.FECHA_UPD = DateTime.Now;


                await _repository.Update(periodos);


               

                var resultDto = await MapPeriodosDto(periodos);
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

        public async Task<ResultDto<RhPeriodosResponseDto>> UpdateDatosPreCierre(RhPeriodosUsuarioFechaUpdate dto)
        {

            ResultDto<RhPeriodosResponseDto> result = new ResultDto<RhPeriodosResponseDto>(null);
            try
            {

                var periodos = await _repository.GetByCodigo(dto.CodigoPeriodo);
                if (periodos == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo no existe";
                    return result;
                }


                FechaDto fechaPreCierre = GetFechaDto(dto.Fecha);
                if (fechaPreCierre==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Cierre Invalida";
                    return result;
                }
                periodos.USUARIO_PRECIERRE = dto.Usuario;
                periodos.FECHA_PRECIERRE=dto.Fecha;
             
              
             

                var conectado = await _sisUsuarioRepository.GetConectado();
                periodos.CODIGO_EMPRESA = conectado.Empresa;
                periodos.USUARIO_UPD = conectado.Usuario;
                periodos.FECHA_UPD = DateTime.Now;


                await _repository.Update(periodos);


               

                var resultDto = await MapPeriodosDto(periodos);
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

         public async Task<ResultDto<RhPeriodosResponseDto>> UpdateDatosPrenomina(RhPeriodosUsuarioFechaUpdate dto)
        {

            ResultDto<RhPeriodosResponseDto> result = new ResultDto<RhPeriodosResponseDto>(null);
            try
            {

                var periodos = await _repository.GetByCodigo(dto.CodigoPeriodo);
                if (periodos == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo no existe";
                    return result;
                }


                
               

                FechaDto fechaPreNomina = GetFechaDto(dto.Fecha);
                if (fechaPreNomina==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha PreNomina Invalida";
                    return result;
                }
                periodos.USUARIO_PRENOMINA = dto.Usuario;
                periodos.FECHA_PRENOMINA = dto.Fecha;

                var conectado = await _sisUsuarioRepository.GetConectado();
                periodos.CODIGO_EMPRESA = conectado.Empresa;
                periodos.USUARIO_UPD = conectado.Usuario;
                periodos.FECHA_UPD = DateTime.Now;


                await _repository.Update(periodos);


               

                var resultDto = await MapPeriodosDto(periodos);
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
        public async Task<ResultDto<RhPeriodosResponseDto>> Create(RhPeriodosUpdate dto)
        {

            ResultDto<RhPeriodosResponseDto> result = new ResultDto<RhPeriodosResponseDto>(null);
            try
            {


                var codigoTipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.CodigoTipoNomina);
                if (codigoTipoNomina==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tipo Nomina  Invalido";
                    return result;
                }
               
                FechaDto fechanomina = GetFechaDto(dto.FechaNomina);
                if (fechanomina==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Invalida";
                    return result;
                }

                var periodo = GetListPeriodo().Where(x => x.Codigo == dto.Periodo).FirstOrDefault();
                
               if (periodo==null)
                  {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo Invalido";
                    return result;
                  }

               var tipoNomina = GetListTipoNomina().Where(x => x.Codigo == dto.TipoNomina).FirstOrDefault();
                if (tipoNomina==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Nomina Invalido";
                    return result;
                }
                
                RH_PERIODOS entity = new RH_PERIODOS();
                
                entity.CODIGO_PERIODO = await _repository.GetNextKey();
                entity.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                entity.FECHA_NOMINA = dto.FechaNomina;
                entity.PERIODO = dto.Periodo;
                entity.TIPO_NOMINA = dto.TipoNomina;
                entity.EXTRA1 = dto.EXTRA1;
                entity.EXTRA2 = dto.EXTRA2;
                entity.EXTRA3 = dto.EXTRA3;
                entity.CODIGO_CUENTA_EMPRESA = dto.CodigoCuentaEmpresa;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.DESCRIPCION = dto.Descripcion;


                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_UPD = conectado.Usuario;
                entity.FECHA_UPD = DateTime.Now;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPeriodosDto(created.Data);
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

        public async Task<ResultDto<RhPeriodosDeleteDto>> Delete(RhPeriodosDeleteDto dto)
        {

            ResultDto<RhPeriodosDeleteDto> result = new ResultDto<RhPeriodosDeleteDto>(null);
            try
            {

                var periodo = await _repository.GetByCodigo(dto.CodigoPeriodo);
                if (periodo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPeriodo);

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
        public async Task<RhPeriodosResponseDto> GetPeriodoAbierto(RhTiposNominaFilterDto dto)
        {
            try
            {

                var periodo = await _repository.GetPeriodoAbierto(dto.CodigoTipoNomina, dto.TipoNomina);
                var resulDto = await MapPeriodosDto(periodo);
                return resulDto;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
    }


}

