using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EndTermProjectBack.Models.EFModels
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("name=AppDbContext")
		{
		}

		public virtual DbSet<BannerCarousel> BannerCarousels { get; set; }
		public virtual DbSet<Brand> Brands { get; set; }
		public virtual DbSet<Capacity> Capacities { get; set; }
		public virtual DbSet<CartItem> CartItems { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<City> Cities { get; set; }
		public virtual DbSet<District> Districts { get; set; }
		public virtual DbSet<FAQ> FAQs { get; set; }
		public virtual DbSet<Favorite> Favorites { get; set; }
		public virtual DbSet<Member> Members { get; set; }
		public virtual DbSet<News> News { get; set; }
		public virtual DbSet<Note> Notes { get; set; }
		public virtual DbSet<OrderItem> OrderItems { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<Paytype> Paytypes { get; set; }
		public virtual DbSet<ProductImage> ProductImages { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<SecondaryCategory> SecondaryCategories { get; set; }
		public virtual DbSet<Status> Status { get; set; }
		public virtual DbSet<TextCarousel> TextCarousels { get; set; }
		public virtual DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Brand>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.Brand)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Capacity>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.Capacity)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Category>()
				.HasMany(e => e.SecondaryCategories)
				.WithRequired(e => e.Category)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<City>()
				.HasMany(e => e.Districts)
				.WithRequired(e => e.City)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<District>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.District)
				.HasForeignKey(e => e.DistrictsId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Favorites)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Note>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.Note)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Order>()
				.HasMany(e => e.OrderItems)
				.WithRequired(e => e.Order)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Paytype>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Paytype)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.CartItems)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.Favorites)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.OrderItems)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.ProductImages)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SecondaryCategory>()
				.HasMany(e => e.FAQs)
				.WithRequired(e => e.SecondaryCategory)
				.HasForeignKey(e => e.SecondaryCategoriesId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SecondaryCategory>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.SecondaryCategory)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Status>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Status)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.Property(e => e.Password)
				.IsUnicode(false);
		}
	}
}
