using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

var consumer = new EventingBasicConsumer(chanel);

consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();

    var message = Encoding.UTF8.GetString(body);
    
    Console.WriteLine($"New ticket processing started - {message}");
};

chanel.BasicConsume("bookings", true, consumer);

Console.ReadKey();