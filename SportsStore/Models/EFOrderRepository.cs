
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EFOrderRepository(StoreDbContext context) : IOrderRepository
    {
        private StoreDbContext _context = context;
        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.Id == 0)
                _context.Orders.Add(order);

            _context.SaveChanges();
        }
    }
}
