using System.Text;

namespace Convertidor.Utility
{
	public class LimpiarCaracteres
	{
	
        public static string LimpiarEnter(string textoConMultiplesEnters)
        {
            string literal = "";
    
            
            StringBuilder sb = new StringBuilder();
            bool primerCaracter = true;

            foreach (char c in textoConMultiplesEnters)
            {
                if (c != '\t' || primerCaracter)
                {
                    sb.Append(c);
                    primerCaracter = false;
                }
            }

          
            literal = sb.ToString();

            
            
            
            return literal.Trim();
        }
    }
}

