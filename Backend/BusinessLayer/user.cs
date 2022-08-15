using IntroSE.Kanban.Backend.DataAccessLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class User
    {
        private string email;
        [NonSerialized] private string password;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private UserDTO userDTO;
        bool islogedIn;
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set { password = value; userDTO.Password = value; } }
        public User(string email, string password, bool logIn)
        {
            this.email = email;
            this.password = password;
            this.islogedIn = logIn;
            userDTO =new UserDTO(email, password, logIn);
        }
         /// <summary>
        /// This method validate that given string is this user password
        /// </summary>
        /// /// <param name="pass">The given string.</param>
        /// <returns>list of the tasks that not done in the board, unless an error occurs </returns>
        public bool validatePassword(string pass) { 
            return password == pass;
        }
                /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        public void login() {
            if (islogedIn)
            {
                log.Error($"user {email} commit login but alredy loged in");
                throw new Exception("user alredy logged in");
            }
            userDTO.log = true;
            islogedIn = true;
        }
            /// <summary>
        ///  This method logs out an existing user.
        /// </summary>
        public void logout() {
            if (!islogedIn)
            {
                log.Error($"user {email} commit logout but alredy logged out ");
                throw new Exception("user alredy logged out");
            }
            userDTO.log = false;
            islogedIn = false;
        }
       public UserDTO GetDTO()
        {
            return userDTO;
        } 
        public bool isLogedIn()
        {
            return islogedIn;
        }
    }
}
