using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models
{
    [Table("ATTRIBUTE.SubAttribute")]
    public partial class SubAttribute
    {
        public SubAttribute()
        {
            AttributeRelationships = new HashSet<AttributeRelationship>();
            AttributeRelationships1 = new HashSet<AttributeRelationship>();
        }

        public int Id { get; set; }

        public int AttributeId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string TextColor { get; set; }

        [Required]
        [StringLength(20)]
        public string BackgroundColor { get; set; }

        public int? Index { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Attribute Attribute { get; set; }

        public virtual ICollection<AttributeRelationship> AttributeRelationships { get; set; }

        public virtual ICollection<AttributeRelationship> AttributeRelationships1 { get; set; }
    }
}
