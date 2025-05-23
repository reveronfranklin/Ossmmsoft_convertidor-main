using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmPagoElectronicoRepository : IAdmPagoElectronicoRepository

    {
    private readonly DataContextAdm _context;

    public AdmPagoElectronicoRepository(DataContextAdm context)
    {
        _context = context;
    }





    public async Task<ResultDto<List<ADM_PAGOS_ELECTRONICOS>>> GetByLote(int codigoEmpresa,int codigoLote,int codigoPresupuesto,int usuario)
    {

        ResultDto<List<ADM_PAGOS_ELECTRONICOS>> result = new ResultDto<List<ADM_PAGOS_ELECTRONICOS>>(null);

        try
        {

            FormattableString xquerySaldo = $"CALL ADM_P_PAGO_ELECTRONICO_BANESCO({codigoLote},{codigoPresupuesto},{codigoEmpresa},{usuario})";
            var resultQUERY = await _context.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);

            var pagos = await _context.ADM_PAGOS_ELECTRONICOS
                .Where(e => e.CODIGO_LOTE == codigoLote).ToListAsync();


            result.CantidadRegistros = pagos.Count;
            result.TotalPage = 1;
            result.Page = 1;
            result.IsValid = true;
            result.Message = "";
            result.Data = pagos;
            return result;
        }
        catch (Exception ex)
        {
            result.CantidadRegistros = 0;
            result.IsValid = false;
            result.Message = ex.Message;
            result.Data = null;
            return result;
        }
    }


    }
}
