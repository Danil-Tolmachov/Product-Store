using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure.Data;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Infrastructure
{
    public class StoreDbContext : DbContext
    {
        private readonly IDataFactory factory;

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Person> Persons { get; set; }
		public DbSet<Position> Positions { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Specification> Specifications { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<User> Users { get; set; }

		public StoreDbContext(DbContextOptions options, IDataFactory factory) : base(options)
        {
            this.factory = factory;

			Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Seed database
			modelBuilder.Entity<Contact>().HasData(factory.GetContactData());
			modelBuilder.Entity<Person>().HasData(factory.GetPersonData());
			modelBuilder.Entity<User>().HasData(factory.GetUserData());
			modelBuilder.Entity<Position>().HasData(factory.GetPositionData());
			modelBuilder.Entity<Employee>().HasData(factory.GetEmployeeData());
			modelBuilder.Entity<Category>().HasData(factory.GetCategoryData());
			modelBuilder.Entity<Specification>().HasData(factory.GetSpecificationData());
			modelBuilder.Entity<Product>().HasData(factory.GetProductData());
			modelBuilder.Entity<Order>().HasData(factory.GetOrderData());
			modelBuilder.Entity<OrderDetail>().HasData(factory.GetOrderDetailData());
			modelBuilder.Entity<Status>().HasData(factory.GetStatusData());
			modelBuilder.Entity<ProductImage>().HasData(factory.GetProductImageData());

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
				entity.HasIndex(p => p.Username)
				      .IsUnique();

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
					  .WithMany(p => p.Specifications)
					  .UsingEntity(e =>
						e.HasData(factory.GetProductSpecificationData())
					  );
			});

			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasOne(p => p.Category)
					  .WithMany(c => c.Products)
					  .HasForeignKey(p => p.CategoryId);

				entity.HasMany(p => p.Specifications)
					  .WithMany(s => s.Products);

				entity.HasMany(p => p.Images)
					  .WithOne(i => i.Product)
					  .HasForeignKey(i => i.ProductId);
			});

			modelBuilder.Entity<ProductImage>(entity =>
			{
				entity.HasOne(i => i.Product)
					  .WithMany(p => p.Images)
					  .HasForeignKey(i => i.ProductId);
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
