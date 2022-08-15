using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class DBoardController : DalController
    {
        public DBoardController(string tableName) : base(tableName)
        {
        }
        public List<BoardDTO> selectAllBoards()
        {
            List<BoardDTO> boards = select().Cast<BoardDTO>().ToList();
            return boards;
        }

        public bool insert(BoardDTO board)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({"name"},{"taskCounter"},{"boardId"},{"boardOwner"})" +
                        $"VALUES (@name,@counter,@boardId,@boardOwner);";

                    SQLiteParameter boardName = new SQLiteParameter(@"name", board.Name);
                    SQLiteParameter taskCounter = new SQLiteParameter(@"counter", board.TaskCounter);
                    SQLiteParameter id= new SQLiteParameter(@"boardId", board.BoardId);
                    SQLiteParameter owner = new SQLiteParameter(@"boardOwner", board.owner);

                    command.Parameters.Add(boardName);
                    command.Parameters.Add(taskCounter);
                    command.Parameters.Add(id);
                    command.Parameters.Add(owner);
                    res = command.ExecuteNonQuery();


                }
                catch
                {
                    log.Error("user tried insert new board to the system but failed");
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
            BoardDTO result = new BoardDTO(reader.GetString(0), reader.GetInt32(2),reader.GetInt32(1),reader.GetString(3));
            return result;
        }
    }
}
