using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm.Replica
{
    public class BmReplicaRepository : IBmReplicaRepository
    {

        private readonly DataContextBm _context;
        private readonly DataContextBmConteo _contextBmConteo;
        private readonly DataContextRhC _contextRhC;
        private readonly DataContext _dataContextRh;

        public BmReplicaRepository(DataContextBm context, DataContextBmConteo contextBmConteo,DataContextRhC contextRhC,DataContext dataContextRh)
        {
            _context = context;
            _contextBmConteo = contextBmConteo;
            _contextRhC = contextRhC;
            _dataContextRh = dataContextRh;
        }



        public async Task<List<BM_ARTICULOS>> GetAllArticulos()
        {
            try
            {
                var result = await _context.BM_ARTICULOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<BM_ARTICULOS>> GetAllArticulosConteo()
        {
            try
            {
                var result = await _contextBmConteo.BM_ARTICULOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task ReplicarArticulos()
        {
            try
            {
                var articulos = await GetAllArticulos();
                //var articulosConteo = await GetAllArticulosConteo();
                if (articulos != null && articulos.Count > 0)
                {

                    FormattableString xquerySaldo = $"DELETE FROM BMC.\"BM_ARTICULOS\"";
                    await _contextBmConteo.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);
                    await _contextBmConteo.BM_ARTICULOS.AddRangeAsync(articulos);
                    await _contextBmConteo.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }


        public async Task<List<BM_BIENES>> GetAllBienes()
        {
            try
            {
                var result = await _context.BM_BIENES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task ReplicarBienes()
        {
            var bienes = await GetAllBienes();
            if (bienes != null && bienes.Count > 0)
            {

                FormattableString xquerySaldo = $"DELETE FROM BMC.\"BM_BIENES\"";
                await _contextBmConteo.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);
                await _contextBmConteo.BM_BIENES.AddRangeAsync(bienes);
                await _contextBmConteo.SaveChangesAsync();
            }

        }

        public async Task<List<BM_MOV_BIENES>> GetAllMovimientosBienes()
        {
            try
            {
                var result = await _context.BM_MOV_BIENES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task ReplicarMovimientosBienes()
        {
            var bienes = await GetAllMovimientosBienes();
            if (bienes != null && bienes.Count > 0)
            {

                FormattableString xquerySaldo = $"DELETE FROM BMC.\"BM_MOV_BIENES\"";
                await _contextBmConteo.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);
                await _contextBmConteo.BM_MOV_BIENES.AddRangeAsync(bienes);
                await _contextBmConteo.SaveChangesAsync();
            }

        }



        public async Task<List<BM_DIR_BIEN>> GetAllDireccionesBienes()
        {
            try
            {
                var result = await _context.BM_DIR_BIEN.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task ReplicarDireccionesBienes()
        {
            var bienes = await GetAllDireccionesBienes();
            if (bienes != null && bienes.Count > 0)
            {

                FormattableString xquerySaldo = $"DELETE FROM BMC.\"BM_DIR_BIEN\"";
                await _contextBmConteo.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);
                await _contextBmConteo.BM_DIR_BIEN.AddRangeAsync(bienes);
                await _contextBmConteo.SaveChangesAsync();
            }

        }

        
        
        public async Task<List<BM_CLASIFICACION_BIENES>> GetAllClasificacionesBienes()
        {
            try
            {
                var result = await _context.BM_CLASIFICACION_BIENES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
         public async Task ReplicarClasificacionesBienes()
        {
            var bienes = await GetAllClasificacionesBienes();
            if (bienes != null && bienes.Count > 0)
            {

                FormattableString xquerySaldo = $"DELETE FROM BMC.\"BM_CLASIFICACION_BIENES\"";
                await _contextBmConteo.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);
                await _contextBmConteo.BM_CLASIFICACION_BIENES.AddRangeAsync(bienes);
                await _contextBmConteo.SaveChangesAsync();
            }

        }


        public async Task<List<RH_PERSONAS>> GetAllPersonas()
        {
            try
            {
                var result = await _dataContextRh.RH_PERSONAS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

          public async Task ReplicarPersonas()
        {
            var personas = await GetAllPersonas();
            if (personas != null && personas.Count > 0)
            {

                FormattableString xquerySaldo = $"DELETE FROM BMC.\"RH_PERSONAS\"";
                await _dataContextRh.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);
                await _dataContextRh.RH_PERSONAS.AddRangeAsync(personas);
                await _dataContextRh.SaveChangesAsync();
            }

        }


        




    }
}

