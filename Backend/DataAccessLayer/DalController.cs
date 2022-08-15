using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*using System.Collections.Generic;*/
using System.Data.SQLite;
using System.IO;
using log4net;
using System.Reflection;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public abstract class DalController
    {
        protected readonly string _connectionString;
        protected readonly string tableName;
        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public DalController(string tableName)
        {
            string path=Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "kanban.db");
/*            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
*/            Console.WriteLine(path);
            this._connectionString = $"Data Source={path}; Version=3;";
            this.tableName = tableName;
        }
        public bool update(LinkedList<string> keysName,LinkedList<object> actualKeys ,string attributeName,object value)
        {
            string text = buildText(keysName,actualKeys);
            int res = -1;
            using (var connection = new SQLiteConnection(this._connectionString))
            {
                if (value is string)
                {
                    SQLiteCommand command = new SQLiteCommand
                    {
                        Connection = connection,
                        CommandText = $"UPDATE {tableName} SET {attributeName} = '{value}' " + text
                    };
                    try
                    {
                        connection.Open();
                        res = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        log.Error("user tried to update a user in table but faild");
                        return false;
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Close();
                    }
                }
                else
                {
                    SQLiteCommand command = new SQLiteCommand
                    {
                        Connection = connection,
                        CommandText = $"UPDATE {tableName} SET {attributeName} = {value} " + text
                    };
                    try
                    {
                        connection.Open();
                        res = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        log.Error("user tried to update a user in table but faild");
                        return false;
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Close();
                    }
                }
            }
            return res > 0;
        }

       public List<DTO> select()
        {
            List<DTO> results = new List<DTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {tableName};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));
                    }
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }
        protected abstract DTO ConvertReaderToObject(SQLiteDataReader reader);



        public bool Delete(LinkedList<string> keysName, LinkedList<object> keysValue)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {tableName} " + buildText(keysName,keysValue)
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
           return res > 0;
        }

        
        public bool DeleteAll()
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {tableName}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;

        }

        private string buildText(LinkedList<string> names, LinkedList<object> values )
        {
            string text = "";
            if (names.Count > 1) {
                if(values.ElementAt(0) is string)
                {
                    text = "WHERE " + names.ElementAt(0) + "=\"" + $"{values.ElementAt(0)}" + "\"";

                }
                else
                    text = "WHERE " + names.ElementAt(0) + $"={values.ElementAt(0)}";
                for (int i = 1; i < names.Count; i++)
                {

                    if (values.ElementAt(i) is string)
                    {
                        text += " " + "and " + names.ElementAt(i) + "=\""  + $"{values.ElementAt(i)}" + "\"";

                    }
                    else
                        text += " " + "and " + names.ElementAt(i) + $"={values.ElementAt(i)}";
                }
            }
            else
            {
                if (values.ElementAt(0) is string)
                {
                    text = "WHERE " + names.ElementAt(0) + "=\"" + $"{values.ElementAt(0)}" + "\"";

                }
                else
                    text = "WHERE " + names.ElementAt(0) + $"={values.ElementAt(0)}";
            }
            return text;
        }
    }
}
