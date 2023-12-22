using MassTransit;
using Microsoft.EntityFrameworkCore;
using Stock.API.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context, _configure) =>
    {
        _configure.Host(builder.Configuration["RabbitMQ"]);
    });
});

builder.Services.AddDbContext<StockContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServer")));


var app = builder.Build();



app.Run();
