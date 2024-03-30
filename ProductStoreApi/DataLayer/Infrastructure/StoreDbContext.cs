using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure.Data;
using StoreDAL.Interfaces.Repositories;

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
			// Seed database
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

			// Configure entities
			modelBuilder.Entity<Contact>(entity =>
			{
				entity.HasOne(c => c.Person)
					  .WithMany(p => p.Contacts)
					  .HasForeignKey(c => c.PersonId);
			});

			modelBuilder.Entity<Person>(entity =>
			{
				entity.HasMany(p => p.Contacts)
					  .WithOne(c => c.Person)
					  .HasForeignKey(p => p.PersonId);

				entity.HasOne(p => p.User)
					  .WithOne(u => u.Person)
					  .HasForeignKey<User>(u => u.PersonId);
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.HasOne(u => u.Person)
					  .WithOne(p => p.User)
					  .HasForeignKey<User>(p => p.PersonId);

				entity.HasOne(u => u.Cart)
					  .WithOne(c => c.User)
					  .HasForeignKey<Cart>(c => c.UserId);

				entity.HasMany(u => u.Orders)
					  .WithOne(o => o.User)
					  .HasForeignKey(o => o.UserId);
			});

			modelBuilder.Entity<Position>(entity =>
			{
				entity.HasMany(p => p.Employees)
					  .WithOne(e => e.Position)
					  .HasForeignKey(p => p.PositionId);
			});

			modelBuilder.Entity<Employee>(entity =>
			{
				entity.HasOne(e => e.User)
					  .WithOne()
					  .HasForeignKey<Employee>(e => e.UserId);

				entity.HasOne(e => e.Position)
					  .WithMany(p => p.Employees)
					  .HasForeignKey(e => e.PositionId);
			});

			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasMany(c => c.Products)
					  .WithOne(p => p.Category)
					  .HasForeignKey(p => p.CategoryId);
			});

			modelBuilder.Entity<Specification>(entity =>
			{
				entity.HasMany(s => s.Products)
					  .WithMany(p => p.Specifications);
			});

			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasOne(p => p.Category)
					  .WithMany(c => c.Products)
					  .HasForeignKey(p => p.CategoryId);

				entity.HasMany(p => p.Specifications)
					  .WithMany(s => s.Products);
			});

			modelBuilder.Entity<Order>(entity =>
			{
				entity.HasOne(o => o.User)
					  .WithMany(u => u.Orders)
					  .HasForeignKey(o => o.UserId);

				entity.HasOne(o => o.Employee)
					  .WithMany(e => e.Orders)
					  .HasForeignKey(o => o.EmployeeId);

				entity.HasOne(o => o.Status)
					  .WithMany()
					  .HasForeignKey(o => o.StatusId);

				entity.HasMany(o => o.Details)
					  .WithOne(d => d.Order)
					  .HasForeignKey(d => d.OrderId);
			});

			modelBuilder.Entity<OrderDetail>(entity =>
			{
				entity.HasKey(d => new { d.ProductId, d.OrderId });

				entity.HasOne(d => d.Order)
					  .WithMany(o => o.Details)
					  .HasForeignKey(d => d.OrderId);

				entity.HasOne(d => d.Product)
					  .WithMany()
					  .HasForeignKey(d => d.ProductId);
			});

			modelBuilder.Entity<Cart>(entity =>
			{
				entity.HasOne(c => c.User)
					  .WithOne(p => p.Cart)
					  .HasForeignKey<Cart>(c => c.UserId);
			});

			modelBuilder.Entity<CartItem>(entity =>
			{
				entity.HasKey(i => new { i.ProductId, i.CartId });

				entity.HasOne(i => i.Cart)
					  .WithMany(c => c.CartItems)
					  .HasForeignKey(i => i.CartId);

				entity.HasOne(i => i.Product)
					  .WithMany()
					  .HasForeignKey(i => i.ProductId);
			});

			base.OnModelCreating(modelBuilder);
        }
    }
}
