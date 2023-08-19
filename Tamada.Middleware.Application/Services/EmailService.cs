using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Net.Mime; 
    public class EmailService : IEmailService
    {
        private readonly SMTPConfig SMTPconfig; 
        private readonly ILogger<EmailService> logger;
        private readonly IMapper mapper;

        public EmailService(IOptions<SMTPConfig> SMTPconfig, ILogger<EmailService> logger, IMapper mapper)
        {
            this.SMTPconfig = SMTPconfig.Value ?? throw new ArgumentNullException(nameof(SMTPConfig));
            this.logger = logger ?? throw new ArgumentNullException(nameof(ILogger<EmailService>));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        
        public async Task<EmailResponse> SendAsync(EmailRequest emailRequest)
        {
            var email_details = mapper.Map<Email>(emailRequest);
            email_details.ToAddresses = (SMTPconfig.UseTest == "1" ? SMTPconfig.TestToAddresses : email_details.ToAddresses) ?? ""; 
            EmailResponse emailResponse = new EmailResponse();
            
                
                MailMessage mailmessage = new();
                mailmessage.From = new MailAddress(SMTPconfig.FROMADDRESS ?? "tamada-noreply@accessbankplc.com", "Tamada Bot");
                mailmessage.Subject = email_details.Subject;
                mailmessage.Body = "<p>" + email_details.Body + "</p>";
                mailmessage.IsBodyHtml = true;  
                
                mailmessage.AddToAddresses(email_details.ToAddresses);

                if (mailmessage.To.Count < 1)
                {
                  throw new BadRequestException("Incorrect or no email_details address supplied For TO"); 
                }  

              if(email_details.HasAttachment || email_details.EmailAttachments?.Count > 0)
              {
                  mailmessage.PrepareAttachments(email_details.EmailAttachments);
              }
        
                   // SmtpClient smtp = new SmtpClient();
               using (SmtpClient smtp = new SmtpClient(SMTPconfig.SMTP_IP ?? throw new BadRequestException("SMTP host is required"),SMTPconfig.SMTP_PORT <= 0 ?  throw new BadRequestException("") : SMTPconfig.SMTP_PORT))
               {  

                    
                    await smtp.SendMailAsync(mailmessage);  
                    emailResponse.Sent = true;
               }    
            
             return emailResponse;
        }
 
 

}
