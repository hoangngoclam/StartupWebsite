using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("ORDER.Order")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        public long StartupId { get; set; }
        public long OrderId { get; set; }
        public Guid? CustomerId { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(700)]
        public string Address { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public bool? IsTrial { get; set; }

        public long? TotalPrice { get; set; }

        [Column(TypeName = "ntext")]
        public string Note { get; set; }

        public long? ShipPrice { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Startup Startup { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
