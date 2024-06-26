﻿namespace Convertidor.Data.Repository.Rh
{
	public class RhDescriptivasService: IRhDescriptivasService
    {
		




   
        private readonly IRhDescriptivaRepository _repository;

     

        public RhDescriptivasService(IRhDescriptivaRepository repository)
        {
            _repository = repository;
          
        }
        public async Task<RH_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId)
        {
            try
            {
                var descriptiva = await _repository.GetByCodigoDescriptiva(descripcionId);
              


                return descriptiva;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<string> GetDescripcionByCodigoDescriptiva(int descripcionId)
        {
            try
            {
                var descriptiva = await _repository.GetByCodigoDescriptiva(descripcionId);
                var result = string.Empty;
                if (descriptiva != null)
                {
                    result = descriptiva.DESCRIPCION;
                }


                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        public async Task<List<SelectListDescriptiva>> GetByTitulo(int tituloId)
        {

            List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
            try
            {
                var descriptivas = await _repository.GetByTituloId(tituloId);
             
                if (descriptivas.Count>0)
                {

                    foreach (var item in descriptivas)
                    {
                        
                        SelectListDescriptiva resultItem  = new SelectListDescriptiva();
                        
                        resultItem.Id = item.DESCRIPCION_ID;
                        
                        resultItem.Descripcion = item.DESCRIPCION;
                        result.Add(resultItem);
                        
                        
                    }
                }


                return result.OrderBy(x=>x.Descripcion).ToList();
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_DESCRIPTIVAS>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll();
             


                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

    }
}

