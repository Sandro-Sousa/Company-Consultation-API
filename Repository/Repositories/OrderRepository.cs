
using Entities.Entites;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;

        public OrderRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task SaveAsync(Order order)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Cnpj == order.Cnpj);
            if (existingOrder != null)
            {
                existingOrder.Result = order.Result;
                _context.Orders.Update(existingOrder);
            }
            else
            {
                await _context.Orders.AddAsync(order);
            }

            await _context.SaveChangesAsync();
        }
    }
}
