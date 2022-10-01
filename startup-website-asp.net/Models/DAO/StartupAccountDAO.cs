using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Areas.Startup.Models.DAO
{
    public class StartupAccountDAO:BaseDAO
    {
        public StartupAccountDAO()
        {
            db = new StartupWebsite();
        }

        public StartupAccount GetByUserName(string userName)
        {
            return db.StartupAccounts.SingleOrDefault(x => x.UserName == userName);
        }

        public int Login(string userName, string password)
        {
            var result = GetByUserName(userName);
            if (result == null)
            {
                return 0; //Tài khoản không tôn tại
            }
            else
            {
                if (result.Password == password)
                {
                    return 1; //Đăng nhập thành công
                }
                else
                {
                    return -1;//Mật khẩu không đúng
                }
            }
        }
        //Thêm khách hàng vào dữ liệu
        public int Insert(StartupAccount entity)
        {
            try
            {
                db.StartupAccounts.Add(entity);
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
           
            return entity.StartupAccountId;
        }
        public void UpdateStartupId(long? startupId, int startupAccountId)
        {
            StartupAccount startupAccount = db.StartupAccounts.Find(startupAccountId);
            if (startupAccount == null)
                return;
            startupAccount.StatupId = startupId;
            db.SaveChanges();
        }
        public bool CheckUserName(string userName)
        {
            return db.StartupAccounts.Count(x => x.UserName == userName) > 0;
        }
    }
}