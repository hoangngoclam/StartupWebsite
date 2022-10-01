using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Classes
{
    public class StartupStatus
    {
        public StartupStatus(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }
}