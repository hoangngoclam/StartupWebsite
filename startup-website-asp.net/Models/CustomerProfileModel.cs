using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models
{
    public class CustomerProfileModel
    {

        [Required(ErrorMessage = "Nhập tên!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nhập địa chỉ!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Chọn giới tính!")]
        public string Gender { get; set; }

        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        [Required(ErrorMessage = "Nhập số điện thoại!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Nhập email!")]
        public string Email { get; set; }


    }
}