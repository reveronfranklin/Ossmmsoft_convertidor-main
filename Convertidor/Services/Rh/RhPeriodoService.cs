namespace Convertidor.Data.Repository.Rh
{
	public class RhPeriodoService: IRhPeriodoService
    {

   
        private readonly IRhPeriodoRepository _repository;
        private readonly IRhTipoNominaRepository _rhTipoNominaRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      

        public RhPeriodoService(IRhPeriodoRepository repository,IRhTipoNominaRepository rhTipoNominaRepository,ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _rhTipoNominaRepository = rhTipoNominaRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        public async Task<List<RhPeriodosResponseDto>> GetAll(PeriodoFilterDto filter)
        {
            try
            {
                var result = await _repository.GetAll(filter);
                var resultDto = await MapListPeriodoDto(result);
                return (List<RhPeriodosResponseDto>)resultDto;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

       

        public async Task<List<RhPeriodosResponseDto>> GetByTipoNomina(int tipoNomina)
           {
            try
            {

                var result = await _repository.GetByTipoNomina(tipoNomina);
                var resultDto = await MapListPeriodoDto(result);
                return (List<RhPeriodosResponseDto>)resultDto;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<RhPeriodosResponseDto> GetByPeriodo(int codigoPeriodo)
        {
            try
            {

                var result = await _repository.GetByCodigo(codigoPeriodo);
                var resultDto = await MapPeriodosDto(result);
                return (RhPeriodosResponseDto)resultDto;
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

                var data = await _repository.GetByYear(ano);
                var resultDto = await MapListPeriodoDto(data);
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
            try
            {
              
                FechaDesdeObj.Year = fecha.Year.ToString();
                string month = "00" + fecha.Month.ToString();
                string day = "00" + fecha.Day.ToString();
                FechaDesdeObj.Month = month.Substring(month.Length - 2);
                FechaDesdeObj.Day = day.Substring(day.Length - 2);

                return FechaDesdeObj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return FechaDesdeObj;

            }
           
        }
        public string GetFechaString(DateTime? fecha)
        {
            var result = "";
            try
            {
              
                if (fecha != null)
                {
                    result = $"{fecha:MM/dd/yyyy}";
                }
         

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return result;
              
            }
           
          
        }
        
      
        

        public async Task<RhPeriodosResponseDto> MapPeriodosDto(RH_PERIODOS dtos)
        {


            RhPeriodosResponseDto itemResult = new RhPeriodosResponseDto();
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.CodigoTipoNomina = dtos.CODIGO_TIPO_NOMINA;

            itemResult.DescripcionPeriodo = GetPeriodo(dtos.PERIODO);
            itemResult.DescripcionTipoNomina = GetTipoNomina(dtos.TIPO_NOMINA);
            
            itemResult.FechaNomina = dtos.FECHA_NOMINA;
            itemResult.FechaNominaString =$"{dtos.FECHA_NOMINA:MM/dd/yyyy}";
            FechaDto fechaNominaObj = GetFechaDto(dtos.FECHA_NOMINA);
            itemResult.FechaNominaObj = (FechaDto)fechaNominaObj;
            itemResult.Periodo = dtos.PERIODO;
            itemResult.TipoNomina = dtos.TIPO_NOMINA;
            itemResult.EXTRA1 = dtos.EXTRA1;
            itemResult.EXTRA2 = dtos.EXTRA2;
            itemResult.EXTRA3 = dtos.EXTRA3;
            itemResult.UsuarioPreCierre = dtos.USUARIO_PRECIERRE;
           
            itemResult.FechaPreCierre = dtos.FECHA_PRECIERRE;
            itemResult.FechaPreCierreString = "";
            itemResult.FechaPreCierreObj = null;
            if (dtos.FECHA_PRECIERRE != null)
            {
                itemResult.FechaPreCierreString =$"{dtos.FECHA_PRECIERRE:MM/dd/yyyy}";
                FechaDto fechaPrecierreObj = GetFechaDto((DateTime)dtos.FECHA_PRECIERRE);
                itemResult.FechaPreCierreObj = (FechaDto)fechaPrecierreObj;
            }    
           
            
            itemResult.UsuarioCierre = dtos.USUARIO_CIERRE;
            itemResult.FechaCierre = dtos.FECHA_CIERRE;
            itemResult.FechaCierreString = "";
            itemResult.FechaCierreObj = null;
            if (dtos.FECHA_CIERRE!=null)
            {
                itemResult.FechaCierre = dtos.FECHA_CIERRE;
                itemResult.FechaCierreString =$"{dtos.FECHA_CIERRE:MM/dd/yyyy}";
                FechaDto fechaCierreObj = GetFechaDto((DateTime)dtos.FECHA_CIERRE);
                itemResult.FechaCierreObj = (FechaDto)fechaCierreObj;
            }
        
            
            itemResult.CodigoCuentaEmpresa = dtos.CODIGO_CUENTA_EMPRESA;
            itemResult.UsuarioPreNomina = dtos.USUARIO_PRENOMINA;
            itemResult.FechaPrenomina = dtos.FECHA_PRENOMINA;
            itemResult.FechaPrenominaString = "";
            itemResult.FechaPrenominaObj = null;
            if (dtos.FECHA_PRENOMINA != null)
            {
                itemResult.FechaPrenominaString = $"{dtos.FECHA_PRENOMINA:MM/dd/yyyy}";
                FechaDto fechaPrenominaObj = GetFechaDto((DateTime)dtos.FECHA_PRENOMINA);
                itemResult.FechaPrenominaObj = (FechaDto)fechaPrenominaObj;
            }
           
            
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.Descripcion = dtos.DESCRIPCION;




            return itemResult;



        }
       
        public async Task<List<RhPeriodosResponseDto>> MapListPeriodoDto(List<RH_PERIODOS> dtos)
        {
            List<RhPeriodosResponseDto> result = new List<RhPeriodosResponseDto>();

            try
            {


                foreach (var item in dtos)
                {
                    var dto = await MapPeriodosDto(item);
                    result.Add(dto);
                }
                
                
                /*  var data = from s in dtos
                group s by new
                {
                    CodigoPeriodo = s.CODIGO_PERIODO,
                    CodigoTipoNomina = s.CODIGO_TIPO_NOMINA,
                    FechaNomina = s.FECHA_NOMINA,
                    FechaNominaString = $"{s.FECHA_NOMINA:MM/dd/yyyy}",
                    FechaNominaObj = GetFechaDto(s.FECHA_NOMINA),
                    Periodo = s.PERIODO,
                    TipoNomina = s.TIPO_NOMINA,
                    EXTRA1 = s.EXTRA1,
                    EXTRA2 = s.EXTRA2,
                    EXTRA3 = s.EXTRA3,
                    UsuarioPreCierre = s.USUARIO_PRECIERRE,
                    FechaPreCierre = s.FECHA_PRECIERRE,
                    FechaPreCierreObj =  GetFechaDto((DateTime)s.FECHA_PRECIERRE),
                    UsuarioCierre = s.USUARIO_CIERRE,
                    FechaCierre = s.FECHA_CIERRE,
                    FechaCierreObj = GetFechaDto((DateTime)s.FECHA_CIERRE),
                    CodigoCuentaEmpresa = s.CODIGO_CUENTA_EMPRESA,
                    UsuarioPreNomina = s.USUARIO_PRENOMINA,
                    FechaPrenomina = s.FECHA_PRENOMINA,
                    FechaPrenominaObj =  GetFechaDto((DateTime)s.FECHA_PRENOMINA),
                    CodigoPresupuesto = s.CODIGO_PRESUPUESTO,
                    Descripcion = s.DESCRIPCION,
                  
                            
                } into g
                select new RhPeriodosResponseDto
                {
                    CodigoPeriodo=g.Key.CodigoPeriodo,
                    CodigoTipoNomina=g.Key.CodigoTipoNomina,
                    FechaNomina=g.Key.FechaNomina,
                    FechaNominaString=g.Key.FechaNominaString,
                    FechaNominaObj=g.Key.FechaNominaObj,
                    Periodo=g.Key.Periodo,
                    DescripcionPeriodo = GetPeriodo(g.Key.Periodo),
                    TipoNomina=g.Key.TipoNomina,
                    DescripcionTipoNomina = GetTipoNomina(g.Key.TipoNomina),
                    EXTRA1=g.Key.EXTRA1,
                    EXTRA2=g.Key.EXTRA2,
                    EXTRA3=g.Key.EXTRA3, 
                    UsuarioPreCierre=g.Key.UsuarioPreCierre,
                    FechaPreCierre=g.Key.FechaPreCierre,
                
                    FechaPreCierreObj=g.Key.FechaPreCierreObj,
                    UsuarioCierre=g.Key.UsuarioCierre,
                    FechaCierre=g.Key.FechaCierre,
                  
                    FechaCierreObj=g.Key.FechaCierreObj,
                    CodigoCuentaEmpresa=g.Key.CodigoCuentaEmpresa,
                    UsuarioPreNomina=g.Key.UsuarioPreNomina,
                    FechaPrenomina=g.Key.FechaPrenomina,
                 
                    FechaPrenominaObj=g.Key.FechaPrenominaObj,
                    CodigoPresupuesto=g.Key.CodigoPresupuesto,
                    Descripcion=g.Key.Descripcion,
                            
                };*/
            //     FechaPreCierreString = GetFechaString( s.FECHA_PRECIERRE ?? default(DateTime)),

         
            return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return result;
            }
            
        



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
        
        public string GetPeriodo(int periodo)
        {
            string result = "";
            var periodoObj = GetListPeriodo().Where(x => x.Codigo == periodo).First();
            result = periodoObj.Decripcion;
          
            return result;
        }
        public string GetTipoNomina(string tipoNomina)
        {
            string result = "";
            var tipoNominaObj = GetListTipoNomina().Where(x => x.Codigo == tipoNomina).First();
            result = tipoNominaObj.Decripcion;
          
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
                if (periodos.FECHA_CIERRE != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo CERRADO no puede ser modificado";
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
            
                if (string.IsNullOrEmpty(dto.Descripcion))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

                periodos.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                periodos.FECHA_NOMINA = dto.FechaNomina;
                periodos.PERIODO = dto.Periodo;
                periodos.TIPO_NOMINA = dto.TipoNomina;
                periodos.DESCRIPCION = dto.Descripcion;


                var conectado = await _sisUsuarioRepository.GetConectado();
                periodos.CODIGO_EMPRESA = conectado.Empresa;
                periodos.USUARIO_UPD = conectado.Usuario;
                periodos.FECHA_UPD = DateTime.Now;


                var updated=await _repository.Update(periodos);

                if (!updated.IsValid)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = updated.Message;
                }
               

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
          
                if (string.IsNullOrEmpty(dto.Descripcion))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }
                
                RH_PERIODOS entity = new RH_PERIODOS();
                
                entity.CODIGO_PERIODO = await _repository.GetNextKey();
                entity.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                entity.FECHA_NOMINA = dto.FechaNomina;
                entity.PERIODO = dto.Periodo;
                entity.TIPO_NOMINA = dto.TipoNomina;
                entity.DESCRIPCION = dto.Descripcion;


                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;


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
                if (periodo.FECHA_CIERRE != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo CERRADO no puede ser modificado";
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

