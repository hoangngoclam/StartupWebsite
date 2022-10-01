using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models
{
    public class DbContextSt : StartupWebsite
    {
        private static StartupWebsite db;
        public static StartupWebsite Db
        {
            get
            {
                if (db == null)
                {
                    db = new StartupWebsite();
                    return db;
                }
                else return db;
            }
        }
    }
}