using MicroserviceForRabbitMQ.Banking.Application.Interfaces;
using MicroserviceForRabbitMQ.Banking.Application.Models;
using MicroserviceForRabbitMQ.Banking.Data.Context;
using MicroserviceForRabbitMQ.Infra.IoC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BankingDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankingDbConnection")));

// Register MediatR
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Add swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MicroserviceForRabbitMQ.Banking.API", Version = "v1" });
});

RegisterServices(builder.Services);

void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/api/banking/accounts", (IAccountService accountService) =>
{
    return accountService.GetAccounts();
});

app.MapPost("/api/banking/accounts", ([FromBody] AccountTransfer accountTransfer, IAccountService accountService) =>
{
    accountService.Transfer(accountTransfer, accountTransfer.TransferAmount);
});

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroserviceForRabbitMQ.Banking.API v1"));

app.Run();
