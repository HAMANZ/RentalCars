using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.CommonObjects.Requests;
using RentalCar.DomainLayer.Model;
using RentalCar.ServiceLayer.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Implementation
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public DynamicResponse<bool> SendEmail(MailRequest mailRequest)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();
          
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("oonlinetutoring@gmail.com", "RentalCar");
                mail.To.Add(mailRequest.ToEmail);
                mail.Subject = mailRequest.Subject;
                mail.Body = mailRequest.Body;
                mail.IsBodyHtml =true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("oonlinetutoring@gmail.com", "thtyjzwtbfauboof");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                //123310On%%
                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                return response;

            }
            catch (Exception ex)
            {
                //Add Logger 

                _logger.LogInformation("Email failed {DT}", DateTime.UtcNow.ToLongTimeString());
                response.Message= "Email failed {DT}"+ DateTime.UtcNow.ToLongTimeString();
                response.ServerMessage = ex.Message;
                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }

}
