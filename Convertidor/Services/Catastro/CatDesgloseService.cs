using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatDesgloseService : ICatDesgloseService
    {
        private readonly ICatDesgloseRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CatDesgloseService(ICatDesgloseRepository repository,
                                  ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CatDesgloseResponseDto> MapCatDesglose(CAT_DESGLOSE entity)
        {
            CatDesgloseResponseDto dto = new CatDesgloseResponseDto();

            dto.CodigoDesglose = entity.CODIGO_DESGLOSE;
            dto.CodigoDesgloseFk = entity.CODIGO_DESGLOSE_FK;
            dto.CodigoDesglosePk = entity.CODIGO_DESGLOSE_PK;
            dto.CodigoParcela = entity.CODIGO_PARCELA;
            dto.CodigoCatastro = entity.CODIGO_CATASTRO;
            dto.Titulo = entity.TITULO;
            dto.AreaTerrenoTotal = entity.AREA_TERRENO_TOTAL;
            dto.AreaConTrucTotal = entity.AREA_CONTRUC_TOTAL;
            dto.AreaTrrTotalVendi = entity.AREA_TRR_TOTAL_VENDI;
            dto.AreaTerrComun = entity.AREA_TERR_COMUN;
            dto.AreaContrucComun = entity.AREA_CONTRUC_COMUN;
            dto.AreaTerrSinCond = entity.AREA_TERR_SIN_COND;
            dto.Area = entity.AREA;
            dto.EstacionaTerr = entity.ESTACIONA_TERR;
            dto.EstacionaContruc = entity.ESTACIONA_CONTRUC;
            dto.PorcentajCondominio = entity.PORCENTAJ_CONDOMINIO;
            dto.ManualTerreno = entity.MANUAL_TERRENO;
            dto.ManualConstruccion = entity.MANUAL_CONSTRUCCION;
            dto.MaleteroTerreno = entity.MALETERO_TERRENO;
            dto.MaleteroConstruccion = entity.MALETERO_CONSTRUCCION;
            dto.Observacion = entity.OBSERVACION;
            dto.NivelId = entity.NIVEL_ID;
            dto.UnidadId = entity.UNIDAD_ID;
            dto.TipoOperacionId = entity.TIPO_OPERACION_ID;
            dto.TipoTransaccion = entity.TIPO_TRANSACCION;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra1String = dto.Extra1.ToString("u");
            FechaDto extra1Obj = FechaObj.GetFechaDto(dto.Extra1);
            dto.Extra1Obj = (FechaDto)extra1Obj;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.Extra4 = entity.EXTRA4;
            dto.Extra5 = entity.EXTRA5;
            


            return dto;

        }

        public async Task<List<CatDesgloseResponseDto>> MapListCatDesglose(List<CAT_DESGLOSE> dtos)
        {
            List<CatDesgloseResponseDto> result = new List<CatDesgloseResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCatDesglose(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDesgloseResponseDto>>> GetAll()
        {

            ResultDto<List<CatDesgloseResponseDto>> result = new ResultDto<List<CatDesgloseResponseDto>>(null);
            try
            {
                var desglose = await _repository.GetAll();
                var cant = desglose.Count();
                if (desglose != null && desglose.Count() > 0)
                {
                    var listDto = await MapListCatDesglose(desglose);

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

        public async Task<ResultDto<CatDesgloseResponseDto>> Create(CatDesgloseUpdateDto dto)
        {

            ResultDto<CatDesgloseResponseDto> result = new ResultDto<CatDesgloseResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoDesgloseFk <= 0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Desglose Fk Invalido ";
                    return result;
                }

                if (dto.CodigoDesglosePk <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Desglose Pk Invalido";
                    return result;

                }

                if (dto.CodigoParcela <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Parcela Invalido";
                    return result;

                }



                if (dto.CodigoCatastro.Length > 30)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Catastro Invalido";
                    return result;

                }

                if (dto.Titulo.Length > 500)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;

                }

                if (dto.AreaTerrenoTotal <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Terreno Total Invalida";
                    return result;

                }

                if (dto.AreaConTrucTotal <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Contruccion total Invalida";
                    return result;

                }

                if (dto.AreaTrrTotalVendi <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Terreno Total vendido Invalida";
                    return result;

                }

                if (dto.AreaTerrComun <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Terreno Comun Invalida";
                    return result;

                }

                if (dto.AreaContrucComun <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Construccion Comun Invalida";
                    return result;

                }

                if (dto.AreaTerrSinCond <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Terreno Sin Condicion Invalida";
                    return result;

                }

                if (dto.AreaContrucSinCond <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Construccion Sin Condicion Invalida";
                    return result;

                }


                if (dto.Area <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area  Invalida";
                    return result;

                }

                if (dto.EstacionaTerr <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estaciona Terreno Invalida";
                    return result;

                }

                if (dto.EstacionaContruc <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estaciona Construccion Invalida";
                    return result;

                }

                if (dto.PorcentajCondominio <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Porcentaje Condominio Invalido";
                    return result;

                }

                if (dto.ManualTerreno <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Manual Terreno Invalido";
                    return result;

                }

                if (dto.ManualConstruccion <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Manual Construccion Invalida";
                    return result;

                }

                if (dto.MaleteroTerreno <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Maletero Terreno Invalido";
                    return result;

                }

                if (dto.MaleteroConstruccion <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Maletero Construccion Invalido";
                    return result;

                }

                if (dto.Observacion.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observacion Invalida";
                    return result;

                }

                if (dto.NivelId.Length > 3)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nivel Id Invalido";
                    return result;

                }

                if (dto.UnidadId.Length > 3)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nivel Id Invalido";
                    return result;

                }

                if (dto.TipoOperacionId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Operacion Id Invalido";
                    return result;

                }

                if (dto.TipoTransaccion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Invalida";
                    return result;

                }


                if (dto.Extra1 == null)
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

                if (dto.Extra4 is not null && dto.Extra4.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra4 Invalido";
                    return result;
                }
                if (dto.Extra5 is not null && dto.Extra5.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra5 Invalido";
                    return result;
                }

                



                CAT_DESGLOSE entity = new CAT_DESGLOSE();
                entity.CODIGO_DESGLOSE = await _repository.GetNextKey();
                entity.CODIGO_DESGLOSE_FK = dto.CodigoDesgloseFk;
                entity.CODIGO_DESGLOSE_PK = dto.CodigoDesglosePk;
                entity.CODIGO_PARCELA = dto.CodigoParcela;
                entity.CODIGO_CATASTRO = dto.CodigoCatastro;
                entity.TITULO = dto.Titulo;
                entity.AREA_TERRENO_TOTAL = dto.AreaTerrenoTotal;
                entity.AREA_CONTRUC_TOTAL = dto.AreaConTrucTotal;
                entity.AREA_TRR_TOTAL_VENDI = dto.AreaTrrTotalVendi;
                entity.AREA_TERR_COMUN = dto.AreaTerrComun;
                entity.AREA_CONTRUC_COMUN = dto.AreaContrucComun;
                entity.AREA_TERR_SIN_COND = dto.AreaTerrSinCond;
                entity.AREA_CONTRUC_SIN_COND = dto.AreaContrucSinCond;
                entity.AREA = dto.Area;
                entity.ESTACIONA_TERR = dto.EstacionaTerr;
                entity.ESTACIONA_CONTRUC = dto.EstacionaContruc;
                entity.PORCENTAJ_CONDOMINIO = dto.PorcentajCondominio;
                entity.MANUAL_TERRENO = dto.ManualTerreno;
                entity.MANUAL_CONSTRUCCION = dto.ManualConstruccion;
                entity.MALETERO_TERRENO = dto.MaleteroTerreno;
                entity.MALETERO_CONSTRUCCION = dto.MaleteroConstruccion;
                entity.OBSERVACION = dto.Observacion;
                entity.NIVEL_ID = dto.NivelId;
                entity.UNIDAD_ID = dto.UnidadId;
                entity.TIPO_OPERACION_ID = dto.TipoOperacionId;
                entity.TIPO_TRANSACCION = dto.TipoTransaccion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.EXTRA4 = dto.Extra4;
                entity.EXTRA5 = dto.Extra5;
 


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCatDesglose(created.Data);
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

        public async Task<ResultDto<CatDesgloseResponseDto>> Update(CatDesgloseUpdateDto dto)
        {

            ResultDto<CatDesgloseResponseDto> result = new ResultDto<CatDesgloseResponseDto>(null);
            try
            {


                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoDesglose = await _repository.GetByCodigo(dto.CodigoDesglose);



                if (codigoDesglose == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Desglose Invalido";
                    return result;

                }

                if (dto.CodigoDesgloseFk <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Desglose Fk Invalido ";
                    return result;
                }

                if (dto.CodigoDesglosePk <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Desglose Pk Invalido";
                    return result;

                }

                if (dto.CodigoParcela <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Parcela Invalido";
                    return result;

                }



                if (dto.CodigoCatastro.Length > 30)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Catastro Invalido";
                    return result;

                }

                if (dto.Titulo.Length > 500)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;

                }

                if (dto.AreaTerrenoTotal <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Terreno Total Invalida";
                    return result;

                }

                if (dto.AreaConTrucTotal <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Contruccion total Invalida";
                    return result;

                }

                if (dto.AreaTrrTotalVendi <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Terreno Total vendido Invalida";
                    return result;

                }

                if (dto.AreaTerrComun <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Terreno Comun Invalida";
                    return result;

                }

                if (dto.AreaContrucComun <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Construccion Comun Invalida";
                    return result;

                }

                if (dto.AreaTerrSinCond <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Terreno Sin Condicion Invalida";
                    return result;

                }

                if (dto.AreaContrucSinCond <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Construccion Sin Condicion Invalida";
                    return result;

                }


                if (dto.Area <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area  Invalida";
                    return result;

                }

                if (dto.EstacionaTerr <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estaciona Terreno Invalida";
                    return result;

                }

                if (dto.EstacionaContruc <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estaciona Construccion Invalida";
                    return result;

                }

                if (dto.PorcentajCondominio <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Porcentaje Condominio Invalido";
                    return result;

                }

                if (dto.ManualTerreno <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Manual Terreno Invalido";
                    return result;

                }

                if (dto.ManualConstruccion <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Manual Construccion Invalida";
                    return result;

                }

                if (dto.MaleteroTerreno <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Maletero Terreno Invalido";
                    return result;

                }

                if (dto.MaleteroConstruccion <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Maletero Construccion Invalido";
                    return result;

                }

                if (dto.Observacion.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observacion Invalida";
                    return result;

                }

                if (dto.NivelId.Length > 3)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nivel Id Invalido";
                    return result;

                }

                if (dto.UnidadId.Length > 3)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nivel Id Invalido";
                    return result;

                }

                if (dto.TipoOperacionId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Operacion Id Invalido";
                    return result;

                }

                if (dto.TipoTransaccion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Invalida";
                    return result;

                }


                if (dto.Extra1 == null)
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

                if (dto.Extra4 is not null && dto.Extra4.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra4 Invalido";
                    return result;
                }
                if (dto.Extra5 is not null && dto.Extra5.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra5 Invalido";
                    return result;
                }


                codigoDesglose.CODIGO_DESGLOSE = dto.CodigoDesglose;
                codigoDesglose.CODIGO_DESGLOSE_FK = dto.CodigoDesgloseFk;
                codigoDesglose.CODIGO_DESGLOSE_PK = dto.CodigoDesglosePk;
                codigoDesglose.CODIGO_PARCELA = dto.CodigoParcela;
                codigoDesglose.CODIGO_CATASTRO = dto.CodigoCatastro;
                codigoDesglose.TITULO = dto.Titulo;
                codigoDesglose.AREA_TERRENO_TOTAL = dto.AreaTerrenoTotal;
                codigoDesglose.AREA_CONTRUC_TOTAL = dto.AreaConTrucTotal;
                codigoDesglose.AREA_TRR_TOTAL_VENDI = dto.AreaTrrTotalVendi;
                codigoDesglose.AREA_TERR_COMUN = dto.AreaTerrComun;
                codigoDesglose.AREA_CONTRUC_COMUN = dto.AreaContrucComun;
                codigoDesglose.AREA_TERR_SIN_COND = dto.AreaTerrSinCond;
                codigoDesglose.AREA_CONTRUC_SIN_COND = dto.AreaContrucSinCond;
                codigoDesglose.AREA = dto.Area;
                codigoDesglose.ESTACIONA_TERR = dto.EstacionaTerr;
                codigoDesglose.ESTACIONA_CONTRUC = dto.EstacionaContruc;
                codigoDesglose.PORCENTAJ_CONDOMINIO = dto.PorcentajCondominio;
                codigoDesglose.MANUAL_TERRENO = dto.ManualTerreno;
                codigoDesglose.MANUAL_CONSTRUCCION = dto.ManualConstruccion;
                codigoDesglose.MALETERO_TERRENO = dto.MaleteroTerreno;
                codigoDesglose.MALETERO_CONSTRUCCION = dto.MaleteroConstruccion;
                codigoDesglose.OBSERVACION = dto.Observacion;
                codigoDesglose.NIVEL_ID = dto.NivelId;
                codigoDesglose.UNIDAD_ID = dto.UnidadId;
                codigoDesglose.TIPO_OPERACION_ID = dto.TipoOperacionId;
                codigoDesglose.TIPO_TRANSACCION = dto.TipoTransaccion;
                codigoDesglose.EXTRA1 = dto.Extra1;
                codigoDesglose.EXTRA2 = dto.Extra2;
                codigoDesglose.EXTRA3 = dto.Extra3;
                codigoDesglose.EXTRA4 = dto.Extra4;
                codigoDesglose.EXTRA5 = dto.Extra5;


                codigoDesglose.CODIGO_EMPRESA = conectado.Empresa;
                codigoDesglose.USUARIO_INS = conectado.Usuario;
                codigoDesglose.FECHA_INS = DateTime.Now;

                await _repository.Update(codigoDesglose);
                var resultDto = await MapCatDesglose(codigoDesglose);
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
    }
}
