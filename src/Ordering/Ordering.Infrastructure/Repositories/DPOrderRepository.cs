using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class DPOrderRepository : DPRepository<Order>,IOrderRepository
    {
        private string conStr;
        public DPOrderRepository()
        {
            conStr = "User ID=postgres;Password=mysecretpassword;Server=orderdb;Port=5432;Database=OrderDb;Integrated Security=true;Pooling=true;";
        }

        public async Task<IEnumerable<Order>> GetOrdesByUserName(string userName)
        {
            IEnumerable<Order> orders = new List<Order>();
            using (var conn = new NpgsqlConnection(conStr))
            {
                conn.Open();
                orders = await conn.QueryAsync<Order>("Select * from orders Where \"UserName\"=@UserName", new { UserName = userName});
                    //await conn.QueryAsync<Order>("Select * from Orders Where UserName=@UserName", new { UserName = userName });
            }
            return orders;
        }
    }
}
