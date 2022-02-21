namespace StationeryShop.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("product")]
	public partial class product
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public product()
		{
			carts = new HashSet<cart>();
			order_details = new HashSet<order_details>();
			promotion_details = new HashSet<promotion_details>();
		}

		public long id { get; set; }

		public long cat_id { get; set; }

		[StringLength(200)]
		public string name { get; set; }

		[StringLength(200)]
		public string origin { get; set; }

		public int price { get; set; }

		[StringLength(500)]
		public string image { get; set; }

		[Column(TypeName = "ntext")]
		public string description { get; set; }

		public int amount { get; set; }

		[StringLength(100)]
		public string unit { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<cart> carts { get; set; }

		public virtual category category { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<order_details> order_details { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<promotion_details> promotion_details { get; set; }
		public int Discount
		{
			get
			{
				var today = DateTime.Today;
				return promotion_details.Where(d => d.promotion.from_date <= today && today <= d.promotion.to_date).Select(d => d.discount).DefaultIfEmpty(0).Max();
			}
		}
		public int PromoPrice { get => (int)(price*(1-Discount/100f)); }
	}
}
