namespace StationeryShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin")]
    public partial class admin
    {
        public long id { get; set; }

        [StringLength(200)]
        public string username { get; set; }

        [StringLength(200)]
        public string password { get; set; }

        public bool active { get; set; }
    }
}
