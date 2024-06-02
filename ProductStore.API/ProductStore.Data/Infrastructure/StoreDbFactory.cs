using Microsoft.EntityFrameworkCore;
using ProductStore.Data.Infrastructure.Data;

namespace ProductStore.Data.Infrastructure
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
			return new DbContextOptionsBuilder<StoreDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;
		}
    }
}
