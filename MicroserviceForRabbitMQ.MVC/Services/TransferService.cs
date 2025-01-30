using MicroserviceForRabbitMQ.MVC.Models.DTO;
using Newtonsoft.Json;
using System.Text;

namespace MicroserviceForRabbitMQ.MVC.Services
{
    public class TransferService : ITransferService
    {
        private readonly HttpClient _apiClient;

        public TransferService(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task Transfer(TransferDTO transferDTO)
        {
            // For learning purposes, we will hardcode the URI here
            var uri = "https://localhost:7021/api/banking/accounts";
            var transferContent = new StringContent(JsonConvert.SerializeObject(transferDTO), Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, transferContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
