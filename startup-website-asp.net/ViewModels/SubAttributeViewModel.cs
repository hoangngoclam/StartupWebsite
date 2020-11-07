using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models
{
    public class SubAttributeViewModel
    {
        public SubAttributeViewModel()
        {

        }

        public int AttributeRelationshipId { get; set; }

        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public long ProductId { get; set; }
        public int SubAttributeId1 { get; set; }
        public long price { get; set; }
    }
}