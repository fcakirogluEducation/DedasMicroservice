using Bus.Shared;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API;
using Order.API.Models;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("inmemorydb"); });
//builder.Services.AddDbContextPool<Appd>()

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddSingleton(sp =>
{
    var connectionFactory = new ConnectionFactory()
    {
        Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")!)
    };

    var connection = connectionFactory.CreateConnection();

    var channel = connection.CreateModel();
    channel.ConfirmSelect();

    channel.ExchangeDeclare(BusConst.OrderCreatedEventExchange, ExchangeType.Fanout, true, false, null);
    return channel;
});


builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(builder.Configuration.GetConnectionString("RabbitMQ")!), host => { });
    });
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