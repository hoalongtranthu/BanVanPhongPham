namespace StationeryShop.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("promotion")]
	public partial class promotion
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public promotion()
		{
			promotion_details = new HashSet<promotion_details>();
		}

		public long id { get; set; }

		[StringLength(200)]
		public string name { get; set; }

		[Column(TypeName = "date")]
		public DateTime from_date { get; set; }

		[Column(TypeName = "date")]
		public DateTime to_date { get; set; }

		[Column(TypeName = "ntext")]
		public string description { get; set; }

		[Column(TypeName = "ntext")]
		public string content { get; set; }

		[StringLength(500)]
		public string image { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<promotion_details> promotion_details { get; set; }
	}
}
