using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace startup_website_asp.net.Models.EF
{
    [Table("STARTUP.StartupAccount")]
    public partial class StartupAccount
    {
        public StartupAccount()
        {
            Blogs = new HashSet<Blog>();
        }

        public int StartupAccountId { get; set; }

        public long? StatupId { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        [StringLength(500)]
        public string DisplayName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(500)]
        public string AvatarUrl { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDay { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        
        public virtual ICollection<Blog> Blogs { get; set; }

        public virtual Startup Startup { get; set; }
    }
}
