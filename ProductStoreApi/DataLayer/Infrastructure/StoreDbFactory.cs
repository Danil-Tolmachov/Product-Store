using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Infrastructure.Data;

namespace StoreDAL.Infrastructure
{
    public class StoreDbFactory : IDbContextFactory<StoreDbContext>
    {
        private readonly IDataFactory factory;
        public StoreDbFactory(IDataFactory factory)
        {
            this.factory = factory;
        }
        public StoreDbContext CreateDbContext()
        {
            var context = new StoreDbContext(this.CreateOptions(), factory);
			context.Database.EnsureDeleted(); // Disable when production !!!!!
			context.Database.EnsureCreated();
            return context;
        }
        public DbContextOptions<StoreDbContext> CreateOptions()
        {
            throw new NotImplementedException();
        }
    }
}
