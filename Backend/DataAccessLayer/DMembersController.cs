using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class DMembersController : DalController
    {
        public DMembersController(string tableName) : base(tableName)
        {
        }
        public List<MemberDTO> selectAllMembers()
        {
            List<MemberDTO> members = select().Cast<MemberDTO>().ToList();  
            return members;
        }

        public bool insert( MemberDTO member)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {tableName} ({"email"},{"boardId"})" +
                        $" VALUES (@email,@boardId)";

                    SQLiteParameter MemberName = new SQLiteParameter(@"email", member.Email);
                    SQLiteParameter id = new SQLiteParameter(@"boardId", member.BoardId);

                    command.Parameters.Add(MemberName);
                    command.Parameters.Add(id);
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
            MemberDTO result = new MemberDTO(reader.GetString(0), reader.GetInt32(1));
            return result;
        }
    }
}
