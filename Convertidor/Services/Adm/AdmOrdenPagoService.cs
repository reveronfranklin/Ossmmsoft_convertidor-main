using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmOrdenPagoService : IAdmOrdenPagoService
    {
        private readonly IAdmOrdenPagoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmOrdenPagoService(IAdmOrdenPagoRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmProveedoresRepository admProveedoresRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admProveedoresRepository = admProveedoresRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

      
        public async Task<AdmOrdenPagoResponseDto> MapOrdenPagoDto(ADM_ORDEN_PAGO dtos)
        {
            AdmOrdenPagoResponseDto itemResult = new AdmOrdenPagoResponseDto();
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.ANO = dtos.ANO;
            itemResult.CodigoCompromiso = dtos.CODIGO_COMPROMISO;
            itemResult.CodigoOrdenCompra = dtos.CODIGO_ORDEN_COMPRA;
            itemResult.CodigoContrato = dtos.CODIGO_CONTRATO;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.NumeroOrdenPago = dtos.NUMERO_ORDEN_PAGO;
            itemResult.ReferenciaOrdenPago = dtos.REFERENCIA_ORDEN_PAGO;
            itemResult.FechaOrdenPago = dtos.FECHA_ORDEN_PAGO;
            itemResult.FechaOrdenPagoString =Fecha.GetFechaString(dtos.FECHA_ORDEN_PAGO);
            FechaDto fechaOrdenPagoObj = Fecha.GetFechaDto(dtos.FECHA_ORDEN_PAGO);
            itemResult.FechaOrdenPagoObj = (FechaDto)fechaOrdenPagoObj;
            itemResult.TipoOrdenPagoId = dtos.TIPO_ORDEN_PAGO_ID;
            itemResult.FechaPlazoDesde = dtos.FECHA_PLAZO_DESDE;
            itemResult.FechaPlazoDesdeString = Fecha.GetFechaString(dtos.FECHA_PLAZO_DESDE);
            FechaDto fechaPlazoDesdeObj = Fecha.GetFechaDto(dtos.FECHA_PLAZO_DESDE);
            itemResult.FechaPlazoDesdeObj = (FechaDto)fechaPlazoDesdeObj;
            itemResult.FechaPlazoHasta = dtos.FECHA_PLAZO_HASTA;
            itemResult.FechaPlazoHastaString = Fecha.GetFechaString(dtos.FECHA_PLAZO_HASTA);
            FechaDto fechaPlazoHastaObj = Fecha.GetFechaDto(dtos.FECHA_PLAZO_HASTA);
            itemResult.FechaPlazoHastaObj = (FechaDto)fechaPlazoHastaObj;
            itemResult.CantidadPago = dtos.CANTIDAD_PAGO;
            itemResult.NumeroPago = dtos.NUMERO_PAGO;
            itemResult.FrecuenciaPagoId = dtos.FRECUENCIA_PAGO_ID;
            itemResult.TipoPagoId = dtos.TIPO_PAGO_ID;
            itemResult.NumeroValuacion = dtos.NUMERO_VALUACION;
            itemResult.Status = dtos.STATUS;
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.Extra4 = dtos.EXTRA4;
            itemResult.Extra5 = dtos.EXTRA5;
            itemResult.Extra6 = dtos.EXTRA6;
            itemResult.Extra1 = dtos.EXTRA7;
            itemResult.Extra2 = dtos.EXTRA8;
            itemResult.Extra3 = dtos.EXTRA10;
            itemResult.Extra1 = dtos.EXTRA11;
            itemResult.Extra2 = dtos.EXTRA12;
            itemResult.Extra3 = dtos.EXTRA13;
            itemResult.Extra2 = dtos.EXTRA14;
            itemResult.Extra3 = dtos.EXTRA15;
            itemResult.NumeroComprobante = dtos.NUMERO_COMPROBANTE;
            itemResult.FechaComprobante = dtos.FECHA_COMPROBANTE;
            itemResult.FechaComprobanteString = Fecha.GetFechaString(dtos.FECHA_COMPROBANTE);
            FechaDto fechaComprobanteObj = Fecha.GetFechaDto(dtos.FECHA_COMPROBANTE);
            itemResult.FechaComprobanteObj = (FechaDto)fechaComprobanteObj;
            itemResult.NumeroComprobante2 = dtos.NUMERO_COMPROBANTE2;
            itemResult.numeroComprobante3 = dtos.NUMERO_COMPROBANTE3;
            itemResult.NumeroComprobante4 = dtos.NUMERO_COMPROBANTE4;

            return itemResult;
        }

        public async Task<ResultDto<List<AdmOrdenPagoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmOrdenPagoResponseDto>> result = new ResultDto<List<AdmOrdenPagoResponseDto>>(null);
            try
            {
                var ordenPago = await _repository.GetAll();
                var cant = ordenPago.Count();
                if (ordenPago != null && ordenPago.Count() > 0)
                {
                    var listDto = await MapListOrdenPagoDto(ordenPago);

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

        public async Task<List<AdmOrdenPagoResponseDto>> MapListOrdenPagoDto(List<ADM_ORDEN_PAGO> dtos)
        {
            List<AdmOrdenPagoResponseDto> result = new List<AdmOrdenPagoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapOrdenPagoDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<AdmOrdenPagoResponseDto>> Update(AdmOrdenPagoUpdateDto dto)
        {
            ResultDto<AdmOrdenPagoResponseDto> result = new ResultDto<AdmOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }
                if (dto.ANO < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año invalido";
                    return result;
                }

                //TODO VALIDAR CON REPOSITORIO PROVEEDORES

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }
                if (dto.NumeroOrdenPago is not null && dto.NumeroOrdenPago.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero orden pago Invalido";
                    return result;
                }

                if (dto.ReferenciaOrdenPago is not null && dto.ReferenciaOrdenPago.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Referencia orden pago invalida";
                    return result;

                }

                if (dto.FechaOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Orden Pago Invalida";
                    return result;
                }
                var tipoOrdenPagoId = await _admDescriptivaRepository.GetByIdAndTitulo(14, dto.TipoOrdenPagoId);
                if (tipoOrdenPagoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Orden Pago Invalido";
                    return result;
                }

                if (dto.FechaPlazoDesde == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha plazo desde Invalida";
                    return result;
                }

                if (dto.FechaPlazoHasta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha plazo hasta Invalida";
                    return result;
                }

                if (dto.CantidadPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Pago Invalida";
                    return result;
                }

                if (dto.NumeroPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Pago Invalido";
                    return result;
                }
                var frecuenciaPagoId = await _admDescriptivaRepository.GetByIdAndTitulo(15, dto.FrecuenciaPagoId);
                if (frecuenciaPagoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia Pago Invalido";
                    return result;
                }

                var tipoPagoId = await _admDescriptivaRepository.GetByIdAndTitulo(16, dto.TipoPagoId);
                if (tipoPagoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Pago Id Invalido";
                    return result;
                }

                if (dto.NumeroValuacion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero valuacion Invalido";
                    return result;
                }

                if (dto.Status == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }

                if (dto.Motivo == null)
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

                var codigopresupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
                if (dto.Extra4 is not null && dto.Extra4.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra4 Invalido";
                    return result;
                }

                if (dto.Extra13 is not null && dto.Extra13.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra13 Invalido";
                    return result;
                }
                if (dto.Extra14 is not null && dto.Extra14.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra14 Invalido";
                    return result;
                }

                if (dto.Extra15 is not null && dto.Extra15.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra15 Invalido";
                    return result;
                }

                if (dto.NumeroComprobante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante Invalido";
                    return result;
                }
                if (dto.FechaComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha comprobante Invalida";
                    return result;
                }

                if (dto.NumeroComprobante2 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante2 Invalido";
                    return result;
                }

                codigoOrdenPago.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                codigoOrdenPago.ANO = dto.ANO;
                codigoOrdenPago.CODIGO_COMPROMISO = dto.CodigoCompromiso;
                codigoOrdenPago.CODIGO_ORDEN_COMPRA = dto.CodigoOrdenCompra;
                codigoOrdenPago.CODIGO_CONTRATO = dto.CodigoContrato;
                codigoOrdenPago.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                codigoOrdenPago.NUMERO_ORDEN_PAGO = dto.NumeroOrdenPago;
                codigoOrdenPago.REFERENCIA_ORDEN_PAGO = dto.ReferenciaOrdenPago;
                codigoOrdenPago.FECHA_ORDEN_PAGO = dto.FechaOrdenPago;
                codigoOrdenPago.TIPO_ORDEN_PAGO_ID = dto.TipoOrdenPagoId;
                codigoOrdenPago.FECHA_PLAZO_DESDE = dto.FechaPlazoDesde;
                codigoOrdenPago.FECHA_PLAZO_HASTA = dto.FechaPlazoHasta;
                codigoOrdenPago.CANTIDAD_PAGO = dto.CantidadPago;
                codigoOrdenPago.NUMERO_PAGO = dto.NumeroPago;
                codigoOrdenPago.FRECUENCIA_PAGO_ID = dto.FrecuenciaPagoId;
                codigoOrdenPago.TIPO_PAGO_ID = dto.TipoPagoId;
                codigoOrdenPago.NUMERO_VALUACION = dto.NumeroValuacion;
                codigoOrdenPago.STATUS = dto.Status;
                codigoOrdenPago.MOTIVO = dto.Motivo;
                codigoOrdenPago.EXTRA1 = dto.Extra1;
                codigoOrdenPago.EXTRA2 = dto.Extra2;
                codigoOrdenPago.EXTRA3 = dto.Extra3;
                codigoOrdenPago.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoOrdenPago.EXTRA4 = dto.Extra4;
                codigoOrdenPago.EXTRA5 = dto.Extra5;
                codigoOrdenPago.EXTRA6 = dto.Extra6;
                codigoOrdenPago.EXTRA7 = dto.Extra7;
                codigoOrdenPago.EXTRA8 = dto.Extra8;
                codigoOrdenPago.EXTRA9 = dto.Extra9;
                codigoOrdenPago.EXTRA10 = dto.Extra10;
                codigoOrdenPago.EXTRA11 = dto.Extra11;
                codigoOrdenPago.EXTRA12 = dto.Extra12;
                codigoOrdenPago.EXTRA13 = dto.Extra13;
                codigoOrdenPago.EXTRA14 = dto.Extra14;
                codigoOrdenPago.EXTRA15 = dto.Extra15;
                codigoOrdenPago.NUMERO_COMPROBANTE = dto.NumeroComprobante;
                codigoOrdenPago.FECHA_COMPROBANTE = dto.FechaComprobante;
                codigoOrdenPago.NUMERO_COMPROBANTE2=dto.NumeroComprobante2;
                codigoOrdenPago.NUMERO_COMPROBANTE3 = dto.NumeroComprobante3;
                codigoOrdenPago.NUMERO_COMPROBANTE4 = dto.NumeroComprobante4;


                
                codigoOrdenPago.CODIGO_EMPRESA = conectado.Empresa;
                codigoOrdenPago.USUARIO_UPD = conectado.Usuario;
                codigoOrdenPago.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoOrdenPago);

                var resultDto = await MapOrdenPagoDto(codigoOrdenPago);
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

        public async Task<ResultDto<AdmOrdenPagoResponseDto>> Create(AdmOrdenPagoUpdateDto dto)
        {
            ResultDto<AdmOrdenPagoResponseDto> result = new ResultDto<AdmOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }
                if (dto.ANO < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año invalido";
                    return result;
                }

                //TODO VALIDAR CON REPOSITORIO PROVEEDORES

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }
                if (dto.NumeroOrdenPago is not null && dto.NumeroOrdenPago.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero orden pago Invalido";
                    return result;
                }

                if (dto.ReferenciaOrdenPago is not null && dto.ReferenciaOrdenPago.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Referencia orden pago invalida";
                    return result;

                }

                if (dto.FechaOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Orden Pago Invalida";
                    return result;
                }
                var tipoOrdenPagoId = await _admDescriptivaRepository.GetByIdAndTitulo(14, dto.TipoOrdenPagoId);
                if (tipoOrdenPagoId==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Orden Pago Invalido";
                    return result;
                }

                if (dto.FechaPlazoDesde == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha plazo desde Invalida";
                    return result;
                }

                if (dto.FechaPlazoHasta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha plazo hasta Invalida";
                    return result;
                }

                if (dto.CantidadPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Pago Invalida";
                    return result;
                }

                if (dto.NumeroPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Pago Invalido";
                    return result;
                }
                var frecuenciaPagoId = await _admDescriptivaRepository.GetByIdAndTitulo(15, dto.FrecuenciaPagoId);
                if (frecuenciaPagoId==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia Pago Invalido";
                    return result;
                }

                var tipoPagoId = await _admDescriptivaRepository.GetByIdAndTitulo(16,dto.TipoPagoId);
                if (tipoPagoId==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Pago Id Invalido";
                    return result;
                }

                if (dto.NumeroValuacion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero valuacion Invalido";
                    return result;
                }

                if (dto.Status == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }

                if (dto.Motivo == null)
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


                var codigopresupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                if (dto.Extra4 is not null && dto.Extra4.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra4 Invalido";
                    return result;
                }
               
                if (dto.Extra13 is not null && dto.Extra13.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra13 Invalido";
                    return result;
                }
                if (dto.Extra14 is not null && dto.Extra14.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra14 Invalido";
                    return result;
                }

                if (dto.Extra15 is not null && dto.Extra15.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra15 Invalido";
                    return result;
                }

                if (dto.NumeroComprobante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante Invalido";
                    return result;
                }
                if (dto.FechaComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha comprobante Invalida";
                    return result;
                }

                if (dto.NumeroComprobante2 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante2 Invalido";
                    return result;
                }
               

            ADM_ORDEN_PAGO entity = new ADM_ORDEN_PAGO();
            entity.CODIGO_ORDEN_PAGO = await _repository.GetNextKey();
            entity.ANO = dto.ANO;
            entity.CODIGO_COMPROMISO = dto.CodigoCompromiso;
            entity.CODIGO_ORDEN_COMPRA = dto.CodigoOrdenCompra;
            entity.CODIGO_CONTRATO = dto.CodigoContrato;
            entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
            entity.NUMERO_ORDEN_PAGO = dto.NumeroOrdenPago;
            entity.REFERENCIA_ORDEN_PAGO = dto.ReferenciaOrdenPago;
            entity.FECHA_ORDEN_PAGO = dto.FechaOrdenPago;
            entity.TIPO_ORDEN_PAGO_ID = dto.TipoOrdenPagoId;
            entity.FECHA_PLAZO_DESDE = dto.FechaPlazoDesde;
            entity.FECHA_PLAZO_HASTA = dto.FechaPlazoHasta;
            entity.CANTIDAD_PAGO = dto.CantidadPago;
            entity.NUMERO_PAGO = dto.NumeroPago;
            entity.FRECUENCIA_PAGO_ID = dto.FrecuenciaPagoId;
            entity.TIPO_PAGO_ID = dto.TipoPagoId;
            entity.NUMERO_VALUACION = dto.NumeroValuacion;
            entity.STATUS = dto.Status;
            entity.MOTIVO = dto.Motivo;
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            entity.EXTRA4 = dto.Extra4;
            entity.EXTRA5 = dto.Extra5;
            entity.EXTRA6 = dto.Extra6;
            entity.EXTRA7 = dto.Extra7;
            entity.EXTRA8 = dto.Extra8;
            entity.EXTRA9 = dto.Extra9;
            entity.EXTRA10 = dto.Extra10;
            entity.EXTRA11 = dto.Extra11;
            entity.EXTRA12 = dto.Extra12;
            entity.EXTRA13 = dto.Extra13;
            entity.EXTRA14 = dto.Extra14;
            entity.EXTRA15 = dto.Extra15;
            entity.NUMERO_COMPROBANTE = dto.NumeroComprobante;
            entity.FECHA_COMPROBANTE = dto.FechaComprobante;
            entity.NUMERO_COMPROBANTE2 = dto.NumeroComprobante2;
            entity.NUMERO_COMPROBANTE3 = dto.NumeroComprobante3;
            entity.NUMERO_COMPROBANTE4 = dto.NumeroComprobante4;


            
            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapOrdenPagoDto(created.Data);
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

        public async Task<ResultDto<AdmOrdenPagoDeleteDto>> Delete(AdmOrdenPagoDeleteDto dto) 
        {
            ResultDto<AdmOrdenPagoDeleteDto> result = new ResultDto<AdmOrdenPagoDeleteDto>(null);
            try
            {

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoOrdenPago);

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

