using EmailService.Models.Dto;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService.Service
{
    public class MailService
    {
       private readonly string _email;

        private readonly string _password;

        private readonly IConfiguration _configuration;


        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _email = configuration.GetSection("Emailsettings:Email").Value;
            _password = configuration.GetSection("Emailsettings:Password").Value;

        }

        public async Task SendMail(UserMessageDTO user , string Message) { 
        

            // Building the Message
        MimeMessage message1 = new MimeMessage();

            message1.From.Add(new MailboxAddress("Safari",_email));
            message1.To.Add(new MailboxAddress(user.Name, user.Email));
            message1.Subject = "Welcome to Safari Travels";

            var body = new TextPart("html")
            {
                Text = Message.ToString(),
            };

            message1.Body = body;

            // Sending the Message 

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com",587,false);
            client.Authenticate(_email, _password);
            await client.SendAsync(message1);
            await client.DisconnectAsync(true);

        }
    }
}
