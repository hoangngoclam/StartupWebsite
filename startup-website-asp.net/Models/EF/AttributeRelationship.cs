using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("ATTRIBUTE.AttributeRelationship")]
    public partial class AttributeRelationship
    {
        public AttributeRelationship()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int AttributeRelationshipId { get; set; }

        public long ProductId { get; set; }

        public int? SubAttributeId1 { get; set; }

        public int? SubAttributeId2 { get; set; }

        public long? price { get; set; }

        [StringLength(500)]
       
        public string imageUrl { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Product Product { get; set; }

        public virtual SubAttribute SubAttribute { get; set; }

        public virtual SubAttribute SubAttribute1 { get; set; }

        
        public virtual ICollection<Cart> Carts { get; set; }

        
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
