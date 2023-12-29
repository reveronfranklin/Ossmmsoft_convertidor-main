using System;
using Convertidor.Dtos;

namespace Convertidor.Utility
{
	public class DateValidate
	{
		
        public static Boolean IsDate(String fecha)
        {
            try
            {
                DateTime.Parse(fecha);
                return true;
            }
            catch
            {
                return false;
            }
        }
     


    }
}

