using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>,IOrderRepository
    {
        private OrderContext _context;
        public OrderRepository(OrderContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdesByUserName(string userName)
        {
            var orderList = await _context.Orders.Where(o => o.UserName == userName).ToListAsync();
            return orderList;
        }
    }
}
