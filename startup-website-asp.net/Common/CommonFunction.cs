using Glimpse.AspNet.Tab;
using startup_website_asp.net.Common;
using startup_website_asp.net.Models;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Common
{
    public static class CommonFunction
    {
        public static string LimitLength(this string str, int length)
        {
            if (str == null)
                return str;
            return str.Substring(0, Math.Min(Math.Max(0, length), str.Length));
        }
    }
}