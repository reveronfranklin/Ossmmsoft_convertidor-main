using System;
using Convertidor.Data.Entities;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Convertidor.Dtos.PetroBsGetDto;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class RhRelacionCargosRepository : IRhRelacionCargosRepository
    {
		
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        public RhRelacionCargosRepository(DataContext context, IConfiguration configuration, ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
            _configuration = configuration;
        }


     
        public async Task<List<RH_RELACION_CARGOS>> GetAll()
        {
            try
            {

                var result = await _context.RH_RELACION_CARGOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<List<RH_RELACION_CARGOS>> GetAllByPreCodigoRelacionCargos(int preCodigoRelaciconCargo)
        {
            try
            {

                var result = await _context.RH_RELACION_CARGOS.Where(x=>x.CODIGO_RELACION_CARGO_PRE== preCodigoRelaciconCargo).DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }


    

        public async Task<RH_RELACION_CARGOS> GetByCodigo(int codigoRelacionCargo)
        {
            try
            {

                var result = await _context.RH_RELACION_CARGOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_RELACION_CARGO == codigoRelacionCargo)
                    .FirstOrDefaultAsync();
                return (RH_RELACION_CARGOS)result!;
               
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }

    

  
  

        public async Task<ResultDto<RH_RELACION_CARGOS>> Add(RH_RELACION_CARGOS entity)
        {
            ResultDto<RH_RELACION_CARGOS> result = new ResultDto<RH_RELACION_CARGOS>(null);
            try
            {

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;
                await _context.RH_RELACION_CARGOS.AddAsync(entity);
                _context.SaveChanges();


                result.Data = entity;
                result.IsValid = true;
                result.Message = "";
                return result;

            
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
            


        }

        public async Task<ResultDto<RH_RELACION_CARGOS>> Update(RH_RELACION_CARGOS entity)
        {
            ResultDto<RH_RELACION_CARGOS> result = new ResultDto<RH_RELACION_CARGOS>(null);
          
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                RH_RELACION_CARGOS entityUpdate = await GetByCodigo(entity.CODIGO_RELACION_CARGO);
                if (entityUpdate != null)
                {

                    entityUpdate.CODIGO_EMPRESA = conectado.Empresa;
                    entityUpdate.USUARIO_UPD = conectado.Usuario;
                    entityUpdate.FECHA_UPD = DateTime.Now;

                    _context.RH_RELACION_CARGOS.Update(entity);
                    _context.SaveChanges();
                    result.Data = entity;
                    result.IsValid = true;
                    result.Message = "";
                    
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }

        public async Task<string> Delete(int codigoRelacionCargo)
        {

            try
            {
                RH_RELACION_CARGOS entity = await GetByCodigo(codigoRelacionCargo);
                if (entity != null)
                {
                    _context.RH_RELACION_CARGOS.Remove(entity);
                    _context.SaveChanges();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
         


        }

   

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.RH_RELACION_CARGOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_RELACION_CARGO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result= last.CODIGO_RELACION_CARGO + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

        
        public async Task<RH_RELACION_CARGOS> GetUltimoCargoPorPersona(int codigoPersona)
        {
            try
            {

                var result = await _context.RH_RELACION_CARGOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PERSONA == codigoPersona)
                    .OrderByDescending(x=>x.FECHA_INI)
                    .FirstOrDefaultAsync();
                return (RH_RELACION_CARGOS)result!;
               
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }

    }
}

