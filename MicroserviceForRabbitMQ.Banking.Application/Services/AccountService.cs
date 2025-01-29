using MicroserviceForRabbitMQ.Banking.Application.Interfaces;
using MicroserviceForRabbitMQ.Banking.Application.Models;
using MicroserviceForRabbitMQ.Banking.Domain.Commands;
using MicroserviceForRabbitMQ.Banking.Domain.Interfaces;
using MicroserviceForRabbitMQ.Banking.Domain.Models;
using MicroserviceForRabbitMQ.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceForRabbitMQ.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _bus;

        public AccountService(IAccountRepository accountRepository, IEventBus eventBus)
        {
            _accountRepository = accountRepository;
            _bus = eventBus;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer, decimal amount)
        {
            var createTransferCommand = new CreateTransferCommand(
                accountTransfer.FromAccount,
                accountTransfer.ToAccount,
                accountTransfer.TransferAmount
                );
            _bus.SendCommand(createTransferCommand);
        }
    }
}
