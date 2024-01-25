using System;
using System.Reflection;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

public  class EmailHelper
    {

       private static string GetTemplateContent(string fileName)
        {
            var assembly = typeof(EmailHelper).GetTypeInfo().Assembly;
            var resourceStream = assembly.GetManifestResourceStream($"COMMON.templates.{fileName}");
            using(var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return  reader.ReadToEnd();
            }
        }
        public static bool SendHtmlEmail(string email, string code,out string message)
        {
            try
            {
                message = string.Empty;
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("elorda.com", $"verify@elorda.com"));
                mimeMessage.To.Add(new MailboxAddress("",email));
                mimeMessage.Subject = "Email анықтау";
                string htmlContet  = GetTemplateContent("emailvertification.html");
                htmlContet = htmlContet.Replace("{{code}}",code);
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = htmlContet
                };

                mimeMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 465, true); //if port is 587 false
                    client.Authenticate("liustra.kz@gmail.com", "yafo jszp qewe vjry");
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
                return true;
            }catch(Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }