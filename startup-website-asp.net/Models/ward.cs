using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models
{
    [Table("LOCATION.ward")]
    public partial class ward
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ward()
        {
            Customers = new HashSet<Customer>();
            Startups = new HashSet<Startup>();
        }

        [StringLength(6)]
        public string Id { get; set; }

        [Required]
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

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual District District { get; set; }

        public virtual ICollection<Startup> Startups { get; set; }
    }
}
