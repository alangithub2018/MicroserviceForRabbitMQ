using MediatR;
using MicroserviceForRabbitMQ.Banking.Application.Interfaces;
using MicroserviceForRabbitMQ.Banking.Application.Services;
using MicroserviceForRabbitMQ.Banking.Data.Context;
using MicroserviceForRabbitMQ.Banking.Data.Repository;
using MicroserviceForRabbitMQ.Banking.Domain.CommandHandlers;
using MicroserviceForRabbitMQ.Banking.Domain.Commands;
using MicroserviceForRabbitMQ.Banking.Domain.Interfaces;
using MicroserviceForRabbitMQ.Domain.Core.Bus;
using MicroserviceForRabbitMQ.Infra.Bus;
using MicroserviceForRabbitMQ.Transfer.Application.Interfaces;
using MicroserviceForRabbitMQ.Transfer.Application.Services;
using MicroserviceForRabbitMQ.Transfer.Data.Context;
using MicroserviceForRabbitMQ.Transfer.Data.Repository;
using MicroserviceForRabbitMQ.Transfer.Domain.EventHandlers;
using MicroserviceForRabbitMQ.Transfer.Domain.Events;
using MicroserviceForRabbitMQ.Transfer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceForRabbitMQ.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>()!, scopeFactory);
            });

            // Subscriptions
            services.AddTransient<TransferEventHandler>();

            // Domain Events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            // Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            // Application Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransferService, TransferService>();

            // Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<BankingDBContext>();
            services.AddTransient<TransferDBContext>();
        }
    }
}
