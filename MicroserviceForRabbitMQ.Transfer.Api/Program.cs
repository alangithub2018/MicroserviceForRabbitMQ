using MicroserviceForRabbitMQ.Domain.Core.Bus;
using MicroserviceForRabbitMQ.Infra.IoC;
using MicroserviceForRabbitMQ.Transfer.Application.Interfaces;
using MicroserviceForRabbitMQ.Transfer.Data.Context;
using MicroserviceForRabbitMQ.Transfer.Domain.EventHandlers;
using MicroserviceForRabbitMQ.Transfer.Domain.Events;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TransferDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TransferDbConnection")));

// Register MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

//Add swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MicroserviceForRabbitMQ.Transfer.API", Version = "v1" });
});

RegisterServices(builder.Services);

void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroserviceForRabbitMQ.Transfer.API v1"));

ConfigureEventBus(app);

void ConfigureEventBus(WebApplication app)
{
    var eventBus = app.Services.GetRequiredService<IEventBus>();
    eventBus.Subscribe<TransferCreatedEvent, TransferEventHandler>();
}

app.MapGet("/api/transfers", (ITransferService transferService) =>
{
    return transferService.GetTransferLogs();
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
