using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class MemberDTO :DTO
    {
        public const string tableName = "MembersInBoard";
        private string email;
        private int boardId;
        DMembersController dm = new DMembersController(tableName);
        public string Email { get { return email; } set { email = value; } }
        public int BoardId { get { return boardId; } set { boardId = value;} }
        public MemberDTO(string name,int boardId) : base(new DMembersController(tableName))
        {
            this.email = name;  
            this.boardId = boardId;
        }

    }
}
