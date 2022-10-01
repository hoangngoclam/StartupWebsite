using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.ApiViewModels
{
    public class RateItemApiViewModel
    {
        public RateItemApiViewModel()
        {

        }

        public RateItemApiViewModel(Product productInput, OrderDetail orderDetailInput,long startupIdInput)
        {
            StartupId = startupIdInput;
            ProductId = productInput.ProductId;
            OrderDetailId = orderDetailInput.OrderDetailId;
            ProductImageUrl = productInput.MainImage;
            ProductName = productInput.Name;
            Quality = orderDetailInput.Quality;
            TotalPrice = orderDetailInput.TotalPrice;
        }
        public RateItemApiViewModel(Rate rateInput)
        {
            Product product = rateInput.Product;
            OrderDetail orderDetail = rateInput.OrderDetail;
            StartupId = rateInput.StartupId;
            ProductId = rateInput.ProductId;
            OrderDetailId = rateInput.OrderDetailId;
            ImageUrl = rateInput.ImagesUrl;
            RateNumber = rateInput.RateNumber;
            ProductImageUrl = product.MainImage;
            ProductName = product.Name;
            Quality = orderDetail.Quality;
            TotalPrice = orderDetail.TotalPrice;
            Content = rateInput.Content;
        }

        public long StartupId { get; set; }
        public long ProductId { get; set; }
        public long OrderDetailId { get; set; }
        public string ImageUrl { get; set; }
        public byte? RateNumber { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductName { get; set; }
        public int? Quality { get; set; }
        public long? TotalPrice { get; set; }
        public string Content { get; set; }
    }
}