/*using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace simpleExample
{
    public class Connection//TODO: change it to work with our project
    {
        public static void Main(string[] args)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            Console.WriteLine(path);
            string connectionString = $"Data Source={path}; Version=3;";

            const string MessageTableName = "Message";
            const string IDColumnName = "ID";
            const string MessageTitleColumnName = "Title";
            const string MessageBodyColumnName = "Body";
            const string MessageForumColumnName = "Forum";



            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    /* command.CommandText = $"UPDATE {MessageTableName} SET {MessageBodyColumnName} = @body_val WHERE {MessageTitleColumnName} = @title_val; ";
                    SQLiteParameter bodyParam = new SQLiteParameter(@"body_val", "new body");
                    SQLiteParameter titleParam = new SQLiteParameter(@"title_val", "title");

                    command.Parameters.Add(bodyParam);
                    command.Parameters.Add(titleParam);
                                    

                    command.Prepare(); 

                    res = command.ExecuteNonQuery();

                    Console.WriteLine(res);
                    */

                    command.CommandText = $"SELECT * FROM {MessageTableName}";

                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Console.WriteLine(reader[MessageTitleColumnName] + ", " + reader[MessageBodyColumnName]);

                        int ID = reader.GetInt32(0);
                        string title = reader.GetString(1);

                        Console.WriteLine("ID: " + ID + " - " + "Title: " + title);
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(e.ToString());
                    //log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
        }

    }
}
