using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;


namespace Convertidor.Services.Adm.AdmRetencionesOp
{
    // Usa 'partial' para indicar que la clase se define en múltiples archivos
    public partial class AdmRetencionesOpService
    {
        public async Task<ResultDto<AdmRetencionesOpDeleteDto>> Delete(AdmRetencionesOpDeleteDto dto)
        {
            ResultDto<AdmRetencionesOpDeleteDto> result = new ResultDto<AdmRetencionesOpDeleteDto>(null);
            try
            {

                
                
                
                var codigoRetencionOp = await _repository.GetCodigoRetencionOp(dto.CodigoRetencionOp);
                if (codigoRetencionOp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo retencion op no existe";
                    return result;
                }

                var ordenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(codigoRetencionOp.CODIGO_ORDEN_PAGO);
                if (ordenPago != null && ordenPago.STATUS == "AP")
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "No puede eliminar, Orden de Pago APROBADA";
                    return result;
                }
                

                var deleted = await _repository.Delete(dto.CodigoRetencionOp);

                await ReplicaRetencionesEnAdmBeneficiariosOp(codigoRetencionOp.CODIGO_ORDEN_PAGO);
                await ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(codigoRetencionOp.CODIGO_ORDEN_PAGO);

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