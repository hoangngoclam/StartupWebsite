using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models
{
    [Table("PRODUCT.Gift")]
    public partial class Gift
    {
        public Gift()
        {
            ProductGifts = new HashSet<ProductGift>();
        }

        public long Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public int? Quality { get; set; }

        public long? price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<ProductGift> ProductGifts { get; set; }
    }
}
