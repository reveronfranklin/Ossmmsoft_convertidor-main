using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class SisSerieDocumentosRepository: Interfaces.Sis.ISisSerieDocumentosRepository
    {
		

        private readonly DataContextSis _context;
        private readonly IConfiguration _configuration;
        public SisSerieDocumentosRepository(DataContextSis context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<SIS_SERIE_DOCUMENTOS>> GetALL()
        {
            try
            {
                var result = await _context.SIS_SERIE_DOCUMENTOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        

        public async Task<SIS_SERIE_DOCUMENTOS> GetById(int id)
        {
            try
            {
                var result = await _context.SIS_SERIE_DOCUMENTOS.DefaultIfEmpty().Where(x => x.CODIGO_SERIE_DOCUMENTO == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<SIS_SERIE_DOCUMENTOS>> Add(SIS_SERIE_DOCUMENTOS entity)
        {
            ResultDto<SIS_SERIE_DOCUMENTOS> result = new ResultDto<SIS_SERIE_DOCUMENTOS>(null);
            try
            {

                entity.CODIGO_SERIE_DOCUMENTO = await GetNextKey();

                await _context.SIS_SERIE_DOCUMENTOS.AddAsync(entity);
                await _context.SaveChangesAsync();


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

        public async Task<ResultDto<SIS_SERIE_DOCUMENTOS>> Update(SIS_SERIE_DOCUMENTOS entity)
        {
            ResultDto<SIS_SERIE_DOCUMENTOS> result = new ResultDto<SIS_SERIE_DOCUMENTOS>(null);

            try
            {
                SIS_SERIE_DOCUMENTOS entityUpdate = await GetById(entity.CODIGO_SERIE_DOCUMENTO);
                if (entityUpdate != null)
                {


                    _context.SIS_SERIE_DOCUMENTOS.Update(entity);
                    await _context.SaveChangesAsync();
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

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.SIS_SERIE_DOCUMENTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_SERIE_DOCUMENTO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_SERIE_DOCUMENTO + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

        public async Task Execute(string codigo)
        {
            try
            {
                //FormattableString xqueryDiario =  $"DECLARE VALOR VARCHAR2(20); \nBEGIN\n VALOR:= SIS.SIS_F_SERIE_DOCUMENTOS('SERIE_COMPUESTA_ACTUAL','{codigo}',13, -1);\nEND;";
                FormattableString xqueryDiario =  $"CALL SIS.SIS_P_SERIE_DOCUMENTOS('SERIE_COMPUESTA_ACTUAL','{codigo}',13, -1,?);";

                var a = $"CALL SIS.SIS_P_SERIE_DOCUMENTOS";
                var resultDiario = await _context.Database.ExecuteSqlInterpolatedAsync(xqueryDiario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
        
        public async Task<string> GenerateNextSerie(int tipoDocumentoId,string codigo)
        {

            string result = "";
            try
            {

           
                
                var serieDocumentos = await _context.SIS_SERIE_DOCUMENTOS.DefaultIfEmpty().Where(x => x.TIPO_DOCUMENTO_ID == tipoDocumentoId && x.FECHA_VIGENCIA_FIN == null).FirstOrDefaultAsync();
                if (serieDocumentos != null)
                {
                    var serieCompuesta = "";
                    serieDocumentos.NUMERO_SERIE_ACTUAL =serieDocumentos.NUMERO_SERIE_ACTUAL+ 1;
                    int number =  serieDocumentos.NUMERO_SERIE_ACTUAL;;
                    string paddedNumber = number.ToString().PadLeft(serieDocumentos.MAX_DIGITOS, '0');
                    serieCompuesta = $"{serieDocumentos.SERIE_LETRAS}{paddedNumber}";
                    result = serieCompuesta;
                    serieDocumentos.SERIE_COMPUESTA_ACTUAL = serieCompuesta;
                    _context.SIS_SERIE_DOCUMENTOS.Update(serieDocumentos);
                    await _context.SaveChangesAsync();
                    

                }
                else
                {
                    result = "";
                }
                return result;
                
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
        
        public async Task<string> GenerateNextSerieOracle(int tipoDocumentoId,string codigo)
        {

            string result = "";
            try
            {

                await Execute(codigo);
                
                var serieDocumentos = await _context.SIS_SERIE_DOCUMENTOS.DefaultIfEmpty().Where(x => x.TIPO_DOCUMENTO_ID == tipoDocumentoId && x.FECHA_VIGENCIA_FIN == null).FirstOrDefaultAsync();
                if (serieDocumentos != null)
                {
                    result = serieDocumentos.SERIE_COMPUESTA_ACTUAL;
                }
                else
                {
                    result = "";
                }
                return result;
                
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        

    }

}

