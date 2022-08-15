using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class DUserController : DalController
    {
        public DUserController(string tableName) : base(tableName)
        {
        }
        public List<UserDTO> SelectALLUsers()
        {
            List<UserDTO> users = select().Cast<UserDTO>().ToList();
            return users;
        }

        public bool insert(UserDTO user)
        {
            using(var connection =  new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({"email"},{"password"},{"isLoggedIn"})" +
                        $"VALUES (@emailVal,@passwordVal,@logVal);";

                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", user.Email);
                    SQLiteParameter passParam = new SQLiteParameter(@"passwordVal", user.Password);
                    SQLiteParameter logParam = new SQLiteParameter(@"logVal", "1");

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passParam);
                    command.Parameters.Add(logParam);
                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    log.Error("user tried insert new user to system but faild");
                    return false;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            try
            {
                Console.WriteLine(reader.ToString());   
                string isLog = reader.GetString(2);
                bool boolIsLog;
                if (isLog == "1")
                    boolIsLog = true;
                else
                    boolIsLog = false;
                UserDTO result = new UserDTO(reader.GetString(0), reader.GetString(1), boolIsLog);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("reader problem: "+e);
            }
        }

        public void updateUserPass(UserDTO userDTO)
        {
            LinkedList<string> keysNames = new LinkedList<string>();
            LinkedList<object> values = new LinkedList<object>();
            string column = "password";
            object value = userDTO.Password;
            keysNames.AddFirst("email");
            values.AddFirst(userDTO.Email);
            update(keysNames, values, column, value);
        }
        public void updateLogInUser(UserDTO userDTO)
        {
            LinkedList<string> keysNames = new LinkedList<string>();
            LinkedList<object> values = new LinkedList<object>();
            string column = "isLoggedIn";
            string value = "1";
            keysNames.AddFirst("email");
            values.AddFirst(userDTO.Email);
            update(keysNames, values, column, value);
        }

        public void updateLogoutUser(UserDTO userDTO)
        {
            LinkedList<string> keysNames = new LinkedList<string>();
            LinkedList<object> values = new LinkedList<object>();
            string column = "isLoggedIn";
            string value = "0";
            keysNames.AddFirst("email");
            values.AddFirst(userDTO.Email);
            update(keysNames, values, column, value);
        }

        public void updateUser(UserDTO userDTO)
        {
            updateUserPass(userDTO);
        }
        public UserDTO getUser(string email)
        {
            //TODO: implemnt as needed
            return null;
        }

        public bool unRegisterAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"UPDATE {tableName} SET {"isLoggedIn"} = '{"0"}'";
                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    log.Error("user tried insert new user to system but faild");
                    return res > 0;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }
    }
}
