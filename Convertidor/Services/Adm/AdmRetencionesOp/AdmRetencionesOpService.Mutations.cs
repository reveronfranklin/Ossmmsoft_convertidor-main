using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;


namespace Convertidor.Services.Adm.AdmRetencionesOp
{
    // Usa 'partial' para indicar que la clase se define en múltiples archivos
    public partial class AdmRetencionesOpService
    {
        public async Task<ResultDto<AdmRetencionesOpResponseDto>> Update(AdmRetencionesOpUpdateDto dto)
        {
            ResultDto<AdmRetencionesOpResponseDto> result = new ResultDto<AdmRetencionesOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoRetencionOp = await _repository.GetCodigoRetencionOp(dto.CodigoRetencionOp);
                if (codigoRetencionOp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retencion op no existe";
                    return result;
                }
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }
                var tipoRetencionId = await _admDescriptivaRepository.GetByCodigo(dto.TipoRetencionId);
                if (tipoRetencionId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo retencion Id invalido";
                    return result;
                }

                var retenciones = await _admRetencionesRepository.GetAll();
                var retencionesPorTipo =
                    retenciones.Where(x => x.TIPO_RETENCION_ID == dto.TipoRetencionId).FirstOrDefault();
                if (retencionesPorTipo != null)
                {
                    var admRetencion = await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
                    if (admRetencion == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Codigo retencion invalido";
                        return result;
                    }
                }
                if (dto.PorRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por retencion invalido";
                    return result;
                }
                if (dto.MontoRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retencion invalido";
                    return result;
                }

                if (dto.NumeroComprobante != null && dto.NumeroComprobante.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "La longitud Maxima del Comprobante es de 20 digitos";
                    return result;
                }


                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                if (dto.BaseImponible < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible Invalida";
                    return result;
                }


                codigoRetencionOp.CODIGO_RETENCION_OP = dto.CodigoRetencionOp;
                codigoRetencionOp.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                codigoRetencionOp.TIPO_RETENCION_ID = dto.TipoRetencionId;
                codigoRetencionOp.CODIGO_RETENCION = dto.CodigoRetencion;
                codigoRetencionOp.POR_RETENCION = dto.PorRetencion;
                codigoRetencionOp.MONTO_RETENCION = dto.MontoRetencion;

                codigoRetencionOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoRetencionOp.BASE_IMPONIBLE = dto.BaseImponible;

                codigoRetencionOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoRetencionOp.USUARIO_UPD = conectado.Usuario;
                codigoRetencionOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoRetencionOp);

                await ReplicaRetencionesEnAdmBeneficiariosOp(dto.CodigoOrdenPago);
                await ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(dto.CodigoOrdenPago);

                var resultDto = await MapRetencionesOpDto(codigoRetencionOp);
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


        public async Task<string> GetNumeroComprobanteIva(string codigoTipoRetencion,int codigoPesupuesto,int codigoOrdenPago)
        {
            string result = "";
         
            
            var ordenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(codigoOrdenPago);
            if (ordenPago != null )
            {
                if (ordenPago.NUMERO_COMPROBANTE > 0)
                {
                    result = ordenPago.NUMERO_COMPROBANTE.ToString();
              
                }
                else
                {
                    var sisDescriptiva = await _sisDescriptivaRepository.GetByExtra1(codigoTipoRetencion);
                    if (sisDescriptiva != null)
                    {
                        var numeroSolicitud = await _serieDocumentosRepository.GenerateNextSerie(codigoPesupuesto, sisDescriptiva.DESCRIPCION_ID, sisDescriptiva.CODIGO_DESCRIPCION);
                        result = numeroSolicitud.Data;
                        ordenPago.NUMERO_COMPROBANTE=decimal.Parse(result);
                        await _admOrdenPagoRepository.Update(ordenPago);
                    }

                }
               
                
            }
        
            
           
            return result;

        } 
        

        public async Task<ResultDto<AdmRetencionesOpResponseDto>> Create(AdmRetencionesOpUpdateDto dto)
        {
            ResultDto<AdmRetencionesOpResponseDto> result = new ResultDto<AdmRetencionesOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoRetencionOp = await _repository.GetCodigoRetencionOp(dto.CodigoRetencionOp);
                if (codigoRetencionOp != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retención orden pago ya existe";
                    return result;
                }
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }
                var tipoRetencion = await _admDescriptivaRepository.GetByCodigo(dto.TipoRetencionId);
                if (tipoRetencion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo retención Id invalido";
                    return result;
                }

                var retenciones = await _admRetencionesRepository.GetAll();
                var retencionesPorTipo =
                    retenciones.Where(x => x.TIPO_RETENCION_ID == dto.TipoRetencionId).FirstOrDefault();
                if (retencionesPorTipo != null)
                {
                    var admRetencion = await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
                    if (admRetencion == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Codigo retención invalido";
                        return result;
                    }
                }

                if (dto.PorRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por retención invalido";
                    return result;
                }
                if (dto.MontoRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retención invalido";
                    return result;
                }


                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                if (dto.BaseImponible < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible Invalida";
                    return result;
                }

                var retencionOp =
                    await _repository.GetByOrdenPagoCodigoRetencionTipoRetencionPorcentaje(
                        dto.CodigoOrdenPago, dto.CodigoRetencion, dto.TipoRetencionId, dto.PorRetencion);
                if (retencionOp != null)
                {
                    var admRetencion = await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
                    
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Ya existe esta retención para la Orden de Pago:{dto.CodigoOrdenPago}- {tipoRetencion.DESCRIPCION}-{admRetencion.CONCEPTO_PAGO}-{dto.PorRetencion}%";
                    return result;
                }

                ADM_RETENCIONES_OP entity = new ADM_RETENCIONES_OP();
                entity.CODIGO_RETENCION_OP = await _repository.GetNextKey();
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.TIPO_RETENCION_ID = dto.TipoRetencionId;
                entity.CODIGO_RETENCION = dto.CodigoRetencion;
                entity.POR_RETENCION = dto.PorRetencion;
                entity.MONTO_RETENCION = dto.MontoRetencion;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.BASE_IMPONIBLE = dto.BaseImponible;
                if (tipoRetencion.CODIGO == "ISLR")
                {
                    string paddedNumber = dto.CodigoOrdenPago.ToString().PadLeft(8, '0');

                    var mes = DateTime.Now.Month.ToString().PadLeft(2, '0');
                    var serieLetras = $"{DateTime.Now.Year}{mes} ";
                    entity.NUMERO_COMPROBANTE = $"{serieLetras.Trim()}{paddedNumber.Trim()}";
                }
                else
                {
                    entity.NUMERO_COMPROBANTE = await GetNumeroComprobanteIva(tipoRetencion.CODIGO,(int)entity.CODIGO_PRESUPUESTO,dto.CodigoOrdenPago);
                  
                }


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    
                    await ReplicaRetencionesEnAdmBeneficiariosOp(dto.CodigoOrdenPago);
                    await ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(dto.CodigoOrdenPago);

                    var resultDto = await MapRetencionesOpDto(created.Data);
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
    }
}