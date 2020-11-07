using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("PRODUCT.ProductGift")]
    public partial class ProductGift
    {
        public long ProductGiftId { get; set; }

        public long ProductId { get; set; }

        public long GiftId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Gift Gift { get; set; }

        public virtual Product Product { get; set; }
    }
}
