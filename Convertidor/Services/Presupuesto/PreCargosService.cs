using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public class PreCargosService: IPreCargosService
    {

      
        private readonly IPreCargosRepository _repository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IPRE_RELACION_CARGOSRepository _preRelacionCargoRepository;
        private readonly IConfiguration _configuration;
        public PreCargosService(IPreCargosRepository repository,
                                      IConfiguration configuration,
                                      IPreDescriptivaRepository repositoryPreDescriptiva,
                                      IPRE_RELACION_CARGOSRepository preRelacionCargoRepository)
		{
            _repository = repository;
            _configuration = configuration;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _preRelacionCargoRepository = preRelacionCargoRepository;


        }


        public async Task<ResultDto<List<PreCargosGetDto>>> GetAll()
        {

            ResultDto<List<PreCargosGetDto>> result = new ResultDto<List<PreCargosGetDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();

               

                if (titulos.Count() > 0)
                {
                    List<PreCargosGetDto> listDto = new List<PreCargosGetDto>();

                    foreach (var item in titulos)
                    {
                        var dto = await MapPreCargo(item);
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



        public async Task<ResultDto<List<PreCargosGetDto>>> GetAllByPresupuesto(FilterByPresupuestoDto filter)
        {

            ResultDto<List<PreCargosGetDto>> result = new ResultDto<List<PreCargosGetDto>>(null);
            try
            {

                var cargos = await _repository.GetAllByPresupuesto(filter.CodigoPresupuesto);



                if (cargos.Count() > 0)
                {
                    List<PreCargosGetDto> listDto = new List<PreCargosGetDto>();

                    foreach (var item in cargos)
                    {
                        var dto = await MapPreCargo(item);
                        listDto.Add(dto);
                    }


                    result.Data = listDto.OrderBy(x=>x.DescripcionTipoPersonal).ThenBy(x=>x.DescripcionTipoCargo).ToList();

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

        public async Task<ResultDto<PreCargosGetDto>> Update(PreCargosUpdateDto dto)
        {

            ResultDto<PreCargosGetDto> result = new ResultDto<PreCargosGetDto>(null);
            try
            {

                var cargoUpdate = await _repository.GetByCodigo(dto.CodigoCargo);
                if (cargoUpdate == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo no existe";
                    return result;
                }
                if (dto.Denominacion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }

                if (dto.TipoPersonalId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Personal Invalido";
                    return result;
                }
                if (dto.TipoCargoId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Cargo Invalido";
                    return result;
                }

                var descriptivaTipoPersonal= await _repositoryPreDescriptiva.GetByCodigo(dto.TipoPersonalId);

                if (descriptivaTipoPersonal ==null)
                {
                   
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo Personal Invalido";
                        return result;
             

                }


                var descriptivaTipoCargo = await _repositoryPreDescriptiva.GetByCodigo(dto.TipoCargoId);

                if (descriptivaTipoCargo == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Cargo Invalido";
                    return result;


                }

                cargoUpdate.TIPO_CARGO_ID = dto.TipoCargoId;
                cargoUpdate.TIPO_PERSONAL_ID = dto.TipoPersonalId;
                cargoUpdate.DENOMINACION = dto.Denominacion;
                cargoUpdate.DESCRIPCION = dto.Descripcion;
                cargoUpdate.GRADO = dto.Grado;
                cargoUpdate.EXTRA1 = dto.Extra1;
                cargoUpdate.EXTRA2 = dto.Extra2;
                cargoUpdate.EXTRA3 = dto.Extra3;
                cargoUpdate.FECHA_UPD = DateTime.Now;




                await _repository.Update(cargoUpdate);

                var resultDto =await  MapPreCargo(cargoUpdate);
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

        public async Task<ResultDto<PreCargosGetDto>> Create(PreCargosUpdateDto dto)
        {

            ResultDto<PreCargosGetDto> result = new ResultDto<PreCargosGetDto>(null);
            try
            {

                var cargo = await _repository.GetByCodigo(dto.CodigoCargo);
                if (cargo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo existe";
                    return result;
                }

                if (dto.Denominacion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }

                var descriptivaTipoPersonal = await _repositoryPreDescriptiva.GetByCodigo(dto.TipoPersonalId);

                if (descriptivaTipoPersonal == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Personal Invalido";
                    return result;


                }
                var descriptivaTipoCargo = await _repositoryPreDescriptiva.GetByCodigo(dto.TipoCargoId);

                if (descriptivaTipoCargo == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Cargo Invalido";
                    return result;


                }




                PRE_CARGOS entity = new PRE_CARGOS();
                entity.CODIGO_CARGO = await _repository.GetNextKey();
                entity.TIPO_CARGO_ID = dto.TipoCargoId;
                entity.TIPO_PERSONAL_ID = dto.TipoPersonalId;
                entity.DENOMINACION = dto.Denominacion;
                entity.DESCRIPCION = dto.Descripcion;
                entity.GRADO = dto.Grado;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.FECHA_UPD = DateTime.Now;
                entity.FECHA_INS= DateTime.Now;
                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreCargo(created.Data);
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
        public async Task<PreCargosGetDto> MapPreCargo(PRE_CARGOS item)
        {
            PreCargosGetDto dto = new PreCargosGetDto();
            dto.CodigoCargo = item.CODIGO_CARGO;
            dto.TipoPersonalId = item.TIPO_PERSONAL_ID;
            dto.DescripcionTipoPersonal = "";
            var descriptiva = await _repositoryPreDescriptiva.GetByCodigo(dto.TipoPersonalId);
            if (descriptiva != null)
            {
                dto.DescripcionTipoPersonal = descriptiva.DESCRIPCION;
            }
            dto.TipoCargoId = item.TIPO_CARGO_ID;
            dto.DescripcionTipoCargo = "";
            descriptiva = await _repositoryPreDescriptiva.GetByCodigo(dto.TipoCargoId);
            if (descriptiva != null)
            {
                dto.DescripcionTipoCargo = descriptiva.DESCRIPCION;
            }
            if (item.DENOMINACION == null) item.DENOMINACION = "";
            dto.Denominacion = item.DENOMINACION;
            if (item.DESCRIPCION == null) item.DESCRIPCION = "";
            dto.Descripcion = item.DESCRIPCION;
            dto.Grado = item.GRADO;
            if (item.EXTRA1 == null) item.EXTRA1 = "";
            if (item.EXTRA2 == null) item.EXTRA2 = "";
            if (item.EXTRA3 == null) item.EXTRA3 = "";
            dto.Extra1 = item.EXTRA1;
            dto.Extra2 = item.EXTRA2;
            dto.Extra3 = item.EXTRA3;
            dto.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;

            return dto;

        }


        public async Task<ResultDto<PreCargosDeleteDto>> Delete(PreCargosDeleteDto dto)
        {

            ResultDto<PreCargosDeleteDto> result = new ResultDto<PreCargosDeleteDto>(null);
            try
            {

                var titulo = await _repository.GetByCodigo(dto.CodigoCargo);
                if (titulo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Cargo no existe";
                    return result;
                }

                var relacionCargo = await _preRelacionCargoRepository.GetByCodigoCargo(dto.CodigoCargo);
                if (relacionCargo != null && relacionCargo.Count>0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = $"Cargo esta asociado a un Presupuesto";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoCargo);

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

