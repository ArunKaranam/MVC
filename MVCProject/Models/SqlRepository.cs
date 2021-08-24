using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class SqlRepository:ISqlRepository
    {

        MyDbContext db;

        public SqlRepository()
        {
            db = new MyDbContext();
        
        }


        public bool AddUser(User user)
        {
            try
            {
                db.users.Add(user);
                db.SaveChanges();
                return true;

            }
            catch 
            
            {
                throw;
            }

           
        }

        public bool UserExsist(User user)
        {
            try
            {
                bool isvalid = db.users.Any(x => x.UserName == user.UserName && x.PassWord == user.PassWord && x.RoleName == user.RoleName);
                if (isvalid)
                    return true;
                else
                    return false;

            }
            catch
            {
                throw;
            
            }


            
        }

    }
}