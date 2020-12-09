using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory logger, int? retry = 0)
        {
            int retryForAvailiability = retry.Value;
            try
            {
                orderContext.Database.Migrate(); // auto migrate(if database not exist this code will create database and tables!)
                if (!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreconfiguredOrders());
                    await orderContext.SaveChangesAsync();
                }
                
            }
            catch (Exception exception)
            {
                if (retryForAvailiability < 3)
                {
                    retryForAvailiability++;
                    var log = logger.CreateLogger<OrderContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(orderContext, logger, retryForAvailiability);
                }
            }
        }
        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order(){UserName = "swn", FirstName ="Hüseyin", LastName = "Özsoy",
                    EmailAddress ="huseyin@gmail.com", AddressLine="İstanbul", Country="Turkey"}
            };
        }
    }
}
