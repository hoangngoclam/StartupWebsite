using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models
{
    [Serializable]
    public class FavoriteProductViewModel
    {
        public Product Product { get; set; }
    }
}