using System.Text;
using System.Text.Json;
using Common.Dtos;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient, IDisposable
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration config)
        {
            _config = config;
            
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMqHost"],
                Port = int.Parse(_config["RabbitMqPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare("trigger", ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMqConnectionShutdown;

                Console.WriteLine($"Connected to message bus");
            }
            catch(Exception ex)
            {
                Console.Write($"Expection occurred during RabbitMq connection: {ex.Message}");
            }
        }

        public void PublishNewPlatform(PlatformPublishDto platformPublishDto)
        {
            var message = JsonSerializer.Serialize(platformPublishDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine($"RabbitMq connection open. Sending message");
                SendMessage(message);
            }
            else
                Console.WriteLine($"RabbitMq connection closed");
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            
            _channel.BasicPublish("trigger", "", null, body);

            Console.WriteLine($"Messgae sent {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("Message bus dispose");
            if (_channel.IsOpen)
                _channel.Close();

            _connection.Close();
        }

        private void RabbitMqConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"Connection has shut down because of {e.Cause}");
        }
    }
}