using Convertidor.Dtos;

namespace Convertidor.Services.Sis;

public class UtilityService:IUtilityService
{
    public FechaDto GetFechaDto(DateTime fecha)
    {
        var FechaDesdeObj = new FechaDto();
        FechaDesdeObj.Year = fecha.Year.ToString();
        string month = "00" + fecha.Month.ToString();
        string day = "00" + fecha.Day.ToString();
        FechaDesdeObj.Month = month.Substring(month.Length - 2);
        FechaDesdeObj.Day = day.Substring(day.Length - 2);
    
        return FechaDesdeObj;
    }
    
    public List<string> GetListNacionalidad()
    {
        List<string> result = new List<string>();
        result.Add("V");
        result.Add("E");
        return result;
    }
    public List<string> GetListSexo()
    {
        List<string> result = new List<string>();
        result.Add("M");
        result.Add("F");
        return result;
    }
    public List<string> GetListStatus()
    {
        List<string> result = new List<string>();
        result.Add("A");
        result.Add("E");
        result.Add("S");
        return result;
    }
    public List<string> GetListManoHabil()
    {
        List<string> result = new List<string>();
        result.Add("D");
        result.Add("I");
          
        return result;
    }

}