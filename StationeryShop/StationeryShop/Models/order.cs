namespace StationeryShop.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	[Table("order")]
	public partial class order
	{
		public static class Status
		{
			public const string PENDING = "Đang chờ xử lý";
			public const string ACCEPTED = "Đơn hàng được chấp nhận";
			public const string CANCELED = "Đơn hàng bị hủy";
			public const string DENIED = "Đơn hàng bị từ chối";
			public const string DELIVERING = "Đơn hàng đang được giao";
			public const string DELIVERED = "Giao hàng thành công";
		}
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public order()
		{
			order_details = new HashSet<order_details>();
		}

		public long id { get; set; }

		public long cus_id { get; set; }

		[Column(TypeName = "date")]
		public DateTime order_date { get; set; }

		[StringLength(100)]
		public string status { get; set; }

		[StringLength(500)]
		public string notes { get; set; }

		[StringLength(500)]
		public string address { get; set; }

		[StringLength(12)]
		public string tel { get; set; }

		[StringLength(200)]
		public string name { get; set; }

		public virtual customer customer { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<order_details> order_details { get; set; }
		public int total { get => order_details.Sum(d => d.price * d.quantity); }
	}
}
