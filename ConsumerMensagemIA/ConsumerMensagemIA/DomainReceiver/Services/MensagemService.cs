using ConsumerMensagemIA.DomainReceiver.Models;
using ConsumerMensagemIA.DomainReceiver.Services.Interfaces;
using ConsumerMensagemIA.Integration.Facades.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ConsumerMensagemIA.DomainReceiver.Services
{
	public class MensagemService : IMensagemService
	{
		private readonly ConnectionFactory _factory;
		private readonly IConnection _connection;
		private readonly IModel _channel;
		private readonly string QUEUE_NAME = "message";

		private IEmailFacade _emailFacade;

		public MensagemService(IEmailFacade emailFacade)
		{
			_factory = new ConnectionFactory 
			{
				HostName = "localhost",
			};

			_emailFacade = emailFacade;

			_connection = _factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.QueueDeclare(
				queue: QUEUE_NAME,
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: null);
		}

		public Task ConsumeMessage()
		{
			var consumer = new EventingBasicConsumer(_channel);

			consumer.Received += (sender, args) =>
			{
				var contentArray = args.Body.ToArray();
				var contentString = Encoding.UTF8.GetString(contentArray);
				var message = JsonConvert.DeserializeObject<Mensagem>(contentString);

				_emailFacade.SendEmail(message);

				_channel.BasicAck(args.DeliveryTag, false);
			};

			_channel.BasicConsume(QUEUE_NAME, false, consumer);

			return Task.CompletedTask;
		}
	}
}
