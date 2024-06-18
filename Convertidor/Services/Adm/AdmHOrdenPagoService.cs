using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmHOrdenPagoService : IAdmHOrdenPagoService
    {
        private readonly IAdmHOrdenPagoRepository _repository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmHOrdenPagoService(IAdmHOrdenPagoRepository repository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmProveedoresRepository admProveedoresRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admProveedoresRepository = admProveedoresRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

     
        public async Task<AdmHOrdenPagoResponseDto> MapHOrdenPagoDto(ADM_H_ORDEN_PAGO dtos)
        {
            AdmHOrdenPagoResponseDto itemResult = new AdmHOrdenPagoResponseDto();
            itemResult.CodigoHOrdenPago = dtos.CODIGO_H_ORDEN_PAGO;
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
            var fechaPlazoDesde = (DateTime)dtos.FECHA_PLAZO_DESDE;
            itemResult.FechaPlazoDesdeString =Fecha.GetFechaString( fechaPlazoDesde);
            FechaDto fechaPlazoDesdeObj = Fecha.GetFechaDto((DateTime)dtos.FECHA_PLAZO_DESDE);
            itemResult.FechaPlazoDesdeObj = (FechaDto)fechaPlazoDesdeObj;
            itemResult.FechaPlazoHasta = dtos.FECHA_PLAZO_HASTA;
            var fechaPlazoHasta = (DateTime)dtos.FECHA_PLAZO_HASTA;
            itemResult.FechaPlazoHastaString = Fecha.GetFechaString(fechaPlazoHasta);
            FechaDto fechaPlazoHastaObj = Fecha.GetFechaDto((DateTime)dtos.FECHA_PLAZO_HASTA);
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
            itemResult.UsuarioHIns = dtos.USUARIO_H_INS;
            itemResult.FechaHIns = (DateTime)dtos.FECHA_H_INS;
            var fechaHIns = (DateTime)dtos.FECHA_INS;
            itemResult.FechaHInsString = Fecha.GetFechaString(fechaHIns);
            FechaDto fechaHInsObj = Fecha.GetFechaDto((DateTime)dtos.FECHA_H_INS);
            itemResult.FechaHInsObj = (FechaDto)fechaHInsObj;
            itemResult.CodigoPresupuesto = (int)dtos.CODIGO_PRESUPUESTO;
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
            var fechaComprobante = (DateTime)dtos.FECHA_COMPROBANTE;
            itemResult.FechaComprobanteString = Fecha.GetFechaString(fechaComprobante);
            FechaDto fechaComprobanteObj = Fecha.GetFechaDto((DateTime)dtos.FECHA_COMPROBANTE);
            itemResult.FechaComprobanteObj = (FechaDto)fechaComprobanteObj;
        

            return itemResult;
        }

        public async Task<List<AdmHOrdenPagoResponseDto>> MapListHOrdenPagoDto(List<ADM_H_ORDEN_PAGO> dtos)
        {
            List<AdmHOrdenPagoResponseDto> result = new List<AdmHOrdenPagoResponseDto>();
            

                foreach (var item in dtos)
                {
                  if (item == null) continue;
                    var itemResult = await MapHOrdenPagoDto(item);

                    result.Add(itemResult);
                }
                return result;
            
        }


        public async Task<ResultDto<List<AdmHOrdenPagoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmHOrdenPagoResponseDto>> result = new ResultDto<List<AdmHOrdenPagoResponseDto>>(null);
            try
            {
                var hOrdenPago = await _repository.GetAll();
                var cant = hOrdenPago.Count();
                if (hOrdenPago!=null && hOrdenPago.Count() > 0)
                {
                    var listDto = await MapListHOrdenPagoDto(hOrdenPago);

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



        public async Task<ResultDto<AdmHOrdenPagoResponseDto>> Update(AdmHOrdenPagoUpdateDto dto)
        {
            ResultDto<AdmHOrdenPagoResponseDto> result = new ResultDto<AdmHOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoHOrdenPago = await _repository.GetCodigoHOrdenPago(dto.CodigoHOrdenPago);
                if (codigoHOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Historico Orden Pago no existe";
                    return result;
                }

                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
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

                if (dto.Status == null && dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }

                if (dto.Motivo == null && dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }

                if(dto.UsuarioHIns < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario H Ins Invalido";
                    return result;

                }

                if (dto.FechaHIns == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha H Ins Invalida";
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


                codigoHOrdenPago.CODIGO_H_ORDEN_PAGO = dto.CodigoHOrdenPago;
                codigoHOrdenPago.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                codigoHOrdenPago.ANO = dto.ANO;
                codigoHOrdenPago.CODIGO_COMPROMISO = dto.CodigoCompromiso;
                codigoHOrdenPago.CODIGO_ORDEN_COMPRA = dto.CodigoOrdenCompra;
                codigoHOrdenPago.CODIGO_CONTRATO = dto.CodigoContrato;
                codigoHOrdenPago.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                codigoHOrdenPago.NUMERO_ORDEN_PAGO = dto.NumeroOrdenPago;
                codigoHOrdenPago.REFERENCIA_ORDEN_PAGO = dto.ReferenciaOrdenPago;
                codigoHOrdenPago.FECHA_ORDEN_PAGO = dto.FechaOrdenPago;
                codigoHOrdenPago.TIPO_ORDEN_PAGO_ID = dto.TipoOrdenPagoId;
                codigoHOrdenPago.FECHA_PLAZO_DESDE = dto.FechaPlazoDesde;
                codigoHOrdenPago.FECHA_PLAZO_HASTA = dto.FechaPlazoHasta;
                codigoHOrdenPago.CANTIDAD_PAGO = dto.CantidadPago;
                codigoHOrdenPago.NUMERO_PAGO = dto.NumeroPago;
                codigoHOrdenPago.FRECUENCIA_PAGO_ID = dto.FrecuenciaPagoId;
                codigoHOrdenPago.TIPO_PAGO_ID = dto.TipoPagoId;
                codigoHOrdenPago.NUMERO_VALUACION = dto.NumeroValuacion;
                codigoHOrdenPago.STATUS = dto.Status;
                codigoHOrdenPago.MOTIVO = dto.Motivo;
                codigoHOrdenPago.EXTRA1 = dto.Extra1;
                codigoHOrdenPago.EXTRA2 = dto.Extra2;
                codigoHOrdenPago.EXTRA3 = dto.Extra3;
                codigoHOrdenPago.USUARIO_H_INS = dto.UsuarioHIns;
                codigoHOrdenPago.FECHA_H_INS = dto.FechaHIns;
                codigoHOrdenPago.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoHOrdenPago.EXTRA4 = dto.Extra4;
                codigoHOrdenPago.EXTRA5 = dto.Extra5;
                codigoHOrdenPago.EXTRA6 = dto.Extra6;
                codigoHOrdenPago.EXTRA7 = dto.Extra7;
                codigoHOrdenPago.EXTRA8 = dto.Extra8;
                codigoHOrdenPago.EXTRA9 = dto.Extra9;
                codigoHOrdenPago.EXTRA10 = dto.Extra10;
                codigoHOrdenPago.EXTRA11 = dto.Extra11;
                codigoHOrdenPago.EXTRA12 = dto.Extra12;
                codigoHOrdenPago.EXTRA13 = dto.Extra13;
                codigoHOrdenPago.EXTRA14 = dto.Extra14;
                codigoHOrdenPago.EXTRA15 = dto.Extra15;
                codigoHOrdenPago.NUMERO_COMPROBANTE = dto.NumeroComprobante;
                codigoHOrdenPago.FECHA_COMPROBANTE = dto.FechaComprobante;
             


                
                codigoHOrdenPago.CODIGO_EMPRESA = conectado.Empresa;
                codigoHOrdenPago.USUARIO_UPD = conectado.Usuario;
                codigoHOrdenPago.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoHOrdenPago);

                var resultDto = await MapHOrdenPagoDto(codigoHOrdenPago);
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

        public async Task<ResultDto<AdmHOrdenPagoResponseDto>> Create(AdmHOrdenPagoUpdateDto dto)
        {
            ResultDto<AdmHOrdenPagoResponseDto> result = new ResultDto<AdmHOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoHOrdenPago = await _repository.GetCodigoHOrdenPago(dto.CodigoHOrdenPago);
                if (codigoHOrdenPago != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago ya existe";
                    return result;
                }

                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (dto.CodigoOrdenPago == null)
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

                if (dto.Status == null && dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }

                if (dto.Motivo == null && dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
                

                if (dto.UsuarioHIns < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario H Ins Invalido";
                    return result;

                }

                if (dto.FechaHIns == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha H Ins Invalida";
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

               

            ADM_H_ORDEN_PAGO entity = new ADM_H_ORDEN_PAGO();
            entity.CODIGO_H_ORDEN_PAGO = await _repository.GetNextKey();
            entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
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
           


            
            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapHOrdenPagoDto(created.Data);
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

        public async Task<ResultDto<AdmHOrdenPagoDeleteDto>> Delete(AdmHOrdenPagoDeleteDto dto) 
        {
            ResultDto<AdmHOrdenPagoDeleteDto> result = new ResultDto<AdmHOrdenPagoDeleteDto>(null);
            try
            {

                var codigoHOrdenPago = await _repository.GetCodigoHOrdenPago(dto.CodigoHOrdenPago);
                if (codigoHOrdenPago == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoHOrdenPago);

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

