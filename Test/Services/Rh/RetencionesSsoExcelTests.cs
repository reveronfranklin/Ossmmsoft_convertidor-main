using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Rh;
using NPOI.XSSF.UserModel;
using Xunit;

namespace Convertidor.Tests.Services.Rh;

public class RetencionesSsoExcelTests
{
    [Fact]
    public void ExcelConservaCentavosYTotalizaCincoMontos()
    {
        var path = Path.Combine(Path.GetTempPath(), $"sso-{Guid.NewGuid()}.xlsx");
        try
        {
            RhRetencionesSsoExcel.Save(path, new()
            {
                new() { MontoSsoTrabajador = 30m, MontoRpeTrabajador = 3.84m,
                    MontoSsoPatrono = 75m, MontoRpePatrono = 15.36m, MontoTotalRetencion = -124.20m },
                new() { MontoSsoTrabajador = 10m, MontoRpeTrabajador = 0m,
                    MontoSsoPatrono = 25m, MontoRpePatrono = 0m, MontoTotalRetencion = -35.25m }
            });
            using var input = File.OpenRead(path);
            using var workbook = new XSSFWorkbook(input);
            var sheet = workbook.GetSheetAt(0);
            Assert.Equal(3, sheet.LastRowNum);
            var expected = new Dictionary<string, double>
            {
                [nameof(RhTmpRetencionesSsoDto.MontoSsoTrabajador)] = 40,
                [nameof(RhTmpRetencionesSsoDto.MontoRpeTrabajador)] = 3.84,
                [nameof(RhTmpRetencionesSsoDto.MontoSsoPatrono)] = 100,
                [nameof(RhTmpRetencionesSsoDto.MontoRpePatrono)] = 15.36,
                [nameof(RhTmpRetencionesSsoDto.MontoTotalRetencion)] = -159.45
            };
            var checkedAmounts = 0;
            for (var column = 0; column < sheet.GetRow(0).LastCellNum; column++)
            {
                var name = sheet.GetRow(0).GetCell(column).StringCellValue;
                if (name == nameof(RhTmpRetencionesSsoDto.NombresApellidos))
                    Assert.Equal("TOTAL", sheet.GetRow(3).GetCell(column).StringCellValue);
                if (!expected.TryGetValue(name, out var amount)) continue;
                var total = sheet.GetRow(3).GetCell(column);
                Assert.Contains("2:", total.CellFormula);
                Assert.Equal(amount, total.NumericCellValue, 2);
                Assert.Equal("#,##0.00", total.CellStyle.GetDataFormatString());
                Assert.Equal("#,##0.00", sheet.GetRow(1).GetCell(column).CellStyle.GetDataFormatString());
                if (name == nameof(RhTmpRetencionesSsoDto.MontoTotalRetencion))
                    Assert.Equal(-124.20, sheet.GetRow(1).GetCell(column).NumericCellValue, 2);
                checkedAmounts++;
            }
            Assert.Equal(5, checkedAmounts);
        }
        finally { File.Delete(path); }
    }
}
