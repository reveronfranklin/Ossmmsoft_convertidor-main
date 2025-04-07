namespace Convertidor.Utility
{
	public class FechaObj
	{
		
     
        public static FechaDto GetFechaDto(DateTime fecha)
        {
            var fechaDesdeObj = new FechaDto();
            fechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            fechaDesdeObj.Month = month.Substring(month.Length - 2);
            fechaDesdeObj.Day = day.Substring(day.Length - 2);
    
            return fechaDesdeObj;
        }
        
        public static string GetFechaString(DateTime? fecha)
        {
            var result = "";
            try
            {
              
                if (fecha != null)
                {
                    
                    string dia = fecha.Value.Day.ToString();
                    result =$"{fecha.Value.Day.ToString()}/{fecha.Value.Month.ToString()}/{fecha.Value.Year.ToString()}";
                }
         

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return result;
              
            }
           
          
        }


    }
}

