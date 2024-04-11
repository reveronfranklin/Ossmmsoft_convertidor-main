using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Sis;
using MathNet.Numerics.RootFinding;
using Microsoft.JSInterop.Infrastructure;

namespace Convertidor.Services.Adm
{
    public class AdmContratosService : IAdmContratosService
    {
        private readonly IAdmContratosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmContratosService(  IAdmContratosRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmProveedoresRepository admProveedoresRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admProveedoresRepository = admProveedoresRepository;
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

        public async Task<AdmContratosResponseDto> MapContratosDto(ADM_CONTRATOS dtos)
        {
            AdmContratosResponseDto itemResult = new AdmContratosResponseDto();
            itemResult.CodigoContrato = dtos.CODIGO_CONTRATO;
            itemResult.ANO = dtos.ANO;
            itemResult.FechaContrato = dtos.FECHA_CONTRATO;
            itemResult.FechaContratoString = dtos.FECHA_CONTRATO.ToString("u");
            FechaDto fechaContratoObj = GetFechaDto(dtos.FECHA_CONTRATO);
            itemResult.FechaContratoObj = (FechaDto)fechaContratoObj;
            itemResult.NumeroContrato = dtos.NUMERO_CONTRATO;
            itemResult.CodigoSolicitante = dtos.CODIGO_SOLICITANTE;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.FechaAprobacion = dtos.FECHA_APROBACION;
            itemResult.FechaAprobacionString = dtos.FECHA_APROBACION.ToString("u");
            FechaDto fechaAprobacionObj = GetFechaDto(dtos.FECHA_APROBACION);
            itemResult.FechaAprobacionObj = (FechaDto)fechaAprobacionObj;
            itemResult.NumeroAprobacion = dtos.NUMERO_APROBACION;
            itemResult.Obra = dtos.OBRA;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Parroquiaid = dtos.PARROQUIA_ID;
            itemResult.FechaIniObra = dtos.FECHA_INI_OBRA;
            itemResult.FechaIniObraString = dtos.FECHA_INI_OBRA.ToString("u");
            FechaDto fechaIniObraObj = GetFechaDto(dtos.FECHA_INI_OBRA);
            itemResult.FechaIniObraObj = (FechaDto)fechaIniObraObj;
            itemResult.FechaFinObra = dtos.FECHA_FIN_OBRA;
            itemResult.FechaFinObraString = dtos.FECHA_FIN_OBRA.ToString("u");
            FechaDto fechaFinObraObj = GetFechaDto(dtos.FECHA_FIN_OBRA);
            itemResult.FechaFinObraObj = (FechaDto)fechaFinObraObj;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.Status = dtos.STATUS;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.MontoOriginal = dtos.MONTO_ORIGINAL;
            itemResult.TipoModificacionId = dtos.TIPO_MODIFICACION_ID;


            return itemResult;
        }

        public async Task<List<AdmContratosResponseDto>> MapListContratosDto(List<ADM_CONTRATOS> dtos)
        {
            List<AdmContratosResponseDto> result = new List<AdmContratosResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapContratosDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmContratosResponseDto>>> GetAll()
        {

            ResultDto<List<AdmContratosResponseDto>> result = new ResultDto<List<AdmContratosResponseDto>>(null);
            try
            {
                var contrato = await _repository.GetAll();
                var cant = contrato.Count();
                if (contrato != null && contrato.Count() > 0)
                {
                    var listDto = await MapListContratosDto(contrato);

                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No data";

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }
        public async Task<ResultDto<AdmContratosResponseDto>> Update(AdmContratosUpdateDto dto)
        {
            ResultDto<AdmContratosResponseDto> result = new ResultDto<AdmContratosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoContrato = await _repository.GetByCodigoContrato(dto.CodigoContrato);
                if (codigoContrato == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo contrato no existe";
                    return result;
                }
                if(dto.ANO < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año invalido";
                    return result;

                }

                if(dto.FechaContrato == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha contrato no existe";
                    return result;
                }
                if(dto.NumeroContrato.Length < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero contrato Invalido";
                    return result;
                }

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if(dto.FechaAprobacion == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha de Aprobacion no existe";
                    return result;

                }

                if(dto.NumeroAprobacion.Length > 20) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Aprobacion Invalido";
                    return result;

                }

                if (dto.Obra.Length > 200)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Obra Invalida";
                    return result;

                }

                if (dto.Descripcion.Length > 4000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;

                }

                if(dto.FechaIniObra == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial de la obra Invalida";
                    return result;

                }

                if(dto.MontoContrato < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto contrato Invalido";
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

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }

                var codigopresupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                if(dto.MontoOriginal < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Original Invalido";
                    return result;

                }

               
               

                codigoContrato.CODIGO_CONTRATO = dto.CodigoContrato;
                codigoContrato.ANO = dto.ANO;
                codigoContrato.FECHA_CONTRATO = dto.FechaContrato;
                codigoContrato.NUMERO_CONTRATO = dto.NumeroContrato;
                codigoContrato.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                codigoContrato.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                codigoContrato.FECHA_APROBACION = dto.FechaAprobacion;
                codigoContrato.OBRA = dto.Obra;
                codigoContrato.DESCRIPCION = dto.Descripcion;
                codigoContrato.PARROQUIA_ID = dto.Parroquiaid;
                codigoContrato.FECHA_INI_OBRA = dto.FechaIniObra;
                codigoContrato.FECHA_FIN_OBRA = dto.FechaFinObra;
                codigoContrato.MONTO_CONTRATO = dto.MontoContrato;
                codigoContrato.EXTRA1 = dto.Extra1;
                codigoContrato.EXTRA2 = dto.Extra2;
                codigoContrato.EXTRA3 = dto.Extra3;
                codigoContrato.STATUS = dto.Status;
                codigoContrato.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoContrato.MONTO_ORIGINAL = dto.MontoOriginal;
                codigoContrato.TIPO_MODIFICACION_ID = dto.TipoModificacionId;




                codigoContrato.CODIGO_EMPRESA = conectado.Empresa;
                codigoContrato.USUARIO_UPD = conectado.Usuario;
                codigoContrato.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoContrato);

                var resultDto = await MapContratosDto(codigoContrato);
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

        public async Task<ResultDto<AdmContratosResponseDto>> Create(AdmContratosUpdateDto dto)
        {
            ResultDto<AdmContratosResponseDto> result = new ResultDto<AdmContratosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoContrato = await _repository.GetByCodigoContrato(dto.CodigoContrato);
                if (codigoContrato != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo contrato ya existe";
                    return result;
                }
                if (dto.ANO < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año invalido";
                    return result;

                }

                if (dto.FechaContrato == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha contrato no existe";
                    return result;
                }
                if (dto.NumeroContrato.Length < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero contrato Invalido";
                    return result;
                }

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.FechaAprobacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha de Aprobacion no existe";
                    return result;

                }

                if (dto.NumeroAprobacion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Aprobacion Invalido";
                    return result;

                }

                if (dto.Obra.Length > 200)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Obra Invalida";
                    return result;

                }

                if (dto.Descripcion.Length > 4000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;

                }

                if (dto.FechaIniObra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial de la obra Invalida";
                    return result;

                }

                if (dto.MontoContrato < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto contrato Invalido";
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

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }

                var codigopresupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                if (dto.MontoOriginal < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Original Invalido";
                    return result;

                }


            ADM_CONTRATOS entity = new ADM_CONTRATOS();
            entity.CODIGO_CONTRATO = await _repository.GetNextKey();
            entity.ANO = dto.ANO;
            entity.FECHA_CONTRATO = dto.FechaContrato;
            entity.NUMERO_CONTRATO = dto.NumeroContrato;
            entity.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
            entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
            entity.FECHA_APROBACION = dto.FechaAprobacion;
            entity.OBRA = dto.Obra;
            entity.DESCRIPCION = dto.Descripcion;
            entity.PARROQUIA_ID = dto.Parroquiaid;
            entity.FECHA_INI_OBRA = dto.FechaIniObra;
            entity.FECHA_FIN_OBRA = dto.FechaFinObra;
            entity.MONTO_CONTRATO = dto.MontoContrato;
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.STATUS = dto.Status;
            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            entity.MONTO_ORIGINAL = dto.MontoOriginal;
            entity.TIPO_MODIFICACION_ID = dto.TipoModificacionId;



            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapContratosDto(created.Data);
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

        public async Task<ResultDto<AdmContratosDeleteDto>> Delete(AdmContratosDeleteDto dto) 
        {
            ResultDto<AdmContratosDeleteDto> result = new ResultDto<AdmContratosDeleteDto>(null);
            try
            {

                var codigoContrato = await _repository.GetByCodigoContrato(dto.CodigoContrato);
                if (codigoContrato == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Contrato no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoContrato);

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

