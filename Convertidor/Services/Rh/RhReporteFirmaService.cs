using System.Globalization;

namespace Convertidor.Data.Repository.Rh
{
	public class RhReporteFirmaService: IRhReporteFirmaService
    {
        
   
        private readonly IRhReporteFirmaRepository _repository;
        private readonly IRhTipoNominaService _rhTipoNominaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
   

        public RhReporteFirmaService(IRhReporteFirmaRepository repository, 
                                        IRhTipoNominaService rhTipoNominaService, 
                                        ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _rhTipoNominaService = rhTipoNominaService;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        public async Task<ResultDto<List<RhReporteFirmaResponseDto>> > GetAll()
        {
            try
            {
                ResultDto<List<RhReporteFirmaResponseDto>> result = new  ResultDto<List<RhReporteFirmaResponseDto>> (null);
                var data = await _repository.GetAll();
                if (data.Count > 0)
                {
                    result.Data = await MapListFirma(data);
                    result.Message = "";
                    result.IsValid = true;
                    
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = "No Data";
                    return result;
                }
            

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public string GetNombreOficina(string oficina)
        {
            var result = "";
            if (oficina == "1")
            {
                result = "Gerencia de Beneficios y Gestión Humana";
            }
            else
            {
                result = "Gerencia de Nómina, Compensación Laboral y Control Presupuestario";

            }

            return result;
        }
        
        public async  Task<List<RhReporteFirmaResponseDto>> MapListFirma(List<RH_V_REPORTE_NOMINA_FIRMA> dtos)
        {
                            var lista = from s in dtos
                            group s by new
                            {
                                Oficina = s.OFICINA,
                                Orden=s.ORDEN,
                                CodigoPersona=s.CODIGO_PERSONA,
                                Nombre=s.NOMBRE,
                                Apellido=s.APELLIDO,
                                Cedula=s.CEDULA,
                                DescripcionCargo=s.DESCRIPCION_CARGO,
                                NombreOficina=s.NOMBRE_OFICINA,
                                AccionResponsable=s.ACCION_RESPONSABLE
                                
                            } into g
                            select new RhReporteFirmaResponseDto()
                            {
                                Oficina = g.Key.Oficina,
                                Orden = g.Key.Orden,
                                CodigoPersona = g.Key.CodigoPersona,
                                Nombre = g.Key.Nombre,
                                Apellido = g.Key.Apellido,
                                Cedula = g.Key.Cedula,
                                DescripcionCargo = g.Key.DescripcionCargo,
                                NombreOficina=g.Key.NombreOficina,
                                AccionResponsable=g.Key.AccionResponsable
                                
                                
                            };
                          
                            
           
            return lista.ToList();

        }

   
        
    }
}

