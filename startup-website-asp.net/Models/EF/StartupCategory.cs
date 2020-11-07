using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("PRODUCT_CATEGORY.StartupCategory")]
    public partial class StartupCategory
    {
        public StartupCategory()
        {
            Products = new HashSet<Product>();
        }

        public int StartupCategoryId { get; set; }

        public long? StartupId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public double? DisplayOrder { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(200)]
        public string SeoTitle { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        
        public virtual ICollection<Product> Products { get; set; }

        public virtual Startup Startup { get; set; }
    }
}
