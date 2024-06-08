using System.Net.Mail;
using VisitorSystem.All;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace VisitorSystem.Services
{
    public class EmailSender
    {
        public async Task SendEmail(string subject, string toEmail, string userName, string message, byte[] pdfBytes = null)
        {
            var apiKey = Crediantial.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("tidkeshubham10@gmail.com", "VisitorSecurityClearanceSystem");
            var to = new EmailAddress(toEmail, userName);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            if (pdfBytes != null)
            {
                msg.AddAttachment("VisitorPass.pdf", Convert.ToBase64String(pdfBytes));
            }

            var response = await client.SendEmailAsync(msg);
        }

    }
}
