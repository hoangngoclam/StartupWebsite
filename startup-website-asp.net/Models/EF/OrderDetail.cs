using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("ORDER.OrderDetail")]
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            Rates = new HashSet<Rate>();
        }

        public long OrderDetailId { get; set; }

        public long ProductId { get; set; }

        public long OrderId { get; set; }

        public int? AttributeRelationshipId { get; set; }

        public int? Quality { get; set; }

        public long? TotalPrice { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual AttributeRelationship AttributeRelationship { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
