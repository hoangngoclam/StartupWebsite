using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("CUSTOMER.Customer")]
    public partial class Customer
    {
        public Customer()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            ProductLikeds = new HashSet<ProductLiked>();
            StartupLikeds = new HashSet<StartupLiked>();
        }
        [StringLength(15)]
        public string ProvinceId { get; set; }
        [StringLength(15)]
        public string DistrictId { get; set; }
        [StringLength(15)]
        public string WardId { get; set; }
        [StringLength(200)]
        public string ProvinceName { get; set; }
        public Guid CustomerId { get; set; }
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(70)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Password { get; set; }

        [StringLength(500)]
        public string FbId { get; set; }

        [StringLength(500)]
        public string GgId { get; set; }

        [StringLength(500)]
        public string DisplayName { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(700)]
        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDay { get; set; }

        [Required]
        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string AvatarUrl { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        
        public virtual ICollection<Cart> Carts { get; set; }

        
        public virtual ICollection<Order> Orders { get; set; }

        
        public virtual ICollection<ProductLiked> ProductLikeds { get; set; }

        
        public virtual ICollection<StartupLiked> StartupLikeds { get; set; }
    }
}
