using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class ColumnDTO : DTO //not sure it should DTO or controller
    {
        public const string tableName = "Columns";
        private int boardId;
        private string name;
        private int columnOrdinal;
        private int taskLimit;
        private LinkedList<TaskDTO> tasks;
        private DColumnController columnController= new DColumnController(tableName);
        public ColumnDTO(int boardId,int columnOrdinal,int taskLimit, string name) : base(new DColumnController(tableName))
        {
          this.name = name;
          this.columnOrdinal = columnOrdinal;
          this.taskLimit = taskLimit;
          this.boardId = boardId;
          tasks = new LinkedList<TaskDTO>();
        }
        public string Name { get => name; set => name = value; }
        public int ColumnOrdinal { get => columnOrdinal; set => columnOrdinal = value; }
        public int TaskLimit
        {
            get => taskLimit;
            set
            {
                updateTaskLimit(value);
                taskLimit = value;
            }
        }
        public LinkedList<TaskDTO> Tasks { get => tasks; set => tasks = value; }
        public DColumnController ColumnController { get => columnController; set => columnController = value; }
        public int id { get => boardId; set => boardId = value; }

        public void updateTaskLimit(int limit)
        {
            LinkedList<string> attributeForUpdate = new LinkedList<string>();
            LinkedList<object> actualKeysForUpdate = new LinkedList<object>();
            attributeForUpdate.AddLast("boardId");
            attributeForUpdate.AddLast("columnOrdinal");
            actualKeysForUpdate.AddLast(boardId);
            actualKeysForUpdate.AddLast(id);
            columnController.update(attributeForUpdate, actualKeysForUpdate, "taskLimit", limit);
        }
    }
}
