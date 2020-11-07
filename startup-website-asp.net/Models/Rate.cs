using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models
{
    [Table("ORDER.Rate")]
    public partial class Rate
    {
        public long Id { get; set; }

        public long OrderDetailId { get; set; }

        public long ProductId { get; set; }

        [StringLength(500)]
        public string ImagesUrl { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        public byte? RateNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }

        public virtual Product Product { get; set; }
    }
}
