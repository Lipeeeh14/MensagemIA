using ProducerMensagemIA.Domain.DTO;

namespace ProducerMensagemIA.Domain.Services.Interfaces
{
	public interface IMensagemService
	{
		void SendMessage(MensagemDTO message);
	}
}
