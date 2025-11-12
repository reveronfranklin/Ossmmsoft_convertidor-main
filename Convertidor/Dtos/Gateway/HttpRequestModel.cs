

public class HttpRequestModel
{
    public string Url { get; set; }
    public string Method { get; set; } = "GET";
    public Dictionary<string, string> Headers { get; set; } = new();
    public Dictionary<string, string> FormData { get; set; } = new();
    public List<FileModel> Files { get; set; }
    public string Body { get; set; } 
    public int TimeoutMs { get; set; } = 30000;
     //public int TimeoutSeconds { get; set; } = 30000;
    
}