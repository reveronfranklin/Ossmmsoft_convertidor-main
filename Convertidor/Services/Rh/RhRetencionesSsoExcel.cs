using Convertidor.Dtos.Rh;
using Ganss.Excel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Convertidor.Data.Repository.Rh;

public static class RhRetencionesSsoExcel
{
    public static void Save(string path, List<RhTmpRetencionesSsoDto> data)
    {
        new ExcelMapper().Save(path, data, "RetencionesSSO", true);
        if (data.Count == 0) return;

        XSSFWorkbook workbook;
        using (var input = File.OpenRead(path)) workbook = new XSSFWorkbook(input);
        using (workbook)
        {
            var sheet = workbook.GetSheetAt(0);
            var header = sheet.GetRow(0);
            var total = sheet.CreateRow(data.Count + 1);
            var amountFields = new HashSet<string>
            {
                nameof(RhTmpRetencionesSsoDto.MontoSsoTrabajador),
                nameof(RhTmpRetencionesSsoDto.MontoRpeTrabajador),
                nameof(RhTmpRetencionesSsoDto.MontoSsoPatrono),
                nameof(RhTmpRetencionesSsoDto.MontoRpePatrono),
                nameof(RhTmpRetencionesSsoDto.MontoTotalRetencion)
            };
            var format = workbook.CreateCellStyle();
            format.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00");
            for (var column = 0; column < header.LastCellNum; column++)
            {
                var name = header.GetCell(column)?.StringCellValue;
                if (name == nameof(RhTmpRetencionesSsoDto.NombresApellidos))
                    total.CreateCell(column).SetCellValue("TOTAL");
                if (name == null || !amountFields.Contains(name)) continue;
                for (var row = 1; row <= data.Count; row++)
                    sheet.GetRow(row).GetCell(column).CellStyle = format;
                var letter = CellReference.ConvertNumToColString(column);
                var cell = total.CreateCell(column);
                cell.SetCellFormula($"ROUND(SUM({letter}2:{letter}{data.Count + 1}),2)");
                cell.CellStyle = format;
                sheet.SetColumnWidth(column, Math.Max(name.Length + 2, 20) * 256);
            }
            workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll();
            using var output = File.Create(path);
            workbook.Write(output);
        }
    }
}
