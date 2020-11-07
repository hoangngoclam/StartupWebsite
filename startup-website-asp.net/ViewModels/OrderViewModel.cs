using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.ViewModels
{
    public class OrderViewModel
    {
        public ICollection<OrderDetail> OrderDetails;
        public OrderViewModel()
        {
            OrderDetails = new List<OrderDetail>();
        }
        
        public OrderViewModel(Order orderEF)
        {
            OrderDetails = new List<OrderDetail>();
            this.OrderId = orderEF.OrderId;
            this.CustomerId = orderEF.CustomerId;
            this.PhoneNumber = orderEF.PhoneNumber;
            this.Address = orderEF.Address;
            this.Name = orderEF.Name;
            this.Email = orderEF.Email;
            this.IsTrial = orderEF.IsTrial;
            this.TotalPrice = orderEF.TotalPrice;
            this.Note = orderEF.Note;
            this.ShipPrice = orderEF.ShipPrice;
            this.Startup = orderEF.Startup;
            this.Status = orderEF.Status;
            this.StartupId = orderEF.StartupId;
            this.CreatedAt = orderEF.CreatedAt;
            this.UpdatedAt = orderEF.UpdatedAt;
        }
        public long OrderId { get; set; }
        public Startup Startup { get; set; }
        public long? StartupId { get; set; }

        public Guid? CustomerId { get; set; }

        
        public string PhoneNumber { get; set; }

        
        public string Address { get; set; }

        
        public string Name { get; set; }

        
        public string Email { get; set; }

        public bool? IsTrial { get; set; }

        public long? TotalPrice { get; set; }

        
        public string Note { get; set; }
        public string Status { get; set; }

        public long? ShipPrice { get; set; }

        
        public DateTime? CreatedAt { get; set; }

        
        public DateTime? UpdatedAt { get; set; }
    }
}