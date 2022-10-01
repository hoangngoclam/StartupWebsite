using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
namespace startup_website_asp.net.Models.DAO
{
	public class ProductDAO:BaseDAO
    {
		private ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
		
		public ProductDAO():base()
		{

		}
		public ProductViewModel GetProductDetail(long productId)
		{
			ProductViewModel productModel = new ProductViewModel();
			productModel = this.GetProductById(productId,null);
			return productModel;
		}

		public Product GetByName(string name)
		{
			return db.Products.SingleOrDefault(x => x.Name == name);
		}
		public IEnumerable<Product> GetProductByStartupId(int startupId)
		{
			return db.Products.Where(x => x.StartupId == startupId);
		}
		public Product GetProductById(long? productId)
        {
            return db.Products.SingleOrDefault(x => x.ProductId == productId);
        }
        public long CreateProduct(ProductViewModel productInput)
		{
			try
			{
				Product newProduct = new Product();
				newProduct.Name = productInput.Name;
				//newProduct.SubImages = productInput.GetStringSubImages();
				newProduct.SubImages = productInput.SubImages;
				//newProduct.AttributeId1 = productInput.AttributeId1;
				newProduct.ProductCategoryId = productInput.ProductCategoryId;
				newProduct.StartupCategoryId = productInput.StartupCategoryId;
				newProduct.metaTitle = productInput.GetMetaTitle();
				newProduct.SeoTitle = productInput.SeoTitle;
				newProduct.StartupId = productInput.StartupId;
				newProduct.Price = productInput.Price;
				newProduct.PromotionPrice = productInput.PromotionPrice;
				newProduct.Quality = productInput.Quality;
				newProduct.Description = productInput.Description;
				newProduct.Status = productInput.Status == null ? "Ẩn" : productInput.Status;
				newProduct.KeyWord = productInput.KeyWord;
				newProduct.MainImage = productInput.MainImage;
				newProduct.isTrial = productInput.isTrial;
				db.Products.Add(newProduct);
                db.SaveChanges();
                return newProduct.ProductId;
            }
			catch (Exception)
			{
				return 0 ;
			}
		}
		public long Update(ProductViewModel productInput, HttpPostedFileBase mainNewImage, HttpPostedFileBase[] subNewImages)
		{
			try
			{
				Product product = db.Products.Find(productInput.ProductId);
				product.Name = productInput.Name;
				string[] subImageArr = productInput.SubImages.Split(',');
				var mainAndSubImagesPath = MainAndSubImagesPath(subImageArr, mainNewImage, subNewImages);
				if(mainAndSubImagesPath.Item1 != "")
				{
					product.MainImage = mainAndSubImagesPath.Item1;
				}
				product.SubImages = mainAndSubImagesPath.Item2;
				//product.SubImages = productInput.GetStringSubImages();
				product.ProductCategoryId = productInput.ProductCategoryId;
				product.StartupCategoryId = productInput.StartupCategoryId;
				product.metaTitle = productInput.GetMetaTitle();
				product.SeoTitle = productInput.SeoTitle + "StartupWebsite";
				product.Price = productInput.Price;
				product.PromotionPrice = productInput.PromotionPrice;
				product.Quality = productInput.Quality;
				product.Description = productInput.Description;
				product.Status = productInput.Status == null ? "Ẩn" : productInput.Status;
				product.KeyWord = productInput.KeyWord;
				//product.MainImage = productInput.MainImage;
				product.isTrial = productInput.isTrial;
				db.SaveChanges();
				return product.ProductId;
			}
			catch (System.IndexOutOfRangeException ex)
			{
				System.ArgumentException argEx = new System.ArgumentException("Index is out of range", "index", ex);
				throw argEx;
			}
		}
		//Save image to server & get image path
		public string ServerSavePath(string path, HttpPostedFileBase file)
		{
			var InputFileName = Path.GetFileName(DateTime.Now.ToFileTime() + file.FileName);
			string imagePath = "/Assets/Images/Startup/Products/" + InputFileName;
			var ServerSavePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(path) + InputFileName);
			file.SaveAs(ServerSavePath);
			return imagePath;
		}
		public Tuple<string, string> MainAndSubImagesPath(string[] subImageArr,HttpPostedFileBase mainNewImage, HttpPostedFileBase[] subNewImages)
		{
			string mainImagePath="";
			string subImagePath;
			string subImagesPath;
			if (mainNewImage != null)
			{
				mainImagePath = ServerSavePath("/Assets/Images/Startup/Products/", mainNewImage);			
			}
			if (subNewImages.Where(x => x != null).Count() > 0)
			{
				for (var i = 0; i < subNewImages.Count(); i++)
				{
					HttpPostedFileBase subImage = subNewImages[i];
					if (subImage != null)
					{
						subImagePath = ServerSavePath("/Assets/Images/Startup/Products/", subImage);
						subImageArr[i] = subImagePath;
					}
				}
			}
			subImagesPath=string.Join(",", subImageArr);
			return Tuple.Create<string, string>(mainImagePath, subImagesPath);
		}
		public bool IsDuplicateName(string name)
		{
			return db.Products.Count(x => x.Name == name) > 0;
		}

		public bool UpdateStatus(int id, string status)
		{
			try
			{
				var product = db.Products.Find(id);
				product.Status = status;
				db.SaveChanges();
				return true;
			}
			catch (Exception )
			{
				return false;
			}
		}
		//Xóa danh mục
		public bool Delete(int id)
		{
			try
			{
				var product = db.Products.Find(id);
				db.Products.Remove(product);
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}
		public List<ProductViewModel> GetProductsByStartupCategory(int startupId,int? categoryId)
		{
			List<ProductViewModel> listProductModel = new List<ProductViewModel>();
			if (categoryId == null)
			{
				listProductModel = db.Database.SqlQuery<ProductViewModel>(
				"EXECUTE dbo.[GetProductByStartupCategory] @startupId, @startupCategoryId, @customerId",
				new SqlParameter("startupId", startupId),
				new SqlParameter("startupCategoryId", DBNull.Value),
				new SqlParameter("CustomerId", new Guid())).ToList();
			}
			else
			{
				listProductModel = db.Database.SqlQuery<ProductViewModel>(
				"EXECUTE dbo.[GetProductByStartupCategory] @startupId, @startupCategoryId, @customerId",
				new SqlParameter("startupId", startupId),
				new SqlParameter("startupCategoryId", categoryId),
				new SqlParameter("CustomerId", new Guid())).ToList();
				
			}
			return listProductModel;

		}
	}
}