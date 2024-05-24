using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Utility;

namespace Convertidor.Services.Bm
{
    public class BmSolMovBienesService: IBmSolMovBienesService
    {

      
        private readonly IBmSolMovBienesRepository _repository;
        private readonly IBmDescriptivaRepository _bmDescriptivaRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public BmSolMovBienesService(IBmSolMovBienesRepository repository,
                                  IBmDescriptivaRepository bmDescriptivaRepository, 
                                  ISisUsuarioRepository sisUsuarioRepository,
                                  IConfiguration configuration)
		{
            _repository = repository;
            _bmDescriptivaRepository = bmDescriptivaRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _configuration = configuration;
           

        }

      
        public async Task<BmSolMovBienesResponseDto> MapBmMovBienesDto(BM_SOL_MOV_BIENES dtos)
        {


            BmSolMovBienesResponseDto itemResult = new BmSolMovBienesResponseDto();
            itemResult.CodigoMovBien = dtos.CODIGO_MOV_BIEN;
            itemResult.CodigoBien = dtos.CODIGO_BIEN;
            itemResult.TipoMovimiento = dtos.TIPO_MOVIMIENTO;
            itemResult.FechaMovimiento = dtos.FECHA_MOVIMIENTO;
            itemResult.FechaMovimientoString =Fecha.GetFechaString(dtos.FECHA_MOVIMIENTO) ;
            FechaDto fechaMovimientoObj = Fecha.GetFechaDto(dtos.FECHA_MOVIMIENTO);
            itemResult.FechaMovimientoObj = (FechaDto)fechaMovimientoObj;
            itemResult.CodigoDirBien = dtos.CODIGO_DIR_BIEN;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;    
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.ConceptoMovId=dtos.CONCEPTO_MOV_ID;
            itemResult.CodigoSolMovBien = dtos.CODIGO_SOL_MOV_BIEN;
            itemResult.NumeroSolicitud = dtos.NUMERO_SOLICITUD;
            itemResult.Aprobado = dtos.APROBADO;
            itemResult.UsuarioSolicita = dtos.USUARIO_SOLICITA;
            itemResult.FechaSolicita = dtos.FECHA_SOLICITA;
            itemResult.FechaSolicitaString = dtos.FECHA_SOLICITA.ToString("u");
            FechaDto fechaSolicitaObj = Fecha.GetFechaDto(dtos.FECHA_SOLICITA);
            itemResult.FechaSolicitaObj = (FechaDto)fechaSolicitaObj;


            return itemResult;

        }
        public async Task<List<BmSolMovBienesResponseDto>> MapListBmMovBienesDto(List<BM_SOL_MOV_BIENES> dtos)
        {
            List<BmSolMovBienesResponseDto> result = new List<BmSolMovBienesResponseDto>();


            foreach (var item in dtos)
            {

                BmSolMovBienesResponseDto itemResult = new BmSolMovBienesResponseDto();
                itemResult = await MapBmMovBienesDto(item);
                
                result.Add(itemResult);
            }
            return result;
        }

