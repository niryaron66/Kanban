using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
  
    public class User
    {
        private string email;
       [NonSerialized] private string password;
        public User(User u)
        {
            email = u.email;
            password = u.password;
        }
        public User (string email,string pass)
        {
            this.email = email;
            password = pass;
        }
        public User(BusinessLayer.User u)
        {
            this.email= u.Email;
            this.password = u.Password;
        }
    }
}
