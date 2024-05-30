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
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(builder.Configuration.GetConnectionString("RabbitMQ")!), host => { });


        config.ReceiveEndpoint("stock.order.created.queue",
            configureEndpoint =>
            {
                configureEndpoint.ConfigureConsumer<OrderCreatedEventConsumerWithMassTransit>(context);
            });
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