using MediatR;
using MicroserviceForRabbitMQ.Banking.Domain.Commands;
using MicroserviceForRabbitMQ.Banking.Domain.Events;
using MicroserviceForRabbitMQ.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceForRabbitMQ.Banking.Domain.CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventBus _bus;

        public TransferCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            // publish event to RabbitMQ
            _bus.Publish(new TransferCreatedEvent(request.From, request.To, request.Amount));

            return Task.FromResult(true);
        }
    }
}
