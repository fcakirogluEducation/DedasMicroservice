// See https://aka.ms/new-console-template for more information

using System.Linq.Expressions;
using System.Text;
using Consumer.App;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Logging;

Console.WriteLine("Consumer");

var connectionFactory = new ConnectionFactory()
{
    Uri = new Uri("amqps://cqwvtmkg:SssJl2QvvDifJS1KRHWCKP6nWUKBnIub@toad.rmq.cloudamqp.com/cqwvtmkg")
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

var consumer = new EventingBasicConsumer(channel);


// event=> delegate => method
consumer.Received += (sender, eventArgs) =>
{
    try
    {
        var messageAsJson = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

        var userCreatedEvent = System.Text.Json.JsonSerializer.Deserialize<UserCreatedEvent>(messageAsJson);

        Console.WriteLine($"Gelen Mesaj: {userCreatedEvent!.Id} - {userCreatedEvent.Email} - {userCreatedEvent.Phone}");

        channel.BasicAck(eventArgs.DeliveryTag, true);
    }
    catch (Exception e)
    {
        //channel.BasicNack(eventArgs.DeliveryTag, true, false);
        Console.WriteLine(e);
        throw;
    }
};

channel.BasicConsume("demo-queue", autoAck: false, consumer);

Console.ReadKey();