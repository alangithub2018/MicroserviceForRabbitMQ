using MicroserviceForRabbitMQ.Banking.Application.Interfaces;
using MicroserviceForRabbitMQ.Banking.Domain.Interfaces;
using MicroserviceForRabbitMQ.Banking.Domain.Models;
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

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }
    }
}
