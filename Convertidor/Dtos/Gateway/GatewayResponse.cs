public class GatewayResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
   
       public object Content { get; set; } 
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public string ErrorMessage { get; set; } = string.Empty;
    public TimeSpan ResponseTime { get; set; }
}