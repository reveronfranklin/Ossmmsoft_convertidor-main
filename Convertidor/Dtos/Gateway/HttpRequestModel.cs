public class HttpRequestModel
{
    public string Method { get; set; } = "GET";
    public string Url { get; set; } = string.Empty;
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public object Body { get; set; }
    public int TimeoutSeconds { get; set; } = 30;
}