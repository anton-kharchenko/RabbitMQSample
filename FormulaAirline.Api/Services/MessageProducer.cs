using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FormulaAirline.Api.Services;

public class MessageProducer : IMessageProducer
{
    public void SendingMessage<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "myuser",
            Password = "mypassword",
            VirtualHost = "/"
        };

        var connection = factory.CreateConnection();

        using var chanel = connection.CreateModel();

        chanel.QueueDeclare("bookings", durable: true, exclusive: false);

        var jsonString = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(jsonString);
        
        chanel.BasicPublish("", "bookings", body: body);
    }
}