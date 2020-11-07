using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace startup_website_asp.net.Models.EF
{
    [Table("ATTRIBUTE.Attribute")]
    public partial class Attribute
    {
        public Attribute()
        {
            Products = new HashSet<Product>();
            Products1 = new HashSet<Product>();
            SubAttributes = new HashSet<SubAttribute>();
        }

        public int AttributeId { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        
        public virtual ICollection<Product> Products { get; set; }

        
        public virtual ICollection<Product> Products1 { get; set; }

        
        public virtual ICollection<SubAttribute> SubAttributes { get; set; }
    }
}
