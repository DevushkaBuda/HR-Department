using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace Unit_TestAuth
{
    public class AuthClass
    {
        public static HumanResourcesDepartmentEntities db = new HumanResourcesDepartmentEntities();
        public static string Auto(string login, string password)
        {
            var currentUser = db.Worker.FirstOrDefault(p => p.Login == login && p.Password == password);
            if (currentUser !=null)
            {
                switch (currentUser.Id_Worker)
                {
                    case 1: return "Администратор";
                    case 2: return "Бухгалтер";
                 
                }
            }
            return "Такого пользователя нет";
        }
    }
}
