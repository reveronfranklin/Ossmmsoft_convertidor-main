using Convertidor.Data;
using Convertidor.Data.Entities;
using Convertidor.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Convertidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoNominaDestinoController : ControllerBase
    {
    
        private readonly IHistoricoNominaRepository _repository;

        public HistoricoNominaDestinoController(IHistoricoNominaRepository repository)
        {  
            _repository = repository;
           
        }

        // GET: api/<HistoricoNominaController>
        [HttpGet]
        public async Task<IEnumerable<RH_HISTORICO_NOMINA>> Get()
        {
            try
            {
                var result = await _repository.Get();
                return (IEnumerable<RH_HISTORICO_NOMINA>)result;
            }
            catch (Exception ex )
            {
                var msg = ex.InnerException.Message;
                return null;
            }
           

           
        }

        // GET api/<HistoricoNominaController>/5
        [HttpGet("{dias}")]
        public async Task<IEnumerable<RH_HISTORICO_NOMINA>> Get(int dias)
        {
            try
            {
                var result = await _repository.GetByLastDay(dias);
                return (IEnumerable<RH_HISTORICO_NOMINA>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }
        }

       


       

        // POST api/<HistoricoNominaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HistoricoNominaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HistoricoNominaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
