using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace startup_website_asp.net.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {

        }
        public ProductViewModel(Product productInput)
        {
            if(productInput != null)
            {
                this.ProductId = productInput.ProductId;
                this.ProductCategoryId = productInput.ProductCategoryId;
                this.StartupId = productInput.StartupId;
                this.AttributeId1 = productInput.AttributeId1;
                this.AttributeId2 = productInput.AttributeId2;
                this.StartupCategoryId = productInput.StartupCategoryId;
                this.Name = productInput.Name;
                this.MetaTitle = productInput.metaTitle;
                this.SeoTitle = productInput.SeoTitle;
                this.Price = productInput.Price;
                this.PromotionPrice = productInput.PromotionPrice;
                this.Quality = productInput.Quality;
                this.Description = productInput.Description;
                this.Status = productInput.Status;
                this.ViewCount = productInput.ViewCount;
                this.KeyWord = productInput.KeyWord;
                this.MainImage = productInput.MainImage;
                this.isTrial = productInput.isTrial;
                this.SubImages = productInput.SubImages;
                this.CreatedAt = productInput.CreatedAt;
                this.ListSubImage = productInput.SubImages.Split(',').ToList();
            }
            
        }
        public long ProductId { get; set; }
        public string idGift { get; set; }
        [Required(ErrorMessage = "sản phẩm chưa có danh mục")]
        public int ProductCategoryId { get; set; }
        public string ProductCategoryRender { get; set; }
        public string ProductCategoryTitle { get; set; }
        public int ChildCategoryId { get; set; }
        public string StartupName { get; set; }
        public bool IsLike { get; set; }
        public string wardName { get; set; }
        public string LogoUrl { get; set; }
        public long likeNumber { get; set; }
        public string  MetaTitle { get; set; }
        public int GrandChildCategoryId { get; set; }
        public long StartupId { get;set; }
        public int? AttributeId1 { get; set; }
        public int? AttributeId2 { get; set; }
        public int? StartupCategoryId { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm")]
        public string Name { get; set; }
        public string SeoTitle { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá của sản phẩm")]
        public long? Price { get; set; }
        public long? PromotionPrice { get; set; }
        public int? Quality { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Sản phẩm chưa có trạng thái")]
        public string Status { get; set; }
        public long? ViewCount { get; set; }
        public string KeyWord { get; set; }
        public string MainImage { get; set; }
        public bool? isTrial { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string SubImages { get; set; }
        public List<String> ListSubImage { get; set; }
        private string MakeMetaTitle(string str)
        {
            str = str.ToLower();
            str = Regex.Replace(str, @" à | á | ạ | ả | ã | â | ầ | ấ | ậ | ẩ | ẫ | ă | ằ | ắ | ặ | ẳ | ẵ", "a");
            str = Regex.Replace(str, @" è | é | ẹ | ẻ | ẽ | ê | ề | ế | ệ | ể | ễ", "e");
            str = Regex.Replace(str, @"ì | í | ị | ỉ | ĩ", "i");
            str = Regex.Replace(str, @"ò | ó | ọ | ỏ | õ | ô | ồ | ố | ộ | ổ | ỗ | ơ | ờ | ớ | ợ | ở | ỡ", "o");
            str = Regex.Replace(str, @"ù | ú | ụ | ủ | ũ | ư | ừ | ứ | ự | ử | ữ", "u");
            str = Regex.Replace(str, @"ỳ | ý | ỵ | ỷ | ỹ", "y");
            str = Regex.Replace(str, @" đ ", "d");
            str = Regex.Replace(str, @"!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\""|\&|\#|\[|\]|~|$|_", "-");
            str = Regex.Replace(str, @" -+- ", "-");//thay thế 2- thành 1-
            /* tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự - */
            str = Regex.Replace(str, @" ^\-+|\-+$", "");
            return str;
        }

        public int GetProductCategoryId()
        {
            int productCategoryId = 0;
            if (this.GrandChildCategoryId != 0)
            {
                productCategoryId = this.GrandChildCategoryId;
            }
            else if (this.ChildCategoryId != 0)
            {
                productCategoryId = this.ChildCategoryId;
            }
            return productCategoryId;
        }
        public string GetMetaTitle()
        {
            return this.MakeMetaTitle(this.Name);
        }
    }
}