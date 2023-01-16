namespace ConsumerMensagemIA.DomainReceiver.Models
{
	public class Mensagem
	{
		public string Plataforma { get; set; }
		public string Remetente { get; set; }
		public string Destinatario { get; set; }
		public string Conteudo { get; set; }
		public DateTime DataEnvio { get; set; }
	}
}
