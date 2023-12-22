using MassTransit;
using Microsoft.EntityFrameworkCore;
using StateMachine.Service.StateDbContexts;
using StateMachine.Service.StateInstance;
using StateMachine.Service.StateMachines;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>().EntityFrameworkRepository(options =>
    {
        options.AddDbContext<DbContext, OrderStateDbContext>((provider, _builder) =>
        {
            _builder.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServer"));
        });
    });


    configurator.UsingRabbitMq((context, _configure) =>
    {
        _configure.Host(builder.Configuration["RabbitMQ"]);
    });
});

var host = builder.Build();
host.Run();
