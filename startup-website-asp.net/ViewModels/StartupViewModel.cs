using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.ViewModels
{
    public class StartupViewModel
    {
        public StartupViewModel()
        {
            IsLike = false;
        }
        public int LikeNumber { get; set; }
        public int ProductNumber { get; set; }
        public bool IsLike { get; set; }
        public long StartupId { get; set; }
        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public string ProvinceName { get; set; }
        public int StartupTypeId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string CoverUrl { get; set; }
        public string Description { get; set; }
        public string Slogan { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string FanpageLink { get; set; }
        public string YoutubeLink { get; set; }
        public string InstagramLink { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}