using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreOrganismosService : IPreOrganismosService
    {
        private readonly IPreOrganismosRepository _repository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreOrganismosService(IPreOrganismosRepository repository,
                                   IAdmDescriptivaRepository admDescriptivaRepository,
                                   IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                         ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PreOrganismosResponseDto>>> GetAll()
        {

            ResultDto<List<PreOrganismosResponseDto>> result = new ResultDto<List<PreOrganismosResponseDto>>(null);
            try
            {

                var preOrganismos = await _repository.GetAll();



                if (preOrganismos.Count() > 0)
                {
                    List<PreOrganismosResponseDto> listDto = new List<PreOrganismosResponseDto>();

                    foreach (var item in preOrganismos)
                    {
                        var dto = await MapPreOrganismos(item);
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


        public async Task<PreOrganismosResponseDto> MapPreOrganismos(PRE_ORGANISMOS dto)
        {
            PreOrganismosResponseDto itemResult = new PreOrganismosResponseDto();
            itemResult.CodigoOrganismo = dto.CODIGO_ORGANISMO;
            itemResult.Ano = dto.ANO;
            itemResult.Denominacion = dto.DENOMINACION;
            itemResult.NumeroRegistro = dto.NUMERO_REGISTRO;
            itemResult.Actividad = dto.ACTIVIDAD;
            itemResult.UbicacionGeografica = dto.UBICACION_GEOGRAFICA;
            itemResult.TipoOrganismoId = dto.TIPO_ORGANISMO_ID;
            itemResult.CapitalSocial = dto.CAPITAL_SOCIAL;
            itemResult.Monto1 = dto.MONTO1;
            itemResult.Monto2 = dto.MONTO2;
            itemResult.Monto4 = dto.MONTO4;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.Monto3 = dto.MONTO3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
           

            return itemResult;

        }

        public async Task<List<PreOrganismosResponseDto>> MapListPreOrganismos(List<PRE_ORGANISMOS> dtos)
        {
            List<PreOrganismosResponseDto> result = new List<PreOrganismosResponseDto>();


            foreach (var item in dtos)
            {

                PreOrganismosResponseDto itemResult = new PreOrganismosResponseDto();

                itemResult = await MapPreOrganismos(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PreOrganismosResponseDto>> Update(PreOrganismosUpdateDto dto)
        {

            ResultDto<PreOrganismosResponseDto> result = new ResultDto<PreOrganismosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();



                if (dto.CodigoOrganismo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Organismo no existe";
                    return result;

                }

                var codigoOrganismo = await _repository.GetByCodigo(dto.CodigoOrganismo);
                if (codigoOrganismo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Organismo no existe";
                    return result;

                }
                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano Invalido";
                    return result;
                }
                

                if (dto.Denominacion.Length > 300)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalido";
                    return result;
                }

                if (dto.NumeroRegistro.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Registro Invalido";
                    return result;
                }

                if (dto.Actividad.Length > 200)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Actividad Invalida";
                    return result;
                }

                if (dto.UbicacionGeografica.Length > 500)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ubicacion geografica";
                    return result;
                }
                if(dto.TipoOrganismoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Organismo Id Invalido";
                    return result;
                }

                var tipoOrganismoId = await _admDescriptivaRepository.GetByIdAndTitulo(13, dto.TipoOrganismoId);
                if(tipoOrganismoId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Organismo Id Invalido";
                    return result;
                }

                if (dto.CapitalSocial < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Capital social Invalido";
                    return result;
                }
                if (dto.Monto1 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto1 Invalido";
                    return result;
                }

                if (dto.Monto2 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto2 Invalido";
                    return result;
                }

                if (dto.Monto4 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto4 Invalido";
                    return result;
                }

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

                if (dto.Monto3 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto2 Invalido";
                    return result;
                }

                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;

                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }


            


                codigoOrganismo.CODIGO_ORGANISMO = dto.CodigoOrganismo;
                codigoOrganismo.ANO = dto.Ano;
                codigoOrganismo.DENOMINACION = dto.Denominacion;
                codigoOrganismo.NUMERO_REGISTRO = dto.NumeroRegistro;
                codigoOrganismo.ACTIVIDAD = dto.Actividad;
                codigoOrganismo.UBICACION_GEOGRAFICA = dto.UbicacionGeografica;
                codigoOrganismo.TIPO_ORGANISMO_ID = dto.TipoOrganismoId;
                codigoOrganismo.CAPITAL_SOCIAL = dto.CapitalSocial;
                codigoOrganismo.MONTO1 = dto.Monto1;
                codigoOrganismo.MONTO2 = dto.Monto2;
                codigoOrganismo.MONTO4 = dto.Monto4;
                codigoOrganismo.EXTRA1 = dto.Extra1;
                codigoOrganismo.EXTRA2 = dto.Extra2;
                codigoOrganismo.EXTRA3 = dto.Extra3;
                codigoOrganismo.MONTO3 = dto.Monto3;
                codigoOrganismo.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;




                codigoOrganismo.CODIGO_EMPRESA = conectado.Empresa;
                codigoOrganismo.USUARIO_UPD = conectado.Usuario;
                codigoOrganismo.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoOrganismo);

                var resultDto = await MapPreOrganismos(codigoOrganismo);
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

        public async Task<ResultDto<PreOrganismosResponseDto>> Create(PreOrganismosUpdateDto dto)
        {

            ResultDto<PreOrganismosResponseDto> result = new ResultDto<PreOrganismosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano Invalido";
                    return result;
                }


                if (dto.Denominacion.Length > 300)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalido";
                    return result;
                }

                if (dto.NumeroRegistro.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Registro Invalido";
                    return result;
                }

                if (dto.Actividad.Length > 200)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Actividad Invalida";
                    return result;
                }

                if (dto.UbicacionGeografica.Length > 500)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ubicacion geografica";
                    return result;
                }
                if (dto.TipoOrganismoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Organismo Id Invalido";
                    return result;
                }

                var tipoOrganismoId = await _admDescriptivaRepository.GetByIdAndTitulo(13, dto.TipoOrganismoId);
                if (tipoOrganismoId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Organismo Id Invalido";
                    return result;
                }

                if (dto.CapitalSocial < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Capital social Invalido";
                    return result;
                }
                if (dto.Monto1 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto1 Invalido";
                    return result;
                }

                if (dto.Monto2 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto2 Invalido";
                    return result;
                }

                if (dto.Monto4 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto4 Invalido";
                    return result;
                }

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

                if (dto.Monto3 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto2 Invalido";
                    return result;
                }

                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;

                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }





                PRE_ORGANISMOS entity = new PRE_ORGANISMOS();
                entity.CODIGO_ORGANISMO = await _repository.GetNextKey();
                entity.ANO = dto.Ano;
                entity.DENOMINACION = dto.Denominacion;
                entity.NUMERO_REGISTRO = dto.NumeroRegistro;
                entity.ACTIVIDAD = dto.Actividad;
                entity.UBICACION_GEOGRAFICA = dto.UbicacionGeografica;
                entity.TIPO_ORGANISMO_ID = dto.TipoOrganismoId;
                entity.CAPITAL_SOCIAL = dto.CapitalSocial;
                entity.MONTO1 = dto.Monto1;
                entity.MONTO2 = dto.Monto2;
                entity.MONTO4 = dto.Monto4;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.MONTO3 = dto.Monto3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreOrganismos(created.Data);
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

        public async Task<ResultDto<PreOrganismosDeleteDto>> Delete(PreOrganismosDeleteDto dto)
        {

            ResultDto<PreOrganismosDeleteDto> result = new ResultDto<PreOrganismosDeleteDto>(null);
            try
            {

                var codigoOrganismo = await _repository.GetByCodigo(dto.CodigoOrganismo);
                if (codigoOrganismo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Organismo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoOrganismo);

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
