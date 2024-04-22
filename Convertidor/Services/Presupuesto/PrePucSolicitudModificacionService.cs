using Convertidor.Data.Interfaces.Presupuesto;

namespace Convertidor.Services.Presupuesto;

public class PrePucSolicitudModificacionService: IPrePucSolicitudModificacionService
{
      
        private readonly IPrePucSolicitudModificacionRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _presupuestosService;
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;
        private readonly IPrePlanUnicoCuentasService _prePlanUnicoCuentasService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_V_SALDOSRepository _preVSaldosRepository;
        private readonly IPreAsignacionesDetalleRepository _preAsignacionesDetalleRepository;

        public PrePucSolicitudModificacionService(    IPrePucSolicitudModificacionRepository repository,
                                        IPRE_PRESUPUESTOSRepository presupuestosService,
                                        IIndiceCategoriaProgramaService indiceCategoriaProgramaService,
                                        IPrePlanUnicoCuentasService prePlanUnicoCuentasService,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IPRE_V_SALDOSRepository preVSaldosRepository,
                                        IPreAsignacionesDetalleRepository preAsignacionesDetalleRepository)
        {
            _repository = repository;
            _presupuestosService = presupuestosService;
            _indiceCategoriaProgramaService = indiceCategoriaProgramaService;
            _prePlanUnicoCuentasService = prePlanUnicoCuentasService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _preVSaldosRepository = preVSaldosRepository;
            _preAsignacionesDetalleRepository = preAsignacionesDetalleRepository;
        }


        public async Task<bool> PresupuestoExiste(int codPresupuesto)
        {
            return await _repository.PresupuestoExiste(codPresupuesto);
        }

        public async Task<bool> ICPExiste(int codigoICP)
        {
            return await _repository.ICPExiste(codigoICP);
        }

        public async Task<bool> PUCExiste(int codigoPUC)
        {
            return await _repository.PUCExiste(codigoPUC);
        }
}