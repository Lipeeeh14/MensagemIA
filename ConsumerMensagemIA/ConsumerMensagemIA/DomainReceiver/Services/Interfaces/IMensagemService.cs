using ConsumerMensagemIA.DomainReceiver.Models;

namespace ConsumerMensagemIA.DomainReceiver.Services.Interfaces
{
	public interface IMensagemService
	{
		Task ConsumeMessage();
	}
}
