namespace StationeryShop.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("cart")]
	public partial class cart
	{
		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long cus_id { get; set; }

		[Key]
		[Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long prod_id { get; set; }

		public int quantity { get; set; }

		public virtual customer customer { get; set; }

		public virtual product product { get; set; }
		public int total { get => product.PromoPrice * quantity; }
	}
}
