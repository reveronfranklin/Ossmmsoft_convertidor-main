using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
namespace Convertidor.Services.Presupuesto
{
    public class PreEquiposService : IPreEquiposService
    {
        private readonly IPreEquiposRepository _repository;
        private readonly IPRE_INDICE_CAT_PRGRepository _pRE_INDICE_CAT_PRGRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreEquiposService(IPreEquiposRepository repository,
                                  IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository,
                                   IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                   ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _pRE_INDICE_CAT_PRGRepository = pRE_INDICE_CAT_PRGRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PreEquiposResponseDto>>> GetAll()
        {
            ResultDto<List<PreEquiposResponseDto>> result = new ResultDto<List<PreEquiposResponseDto>>(null);

            try 
            {
                var equipos = await _repository.GetAll();
                if(equipos.Count() > 0) 
                {
                    List<PreEquiposResponseDto> listDto = new List<PreEquiposResponseDto>();
                    foreach (var item in equipos)
                    {
                        var dto = await MapPreEquipos(item);
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

        public async Task<ResultDto<PreEquiposResponseDto>> GetByCodigo(int codigoEquipo)
        {
            ResultDto<PreEquiposResponseDto> result = new ResultDto<PreEquiposResponseDto>(null);
            try
            {
                var equipos = await _repository.GetByCodigo(codigoEquipo);
                if (equipos != null)
                {
                    var dto = await MapPreEquipos(equipos);
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = "";
                    result.LinkData = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";
                    result.LinkData = "";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = "";
            }



            return result;
        }


        public async Task<PreEquiposResponseDto> MapPreEquipos(PRE_EQUIPOS dto)
        {
            PreEquiposResponseDto itemResult = new PreEquiposResponseDto();
            itemResult.CodigoEquipo = dto.CODIGO_EQUIPO;
            itemResult.Ano = dto.ANO;
            itemResult.Escenario = dto.ESCENARIO;
            itemResult.CodigoIcp = dto.CODIGO_ICP;
            itemResult.Principal = dto.PRINCIPAL;
            itemResult.Denominacion = dto.DENOMINACION;
            itemResult.Reemplazos = dto.REEMPLAZOS;
            itemResult.Deficiencias = dto.DEFICIENCIAS;
            itemResult.CostoUnitario = dto.COSTO_UNITARIO;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;

            return itemResult;

        }

        public async Task<List<PreEquiposResponseDto>> MapListPreEquipos(List<PRE_EQUIPOS> dtos)
        {
            List<PreEquiposResponseDto> result = new List<PreEquiposResponseDto>();

            foreach (var item in dtos)
            {
                PreEquiposResponseDto itemResult = new PreEquiposResponseDto();
                itemResult = await MapPreEquipos(item);
                result.Add(itemResult);
            }

            return result;

        }

        public async Task<ResultDto<PreEquiposResponseDto>> Update(PreEquiposUpdateDto dto)
        {

            ResultDto<PreEquiposResponseDto> result = new ResultDto<PreEquiposResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();



                if (dto.CodigoEquipo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Equipo no existe";
                    return result;

                }

                var codigoEquipo = await _repository.GetByCodigo(dto.CodigoEquipo);
                if (codigoEquipo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Equipo no existe";
                    return result;

                }
                if (dto.CodigoIcp <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                var codigoIcp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                if (dto.Principal > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Principal Invalido";
                    return result;

                }

                if (dto.Principal < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Principal Invalido";
                    return result;
                }

                if (dto.Denominacion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }

                if (dto.CostoUnitario <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Costo unitario Invalido";
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

                if (dto.CodigoPresupuesto <= 0)
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

                dto.Ano = codigoPresupuesto.ANO;



                codigoEquipo.CODIGO_EQUIPO = dto.CodigoEquipo;
                codigoEquipo.ANO = dto.Ano;
                codigoEquipo.ESCENARIO = dto.Escenario;
                codigoEquipo.CODIGO_ICP = dto.CodigoIcp;
                codigoEquipo.PRINCIPAL = dto.Principal;
                codigoEquipo.DENOMINACION = dto.Denominacion;
                codigoEquipo.REEMPLAZOS = dto.Reemplazos;
                codigoEquipo.DEFICIENCIAS = dto.Deficiencias;
                codigoEquipo.COSTO_UNITARIO = dto.CostoUnitario;
                codigoEquipo.EXTRA1 = dto.Extra1;
                codigoEquipo.EXTRA2 = dto.Extra2;
                codigoEquipo.EXTRA3 = dto.Extra3;
                codigoEquipo.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;




                codigoEquipo.CODIGO_EMPRESA = conectado.Empresa;
                codigoEquipo.USUARIO_UPD = conectado.Usuario;
                codigoEquipo.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoEquipo);

                var resultDto = await MapPreEquipos(codigoEquipo);
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

        public async Task<ResultDto<PreEquiposResponseDto>> Create(PreEquiposUpdateDto dto)
        {

            ResultDto<PreEquiposResponseDto> result = new ResultDto<PreEquiposResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoIcp <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                var codigoIcp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                if(dto.Principal > 1) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Principal Invalido";
                    return result;

                }

                if (dto.Principal < 0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Principal Invalido";
                    return result;
                }

                if (dto.Denominacion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }
            
                if (dto.CostoUnitario <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Costo unitario Invalido";
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

                if (dto.CodigoPresupuesto <= 0)
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

                dto.Ano = codigoPresupuesto.ANO;




                PRE_EQUIPOS entity = new PRE_EQUIPOS();
                entity.CODIGO_EQUIPO = await _repository.GetNextKey();
                entity.ANO = dto.Ano;
                entity.ESCENARIO = dto.Escenario;
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.PRINCIPAL = dto.Principal;
                entity.DENOMINACION = dto.Denominacion;
                entity.REEMPLAZOS = dto.Reemplazos;
                entity.DEFICIENCIAS = dto.Deficiencias;
                entity.COSTO_UNITARIO = dto.CostoUnitario;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;



                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreEquipos(created.Data);
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

        public async Task<ResultDto<PreEquiposDeleteDto>> Delete(PreEquiposDeleteDto dto)
        {

            ResultDto<PreEquiposDeleteDto> result = new ResultDto<PreEquiposDeleteDto>(null);
            try
            {

                var codigoEquipo = await _repository.GetByCodigo(dto.CodigoEquipo);
                if (codigoEquipo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Equipo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoEquipo);

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


