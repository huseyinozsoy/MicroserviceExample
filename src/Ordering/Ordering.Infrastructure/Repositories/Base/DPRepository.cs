using Dapper;
using Npgsql;
using Ordering.Core.Entities.Base;
using Ordering.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories.Base
{
    public class DPRepository<T> : IRepository<T> where T : Entity
    {
        //"Data Source=orderdb;Initial Catalog=OrderDb;User ID=sa;Password=<YourStrong@Passw0rd>;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //"User ID=postgre;Password=mysecretpassword;Server=host.docker.internal;Port=5432;Database=OrderDb;Integrated Security=true;Pooling=true;"
        private string conStr = "User ID=postgres;Password=mysecretpassword;Server=orderdb;Port=5432;Database=OrderDb;Integrated Security=true;Pooling=true;";
        //"Data Source=orderdb;Initial Catalog=OrderDb;User ID=sa;Password=<YourStrong@Passw0rd>;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public async Task<T> AddAsync(T entity)
        {
            using (var conn = new NpgsqlConnection(conStr))
            {
                conn.Open();
                await conn.ExecuteAsync("Insert Into orders VALUES(DEFAULT,@UserName,@TotalPrice,@FirstName,@LastName,@EmailAddress,@AddressLine,@Country,@State,@ZipCode,@CardName,@CardNumber,@Expiration,@CVV,@PaymentMethod) ", entity);
                return entity;
            }
            
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
