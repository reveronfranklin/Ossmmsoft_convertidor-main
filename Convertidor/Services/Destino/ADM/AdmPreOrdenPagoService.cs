
using Convertidor.Data.DestinoInterfaces.ADM;
using Convertidor.Data.DestinoInterfaces.PRE;
using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.EntitiesDestino.PRE;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.PreOrdenPago;
using Convertidor.Utility;
using ADM_DOCUMENTOS_OP = Convertidor.Data.Entities.Adm.ADM_DOCUMENTOS_OP;


namespace Convertidor.Services.Destino.ADM
{
    public class AdmPreOrdenPagoService:IAdmPreOrdenPagoService
    {
        private readonly IMapper _mapper;
        private readonly IAdmPreOrdenPagoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;


        public AdmPreOrdenPagoService(
                        IMapper mapper,
                        IAdmPreOrdenPagoRepository repository,
                        ISisUsuarioRepository sisUsuarioRepository
                      
                        )
        {
            _mapper = mapper;
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public AdmPreOdenPagoResponseDto MapPreOrdenPagoDto(ADM_PRE_ORDEN_PAGO dtos)
        {
            AdmPreOdenPagoResponseDto result = new AdmPreOdenPagoResponseDto();

            result.Id = dtos.Id;
            result.NombreEmisor = dtos.NombreEmisor;
            result.DireccionEmisor = dtos.DireccionEmisor;
            result.Rif = dtos.Rif;
            result.NumeroFactura = dtos.NumeroFactura;
            result.FechaEmision = dtos.FechaEmision;
            result.BaseImponible = dtos.BaseImponible;
            result.PorcentajeIva = dtos.PorcentajeIva;
            result.Iva = dtos.Iva;
            result.MontoTotal = dtos.MontoTotal;
            result.Excento = dtos.Excento;
          
            
            return result;
        }

        public async Task<ResultDto<List<AdmPreOdenPagoResponseDto>>> GetAll(AdmPreOrdenPagoFilterDto dto)
        {
            ResultDto<List<AdmPreOdenPagoResponseDto>> result = new ResultDto<List<AdmPreOdenPagoResponseDto>>(null);
            
            List<AdmPreOdenPagoResponseDto> list = new List<AdmPreOdenPagoResponseDto>();
            var data = await _repository.GetAll(dto);
            if (data.Data != null)
            {
                foreach (var item in data.Data)
                {
                    if (item != null)
                    {
                        var resultDto = MapPreOrdenPagoDto(item);
                        list.Add(resultDto);
                    }
                   
                }
            }
            result.Data = list;
            result.IsValid = true;
            result.Message = "";
            
            return result;
        }

        
        public async Task<ResultDto<bool>> CreateLote(List<AdmPreOdenPagoCreateDto> dto)
        {
            ResultDto<bool> result = new ResultDto<bool>(true);

            foreach (var item in dto)
            {
                var created = await Create(item);
                result.IsValid = created.IsValid;
                result.Message = created.Message;
               
            }
            return result;
        }
        
        public async Task<ResultDto<bool>> DeleteAll()
        {
            ResultDto<bool> result = new ResultDto<bool>(true);

            var deleted= await _repository.DeleteALL();
            result.IsValid = deleted.IsValid;
            result.Message = deleted.Message;
            return result;
        }
        
        public async Task<ResultDto<AdmPreOdenPagoResponseDto>> Create(AdmPreOdenPagoCreateDto dto)
        {
            ResultDto<AdmPreOdenPagoResponseDto> result = new ResultDto<AdmPreOdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                
                var factura = await _repository.GetByRifNumeroFactura(dto.Rif,dto.NumeroFactura);
                if (factura == null)
                {
                    ADM_PRE_ORDEN_PAGO entity = new ADM_PRE_ORDEN_PAGO();
                    entity.Id = await _repository.GetNextKey();
                    entity.NombreEmisor = dto.NombreEmisor;
                    entity.DireccionEmisor = dto.DireccionEmisor;
                    entity.Rif = dto.Rif;
                    entity.NumeroFactura = dto.NumeroFactura;
                    entity.FechaEmision = dto.FechaEmision;
                    entity.BaseImponible = dto.BaseImponible;
                
                
                    entity.PorcentajeIva = dto.PorcentajeIva;
                    entity.Iva = dto.Iva;
                
                    entity.MontoTotal = dto.MontoTotal;
                    entity.Excento = dto.Excento;
                    entity.SEARCH_TEXT = dto.SearchText;
                

                    entity.CODIGO_EMPRESA = conectado.Empresa;
                    entity.USUARIO_INS = dto.UsuarioConectado;
                    entity.FECHA_INS = DateTime.Now;

                    var created = await _repository.Add(entity);
                    if (created.IsValid && created.Data != null)
                    {
                        var resultDto = MapPreOrdenPagoDto(created.Data);
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
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Ya existe el registro para esta Factura: {dto.NumeroFactura}";
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

