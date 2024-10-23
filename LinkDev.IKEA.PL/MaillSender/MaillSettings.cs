using System.Net;
using System.Net.Mail;

namespace LinkDev.IKEA.PL.Maill_Settings
{
	public class MaillSender
	{
        private readonly IConfiguration _configuration;

        public MaillSender(IConfiguration configuration)
		{
            _configuration = configuration;
        }

		public Task SenderEmailAsync(string email, string subject, string message)
		{
			string mail = _configuration.GetSection("EmailSettings")["Email"]!;
			string password = _configuration.GetSection("EmailSettings")["Password"]!;

            var Client = new SmtpClient("smtp.gmail.com", 587)
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(mail, password)
			};

			return Client.SendMailAsync(new MailMessage(from: mail, to: email, subject, message));
		}
	}
}
