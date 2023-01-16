using ConsumerMensagemIA.DomainReceiver.Models;
using ConsumerMensagemIA.Integration.Facades.Interfaces;
using System.Net.Mail;

namespace ConsumerMensagemIA.Integration.Facades
{
	public class EmailFacade : IEmailFacade
	{
		public bool SendEmail(Mensagem? mensagem)
		{
			try
			{
				if (mensagem == null) return false;

				MailMessage mail = new()
				{
					From = new MailAddress(mensagem.Remetente)
				};
				mail.To.Add(new MailAddress(mensagem.Destinatario));

				mail.Subject = "MensagemIA: Nova notificação!";
				mail.Body = mensagem.Conteudo;
				mail.Priority = MailPriority.Normal;

				using SmtpClient smtp = new("smtp.gmail.com", 587);
				smtp.Send(mail);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
