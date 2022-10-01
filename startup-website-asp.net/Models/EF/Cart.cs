using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("CUSTOMER_PRODUCT.Cart")]
    public partial class Cart
    {
        public long CartId { get; set; }

        public long ProductId { get; set; }

        public Guid CustomerId { get; set; }

        public int? AttributeRelationshipId { get; set; }

        public int Quality { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual AttributeRelationship AttributeRelationship { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
