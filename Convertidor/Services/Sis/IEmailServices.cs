namespace Convertidor.Services.Sis;

public interface IEmailServices
{
    void SendEmail(EmailDto request);
   byte[] GetFile(string pdfFilePath);

}