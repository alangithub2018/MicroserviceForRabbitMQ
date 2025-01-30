using MicroserviceForRabbitMQ.Transfer.Data.Context;
using MicroserviceForRabbitMQ.Transfer.Domain.Interfaces;
using MicroserviceForRabbitMQ.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceForRabbitMQ.Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly TransferDBContext _ctx;

        public TransferRepository(TransferDBContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _ctx.TransferLogs;
        }
    }
}
