using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmChequesService : IAdmChequesService
    {
        private readonly IAdmChequesRepository _repository;
        private readonly IAdmProveedoresRepository _proveedoresRepository;
        private readonly IAdmContactosProveedorRepository _contactosProveedorRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmChequesService( IAdmChequesRepository repository,
                                      IAdmProveedoresRepository proveedoresRepository,
                                      IAdmContactosProveedorRepository contactosProveedorRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                      IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _proveedoresRepository = proveedoresRepository;
            _contactosProveedorRepository = contactosProveedorRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

       

        public async Task<AdmChequesResponseDto> MapChequesDto(ADM_CHEQUES dtos)
        {
            AdmChequesResponseDto itemResult = new AdmChequesResponseDto();

            itemResult.CodigoCheque = dtos.CODIGO_CHEQUE;
            itemResult.Ano = dtos.ANO;
            itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
            itemResult.NumeroChequera = dtos.NUMERO_CHEQUERA;
            itemResult.NumeroCheque = dtos.NUMERO_CHEQUE;
            itemResult.FechaCheque = dtos.FECHA_CHEQUE;
            itemResult.FechaChequeString = Fecha.GetFechaString(dtos.FECHA_CHEQUE);
            FechaDto fechaChequeObj = Fecha.GetFechaDto(dtos.FECHA_CHEQUE);
            itemResult.FechaChequeObj =(FechaDto) fechaChequeObj;
            itemResult.FechaConciliacion = dtos.FECHA_CONCILIACION;
            itemResult.FechaConciliacionString = Fecha.GetFechaString(dtos.FECHA_CONCILIACION);
            FechaDto fechaConciliacionObj = Fecha.GetFechaDto(dtos.FECHA_CONCILIACION);
            itemResult.FechaConciliacionObj =(FechaDto)fechaConciliacionObj;
            itemResult.FechaAnulacion = dtos.FECHA_ANULACION;
            itemResult.FechaAnulacionString =Fecha.GetFechaString(dtos.FECHA_ANULACION);
            FechaDto fechaanulacionObj = Fecha.GetFechaDto(dtos.FECHA_ANULACION);
            itemResult.FechaAnulacionObj = (FechaDto)fechaanulacionObj;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.CodigoContactoProveedor = dtos.CODIGO_CONTACTO_PROVEEDOR;
            itemResult.PrintCount = dtos.PRINT_COUNT;
            itemResult.Motivo = dtos.MOTIVO;
            itemResult.Status = dtos.STATUS;
            itemResult.Endoso = dtos.ENDOSO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.TipoBeneficiario = dtos.TIPO_BENEFICIARIO;
            itemResult.TipoChequeID = dtos.TIPO_CHEQUE_ID;
            itemResult.FechaEntrega =dtos.FECHA_ENTREGA;
            itemResult.FechaEntregaString =Fecha.GetFechaString(dtos.FECHA_ENTREGA);
            FechaDto fechaEntregaObj = Fecha.GetFechaDto(dtos.FECHA_ENTREGA);
            itemResult.FechaEntregaObj = (FechaDto) fechaEntregaObj;
           


            return itemResult;
        }

        public async Task<List<AdmChequesResponseDto>> MapListChequesDto(List<ADM_CHEQUES> dtos)
        {
            List<AdmChequesResponseDto> result = new List<AdmChequesResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapChequesDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmChequesResponseDto>>> GetAll()
        {

            ResultDto<List<AdmChequesResponseDto>> result = new ResultDto<List<AdmChequesResponseDto>>(null);
            try
            {
                var cheques = await _repository.GetAll();
                var cant = cheques.Count();
                if (cheques != null && cheques.Count() > 0)
                {
                    var listDto = await MapListChequesDto(cheques);

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
        public async Task<ResultDto<AdmChequesResponseDto>> Update(AdmChequesUpdateDto dto)
        {
            ResultDto<AdmChequesResponseDto> result = new ResultDto<AdmChequesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoCheque = await _repository.GetByCodigoCheque(dto.CodigoCheque);
                if (codigoCheque == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cheque no existe";
                    return result;
                }

                
                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano invalido";
                    return result;

                }

                if (dto.CodigoCuentaBanco < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta Banco invalido";
                    return result;
                }
                if (dto.NumeroChequera < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Chequera Invalido";
                    return result;
                }

                if (dto.NumeroCheque == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Cheque Invalido";
                    return result;
                }

               

                if (dto.FechaCheque == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Cheque Invalida";
                    return result;
                }
                if (dto.FechaConciliacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Conciliacion Invalida";
                    return result;

                }
                if (dto.FechaAnulacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Anulacion Invalida";
                    return result;

                }

                var codigoProveedor = await _proveedoresRepository.GetByCodigo(dto.CodigoProveedor);

                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }


                var codigoContactoProveedor = await _contactosProveedorRepository.GetByCodigo(dto.CodigoContactoProveedor);
                if (dto.CodigoContactoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Contacto Proveedor Invalido";
                    return result;

                }

                if (dto.PrintCount < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "PrintCount Invalido";
                    return result;

                }

                if (dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;

                }
                if (dto.Endoso is not null && dto.Endoso.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ensoso Invalido";
                    return result;

                }

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
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

                if(dto.TipoBeneficiario.Length < 1) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Beneficiario Invalido";
                    return result;
                }

                var tipoChequeID = await _admDescriptivaRepository.GetByIdAndTitulo(23, dto.TipoChequeID);
                if(dto.TipoChequeID < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo ChequeId Invalido";
                    return result;
                }




                codigoCheque.CODIGO_CHEQUE = dto.CodigoCheque;
                codigoCheque.ANO = dto.Ano;
                codigoCheque.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                codigoCheque.NUMERO_CHEQUERA = dto.NumeroChequera;
                codigoCheque.NUMERO_CHEQUE = dto.NumeroCheque;
                codigoCheque.FECHA_CHEQUE = dto.FechaCheque;
                codigoCheque.FECHA_CONCILIACION = dto.FechaConciliacion;
                codigoCheque.FECHA_ANULACION = dto.FechaAnulacion;
                codigoCheque.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                codigoCheque.CODIGO_CONTACTO_PROVEEDOR = dto.CodigoContactoProveedor;
                codigoCheque.PRINT_COUNT = dto.PrintCount;
                codigoCheque.MOTIVO = dto.Motivo;
                codigoCheque.STATUS = dto.Status;
                codigoCheque.ENDOSO = dto.Endoso;
                codigoCheque.EXTRA1 = dto.Extra1;
                codigoCheque.EXTRA2 = dto.Extra2;
                codigoCheque.EXTRA3 = dto.Extra3;
                codigoCheque.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoCheque.TIPO_BENEFICIARIO = dto.TipoBeneficiario;
                codigoCheque.TIPO_CHEQUE_ID = dto.TipoChequeID;
                codigoCheque.FECHA_ENTREGA = dto.FechaEntrega;
                codigoCheque.USUARIO_ENTREGA = dto.UsuarioEntrega;


                codigoCheque.CODIGO_EMPRESA = conectado.Empresa;
                codigoCheque.USUARIO_UPD = conectado.Usuario;
                codigoCheque.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoCheque);

                var resultDto = await MapChequesDto(codigoCheque);
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

        public async Task<ResultDto<AdmChequesResponseDto>> Create(AdmChequesUpdateDto dto)
        {
            ResultDto<AdmChequesResponseDto> result = new ResultDto<AdmChequesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoCheque = await _repository.GetByCodigoCheque(dto.CodigoCheque);
                if (codigoCheque != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo cheque Ya existe";
                    return result;
                }

                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano invalido";
                    return result;

                }

                if (dto.CodigoCuentaBanco < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta Banco invalido";
                    return result;
                }
                if (dto.NumeroChequera < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Chequera Invalido";
                    return result;
                }

                if (dto.NumeroCheque == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Cheque Invalido";
                    return result;
                }



                if (dto.FechaCheque == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Cheque Invalida";
                    return result;
                }
                if (dto.FechaConciliacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Conciliacion Invalida";
                    return result;

                }
                if (dto.FechaAnulacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Anulacion Invalida";
                    return result;

                }

                var codigoProveedor = await _proveedoresRepository.GetByCodigo(dto.CodigoProveedor);

                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }


                var codigoContactoProveedor = await _contactosProveedorRepository.GetByCodigo(dto.CodigoContactoProveedor);
                if (dto.CodigoContactoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Contacto Proveedor Invalido";
                    return result;

                }

                if (dto.PrintCount < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "PrintCount Invalido";
                    return result;

                }

                if (dto.Motivo.Length > 2000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;

                }
                if (dto.Endoso is not null && dto.Endoso.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ensoso Invalido";
                    return result;

                }

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
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

                if (dto.TipoBeneficiario.Length < 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Beneficiario Invalido";
                    return result;
                }

                var tipoChequeID = await _admDescriptivaRepository.GetByIdAndTitulo(23, dto.TipoChequeID);
                if (dto.TipoChequeID < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo ChequeId Invalido";
                    return result;
                }



                ADM_CHEQUES entity = new ADM_CHEQUES();

                entity.CODIGO_CHEQUE = dto.CodigoCheque;
                entity.ANO = dto.Ano;
                entity.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                entity.NUMERO_CHEQUERA = dto.NumeroChequera;
                entity.NUMERO_CHEQUE = dto.NumeroCheque;
                entity.FECHA_CHEQUE = dto.FechaCheque;
                entity.FECHA_CONCILIACION = dto.FechaConciliacion;
                entity.FECHA_ANULACION = dto.FechaAnulacion;
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.CODIGO_CONTACTO_PROVEEDOR = dto.CodigoContactoProveedor;
                entity.PRINT_COUNT = dto.PrintCount;
                entity.MOTIVO = dto.Motivo;
                entity.STATUS = dto.Status;
                entity.ENDOSO = dto.Endoso;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.TIPO_BENEFICIARIO = dto.TipoBeneficiario;
                entity.TIPO_CHEQUE_ID = dto.TipoChequeID;
                entity.FECHA_ENTREGA = dto.FechaEntrega;
                entity.USUARIO_ENTREGA = dto.UsuarioEntrega;




            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapChequesDto(created.Data);
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

        public async Task<ResultDto<AdmChequesDeleteDto>> Delete(AdmChequesDeleteDto dto) 
        {
            ResultDto<AdmChequesDeleteDto> result = new ResultDto<AdmChequesDeleteDto>(null);
            try
            {

                var codigoCheques = await _repository.GetByCodigoCheque(dto.CodigoCheque);
                if (codigoCheques == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo cheque Contrato no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoCheque);

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

