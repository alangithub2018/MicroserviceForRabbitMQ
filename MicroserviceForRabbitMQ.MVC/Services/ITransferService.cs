using MicroserviceForRabbitMQ.MVC.Models.DTO;

namespace MicroserviceForRabbitMQ.MVC.Services
{
    public interface ITransferService
    {
        Task Transfer(TransferDTO transferDTO);
    }
}
