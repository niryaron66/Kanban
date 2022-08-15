using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserDTO : DTO
    {
        public const string tableName = "users";
        private string email;
        [NonSerialized] private string password;
        bool islogedIn;
        DUserController userController = new DUserController(tableName);
        public bool log { get=> islogedIn; set => islogedIn=value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set { password = value; controller.update(addKeys(),addValues(), password, value); } }

        private LinkedList<object> addValues()
        {
            LinkedList<object> values = new LinkedList<object>();
            values.AddLast("email");
            return values;
        }

        private LinkedList<string> addKeys()
        {
            LinkedList<string> keys = new LinkedList<string>();
            keys.AddLast("email");
            return keys;
        }

        public UserDTO(string email, string password, bool logIn) : base(new DUserController(tableName))
        {
            this.email = email;
            this.password = password;
            this.islogedIn = logIn;
            //TODO: check if needed, pretty sure it isnt needed
        }
        public DUserController UserController { get => userController; }
    }
}
