using Convertidor.Dtos;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace Convertidor.Services.Sis;

public class EmailServices:IEmailServices
{
    private readonly IConfiguration _configuration;
    
    public EmailServices(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public byte[] GetFile(string pdfFilePath)
    {
        
       
        byte[] bytes = System.IO.File.ReadAllBytes(pdfFilePath);
        return bytes;
        
    }
    public void SendEmail(EmailDto request)
    {

      
        
        
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:UserName").Value));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject= request.Subject;
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = request.Content

        };

        if (request.FilePath.Length > 0)
        {
            var ruta = _configuration.GetSection("settings:ExcelFiles").Value + "/"+ request.FilePath ;
            var builder = new BodyBuilder();
            //builder.HtmlBody = htmlContent;
            builder.TextBody = request.Content;

            // you can either create MimeEntity object(s)
            // this might get handy in case you want to pass multiple attachments from somewhere else
            byte[] myFileAsByteArray = GetFile(ruta);
            var attachments = new List<MimeEntity>
            {
                // from file
                //MimeEntity.Load(ruta),
                // file from stream
                //MimeEntity.Load(new MemoryStream(myFileAsByteArray)),
                // from stream with a content type defined
                MimeEntity.Load(new ContentType("application", "pdf"), new MemoryStream(myFileAsByteArray))
            };

            // or add file directly - there are a few more overloads to this
            //builder.Attachments.Add("myFile.pdf");
            //builder.Attachments.Add("myFile.pdf", myFileAsByteArray);
            //builder.Attachments.Add("myFile.pdf", myFileAsByteArray , new ContentType("application", "pdf"));

            // append previously created attachments
            foreach (var attachment in attachments)
            {
                builder.Attachments.Add(attachment);
            }

            email.Body = builder.ToMessageBody();
        }
        
        using var smtp = new SmtpClient();


        smtp.Connect(
            _configuration.GetSection("Email:Host").Value,
            Convert.ToInt32(_configuration.GetSection("Email:Port").Value),
            SecureSocketOptions.StartTls
        );
        
        smtp.Authenticate(
            _configuration.GetSection("Email:UserName").Value,
            _configuration.GetSection("Email:PassWord").Value
            );
        smtp.Send(email);
        smtp.Disconnect(true);
    }

   
}