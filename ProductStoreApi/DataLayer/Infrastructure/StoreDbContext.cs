using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure.Data;

namespace StoreDAL.Infrastructure
{
    public class StoreDbContext : DbContext
    {
        private readonly IDataFactory factory;

        public DbSet<ICartRepository> Carts { get; set; } = null!;
        public DbSet<ICartItemRepository> CartItems { get; set; } = null!;
        public DbSet<ICategoryRepository> Categories { get; set; } = null!;
        public DbSet<IContactRepository> Contacts { get; set; } = null!;
		public DbSet<IEmployeeRepository> Employees { get; set; } = null!;
		public DbSet<IOrderRepository> Orders { get; set; } = null!;
		public DbSet<IOrderDetailRepository> OrderDetails { get; set; } = null!;
		public DbSet<IPersonRepository> Persons { get; set; } = null!;
		public DbSet<IPositionRepository> Positions { get; set; } = null!;
		public DbSet<IProductRepository> Products { get; set; } = null!;
		public DbSet<ISpecificationRepository> Specifications { get; set; } = null!;
		public DbSet<IStatusRepository> Statuses { get; set; } = null!;
		public DbSet<IUserRepository> Users { get; set; } = null!;

		public StoreDbContext(DbContextOptions options, IDataFactory factory) : base(options)
        {
            this.factory = factory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<IContactRepository>().HasData(factory.GetContactData());
			modelBuilder.Entity<IPersonRepository>().HasData(factory.GetPersonData());
			modelBuilder.Entity<IUserRepository>().HasData(factory.GetUserData());
			modelBuilder.Entity<IPositionRepository>().HasData(factory.GetPositionData());
			modelBuilder.Entity<IEmployeeRepository>().HasData(factory.GetEmployeeData());
			modelBuilder.Entity<ICategoryRepository>().HasData(factory.GetCategoryData());
			modelBuilder.Entity<ISpecificationRepository>().HasData(factory.GetSpecificationData());
			modelBuilder.Entity<IProductRepository>().HasData(factory.GetProductData());
			modelBuilder.Entity<IOrderRepository>().HasData(factory.GetOrderData());
			modelBuilder.Entity<IOrderDetailRepository>().HasData(factory.GetOrderDetailData());

            base.OnModelCreating(modelBuilder);
        }
    }
}
