﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceForRabbitMQ.Banking.Domain.Commands
{
    public class CreateTransferCommand : TransferCommand
    {
        public CreateTransferCommand(int from, int to, decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}
