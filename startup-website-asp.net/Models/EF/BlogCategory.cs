using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("BLOG.BlogCategory")]
    public partial class BlogCategory
    {
        public BlogCategory()
        {
            Blogs = new HashSet<Blog>();
            BlogCategory1 = new HashSet<BlogCategory>();
        }

        public int BlogCategoryId { get; set; }

        public int? ParentId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public double? DisplayOrder { get; set; }

        [StringLength(200)]
        public string SeoTitle { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        
        public virtual ICollection<Blog> Blogs { get; set; }

        
        public virtual ICollection<BlogCategory> BlogCategory1 { get; set; }

        public virtual BlogCategory BlogCategory2 { get; set; }
    }
}
