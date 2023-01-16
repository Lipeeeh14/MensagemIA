using ConsumerMensagemIA.DomainReceiver.Models;

namespace ConsumerMensagemIA.Integration.Facades.Interfaces
{
	public interface IEmailFacade
	{
		bool SendEmail(Mensagem? mensagem);
	}
}
