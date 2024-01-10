using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;

namespace Convertidor.Data.Repository.Rh
{
	public class RhAriService : IRhAriService
    {
        private readonly IRhAriRepository _repository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public RhAriService(IRhAriRepository repository, 
                                        IRhPersonasRepository rhPersonasRepository, 
                                        ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _rhPersonasRepository = rhPersonasRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        public async Task<List<RhAriResponseDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                
                var ari = await _repository.GetByCodigoPersona(codigoPersona);

                var result = await MapListAriDto(ari);


                return (List<RhAriResponseDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        public async Task<RhAriResponseDto> MapAriDto(RH_ARI dtos)
        {


            RhAriResponseDto itemResult = new RhAriResponseDto();
            itemResult.CodigoAri = dtos.CODIGO_ARI;
            itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
            itemResult.FechaAri = dtos.FECHA_ARI;
            itemResult.Mes = dtos.MES;
            itemResult.Ano = dtos.ANO;
            itemResult.EmpresaA = dtos.EMPRESA_A;
            itemResult.EmpresaB = dtos.EMPRESA_B;
            itemResult.EmpresaC = dtos.EMPRESA_C;
            itemResult.EmpresaD = dtos.EMPRESA_D;
            itemResult.AaBs = dtos.A_A_BS;
            itemResult.AbBs = dtos.A_B_BS;
            itemResult.AcBs = dtos.A_C_BS;
            itemResult.AdBs = dtos.A_D_BS;
            itemResult.C1Bs = dtos.C_1_BS;
            itemResult.C2Bs = dtos.C_2_BS;
            itemResult.C3Bs = dtos.C_3_BS;
            itemResult.C4Bs = dtos.C_4_BS;
            itemResult.EuT = dtos.E_UT;
            itemResult.H1Ut = dtos.H_1_UT;
            itemResult.CargaFamiliar = dtos.CARGA_FAMILIAR;
            itemResult.H3Bs = dtos.H_3_BS;
            itemResult.K1Bs = dtos.K_1_BS;
            itemResult.K2Bs = dtos.K_2_BS;
            
          

            return itemResult;



        }

        public async  Task<List<RhAriResponseDto>> MapListAriDto(List<RH_ARI> dtos)
        {
            List<RhAriResponseDto> result = new List<RhAriResponseDto>();
           
            
            foreach (var item in dtos)
            {

                RhAriResponseDto itemResult = new RhAriResponseDto();

                itemResult = await MapAriDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
        public async Task<ResultDto<RhAriResponseDto>> Create(RhAriUpdateDto dto)
        {

            ResultDto<RhAriResponseDto> result = new ResultDto<RhAriResponseDto>(null);
            try
            {



                var persona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (persona is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo persona invalido";
                    return result;
                }

               

                if (dto.FechaAri == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha ari Invalida";
                    return result;
                }
            

            var mes = dto.Mes;
            if (mes < 1)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Mes ari Invalido";
                return result;
            }

            else if (mes > 12)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Mes ari Invalido";
                return result;
            }

            if (dto.Ano == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Ano ari Invalido";
                return result;
            }
            if (dto.Ut == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Unidad tributaria Invalida";
                return result;
            }

            if (dto.EmpresaA == string.Empty)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Empresa A Invalida";
                return result;
            }
            if (dto.EmpresaB == string.Empty)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Empresa B Invalida";
                return result;
            }
            if (dto.EmpresaC == string.Empty)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Empresa C Invalida";
                return result;
            }
           
            if (dto.AaBs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "AaBs Invalido";
                return result;
            }
            if (dto.AbBs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "AbBs Invalido";
                return result;
            }
            if (dto.AcBs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "AcBs Invalido";
                return result;
            }
            if (dto.AdBs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "AdBs Invalido";
                return result;
            }
            if (dto.C1Bs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "C1Bs Invalido";
                return result;
            }
            if (dto.C2Bs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "C2Bs Invalido";
                return result;
            }
            if (dto.C3Bs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "C3Bs Invalido";
                return result;
            }
            if (dto.C4Bs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "C4Bs Invalido";
                return result;
            }

            if (dto.EuT == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "EuT Invalido";
                return result;
            }
            if (dto.H1Ut == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "H1Ut Invalido";
                return result;
            }
            if (dto.CargaFamiliar == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CargaFamiliar Invalida";
                return result;
            }
            if (dto.H3Bs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "H3Bs Invalida";
                return result;
            }
            if (dto.K1Bs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "k1Bs Invalida";
                return result;
            }
            if (dto.K2Bs == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "k2Bs Invalida";
                return result;
            }
            if (dto.JpOr == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "JpOr Invalida";
                return result;
            }
            if (dto.KpOr == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "KpOr Invalida";
                return result;
            }


                RH_ARI entity = new RH_ARI();
                entity.CODIGO_ARI = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.FECHA_ARI = dto.FechaAri;
                entity.MES = dto.Mes;
                entity.ANO = dto.Ano;
                entity.UT = dto.Ut;
                entity.EMPRESA_A = dto.EmpresaA;
                entity.EMPRESA_B = dto.EmpresaB;
                entity.EMPRESA_C = dto.EmpresaC;
                entity.EMPRESA_D = dto.EmpresaD;
                entity.A_A_BS = dto.AaBs;
                entity.A_B_BS = dto.AbBs;
                entity.A_C_BS = dto.AcBs;
                entity.A_D_BS = dto.AdBs;
                entity.C_1_BS = dto.AdBs;
                entity.C_2_BS = dto.AdBs;
                entity.C_3_BS = dto.AdBs;
                entity.C_4_BS = dto.AdBs;
                entity.E_UT = dto.EuT;
                entity.H_1_UT = dto.H1Ut;
                entity.CARGA_FAMILIAR = dto.CargaFamiliar;
                entity.H_3_BS = dto.H3Bs;
                entity.K_1_BS = dto.K1Bs;
                entity.K_2_BS = dto.K2Bs;
                entity.J_POR = dto.JpOr;
                entity.K_POR = dto.KpOr;

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_UPD = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapAriDto(created.Data);
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

        public async Task<ResultDto<RhAriResponseDto>> Update(RhAriUpdateDto dto)
        {

            ResultDto<RhAriResponseDto> result = new ResultDto<RhAriResponseDto>(null);
            try
            {

                var personaAri = await _repository.GetByCodigo(dto.CodigoAri);
                if (personaAri is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ari invalido";
                    return result;
                }

                else
                {
                    var persona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                    if (persona is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Codigo persona invalido";
                        return result;
                    }

                    

                    if (dto.FechaAri==null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Fecha ari Invalida";
                        return result;
                    }
                }

                var mes = dto.Mes;
                if (mes < 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Mes ari Invalido";
                    return result;
                }

                else if (mes > 12)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Mes ari Invalido";
                    return result;
                }

                if (dto.Ano == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano ari Invalido";
                    return result;
                }
                if (dto.Ut == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad tributaria Invalida";
                    return result;
                }

                if (dto.EmpresaA == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Empresa A Invalida";
                    return result;
                }
                if (dto.EmpresaB == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Empresa B Invalida";
                    return result;
                }
                if (dto.EmpresaC == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Empresa C Invalida";
                    return result;
                }
                
                if (dto.AaBs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "AaBs Invalido";
                    return result;
                }
                if (dto.AbBs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "AbBs Invalido";
                    return result;
                }
                if (dto.AcBs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "AcBs Invalido";
                    return result;
                }
                if (dto.AdBs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "AdBs Invalido";
                    return result;
                }
                if (dto.C1Bs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "C1Bs Invalido";
                    return result;
                }
                if (dto.C2Bs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "C2Bs Invalido";
                    return result;
                }
                if (dto.C3Bs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "C3Bs Invalido";
                    return result;
                }
                if (dto.C4Bs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "C4Bs Invalido";
                    return result;
                }

                if (dto.EuT == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "EuT Invalido";
                    return result;
                }
                if (dto.H1Ut == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "H1Ut Invalido";
                    return result;
                }
                if (dto.CargaFamiliar == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "CargaFamiliar Invalida";
                    return result;
                }
                if (dto.H3Bs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "H3Bs Invalida";
                    return result;
                }
                if (dto.K1Bs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "k1Bs Invalida";
                    return result;
                }
                if (dto.K2Bs == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "k2Bs Invalida";
                    return result;
                }
                if (dto.JpOr == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "JpOr Invalida";
                    return result;
                }
                if (dto.KpOr == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "KpOr Invalida";
                    return result;
                }

                personaAri.CODIGO_ARI = dto.CodigoAri;
                personaAri.CODIGO_PERSONA = dto.CodigoPersona;
                personaAri.FECHA_ARI = dto.FechaAri;
                personaAri.MES = dto.Mes;
                personaAri.ANO = dto.Ano;
                personaAri.UT = dto.Ut;
                personaAri.EMPRESA_A = dto.EmpresaA;
                personaAri.EMPRESA_B = dto.EmpresaB;
                personaAri.EMPRESA_C = dto.EmpresaC;
                personaAri.EMPRESA_D = dto.EmpresaD;
                personaAri.A_A_BS = dto.AaBs;
                personaAri.A_B_BS = dto.AbBs;
                personaAri.A_C_BS = dto.AcBs;
                personaAri.A_D_BS = dto.AdBs;
                personaAri.C_1_BS = dto.AdBs;
                personaAri.C_2_BS = dto.AdBs;
                personaAri.C_3_BS = dto.AdBs;
                personaAri.C_4_BS = dto.AdBs;
                personaAri.E_UT = dto.EuT;
                personaAri.H_1_UT = dto.H1Ut;
                personaAri.CARGA_FAMILIAR = dto.CargaFamiliar;
                personaAri.H_3_BS = dto.H3Bs;
                personaAri.K_1_BS = dto.K1Bs;
                personaAri.K_2_BS = dto.K2Bs;
                personaAri.J_POR = dto.JpOr;
                personaAri.K_POR = dto.KpOr;


                var conectado = await _sisUsuarioRepository.GetConectado();
                personaAri.CODIGO_EMPRESA = conectado.Empresa;
                personaAri.USUARIO_UPD = conectado.Usuario;
                personaAri.FECHA_UPD = DateTime.Now;

                await _repository.Update(personaAri);



                var resultDto = await MapAriDto(personaAri);
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


        public async Task<ResultDto<RhAriDeleteDto>> Delete(RhAriDeleteDto dto)
        {

            ResultDto<RhAriDeleteDto> result = new ResultDto<RhAriDeleteDto>(null);
            try
            {

                var personaAri = await _repository.GetByCodigo(dto.CodigoAri);
                if (personaAri == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ari no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoAri);

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

