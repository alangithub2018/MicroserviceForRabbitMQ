using MicroserviceForRabbitMQ.Banking.Application.Interfaces;
using MicroserviceForRabbitMQ.Banking.Application.Services;
using MicroserviceForRabbitMQ.Banking.Data.Context;
using MicroserviceForRabbitMQ.Banking.Data.Repository;
using MicroserviceForRabbitMQ.Banking.Domain.Interfaces;
using MicroserviceForRabbitMQ.Domain.Core.Bus;
using MicroserviceForRabbitMQ.Infra.Bus;
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
            services.AddTransient<IEventBus, RabbitMQBus>();

            // Application Services
            services.AddTransient<IAccountService, AccountService>();

            // Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDBContext>();
        }
    }
}
