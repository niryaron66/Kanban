using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend;
using System.Text.Json;
using log4net;
using System.Reflection;
using log4net.Config;
using System.IO;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private UserController uc;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public UserService(UserController userController)
        {
            this.uc = userController;
        }
        /// <summary>
        /// This method registers a new user to the system.
        /// 
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>response with The string "{}", unless an error occurs</returns>

        public string register(string email, string password)
        {
            try
            {
                uc.register(email, password);
                Response2 res = new Response2(null,null);
                log.Info("user " + email + " registered successfuly!");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "registeration failed!" + e.Message));
            }
        }
        /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>Response with user email, unless an error occurs </returns>

        public string login(string email,string password)
        {
            try
            {
                uc.Login(email, password);
                Response2 ret = new Response2(email,null);
                return JsonSerializer.Serialize(ret);
            }
            catch (Exception e)
            {
                
                ResponseT<string> ret = ResponseT<string>.FromError("log in failed!  " + e.Message);
                return JsonSerializer.Serialize(ret);
            }

        }
        /// <summary>
        /// This method logs out a logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>response with The string "{}", unless an error occurs </returns>

        public string logout(string email)
        {
            try
            {
                uc.logout(email);
                ResponseT<string> ret = new ResponseT<string>(null, null);
                return JsonSerializer.Serialize(ret);
            }
            catch (Exception e)
            {
                ResponseT<string> ret = ResponseT<string>.FromError("logout failed!  " + e.Message);
                return JsonSerializer.Serialize(ret);
            }
        }
        /// <summary>
        /// this method used to change user password
        /// </summary>
        /// <param name="email">the email of the user
        /// <param name="oldPass">the old password of the user - just to athenticate
        /// <param name="newPass">the new password to change
        /// <returns>the string {},unless an error occurs</returns>
        public string updatePassword(string email,string oldPass,string newPass)
        {
            try
            {
                uc.updatePassword(email, oldPass, newPass);
                ResponseT<string> ret = new ResponseT<string>(null, null);
                return JsonSerializer.Serialize(ret);
            }
            catch (Exception e)
            {
                ResponseT<string> ret = ResponseT<string>.FromError("change password failed" + e.Message);
                return JsonSerializer.Serialize(ret);
            }
        }
        public string deleteData()
        {
            try
            {
                uc.deleteData();
                Response2 res = new Response2(null, null);
                log.Info($"user tried to delete all data of users useccessfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "delete data failed!!" + e.Message));
            }
        }

        public string loadData()
        {
            try
            {
                uc.loadData();
                Response2 res = new Response2(null, null);
                log.Info($"user loaded all data uccessfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "load data failed!!" + e.Message));
            }
        }
    }
}
