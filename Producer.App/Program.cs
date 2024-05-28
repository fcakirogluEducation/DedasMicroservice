// See https://aka.ms/new-console-template for more information

using System.Globalization;
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
channel.BasicReturn += (sender, eventArgs) =>
{
    Console.WriteLine($"Mesaj gönderilemedi.");

    var messageAsJson = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
    Console.WriteLine(messageAsJson);
};
channel.CallbackException += (sender, eventArgs) => { Console.WriteLine($"Mesaj gönderilemedi."); };

channel.BasicAcks += (sender, eventArgs) => { Console.WriteLine($"Mesaj gönderildi."); };

channel.ConfirmSelect();

channel.ExchangeDeclare("demo-topic", ExchangeType.Topic, true, false, null);


Enumerable.Range(1, 20).ToList().ForEach(x =>
{
    string routeKey = string.Empty;

    if (x % 2 == 0)
    {
        routeKey = "a.b.c";
    }
    else
    {
        routeKey = "a.e.f";
    }


    var message = new UserCreatedEvent(x, "ahmet@outlook.com", "555 555 55 55");
    // method1(message)

    var body = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(message));


    var properties = channel.CreateBasicProperties();
    properties.Persistent = true;
    properties.Headers = new Dictionary<string, object>()
    {
        { "event-type", "user-created" },
        { "event-date", DateTime.Now.ToString(CultureInfo.InvariantCulture) },
        { "version", "V1" }
    };


    channel.BasicPublish("demo-topic", routeKey, mandatory: true, properties, body);

    channel.WaitForConfirms(TimeSpan.FromSeconds(5));
});


Console.WriteLine("Mesajlar gönderildi.");