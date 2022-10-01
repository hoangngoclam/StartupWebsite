using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("STARTUP.StartupLiked")]
    public partial class StartupLiked
    {
        public long StartupLikedId { get; set; }

        public long StartupId { get; set; }

        public Guid CustomerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Startup Startup { get; set; }
    }
}
