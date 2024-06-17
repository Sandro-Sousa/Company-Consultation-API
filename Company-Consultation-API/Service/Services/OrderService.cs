using Entities.Entites;
using Repository.API.Interfaces;
using Repository.Interfaces;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IExternalApiReceitawsAdapter _apiClient;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository, IExternalApiReceitawsAdapter externalApiReceitaws)
        {
            _orderRepository = orderRepository;
            _apiClient = externalApiReceitaws;
        }

        public async Task ProcessOrderAsync(string cnpj)
        {
            var resultJson = await _apiClient.FetchResultAsync(cnpj);
            var order = new Order
            {
                Cnpj = cnpj,
                Result = resultJson.ToString()
            };

            await _orderRepository.SaveAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrders();
        }
    }
}
