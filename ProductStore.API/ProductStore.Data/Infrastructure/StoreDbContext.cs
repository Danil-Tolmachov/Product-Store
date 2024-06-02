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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Configure entities
			modelBuilder.Entity<Contact>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
				      .ValueGeneratedOnAdd();

				entity.HasOne(c => c.Person)
					  .WithMany(p => p.Contacts)
					  .HasForeignKey(c => c.PersonId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<Person>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					  .ValueGeneratedOnAdd();

				entity.Property(p => p.Discount)
					  .HasColumnType("decimal(5,4)");

				entity.HasMany(p => p.Contacts)
					  .WithOne(c => c.Person)
					  .HasForeignKey(p => p.PersonId);

				entity.HasOne(p => p.User)
					  .WithOne(u => u.Person)
					  .HasForeignKey<User>(u => u.PersonId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					  .ValueGeneratedOnAdd();

				entity.HasIndex(p => p.Username)
				      .IsUnique();

				entity.HasOne(u => u.Person)
					  .WithOne(p => p.User)
					  .HasForeignKey<User>(p => p.PersonId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(u => u.Cart)
					  .WithOne(c => c.User)
					  .HasForeignKey<Cart>(c => c.UserId)
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasMany(u => u.Orders)
					  .WithOne(o => o.User)
					  .HasForeignKey(o => o.UserId);
			});

			modelBuilder.Entity<Position>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					  .ValueGeneratedOnAdd();

				entity.HasMany(p => p.Employees)
					  .WithOne(e => e.Position)
					  .HasForeignKey(p => p.PositionId);
			});

			modelBuilder.Entity<Employee>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					  .ValueGeneratedOnAdd();

				entity.HasOne(e => e.User)
					  .WithOne()
					  .HasForeignKey<Employee>(e => e.UserId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(e => e.Position)
					  .WithMany(p => p.Employees)
					  .HasForeignKey(e => e.PositionId)
					  .OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)

					  .ValueGeneratedOnAdd();
				entity.HasMany(c => c.Products)
					  .WithOne(p => p.Category)
					  .HasForeignKey(p => p.CategoryId);
			});

			modelBuilder.Entity<Specification>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)

					  .ValueGeneratedOnAdd();
				entity.HasMany(s => s.Products)
					  .WithMany(p => p.Specifications)
					  .UsingEntity(e =>
						e.HasData(factory.GetProductSpecificationData())
					  );
			});

			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					  .ValueGeneratedOnAdd();

				entity.Property(p => p.Discount)
					  .HasColumnType("decimal(5,4)");

				entity.Property(p => p.Price)
					  .HasColumnType("smallmoney");

				entity.HasOne(p => p.Category)
					  .WithMany(c => c.Products)
					  .HasForeignKey(p => p.CategoryId)
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasMany(p => p.Specifications)
					  .WithMany(s => s.Products);

				entity.HasMany(p => p.Images)
					  .WithOne(i => i.Product)
					  .HasForeignKey(i => i.ProductId);
			});

			modelBuilder.Entity<ProductImage>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					  .ValueGeneratedOnAdd();

				entity.Property(i => i.Image)
					  .HasColumnType("image");

				entity.HasOne(i => i.Product)
					  .WithMany(p => p.Images)
					  .HasForeignKey(i => i.ProductId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<Order>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					  .ValueGeneratedOnAdd();

				entity.HasOne(o => o.User)
					  .WithMany(u => u.Orders)
					  .HasForeignKey(o => o.UserId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(o => o.Employee)
					  .WithMany(e => e.Orders)
					  .HasForeignKey(o => o.EmployeeId)
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasOne(o => o.Status)
					  .WithMany()
					  .HasForeignKey(o => o.StatusId)
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasMany(o => o.Details)
					  .WithOne(d => d.Order)
					  .HasForeignKey(d => d.OrderId);
			});

			modelBuilder.Entity<OrderDetail>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					  .ValueGeneratedOnAdd();

				entity.HasKey(d => new { d.ProductId, d.OrderId });

				entity.Property(p => p.UnitPrice)
					  .HasColumnType("smallmoney");

				entity.HasOne(d => d.Order)
					  .WithMany(o => o.Details)
					  .HasForeignKey(d => d.OrderId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(d => d.Product)
					  .WithMany()
					  .HasForeignKey(d => d.ProductId)
					  .OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<Cart>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					  .ValueGeneratedOnAdd();

				entity.HasOne(c => c.User)
					  .WithOne(p => p.Cart)
					  .HasForeignKey<Cart>(c => c.UserId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<CartItem>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Id)
					  .ValueGeneratedOnAdd();

				entity.HasKey(i => new { i.ProductId, i.CartId });

				entity.HasOne(i => i.Cart)
					  .WithMany(c => c.CartItems)
					  .HasForeignKey(i => i.CartId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(i => i.Product)
					  .WithMany()
					  .HasForeignKey(i => i.ProductId)
					  .OnDelete(DeleteBehavior.Restrict);
			});

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
			modelBuilder.Entity<Cart>().HasData(factory.GetCartData());


			base.OnModelCreating(modelBuilder);
		}

		public void ApplyMigrations()
		{
			if (Database.GetPendingMigrations().Any())
			{
				Database.Migrate();
			}
		}
	}
}
