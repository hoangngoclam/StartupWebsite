﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Common
{
    [Serializable]
    public class AdminLogin
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}