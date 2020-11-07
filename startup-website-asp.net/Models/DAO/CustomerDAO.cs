using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using startup_website_asp.net.Models.EF;

namespace startup_website_asp.net.Models.DAO
{
    public class CustomerDAO:BaseDAO
    {
        public CustomerDAO()
        {
            db = new StartupWebsite();
        }

        public Customer GetByUserName(string userName)
        {
            return db.Customers.SingleOrDefault(x => x.UserName == userName);
        }

        public int Login(string userName, string password)
        {
            var result = GetByUserName(userName);

            if (result ==null)
            {
                return 0; //Tài khoản không tôn tại
            }
            else
            {
                if (result.Password==password)
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
        public int Insert(Customer entity)
        {
            try
            {
                db.Customers.Add(entity);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool CheckUserName(string userName)
        {
            return db.Customers.Count(x => x.UserName == userName) > 0;
        }


    }
}