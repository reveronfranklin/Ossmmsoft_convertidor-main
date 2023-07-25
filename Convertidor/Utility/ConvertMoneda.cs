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
                literal = "0,00";
            }
            else
            {
                // literal = "Bs. " + value.ToString("n");
                // literal = "Bs. " + value.ToString("n");
                literal = $"{value:N2}";

            }


            return literal;
        }
    }
}

