using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models
{
    [Table("PROMOTION.Code")]
    public partial class Code
    {
        public int Id { get; set; }

        public long StartupId { get; set; }

        public long ProductId { get; set; }

        public byte? PercentDiscount { get; set; }

        public long? PriceDiscount { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? StartTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Product Product { get; set; }

        public virtual Startup Startup { get; set; }
    }
}
