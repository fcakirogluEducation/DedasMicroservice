using MassTransit;
using RabbitMQ.Client;
using Stock.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<OrderCreatedEventConsumer>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedEventConsumerWithMassTransit>();
    x.AddConsumer<PaymentStartingMessageConsumer>();
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(builder.Configuration.GetConnectionString("RabbitMQ")!), host => { });

        config.UseMessageRetry(r => r.Immediate(5));
        config.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(5)));


        config.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15),
            TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(45)));

        config.UseInMemoryOutbox(context);


        config.ReceiveEndpoint("stock.order.created.queue",
            configureEndpoint =>
            {
                // configureEndpoint.ConcurrentMessageLimit = 3;

                configureEndpoint.ConfigureConsumer<OrderCreatedEventConsumerWithMassTransit>(context);
            });


        config.ReceiveEndpoint("payment2.order.created.event",
            configureEndpoint => { configureEndpoint.ConfigureConsumer<PaymentStartingMessageConsumer>(context); });
    });
});

builder.Services.AddSingleton(sp =>
{
    var connectionFactory = new ConnectionFactory()
    {
        Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")!)
    };

    return connectionFactory.CreateConnection();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();