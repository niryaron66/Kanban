using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using log4net;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.IO;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class UserController
    {
        const int TRY_TO_REGISTER=0;
        const int TRY_TO_LOGIN=1;
        const int TRY_TO_GETUSERS=2;
        const int TRY_TO_LOGOUT=3;
        const int PASSWORD_MAX_LEN = 20;
        const int PASSWORD_MIN_LEN = 6;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Dictionary<string, User> users;
        private DUserController duController = new DUserController("users");
        
        public UserController()
        {    
            this.users = new Dictionary<string, User>();
        }
          ///<summary>This method gets user by his username
        /// </summary>
        /// /// <param name="name">The email address of the user to login</param>
        ///<returns>The user object, unless an error occurs </returns>
        public User getUser(string name)
        {
            validInput(name, "user name",TRY_TO_GETUSERS);
            if (!users.ContainsKey(name))
            {
                log.Error($"user {name} does not exist");
                throw new Exception($"user {name} does not exist");
            }
            return users[name];   
        }
         /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        internal void Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email is invalid");
            }
            email = email.ToLower();
            validInput(email, "Email",TRY_TO_LOGIN);
            validInput(password, "Password",TRY_TO_LOGIN);
            if (!users.ContainsKey(email))
            {
                if (duController.getUser(email) == null)
                {
                    log.Error($"user {email} does not exist");
                    throw new Exception("user is not register in the system");
                }
            }
            else if (!users[email].validatePassword(password))
            {
                log.Error($"password for user {email} does not match");
                throw new Exception("wrong password");
            }
          /*  foreach (User u in users.Values)
            {
                if (u.isLogedIn())
                {
                    log.Error($"another user is looged in right now");
                    throw new Exception("another user is looged in right now");
                }
            }*/ // we dont care if anothe user is loggedd in
            try
            {
                users[email].login();
                duController.updateLogInUser(users[email].GetDTO());
                log.Info($"user {email} loggin the system");
            }
            catch (Exception e)
            {
                log.Error($"user {email} failed loggin the system");
                throw new Exception(e.Message);
            } 
        }
         /// <summary>
        ///  This method logs out an existing user.
        /// </summary>
        /// <param name="email">The email address of the user to logout</param>
        /// <param name="password">The password of the user to logot</param>
        internal void logout(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email is invalid");
            }
            email = email.ToLower();
            validInput(email, "Email", TRY_TO_LOGOUT);
            User user = getUser(email);
            try
            {
                user.logout();
                duController.updateLogoutUser(user.GetDTO());
                log.Info($"user {email} logout the system");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        public void register(string email, string pass) 
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email is invalid");
            }
            email = email.ToLower();
            validInput(email, "Email",TRY_TO_REGISTER);
            validInput(pass, "Password",TRY_TO_REGISTER);
            if (users.ContainsKey(email))
            {
                throw new Exception("another user is alredy registred using that name");
            }
            foreach (var E in users.Keys)
            {
                if (E.ToLower() == email)
                {
                    throw new Exception("the user already exists");
                }
            }
            if( pass.Length<PASSWORD_MIN_LEN || pass.Length>PASSWORD_MAX_LEN)
            {
                throw new Exception("pass length must between " + PASSWORD_MIN_LEN + " to " +PASSWORD_MAX_LEN);
            }
            if (!IsValidEmailAddress(email)){
                throw new Exception("email is invalid");
            }
            if (string.IsNullOrWhiteSpace(pass) || !(pass.Length >= PASSWORD_MIN_LEN && pass.Length <= PASSWORD_MAX_LEN) || !(pass.Any(char.IsUpper)) || !(pass.Any(char.IsLower)) || !(pass.Any(char.IsDigit)))
            {
                throw new Exception("This password does not meet the requirements: \n1) length of " + PASSWORD_MIN_LEN +  " to " + PASSWORD_MAX_LEN + "characters \n2)must include at least one uppercase letter, one small character and a number");
            }
           /* if (!validPassword(pass))
            {
                throw new Exception("pass is ilegal");
            }*/
            /*foreach (User u in users.Values)
            {
                if (u.isLogedIn())
                {
                    log.Error($"another user is looged in right now");
                    throw new Exception("another user is looged in right now");
                }   
            }*///we dont need to check if some ine is already logged in
            User user = new User(email, pass, true);
            this.duController.insert(user.GetDTO());
            users.Add(email, user);
            
            

           
        }  
        /// <summary>
        /// this method used to change user password
        /// </summary>
        /// <param name="email">the email of the user
        /// <param name="oldPass">the old password of the user - just to athenticate
        /// <param name="newPass">the new password to change
        internal void updatePassword(string email, string oldPass, string newPass)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email is invalid");
            }
            email = email.ToLower();
            User user = getUser(email);
            if(user == null)
            {
                throw new Exception("email isnt found");
            }
            else if(user.Password != oldPass)
            {
                throw new Exception("old pass dont match");
            }
            else if (!validPassword(newPass))
            {
                throw new Exception("new pass isnt legal");
            }
            user.Password=newPass;
            user.GetDTO().UserController.updateUser(user.GetDTO());
            
        }
        /// <summary>
        /// this method use to valid the password meets conditions
        /// </summary>
        /// <param name="pass">the string that we want to use as password
        /// <returns>true if the password is valid, else false</returns>
        private bool validPassword(string pass)
        {
            bool atLeastOneUpper = false;
            bool atLeastOneLower = false;
            bool atLeastOneNumber = false;
            foreach(char c in pass.ToCharArray())
            {
                if (char.IsUpper(c))
                {
                    atLeastOneUpper = true;
                }
                if (char.IsLower(c))
                {
                    atLeastOneLower = true;
                }
                if (char.IsDigit(c))
                {
                    atLeastOneNumber = true;
                }
            }
            if (!atLeastOneUpper | !atLeastOneNumber | !atLeastOneLower)
            {
                return false;
            }
           /* if (!pass.Any(char.IsUpper)){
                return false;
            }
            if (!pass.Any(char.IsLower))
            {
                return false;
            }
            if (!pass.Any(char.IsNumber))
            {
                return false;
            }*/
            return true;
        }
        /// <summary>
        /// this method use to valid the email meets conditions
        /// </summary>
        /// <param name="email">the wanted email of the user
        /// <returns>true if the email is valid, else false</returns>
        internal bool IsValidEmailAddress(string emailaddress) 
        {
            var email = new EmailAddressAttribute();
            try
            {
              Regex rx = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                    @".)+))([a-zA-Z]{2,6}|[0-9]{1,3})(\]?)$");

              if (rx.IsMatch(emailaddress))
                {
                 Regex emailAttribute = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$");
                    if (emailAttribute.IsMatch(emailaddress))
                    {
                        if(email.IsValid(emailaddress))
                            {
                            return IsValid(emailaddress) & check(emailaddress);
                        }
                    }
                }
              return false;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message + "::IsValidEmailAddress error");
            }
        }
         /// <summary>
        /// this method help to valid the email meets conditions
        /// </summary>
        /// <param name="email">the wanted email of the user
        /// <returns>true if the email is valid, else false</returns>
        public bool check(string email)
        {
            Regex regex = new Regex(@"^[\w!#$%&'+\-/=?\^_`{|}~]+(\.[\w!#$%&'+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            Match match = regex.Match(email);
            return match.Success;
        }
        /// this method help to valid the email meets conditions
        /// </summary>
        /// <param name="email">the wanted email of the user
        /// <returns>true if the email is valid, else false</returns>
        public bool check3(string email)
        {
            char[] charArray = {'א','ב','ג','ד','ה','ו','ז','ח','ט','י','כ','ל','מ','נ','ס','ע','פ','צ','ק','ר','ש','ת'};
             foreach (char c in email.ToCharArray())
                {
                if (charArray.Contains(c))
                    {
                        return false;
                    }
            }
           
                return true;
            }
        /// this method help to valid the email meets conditions
        /// </summary>
        /// <param name="email">the wanted email of the user
        /// <returns>true if the email is valid, else false</returns>
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        /// this method valid the input str and varName
        /// </summary>
        /// <param name="str">the value of the attribute to check
        /// <param name="varName">the name of the attribute to check
        public void validInput(string str, string varName,int x)
        {
            if (str == null | str == "")
            {
                if (x == TRY_TO_REGISTER)
                {
                    if (varName == "email")
                    {
                        log.Debug("User with null or empty mail attempted registeration ");
                        throw new ArgumentException(varName + " is null or emptysapce, please change!");
                    }
                    else
                    {
                        log.Debug("User with null or empty password attempted registeration ");
                        throw new ArgumentException(varName + " is null or emptysapce, please change!");
                    }
                }
                else if(x == TRY_TO_LOGIN)
                {
                    if (varName == "email")
                    {
                        log.Debug("User with null or empty mail attempted login ");
                        throw new ArgumentException(varName + " is null or emptysapce, please change!");
                    }
                    else
                    {
                        log.Debug("User with null or empty password attempted login ");
                        throw new ArgumentException(varName + " is null or emptysapce, please change!");
                    }
                }
                else if(x==TRY_TO_GETUSERS)
                {
                    if (varName == "email")
                    {
                        log.Debug("User with null or empty mail attempted getting users ");
                        throw new ArgumentException(varName + " is null or emptysapce, please change!");
                    }
                    else
                    {
                        log.Debug("User with null or empty password attempted getting users ");
                        throw new ArgumentException(varName + " is null or emptysapce, please change!");
                    }
                }
                else if (x == TRY_TO_LOGIN)
                {
                    if (varName == "email")
                    {
                        log.Debug("User with null or empty mail attempted logout ");
                        throw new ArgumentException(varName + " is null or emptysapce, please change!");
                    }
                    
                }
                else
                {
                    if (varName == "email")
                    {
                        log.Debug("User with null or empty mail attempted using system ");
                        throw new ArgumentException(varName + " is null or emptysapce, please change!");
                    }
                    else
                    {
                        log.Debug("User with null or empty password attempted using system  ");
                        throw new ArgumentException(varName + " is null or emptysapce, please change!");
                    }
                }
            }
        }
         /// this method return if the user is registered identify by email
        /// </summary>
        /// <param name="email">the email of the user to check
        /// <returns>true if this user is registered, else false</returns>
        public bool isRegistred(string email)
        {
            return users.ContainsKey(email);
        }
        /// this method return if the user is login identify by email
        /// </summary>
        /// <param name="email">the email of the user to check
        /// <returns>true if this user is login, else false</returns>
        public bool isLogedIn(string email)
        {
            if (isRegistred(email)) { return getUser(email).isLogedIn(); }
            return false;
            
        }
        public LinkedList<User> getUsers()
        {
            LinkedList<User> usersAsList = new LinkedList<User> (); 
            foreach (User u in users.Values)
            {
                usersAsList.AddLast(u);
            }

          
            return usersAsList;
        }

        public void loadData()
        {
            users = new Dictionary<string, User>();
            try
            {
                bool reset = duController.unRegisterAll();
                if (!reset)
                {
                    throw new Exception("cannot reset register");
                }
                List<UserDTO> us = duController.SelectALLUsers();
                foreach(UserDTO user in us)
                {
                    users.Add(user.Email, new User(user.Email, user.Password, user.log));
                }
                log.Info("user successfuly apdated data from db");
            }catch (Exception ex)
            {
                log.Error("user tried load data but failed");
            }
        }


/*        public string loadData()
        {
            try
            {
                // add try catch
                bc.loadData();
                uc.loadData();
                Response2 res = new Response2(null, null);
                log.Info($"user loaded all data uccessfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "load data failed!!" + e.Message));
            }
        }*/


        public void deleteData()
        {
            duController.DeleteAll();
            this.users = new Dictionary<string, User>();
        }
    }
}
