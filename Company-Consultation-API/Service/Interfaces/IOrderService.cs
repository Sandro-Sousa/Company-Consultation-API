
using Entities.Entites;
using Service.DTOs;

namespace Service.Interfaces
{
    public interface IOrderService
    {
        Task ProcessOrderAsync(string cnpj);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}
