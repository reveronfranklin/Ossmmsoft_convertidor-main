using System;
using System.Globalization;

namespace Convertidor.Utility
{
	public class ConvertidorMoneda
	{
	
        public static string ConvertMoneda(decimal value)
        {
            string literal = "";
            if (value == 0)
            {
                literal = "Bs. 0.0";
            }
            else
            {
                literal = "Bs. " + value.ToString("N", CultureInfo.InvariantCulture);
            }


            return literal;
        }
    }
}

