using ProducerMensagemIA.Domain.DTO;
using ProducerMensagemIA.Domain.Models;
using ProducerMensagemIA.Domain.Services.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProducerMensagemIA.Domain.Services
{
	public class MensagemService : IMensagemService
	{
		private readonly ConnectionFactory _factory;
		private readonly string QUEUE_NAME = "message";

		public MensagemService()
		{
			_factory = new ConnectionFactory
			{
				HostName = "localhost",
			};
		}

		public void SendMessage(MensagemDTO messageDTO)
		{
			var message = new Mensagem(messageDTO.Plataforma, messageDTO.Remetente, messageDTO.Destinatario, messageDTO.Conteudo);

			using var connection = _factory.CreateConnection();
			
			using var channel = connection.CreateModel();
			channel.QueueDeclare(
					queue: QUEUE_NAME,
					durable: false,
					exclusive: false,
					autoDelete: false,
					arguments: null
				);

			var serialize = JsonSerializer.Serialize(message);
			var body = Encoding.UTF8.GetBytes(serialize);

			channel.BasicPublish(
					exchange: "",
					routingKey: QUEUE_NAME,
					basicProperties: null,
					body: body
				);
		}
	}
}
