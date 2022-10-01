using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models
{
    [Table("SYSTEM.Log")]
    public partial class Log
    {
        public long Id { get; set; }

        public int? IdUser { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        [StringLength(500)]
        public string SubConent { get; set; }

        [Column(TypeName = "ntext")]
        public string DetailContent { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }
    }
}
