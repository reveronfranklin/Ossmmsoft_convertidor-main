using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Bm;
using MathNet.Numerics.RootFinding;

namespace Convertidor.Services.Adm
{
    public class AdmReintegrosService : IAdmReintegrosService
    {
        private readonly IAdmReintegrosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmReintegrosService(IAdmReintegrosRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
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
        public async Task<AdmReintegrosResponseDto> MapReintegrosDto(ADM_REINTEGROS dtos)
        {
            AdmReintegrosResponseDto itemResult = new AdmReintegrosResponseDto();
            itemResult.CodigoReintegro = dtos.CODIGO_REINTEGRO;
            itemResult.ANO = dtos.ANO;
            itemResult.CodigoCompromiso = dtos.CODIGO_COMPROMISO;
            itemResult.FechaReintegro = dtos.FECHA_REINTEGRO;
            itemResult.FechaReintegroString = dtos.FECHA_REINTEGRO.ToString("u");
            FechaDto fechaReintegroObj = GetFechaDto(dtos.FECHA_REINTEGRO);
            itemResult.FechaReintegroObj = (FechaDto)fechaReintegroObj;
            itemResult.NumeroReintegro = dtos.NUMERO_REINTEGRO;
            itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
            itemResult.NumeroDeposito = dtos.NUMERO_DEPOSITO;
            itemResult.FechaDeposito = dtos.FECHA_DEPOSITO;
            itemResult.FechaDepositoString = dtos.FECHA_DEPOSITO.ToString("u");
            FechaDto fechaDepositoObj = GetFechaDto(dtos.FECHA_DEPOSITO);
            itemResult.FechaDepositoObj = (FechaDto)fechaDepositoObj;
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.OrigenCompromisoId = dtos.ORIGEN_COMPROMISO_ID;
            return itemResult;
        }

        public async Task<List<AdmReintegrosResponseDto>> MapListReintegrosDto(List<ADM_REINTEGROS> dtos)
        {
            List<AdmReintegrosResponseDto> result = new List<AdmReintegrosResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapReintegrosDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<AdmReintegrosResponseDto>> Update(AdmReintegrosUpdateDto dto)
        {
            ResultDto<AdmReintegrosResponseDto> result = new ResultDto<AdmReintegrosResponseDto>(null);
            try
            {
                var codigoReintegro = await _repository.GetCodigoReintegro(dto.CodigoReintegro);
                if (codigoReintegro == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Reintegro no existe";
                    return result;
                }
                if (dto.ANO<0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año invalido";
                    return result;
                }

                if (dto.CodigoCompromiso <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo compromiso invalido";
                    return result;

                }

                if (dto.FechaReintegro == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha reintegro Invalida";
                    return result;
                }
                if (dto.NumeroReintegro is not null && dto.NumeroReintegro.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero reintegro Invalido";
                    return result;
                }
                if (dto.CodigoCuentaBanco < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta banco Invalido";
                    return result;
                }
                if (dto.NumeroDeposito is not null && dto.NumeroDeposito.Length >10)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero deposito Invalido";
                    return result;
                }

                if (dto.FechaDeposito == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha deposito no existe";
                    return result;

                }

                if (dto.Motivo is not null && dto.Motivo.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
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

                if (dto.Status is not null && dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var origenCompromisoId = await _admDescriptivaRepository.GetByIdAndTitulo(3, dto.OrigenCompromisoId);
                if(origenCompromisoId == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Origen Compromiso Id Invalido";
                    return result;
                }

                codigoReintegro.CODIGO_REINTEGRO = dto.CodigoReintegro;
                codigoReintegro.ANO = dto.ANO;
                codigoReintegro.CODIGO_COMPROMISO = dto.CodigoCompromiso;
                codigoReintegro.FECHA_REINTEGRO = dto.FechaReintegro;
                codigoReintegro.NUMERO_REINTEGRO = dto.NumeroReintegro;
                codigoReintegro.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                codigoReintegro.NUMERO_DEPOSITO = dto.NumeroDeposito;
                codigoReintegro.FECHA_DEPOSITO = dto.FechaDeposito;
                codigoReintegro.MOTIVO = dto.Motivo;
                codigoReintegro.EXTRA1 = dto.Extra1;
                codigoReintegro.EXTRA2 = dto.Extra2;
                codigoReintegro.EXTRA3 = dto.Extra3;
                codigoReintegro.STATUS = dto.Status;
                codigoReintegro.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoReintegro.ORIGEN_COMPROMISO_ID = dto.OrigenCompromisoId;

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoReintegro.CODIGO_EMPRESA = conectado.Empresa;
                codigoReintegro.USUARIO_UPD = conectado.Usuario;
                codigoReintegro.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoReintegro);

                var resultDto = await MapReintegrosDto(codigoReintegro);
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

        public async Task<ResultDto<AdmReintegrosResponseDto>> Create(AdmReintegrosUpdateDto dto)
        {
            ResultDto<AdmReintegrosResponseDto> result = new ResultDto<AdmReintegrosResponseDto>(null);
            try
            {
                var reintegro = await _repository.GetCodigoReintegro(dto.CodigoReintegro);
                if (reintegro != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Reintegro ya existe";
                    return result;
                }
                if (dto.ANO < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año invalido";
                    return result;
                }

                if (dto.CodigoCompromiso < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo compromiso invalido";
                    return result;

                }

                if (dto.FechaReintegro == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha reintegro Invalida";
                    return result;
                }
                if (dto.NumeroReintegro is not null && dto.NumeroReintegro.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero reintegro Invalido";
                    return result;
                }
                if (dto.CodigoCuentaBanco < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta banco Invalido";
                    return result;
                }
                if (dto.NumeroDeposito is not null && dto.NumeroDeposito.Length > 10)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero deposito Invalido";
                    return result;
                }

                if (dto.FechaDeposito == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha deposito no existe";
                    return result;

                }

                if (dto.Motivo is not null && dto.Motivo.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
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
                if (dto.Status is not null && dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var origenCompromisoId = await _admDescriptivaRepository.GetByIdAndTitulo(3, dto.OrigenCompromisoId);
                if (origenCompromisoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Origen Compromiso Id Invalido";
                    return result;
                }

            ADM_REINTEGROS entity = new ADM_REINTEGROS();
            entity.CODIGO_REINTEGRO = await _repository.GetNextKey();
            entity.ANO = dto.ANO;
            entity.CODIGO_COMPROMISO = dto.CodigoCompromiso;
            entity.FECHA_REINTEGRO = dto.FechaReintegro;
            entity.NUMERO_REINTEGRO = dto.NumeroReintegro;
            entity.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
            entity.NUMERO_DEPOSITO = dto.NumeroDeposito;
            entity.FECHA_DEPOSITO = dto.FechaDeposito;
            entity.MOTIVO=dto.Motivo;
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.STATUS = dto.Status;
            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            entity.ORIGEN_COMPROMISO_ID = dto.OrigenCompromisoId;


            var conectado = await _sisUsuarioRepository.GetConectado();
            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapReintegrosDto(created.Data);
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

        public async Task<ResultDto<AdmReintegrosDeleteDto>> Delete(AdmReintegrosDeleteDto dto) 
        {
            ResultDto<AdmReintegrosDeleteDto> result = new ResultDto<AdmReintegrosDeleteDto>(null);
            try
            {

                var codigoReintegro = await _repository.GetCodigoReintegro(dto.CodigoReintegro);
                if (codigoReintegro == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Reintegro no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoReintegro);

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

