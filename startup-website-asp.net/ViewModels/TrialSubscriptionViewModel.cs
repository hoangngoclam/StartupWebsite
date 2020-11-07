
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.ViewModels
{
    public class TrialSubscriptionViewModel
    {
        public TrialSubscriptionViewModel()
        {
            Name = "";
            PhoneNumber = "";
            Email = "";
            Address = "";
            ProductId = 0;
        }
        public TrialSubscriptionViewModel(long productId)
        {
            Name = "";
            PhoneNumber = "";
            Email = "";
            Address = "";
            ProductId = productId;
        }

        public TrialSubscriptionViewModel(string name, string phoneNumber, string address, string email, long productId, long? startupId)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            ProductId = productId;
            StartupId = startupId;
        }
        public long ProductId { get; set; }
        [Required(ErrorMessage = "Nhập họ tên của bạn!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nhập số điện thoại của bạn!")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Nhập địa chỉ nhận hàng của bạn!")]
        public string Address { get; set; }
        public long? StartupId { get; set; }
        public string Email { get; set; }
        public Guid CustomerId { get; set; }
        public string Note { get; set; }
    }
}