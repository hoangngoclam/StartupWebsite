using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("STARTUP.Startup")]
    public partial class Startup
    {
        public Startup()
        {
            StartupBlogCategories = new HashSet<StartupBlogCategory>();
            Products = new HashSet<Product>();
            StartupCategories = new HashSet<StartupCategory>();
            Codes = new HashSet<Code>();
            StartupAccounts = new HashSet<StartupAccount>();
            StartupLikeds = new HashSet<StartupLiked>();
            Rates = new HashSet<Rate>();
            Orders = new HashSet<Order>();
        }

        public long StartupId { get; set; }
        [StringLength(15)]
        public string ProvinceId { get; set; }
        [StringLength(15)]
        public string DistrictId { get; set; }
        [StringLength(15)]
        public string WardId { get; set; }
        [StringLength(200)]
        public string ProvinceName { get; set; }
        [Required]
        public int StartupTypeId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(500)]
        public string LogoUrl { get; set; }

        [StringLength(500)]
        public string CoverUrl { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(300)]
        public string Slogan { get; set; }

        [StringLength(700)]
        public string Address { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string FanpageLink { get; set; }

        [StringLength(500)]
        public string YoutubeLink { get; set; }

        [StringLength(500)]
        public string InstagramLink { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        
        public virtual ICollection<StartupBlogCategory> StartupBlogCategories { get; set; }

        public virtual StartupType StartupType { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        
        public virtual ICollection<StartupCategory> StartupCategories { get; set; }

        
        public virtual ICollection<Code> Codes { get; set; }

        
        public virtual ICollection<StartupAccount> StartupAccounts { get; set; }

        
        public virtual ICollection<StartupLiked> StartupLikeds { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
