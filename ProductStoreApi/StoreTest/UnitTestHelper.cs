using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Moq;
using StoreDAL.Entities;
using StoreDAL.Infrastructure.Data;
using StoreDAL.Infrastructure;
using MockQueryable.Moq;

namespace StoreTest
{
    public static class UnitTestHelper
    {
        public static StoreDbContext GetDbContextMock<TEntity>(string dbSetName, IEnumerable<TEntity> data) where TEntity : class, IBaseEntity
        {
            var entities = data.AsQueryable();
            var mockedDbSet = GetMockedDbSet<TEntity>(entities);
            var mockContext = new Mock<StoreDbContext>(CreateOptions(), new TestDataFactory());

            PropertyInfo? property = typeof(StoreDbContext).GetProperty(dbSetName) ??
                throw new ArgumentException("Invalid dbSetName", nameof(dbSetName));
            property.SetValue(mockContext.Object, mockedDbSet);
            mockContext.Setup(x => x.Set<TEntity>()).Returns(mockedDbSet);
            mockContext.Setup(m => m.SaveChanges()).Verifiable();

            return mockContext.Object;
        }

        private static DbSet<T> GetMockedDbSet<T>(IEnumerable<T> data) where T : class, IBaseEntity
        {
            var mock = data.AsQueryable().BuildMockDbSet();
            return mock.Object;
        }

        public static StoreDbContext CreateContext()
        {
            var context = new StoreDbContext(CreateOptions(), new TestDataFactory());
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        public static DbContextOptions<StoreDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}
