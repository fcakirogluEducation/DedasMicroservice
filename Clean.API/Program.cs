using Clean.Cache;
using Clean.Repository;
using Clean.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICacheService, ServiceCache>();
builder.Services.AddScoped<IProductService>(sp =>
{
    var productService = new ProductService(
        sp.GetRequiredService<IProductRepository>());


    ProductServiceAsCacheDecorator productServiceAsCacheDecorator = new ProductServiceAsCacheDecorator(
        productService,
        sp.GetRequiredService<ICacheService>());

    return productServiceAsCacheDecorator;
});
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
        x => { x.MigrationsAssembly("Clean.Repository"); });
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