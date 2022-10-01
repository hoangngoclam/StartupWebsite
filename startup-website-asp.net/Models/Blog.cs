using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models
{
    [Table("BLOG.Blog")]
    public partial class Blog
    {
        public long Id { get; set; }

        public int? StartupBlogCategoryId { get; set; }

        public int? BlogCategoryId { get; set; }

        public int? SystemAdminAuthId { get; set; }

        public int? StartupAccountId { get; set; }

        [StringLength(500)]
        public string MainImageUrl { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(400)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        public string SeoTitle { get; set; }

        [StringLength(500)]
        public string Label { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public bool? AllowComment { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public long? ViewCount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }

        public virtual StartupAccount StartupAccount { get; set; }

        public virtual StartupBlogCategory StartupBlogCategory { get; set; }

        public virtual SystemAccount SystemAccount { get; set; }
    }
}
