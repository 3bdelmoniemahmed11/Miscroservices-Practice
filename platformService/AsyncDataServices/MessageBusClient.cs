using PaltformService.Controllers.Platforms.DTOs;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PaltformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {

        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channal;


        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {

                _connection = factory.CreateConnection();
                _channal = _connection.CreateModel();
                _channal.ExchangeDeclare("trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ShutdownConnection;

            }
            catch (Exception ex) {

                Console.WriteLine($"----> Error occured while connected to rabbitmq {ex.Message}");
            
            }


        }

    
    
        public void PublishNewPlatform(PlatformPublishDto platformPublish)
        {
            var message=JsonSerializer.Serialize(platformPublish);
            if (_connection.IsOpen)
            {
                SendMessage(message);
            }
        }


        public void SendMessage(string message)
        {
            Console.WriteLine("Sending Message is begin");
            var body=Encoding.UTF8.GetBytes(message);
            _channal.BasicPublish(exchange: "trigger", body: body, routingKey: "", basicProperties: null) ;
            Console.WriteLine($"the message was send to consumer {message}");
            Dispose();
        }

        public void Dispose()
        {
            if (_connection.IsOpen)
            {
                _channal.Close();
                _connection.Close();
            }
            Console.WriteLine("the connection was be disposed");
        }
        public void RabbitMQ_ShutdownConnection(object sender ,ShutdownEventArgs e)
        {
            Console.WriteLine("---> the RabbitMQ Connection is shutdown");
        }
    }
}
