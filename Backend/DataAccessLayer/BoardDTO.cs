using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class BoardDTO : DTO
    {
        //private Column[] columns; there should be another table that will connect for each table its column's
        public const string tableName = "Boards";
        private string name;
        private int taskCounter;
        private int boardId;
        public string boardOwner;
        private LinkedList<string> members;
        private DBoardController boardController = new DBoardController(tableName);

        public BoardDTO(string name,int boardId,int counter,string boardOwner) : base(new DBoardController(tableName))
        {
            this.boardId= boardId;
            this.name= name;
            this.taskCounter = counter;
            this.boardOwner= boardOwner;
            this.members = new LinkedList<string>();
        }
        public int TaskCounter { get => taskCounter;set => taskCounter = value; }   
        public int BoardId { get => boardId; set => boardId = value; }
        public string Name { get => name; set => name = value; }
        public string owner { get => boardOwner; set => boardOwner = value; }
        public LinkedList<string> Members { get => members; set => members = value; }
        public DBoardController BoardController { get => boardController; set => boardController = value; }

    }
}
