using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models
{
    public class ProductCategoryViewModel
    {
        public ProductCategoryViewModel()
        {

        }
        public ProductCategoryViewModel(ProductCategory productCategoryInput)
        {
            this.Name = productCategoryInput.Name;
            this.ProductCategoryId = productCategoryInput.ProductCategoryId;
            this.ParentId = productCategoryInput.ParentCategoryId;
            this.MetaTitle = productCategoryInput.MetaTitle;
            this.DisplayOrder = productCategoryInput.DisplayOrder;
            this.SeoTitle = productCategoryInput.SeoTitle;
            this.Status = productCategoryInput.Status;
            this.CreatedAt = productCategoryInput.CreatedAt;
            this.UpdatedAt = productCategoryInput.UpdatedAt;
        }
        public List<ProductCategoryViewModel> listChild = new List<ProductCategoryViewModel>();
        public string Name { get; set; }
        public string RoundName { get; set; }
        public int? ProductCategoryId { get; set; }
        public string MetaTitle { get; set; }
        public double? DisplayOrder { get; set; }
        public string SeoTitle { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? ParentId { get; set; }

    }
}