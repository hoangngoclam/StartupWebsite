using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models
{
    public class StartupRegisterViewModel
    {
        [Required(ErrorMessage = "Nhập tên tài khoản!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu!")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu dài từ 6 kí tự trở lên")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Nhập lại mật khẩu!")]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp!")]
        public string ConfirmpPassword { get; set; }

        [Required(ErrorMessage = "Nhập tên!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nhập địa chỉ!")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "Chọn giới tính!")]
        //public string Gender { get; set; }

        [Required(ErrorMessage = "Nhập ngày sinh!")]
        public string Birthday { get; set; }


        [Required(ErrorMessage = "Nhập số điện thoại!")]
        public string PhoneNumber { get; set; }
    }
}