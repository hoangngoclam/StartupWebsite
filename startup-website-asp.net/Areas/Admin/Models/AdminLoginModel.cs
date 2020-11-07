using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models
{
    public class AdminLoginModel
    {
        [Required(ErrorMessage = "Nhập tên đăng nhập!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu!")]
        public string Password { get; set; }
    }
}