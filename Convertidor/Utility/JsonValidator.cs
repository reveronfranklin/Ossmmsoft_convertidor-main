
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
namespace Convertidor.Utility;

public class JsonValidator
{
    public static bool IsValidJson(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return false;
        }

        jsonString = jsonString.Trim();
        
        if ((jsonString.StartsWith("{") && jsonString.EndsWith("}")) || // Para objetos JSON
            (jsonString.StartsWith("[") && jsonString.EndsWith("]")))   // Para arrays JSON
        {
            try
            {
                var obj = JToken.Parse(jsonString);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
