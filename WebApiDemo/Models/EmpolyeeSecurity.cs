using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public class EmpolyeeSecurity
    {


        public static bool Login(string username,string password)
        {
                 TestEntities db = new TestEntities();

            return db.Users.Any(user => user.username.Equals(username.ToLower(), StringComparison.OrdinalIgnoreCase) && user.password==password);
        }
    }
}