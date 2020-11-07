using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.ViewModels
{
    public class CartViewModel
    {
        public Startup startup { get; set; }
        public List<CartItemViewModel> carts { get; set; }
    }
}