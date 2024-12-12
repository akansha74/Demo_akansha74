using AllFunctionalityNetCore.Repository.Interface;
using AllFunctionalityNetCore.Views.Email;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AllFunctionalityNetCore.Repository.Service
{
    public class EmailSender : IEmailSender
    {
		private readonly IConfiguration configuration;
		public EmailSender( IConfiguration configuration)
		{
			this.configuration = configuration;

		}
		public async Task<bool> EmailSendAsync(string email, string subject, string msg)
		{
			bool status = false;
			try
			{

				GetEmailSettings getEmailSettings = new GetEmailSettings()
				{
					SecretKey= configuration.GetValue<string>("AppSettings:SecretKey"),
					From=configuration.GetValue<string>("AppSettings:EmailSettings:From"),
                    SmtpServer = configuration.GetValue<string>("AppSettings:EmailSettings:SmtpServer"),
                    Port = configuration.GetValue<int>("AppSettings:EmailSettings:Port"),
                    EnableSSL = configuration.GetValue<bool>("AppSettings:EmailSettings:EnableSSL"),

                };
				MailMessage mailMessage = new MailMessage()
				{
                    From = new MailAddress(getEmailSettings.From),
					Subject = subject,
					Body = msg


                };
				mailMessage.To.Add(email);
				SmtpClient smtpClient = new SmtpClient(getEmailSettings.SmtpServer)
				{
					Port=getEmailSettings.Port,
					Credentials=new NetworkCredential(getEmailSettings.From,getEmailSettings.SecretKey),
					EnableSsl=getEmailSettings.EnableSSL
				};
				await smtpClient.SendMailAsync(mailMessage);
				status= true;
			}
			catch (System.Exception)
			{

                status=false;
            }
			return status;
		}
    }
}
