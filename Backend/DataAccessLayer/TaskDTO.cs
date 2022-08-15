using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskDTO : DTO
    {
        private const string tableName = "tasks";
        private int boardId;
        private int columnId;
        private int id;
        private DateTime CreationTime;
        private string Title;
        private string Description;
        private DateTime DueDate;
        private string taskAssigne;//the user who is assignee to the task;string name
        private DTaskController taskController = new DTaskController(tableName);

        public TaskDTO(int boardId, int columnId,int id,DateTime creation, string title,string des,DateTime due,string taslAssigne) : base(new DTaskController(tableName))
        {
            this.boardId = boardId;
            this.columnId = columnId;
            this.id = id;
            this.CreationTime = creation;
            this.Title = title;
            this.Description = des;
            this.DueDate = due;
            this.taskAssigne = taslAssigne;
            
            taskController.insert(this);
        }
        public void asigneTask(string assignee)
        {
            this.taskAssigne = assignee;
            //add database
        }
        public int BoardId { get { return boardId; } set { boardId = value; } }
        public int ColumnId { get { return columnId; } set { columnId = value; } }
        public int Id { get { return id; } set { id = value; } }
        public DateTime creationTime { get { return CreationTime; } set { CreationTime = value; } }
        public string description{ get { return Description; } set { Description = value; } }
        public DateTime dueDate { get { return DueDate; }set { DueDate = value; } }
        public DTaskController DTaskController { get { return taskController; } }
        public string title { get { return Title; } set { Title = value; } }
        public string asignee { get { return taskAssigne; } 
            set {
                updateTaskAssigne(value);
                taskAssigne = value;
            } }





        public void updateTaskAssigne(string newAssigne)
        {
            LinkedList<string> attributeForUpdate = new LinkedList<string>();
            LinkedList<object> actualKeysForUpdate = new LinkedList<object>();
            attributeForUpdate.AddLast("boardId");
            attributeForUpdate.AddLast("taskId");
            actualKeysForUpdate.AddLast(boardId);
            actualKeysForUpdate.AddLast(id);
            taskController.update(attributeForUpdate, actualKeysForUpdate, "taskAssigne", newAssigne);
        }


        public void updateTaskOridinal(int columnOridinal)
        {
            LinkedList<string> attributeForUpdate = new LinkedList<string>();
            LinkedList<object> actualKeysForUpdate = new LinkedList<object>();
            attributeForUpdate.AddLast("boardId");
            attributeForUpdate.AddLast("taskId");
            actualKeysForUpdate.AddLast(boardId);
            actualKeysForUpdate.AddLast(id);
            taskController.update(attributeForUpdate, actualKeysForUpdate, "columnId", columnOridinal);
        }

        public void updateTaskDescription(string des)
        {
            LinkedList<string> attributeForUpdate = new LinkedList<string>();
            LinkedList<object> actualKeysForUpdate = new LinkedList<object>();
            attributeForUpdate.AddLast("boardId");
            attributeForUpdate.AddLast("taskId");
            actualKeysForUpdate.AddLast(boardId);
            actualKeysForUpdate.AddLast(id);
            taskController.update(attributeForUpdate, actualKeysForUpdate, "description", des);
        }

        public void updateTaskTitle(string title)
        {
            LinkedList<string> attributeForUpdate = new LinkedList<string>();
            LinkedList<object> actualKeysForUpdate = new LinkedList<object>();
            attributeForUpdate.AddLast("boardId");
            attributeForUpdate.AddLast("taskId");
            actualKeysForUpdate.AddLast(boardId);
            actualKeysForUpdate.AddLast(id);
            taskController.update(attributeForUpdate, actualKeysForUpdate, "title", title);
        }


        public void updateTaskDueDate(DateTime dueDate)
        {
            LinkedList<string> attributeForUpdate = new LinkedList<string>();
            LinkedList<object> actualKeysForUpdate = new LinkedList<object>();
            attributeForUpdate.AddLast("boardId");
            attributeForUpdate.AddLast("taskId");
            actualKeysForUpdate.AddLast(boardId);
            actualKeysForUpdate.AddLast(id);
            taskController.update(attributeForUpdate, actualKeysForUpdate, "dueDate", dueDate.ToString());
        }

    }
}
