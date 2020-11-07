using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("PRODUCT.Product")]
    public partial class Product
    {
        public Product()
        {
            AttributeRelationships = new HashSet<AttributeRelationship>();
            Carts = new HashSet<Cart>();
            ProductLikeds = new HashSet<ProductLiked>();
            OrderDetails = new HashSet<OrderDetail>();
            Rates = new HashSet<Rate>();
            Codes = new HashSet<Code>();
            ProductGifts = new HashSet<ProductGift>();
        }

        public long ProductId { get; set; }

        public int ProductCategoryId { get; set; }

        public long StartupId { get; set; }

        public int? AttributeId1 { get; set; }

        public int? AttributeId2 { get; set; }

        public int? StartupCategoryId { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(200)]
        public string metaTitle { get; set; }

        [StringLength(500)]
        public string SeoTitle { get; set; }

        public long? Price { get; set; }

        public long? PromotionPrice { get; set; }

        public int? Quality { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public long? ViewCount { get; set; }

        [StringLength(500)]
        public string KeyWord { get; set; }

        [StringLength(300)]
        public string MainImage { get; set; }

        public bool? isTrial { get; set; }

        [StringLength(500)]
        public string SubImages { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Attribute Attribute { get; set; }

        public virtual Attribute Attribute1 { get; set; }

        
        public virtual ICollection<AttributeRelationship> AttributeRelationships { get; set; }

        
        public virtual ICollection<Cart> Carts { get; set; }

        
        public virtual ICollection<ProductLiked> ProductLikeds { get; set; }

        
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        
        public virtual ICollection<Rate> Rates { get; set; }

        
        public virtual ICollection<Code> Codes { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public virtual StartupCategory StartupCategory { get; set; }

        public virtual Startup Startup { get; set; }

        
        public virtual ICollection<ProductGift> ProductGifts { get; set; }
    }
}
