namespace ProducerMensagemIA.Domain.Models
{
	public class Mensagem
	{
		public string Plataforma { get; set; }
		public string Remetente { get; set; }
		public string Destinatario { get; set; }
		public string Conteudo { get; set; }
		public DateTime DataEnvio { get; set; }

		public Mensagem()
		{
			DataEnvio = DateTime.Now;
		}

		public Mensagem(string plataforma, string remetente, string destinatario, string conteudo)
			: this()
		{
			Plataforma = plataforma;
			Remetente = remetente;
			Destinatario = destinatario;
			Conteudo = conteudo;
		}
	}
}
