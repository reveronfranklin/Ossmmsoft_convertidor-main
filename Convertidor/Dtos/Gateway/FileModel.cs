public class FileModel
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
    public string FormFieldName { get; set; }
}