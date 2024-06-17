
using Entities.Entites;

namespace Repository.Interfaces
{
    public interface IOrderRepository
    {
        public Task SaveAsync(Order order);
        public Task<IEnumerable<Order>> GetAllOrders();
    }
}
