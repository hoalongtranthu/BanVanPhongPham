namespace StationeryShop.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class DBContext : DbContext
	{
		public DBContext()
				: base("name=DBContext")
		{
		}

		public virtual DbSet<admin> admins { get; set; }
		public virtual DbSet<cart> carts { get; set; }
		public virtual DbSet<category> categories { get; set; }
		public virtual DbSet<customer> customers { get; set; }
		public virtual DbSet<order> orders { get; set; }
		public virtual DbSet<order_details> order_details { get; set; }
		public virtual DbSet<product> products { get; set; }
		public virtual DbSet<promotion> promotions { get; set; }
		public virtual DbSet<promotion_details> promotion_details { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<category>()
					.HasMany(e => e.products)
					.WithRequired(e => e.category)
					.HasForeignKey(e => e.cat_id)
					.WillCascadeOnDelete(false);

			modelBuilder.Entity<customer>()
					.Property(e => e.email)
					.IsUnicode(false);

			modelBuilder.Entity<customer>()
					.Property(e => e.tel)
					.IsUnicode(false);

			modelBuilder.Entity<customer>()
					.HasMany(e => e.carts)
					.WithRequired(e => e.customer)
					.HasForeignKey(e => e.cus_id)
					.WillCascadeOnDelete(false);

			modelBuilder.Entity<customer>()
					.HasMany(e => e.orders)
					.WithRequired(e => e.customer)
					.HasForeignKey(e => e.cus_id)
					.WillCascadeOnDelete(false);

			modelBuilder.Entity<order>()
					.HasMany(e => e.order_details)
					.WithRequired(e => e.order)
					.HasForeignKey(e => e.order_id)
					.WillCascadeOnDelete(false);

			modelBuilder.Entity<product>()
					.Property(e => e.image)
					.IsUnicode(false);

			modelBuilder.Entity<product>()
					.HasMany(e => e.carts)
					.WithRequired(e => e.product)
					.HasForeignKey(e => e.prod_id)
					.WillCascadeOnDelete(false);

			modelBuilder.Entity<product>()
					.HasMany(e => e.order_details)
					.WithRequired(e => e.product)
					.HasForeignKey(e => e.prod_id)
					.WillCascadeOnDelete(false);

			modelBuilder.Entity<product>()
					.HasMany(e => e.promotion_details)
					.WithRequired(e => e.product)
					.HasForeignKey(e => e.prod_id)
					.WillCascadeOnDelete(false);

			modelBuilder.Entity<promotion>()
					.HasMany(e => e.promotion_details)
					.WithRequired(e => e.promotion)
					.HasForeignKey(e => e.prom_id)
					.WillCascadeOnDelete(false);
		}
	}
}
