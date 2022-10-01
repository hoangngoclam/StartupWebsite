
using System.Collections.Generic;
using System.Linq;
using startup_website_asp.net.Models.EF;
using System.Data.Entity;
using System;

namespace startup_website_asp.net.Models.DAO
{
	public class ProductCategoryDAO:BaseDAO
	{
		public ProductCategoryDAO()
		{
			db = new StartupWebsite();
		}
		//Danh sách danh mục
		public IEnumerable<ProductCategory> ListAll()
		{
			return db.ProductCategories.ToList();
		}
		//Thêm danh mục mới vào bảng
		public long Insert(ProductCategory entity)
		{			
			db.ProductCategories.Add(entity);
			db.SaveChanges();
			return entity.ProductCategoryId;
		}

		public ProductCategory GetById(int id)
		{
			return db.ProductCategories.SingleOrDefault(x => x.ProductCategoryId == id);
		}
		public ProductCategory ViewDetail(int id)
		{
			return db.ProductCategories.Find(id);
		}
		//Chỉnh sửa danh mục
		public bool Update(ProductCategory entity)
		{
			try
			{
				var category = db.ProductCategories.Find(entity.ProductCategoryId);
				category.ParentCategoryId = entity.ParentCategoryId;
				category.Name = entity.Name;
				category.Status = entity.Status;
				category.MetaTitle = entity.MetaTitle;
				category.SeoTitle = entity.SeoTitle;
				category.DisplayOrder = entity.DisplayOrder;
				db.SaveChanges();
				return true;
			}
			catch(Exception )
			{
				return false;
			}			
		}
		public bool UpdateStatus(int id, string status)
		{
			try
			{
				var category = db.ProductCategories.Find(id);
				category.Status = status;
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
				var category = db.ProductCategories.Find(id);
				db.ProductCategories.Remove(category);
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}
		public List<ProductCategoryViewModel> GetListProductCategoryRender()
		{
			List<ProductCategoryViewModel> result = new List<ProductCategoryViewModel>();
			List<ProductCategoryViewModel> ListProductCategory = GetlistProductCategory("", null, db.ProductCategories.ToList());
			foreach (ProductCategoryViewModel item in ListProductCategory)
			{
				RenderCategory(ref result, item);
			}
			return result;
		}
		public ProductCategoryViewModel RenderCategory(ref List<ProductCategoryViewModel> result, ProductCategoryViewModel parent)
		{
			if (parent.listChild.Count > 0)
			{
				foreach (ProductCategoryViewModel child in parent.listChild)
				{
					RenderCategory(ref result,child);
				}
			}
			result.Add(parent);
			return parent;
		}
		public List<ProductCategoryViewModel> GetlistProductCategory(string roundNameParent,long? ParentCategoryIdInput = null, List<ProductCategory> listProductCategory = null)
		{
			List<ProductCategoryViewModel> productCategoryModels = new List<ProductCategoryViewModel>();
			List<ProductCategory> listByParentId = db.ProductCategories.Where(x => x.ParentCategoryId == ParentCategoryIdInput && x.Status == "Hiện").ToList();
			if(listByParentId.Count > 0)
			{
				foreach (ProductCategory categoryDb in listByParentId)
				{
					ProductCategoryViewModel productCategoryModel = new ProductCategoryViewModel(categoryDb);
					productCategoryModel.RoundName = roundNameParent+">>"+categoryDb.Name;
					productCategoryModel.listChild = GetlistProductCategory(productCategoryModel.RoundName,productCategoryModel.ProductCategoryId, listProductCategory);
					productCategoryModels.Add(productCategoryModel);
				}
			}
			
			return productCategoryModels;
		}
	}
}