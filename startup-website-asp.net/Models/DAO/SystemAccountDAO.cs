using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using startup_website_asp.net.Models.EF;
using System.Web.Mvc;
using startup_website_asp.net.Common;
using RabbitMQ.Client.Impl;

namespace startup_website_asp.net.Models.DAO
{
    public class SystemAccountDAO:BaseDAO
    {

        public SystemAccount GetByUserName(string userName)
        {
            return db.SystemAccounts.SingleOrDefault(x => x.UserName==userName);
        }

        //#Tuple can be defined as a single record of data having various data types
        //Tuple là kiểu dữ liệu giúp việc trả về nhiều kiểu dữ liệu cùng 1 lúc
        public Tuple<int,string,AdminLogin> Login(string userName, string password) 
        {
            var user = GetByUserName(userName);
            password = Encryptor.MD5Hash(password);
            if (user == null)
            {
                return Tuple.Create<int, string, AdminLogin>(0,"Tài khoản không tồn tại!",null); //Item1,Item2,Item3
            }
            else
            {
                if (user.Password == password)
                {
                    var adminSession = new Common.AdminLogin();
                    adminSession.UserName = user.UserName;
                    adminSession.UserID = user.SystemAccountId;
                    adminSession.FullName = user.Name;
                    return Tuple.Create<int, string, AdminLogin>(1, "Đăng nhập thành công!", adminSession); //Login success
                }
                else
                {
                    return Tuple.Create<int, string, AdminLogin>(0, "Sai mật khẩu!", null); //login false
                }
            }
        }
        //Thêm khách hàng vào dữ liệu
        public int Insert(SystemAccount entity)
        {
            db.SystemAccounts.Add(entity);
            db.SaveChanges();
            return entity.SystemAccountId;
        }
        
        public bool CheckUserName(string userName)
        {
            return db.SystemAccounts.Count(x => x.UserName == userName) > 0;
        }


    }
}