        public async Task<BmSolMovBienesResponseDto> GetByCodigoMovBien(int codigoMovBien)
        {
            try
            {
                var movBien = await _repository.GetByCodigoMovBien(codigoMovBien);

                var result = await MapBmMovBienesDto(movBien);


                return (BmSolMovBienesResponseDto)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ResultDto<List<BmSolMovBienesResponseDto>>> GetAll()
        {

            ResultDto<List<BmSolMovBienesResponseDto>> result = new ResultDto<List<BmSolMovBienesResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<BmSolMovBienesResponseDto> listDto = new List<BmSolMovBienesResponseDto>();

                    foreach (var item in titulos)
                    {
                        BmSolMovBienesResponseDto dto = new BmSolMovBienesResponseDto();
                        dto = await MapBmMovBienesDto(item);

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

        


        public async Task<ResultDto<BmSolMovBienesResponseDto>> Update(BmSolMovBienesUpdateDto dto)
        {

            ResultDto<BmSolMovBienesResponseDto> result = new ResultDto<BmSolMovBienesResponseDto>(null);
            try
            {

                var codigoMovBien = await _repository.GetByCodigoMovBien(dto.CodigoMovBien);
                if (codigoMovBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo mov bien no existe";
                    return result;
                }
                var codigoBien = await _repository.GetByCodigoBien(dto.CodigoBien);
                if (codigoBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo bien invalido";
                    return result;
                }
                if (dto.TipoMovimiento is not null && 
                    dto.TipoMovimiento.Length<0 &&
                    dto.TipoMovimiento.Length>1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Movimiento Invalido";
                    return result;
                }
                if (dto.FechaMovimiento==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha movimiento Invalido";
                    return result;
                }
                if (dto.CodigoDirBien <0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo dir bien Invalido";
                    return result;
                }

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 invalido";
                    return result;
                }
               
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 invalido";
                    return result;
                }
                
                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 invalido";
                    return result;
                }

                var conceptoMovimientoId = await _bmDescriptivaRepository.GetByIdAndTitulo(4, dto.ConceptoMovId);
                if(conceptoMovimientoId==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto movimiento Id invalido";
                    return result;
                }

                if (dto.CodigoSolMovBien < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Mov Bien invalido";
                    return result;
                }

                if (dto.NumeroSolicitud is not null && dto.NumeroSolicitud.Length>20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
                    return result;
                }

                if (dto.Aprobado < 0 && dto.Aprobado>1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Aprobado invalido";
                    return result;
                }
               
                if (dto.UsuarioSolicita < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Solicita invalido";
                    return result;
                }

                if (dto.FechaSolicita == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Solicita invalida";
                    return result;
                }


                codigoMovBien.CODIGO_MOV_BIEN = dto.CodigoMovBien;
                codigoMovBien.CODIGO_BIEN = dto.CodigoBien;
                codigoMovBien.TIPO_MOVIMIENTO = dto.TipoMovimiento;
                codigoMovBien.FECHA_MOVIMIENTO = dto.FechaMovimiento;
                codigoMovBien.CODIGO_DIR_BIEN = dto.CodigoDirBien;
                codigoMovBien.EXTRA1 = dto.Extra1;
                codigoMovBien.EXTRA2 = dto.Extra2;
                codigoMovBien.EXTRA3 = dto.Extra3;
                codigoMovBien.CONCEPTO_MOV_ID=dto.ConceptoMovId;
                codigoMovBien.CODIGO_SOL_MOV_BIEN=dto.CodigoSolMovBien;
                codigoMovBien.NUMERO_SOLICITUD = dto.NumeroSolicitud;
                codigoMovBien.APROBADO = dto.Aprobado;
                codigoMovBien.USUARIO_SOLICITA = dto.UsuarioSolicita;
                codigoMovBien.FECHA_SOLICITA=dto.FechaSolicita;


                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoMovBien.CODIGO_EMPRESA = conectado.Empresa;
                codigoMovBien.USUARIO_UPD = conectado.Usuario;
                codigoMovBien.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoMovBien);

                var resultDto = await MapBmMovBienesDto(codigoMovBien);
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

        public async Task<ResultDto<BmSolMovBienesResponseDto>> Create(BmSolMovBienesUpdateDto dto)
        {

            ResultDto<BmSolMovBienesResponseDto> result = new ResultDto<BmSolMovBienesResponseDto>(null);
            try
            {

                var codigoMovBien = await _repository.GetByCodigoMovBien(dto.CodigoMovBien);
                if (codigoMovBien != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mov Bien ya existe";
                    return result;
                }
                var codigoBien = await _repository.GetByCodigoBien(dto.CodigoBien);
                if (codigoBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo bien invalido";
                    return result;
                }
                if (dto.TipoMovimiento is not null 
                    && dto.TipoMovimiento.Length < 0 
                    && dto.TipoMovimiento.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Movimiento Invalido";
                    return result;
                }
                if (dto.FechaMovimiento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha movimiento Invalido";
                    return result;
                }
                if (dto.CodigoDirBien < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo dir bien Invalido";
                    return result;
                }

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 invalido";
                    return result;
                }

                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 invalido";
                    return result;
                }

                var conceptoMovimientoId = await _bmDescriptivaRepository.GetByIdAndTitulo(4, dto.ConceptoMovId);
                if (conceptoMovimientoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto movimiento Id invalido";
                    return result;
                }

                if (dto.CodigoSolMovBien < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Mov Bien invalido";
                    return result;
                }

                if (dto.NumeroSolicitud is not null && dto.NumeroSolicitud.Length < 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Solicitud invalido";
                    return result;
                }

                if (dto.Aprobado < 0 && dto.Aprobado>1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Aprobado invalido";
                    return result;
                }
                
                if (dto.UsuarioSolicita < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Solicita invalido";
                    return result;
                }

                if (dto.FechaSolicita == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Solicita invalida";
                    return result;
                }

                BM_SOL_MOV_BIENES entity = new BM_SOL_MOV_BIENES();        
                entity.CODIGO_MOV_BIEN = await _repository.GetNextKey();
                entity.CODIGO_BIEN = dto.CodigoBien;
                entity.TIPO_MOVIMIENTO = dto.TipoMovimiento;
                entity.FECHA_MOVIMIENTO = dto.FechaMovimiento;
                entity.CODIGO_DIR_BIEN = dto.CodigoDirBien;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CONCEPTO_MOV_ID = dto.ConceptoMovId;
                entity.CODIGO_SOL_MOV_BIEN = dto.CodigoSolMovBien;
                entity.NUMERO_SOLICITUD = dto.NumeroSolicitud;
                entity.APROBADO = dto.Aprobado;
                entity.USUARIO_SOLICITA = dto.UsuarioSolicita;
                entity.FECHA_SOLICITA = dto.FechaSolicita;

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var resultDto = await MapBmMovBienesDto(created.Data);
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

        public async Task<ResultDto<BmSolMovBienesDeleteDto>> Delete(BmSolMovBienesDeleteDto dto)
        {

            ResultDto<BmSolMovBienesDeleteDto> result = new ResultDto<BmSolMovBienesDeleteDto>(null);
            try
            {

                var codigoMovBien = await _repository.GetByCodigoMovBien(dto.CodigoMovBien);
                if (codigoMovBien == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Mov Bien no existe";
                    return result;
                }

              
                var deleted = await _repository.Delete(dto.CodigoMovBien);

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

