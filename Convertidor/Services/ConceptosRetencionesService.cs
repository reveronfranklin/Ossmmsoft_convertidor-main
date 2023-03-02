using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Convertidor.Dtos;

namespace Convertidor.Services
{
    public class ConceptosRetencionesService: IConceptosRetencionesService
    {

        private readonly IConceptosRetencionesRepository _repository;
      

        public ConceptosRetencionesService(IConceptosRetencionesRepository repository)
                               
        {
            _repository = repository;
           
        }

        public async Task<ResultDto<ConceptosRetenciones>> CrearConceptosRetencionBase()
        {

            ResultDto<ConceptosRetenciones> result = new ResultDto<ConceptosRetenciones>(null);

            List<ConceptosRetenciones> destinoList = new List<ConceptosRetenciones>();

            try
            {

                await _repository.DeleteAll();
                destinoList.Add(new ConceptosRetenciones { Titulo= "FAOV",CODIGO_CONCEPTO= 1691 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1672 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1657 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1668 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1448 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1443 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1428 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1376 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1540 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 932 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1451 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1440 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1412 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1407 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FAOV", CODIGO_CONCEPTO = 1535 });

                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1677 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1674 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1655 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1666 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1557 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1374 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1538 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1426 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1441 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1427 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1589 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 926 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1531 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 1438 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "SSO", CODIGO_CONCEPTO = 969 });

                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1678 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1665 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1673 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1656 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1667 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1661 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1558 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1375 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1539 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1427 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1589 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 930 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1532 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 1408 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "PIE", CODIGO_CONCEPTO = 964 });

                destinoList.Add(new ConceptosRetenciones { Titulo = "FJP", CODIGO_CONCEPTO = 1645 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FJP", CODIGO_CONCEPTO = 1662 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FJP", CODIGO_CONCEPTO = 1675 });
                destinoList.Add(new ConceptosRetenciones { Titulo = "FJP", CODIGO_CONCEPTO = 1676 });
                                               


                var adicionados = await _repository.Add(destinoList);
                result.IsValid = true;
                result.Message = $" {destinoList.ToList().Count} Registros Transferidos";
                return result;


            }
            catch (Exception ex)
            {

                result.IsValid = false;
                result.Message = ex.InnerException.Message;
                return result;

            }
        }

        public async Task<ConceptosRetenciones> GetByKey(long codigoConcepto, string titulo)
        {
            return await _repository.GetByKey(codigoConcepto, titulo);
        }
        public async Task<ConceptosRetenciones> GetByConcepto(long codigoConcepto)
        {
            return await _repository.GetByConcepto(codigoConcepto);
        }

    }
}
