using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("LOCATION.District")]
    public partial class District
    {
        public District()
        {
            wards = new HashSet<ward>();
        }

        [StringLength(6)]
        public string DistrictId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        [Required]
        [StringLength(6)]
        public string ProvinceId { get; set; }

        public virtual Province Province { get; set; }

        
        public virtual ICollection<ward> wards { get; set; }
    }
}
