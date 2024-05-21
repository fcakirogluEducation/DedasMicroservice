// See https://aka.ms/new-console-template for more information

using System.Text;
using Producer.App;
using RabbitMQ.Client;

Console.WriteLine("Producer");

var connectionFactory = new ConnectionFactory()
{
    Uri = new Uri("amqps://cqwvtmkg:SssJl2QvvDifJS1KRHWCKP6nWUKBnIub@toad.rmq.cloudamqp.com/cqwvtmkg")
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

channel.ConfirmSelect();
channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);


//var message = "Message 1";
//var body = Encoding.UTF8.GetBytes(message);

var message = new UserCreatedEvent(1, "ahmet@outlook.com", "555 555 55 55");
// method1(message)

var body = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(message));


channel.BasicPublish(string.Empty, "demo-queue", mandatory: false, null, body);

channel.WaitForConfirms(TimeSpan.FromSeconds(5));


Console.WriteLine("Mesajlar gönderildi.");