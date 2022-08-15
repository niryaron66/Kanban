using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class DColumnController : DalController
    {
        public DColumnController(string tableName) : base(tableName)
        {
        }
        public List<ColumnDTO> selectAllColumns()
        {
            List<ColumnDTO> result = select().Cast<ColumnDTO>().ToList();
            return result;
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            ColumnDTO result = new ColumnDTO(reader.GetInt32(0), reader.GetInt32(1),reader.GetInt32(2), reader.GetString(3));
            return result;
        }
        public bool insert(ColumnDTO column)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({"boardId"},{"columnOrdinal"},{"taskLimit"},{"name"})" +
                        $"VALUES (@boardId,@columnOrdinal,@taskLimit,@name);";

                    SQLiteParameter boardId = new SQLiteParameter(@"boardId", column.id);
                    SQLiteParameter taskLimit = new SQLiteParameter(@"taskLimit", column.TaskLimit);
                    SQLiteParameter columnOrdinal = new SQLiteParameter(@"columnOrdinal", column.ColumnOrdinal);
                    SQLiteParameter name = new SQLiteParameter(@"name", column.Name);

                    command.Parameters.Add(boardId);
                    command.Parameters.Add(taskLimit);
                    command.Parameters.Add(columnOrdinal);
                    command.Parameters.Add(name);
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
    }
}
