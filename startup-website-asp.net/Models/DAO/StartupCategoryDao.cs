using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models.DAO
{
    public class StartupCategoryDAO:BaseDAO
    {
		public StartupCategoryDAO()
		{
			db = new StartupWebsite();
		}
		//Danh sách danh mục
		public IEnumerable<StartupCategory> ListAll()
		{
			return db.StartupCategories.ToList();
		}
		//Danh sách danh mục theo startupID
		public IEnumerable<StartupCategory> GetCategoriesByStartupId(int startupId)
		{
			return db.StartupCategories.Where(x => x.StartupId == startupId);
		}
		//Thêm danh mục mới vào bảng
		public long Insert(StartupCategory entity)
		{
			db.StartupCategories.Add(entity);
			db.SaveChanges();
			return entity.StartupCategoryId;
		}

		public StartupCategory GetById(int id)
		{
			return db.StartupCategories.SingleOrDefault(x => x.StartupCategoryId == id);
		}
		public StartupCategory ViewDetail(int id, long? startupId)
		{
			StartupCategory startupCategory = db.StartupCategories.SingleOrDefault(x => x.StartupCategoryId == id && x.StartupId == startupId);
			return startupCategory;
		}
		//Chỉnh sửa danh mục
		public bool Update(StartupCategory entity)
		{
			try
			{
				var category = db.StartupCategories.Find(entity.StartupCategoryId);
				category.StartupCategoryId = entity.StartupCategoryId;
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
				var category = db.StartupCategories.Find(id);
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
				var category = db.StartupCategories.Find(id);
				db.StartupCategories.Remove(category);
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}
    }
}