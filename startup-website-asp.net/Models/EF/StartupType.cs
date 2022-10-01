using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace startup_website_asp.net.Models.EF
{
    [Table("STARTUP.StartupType")]
    public class StartupType
    {
        public StartupType()
        {
            
        }
        [Key]
        public int StartupTypeId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Startup> Startups { get; set; }
    }
}