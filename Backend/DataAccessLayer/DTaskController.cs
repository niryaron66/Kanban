using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class DTaskController : DalController
    {
        public DTaskController(string tableName) : base(tableName)
        {
        }
        public List<TaskDTO> selectAllTasks()
        {
            List<TaskDTO> tasks = select().Cast<TaskDTO>().ToList();    
            return tasks;
        }
        public bool insert(TaskDTO task)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({"boardId"},{"columnId"},{"taskId"},{"creationTime"},{"title"},{"description"},{"dueDate"},{"taskAssigne"})" +
                        $"VALUES (@boardId,@columnId,@taskId,@creation,@title,@des,@due,@taskAsignee);";

                    SQLiteParameter boardId= new SQLiteParameter(@"boardId", task.BoardId);
                    SQLiteParameter columnId = new SQLiteParameter(@"columnId", task.ColumnId);
                    SQLiteParameter taskId = new SQLiteParameter(@"taskId",task.Id);
                    SQLiteParameter creationTime = new SQLiteParameter(@"creation", task.creationTime.ToString());
                    SQLiteParameter title = new SQLiteParameter(@"title", task.title);
                    SQLiteParameter description = new SQLiteParameter(@"des", task.description);
                    SQLiteParameter dueDate = new SQLiteParameter(@"due", task.dueDate.ToString());
                    SQLiteParameter taskAssigne= new SQLiteParameter(@"taskAsignee", task.asignee);

                    command.Parameters.Add(boardId);
                    command.Parameters.Add(columnId);
                    command.Parameters.Add(taskId);
                    command.Parameters.Add(creationTime);
                    command.Parameters.Add(title);
                    command.Parameters.Add(description);
                    command.Parameters.Add(dueDate);
                    command.Parameters.Add(taskAssigne);
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
            string date = reader.GetString(6);
            DateTime dateForDTO = parse2(date);
            string assigne = reader.GetString(7);
            if (assigne == null)
                assigne = "";
            TaskDTO result = new TaskDTO(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),parse2(reader.GetString(3)) ,reader.GetString(4), reader.GetString(5), dateForDTO, assigne);
            return result;
        }

        private DateTime parse(string v)
        {
            DateTime dt =DateTime.ParseExact(v, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            return dt;
        }
        private DateTime parse2(string date)
        {
            DateTime output =DateTime.Parse(date);
            return output;
        }
    }
}
