using ConsumerMensagemIA.DomainReceiver.Models;
using ConsumerMensagemIA.DomainReceiver.Services.Interfaces;
using ConsumerMensagemIA.Integration.Facades.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ConsumerMensagemIA.DomainReceiver.Services
{
	public class MensagemService : IMensagemService
	{
		private readonly ConnectionFactory _factory;
		private readonly string _queueName;

		private IEmailFacade _emailFacade;

		public MensagemService(IEmailFacade emailFacade)
		{
			_queueName = "message";

			_factory = new ConnectionFactory 
			{
				HostName = "localhost",
			};

			_emailFacade = emailFacade; 
		}

		public void GetMessage()
		{
			using (var connection = _factory.CreateConnection())

			using (var channel = connection.CreateModel()) 
			{
				channel.QueueDeclare(queue: _queueName,
					durable: false,
					exclusive: false,
					autoDelete: false,
					arguments: null);

				var consumer = new EventingBasicConsumer(channel);

				var result = new Mensagem();
				consumer.Received += (model, ea) =>
				{
					var body = ea.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);
					result = JsonSerializer.Deserialize<Mensagem>(message);
				};

				channel.BasicConsume(queue: _queueName,
									autoAck: true,
									consumer: consumer);

				_ = _emailFacade.SendEmail(result);
			}
		}
	}
}
