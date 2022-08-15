using IntroSE.Kanban.Backend.DataAccessLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{

   public class Task
    {
        [NonSerialized] private int boardId;
        [NonSerialized] private int columnId;
        private int Id;
        private DateTime CreationTime;
        private string Title;
        private string Description;
        private DateTime DueDate;
        [NonSerialized] const int DESCRIPTION_MAX_LEN = 300;
        [NonSerialized] const int TITLE_MAX_LEN = 50;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string taskAssigne;//the user who is assignee to the task;string name
        private TaskDTO taskDTO;
        public Task(int id,string title, string description, DateTime dueDate, DateTime creationTime,int boardId,int columnOrdinal)
        {
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.CreationTime = creationTime;
            this.Id = id;
            this.taskAssigne = "";
            this.boardId= boardId;
            this.columnId = columnOrdinal;
            this.taskDTO = new TaskDTO(boardId, columnId, Id, creationTime, title, description,dueDate,"");
        }
        public string assigne { get => taskAssigne; 
            set  { taskAssigne = value;
                TaskDTO.asignee = value; } 
                }
        public string title { get => Title; set { Title = value; taskDTO.title = value;  } }
        public string description { get => Description; set { Description = value; taskDTO.description = value;} }
        public DateTime dueDate { get => DueDate; set { DueDate = value; taskDTO.dueDate = value; } }
        public DateTime creationTime { get => CreationTime; set => CreationTime = value; }
        public int id { get => Id; set => Id = value;}

        public int ColumnOridinal { get => columnId; 
            set { columnId = value; TaskDTO.ColumnId = value; } 
        }

        public TaskDTO TaskDTO { get => taskDTO;}


        public void updateTaskTitle(string title,string email) 
        {
            if(!this.assigne.Equals(email))
            {
                log.Error($"the user {email} is not the assigne of the task so cant change it" );
                throw new Exception("this user is not the assinge of the task so cant change it" );
            }
         if(string.IsNullOrWhiteSpace(title)||title.Length == 0 ||  title.Length > TITLE_MAX_LEN)
            {
                log.Error($"trying to update task title but title Lenght must be between 0 and {TITLE_MAX_LEN}" );
                throw new Exception("title Lenght must be between 0 and" + TITLE_MAX_LEN);
            }
            this.title = title;
            this.taskDTO.updateTaskTitle(this.title);

        }


        public void assignTask(string email,string assigneEmail)
        {

            if(!this.assigne.Equals(email))
            {
                log.Error($" user {email} is not the task assinge so he cant assign task");
                throw new Exception("this user is not the task assigne so he cant assign task");
            }  

            else if (this.assigne.Equals(assigneEmail))
            {
                log.Error($" user {assigneEmail} is already this task Assigne");
                throw new Exception("this user is already this task assigne");
            }
            this.assigne=assigneEmail;
        }

        public void updateTaskDescription(string description,string email)
        { 
             if(!this.assigne.Equals(email))
            {
                log.Error($"the user {email} is not the assigne of the task so cant change it" );
                throw new Exception("this user is not the assinge of the task so cant change it" );
            }
            if(description == null)
            {
                log.Error($"trying to update task description but description is null");
                throw new Exception("description cant be null");
            }
            if (description.Length > DESCRIPTION_MAX_LEN)
            {
                log.Error($"trying to update task description but description Lenght must be under {DESCRIPTION_MAX_LEN}");
                throw new Exception("description Lenght must be under" + DESCRIPTION_MAX_LEN);
            }
            this.description = description;
            this.taskDTO.updateTaskDescription(this.description);

        }
        public void updateTaskDueDate(DateTime dueDate,string email) 
        {
             if(!this.assigne.Equals(email))
            {
                log.Error($"the user {email} is not the assigne of the task so cant change it" );
                throw new Exception("this user is not the assinge of the task so cant change it" );
            }
           /* if (dueDate==null)
            {
                log.Error("trying to update task due date but due date cannot be null");
                throw new Exception("due date cant be null");
            }*/
            if (dueDate.AddDays(1) < this.creationTime)
            {
                log.Error($"trying to update task due date but due date cannot be before creation time");
                throw new Exception("dueDate must come after creation time");
            }
            this.DueDate = dueDate;
            this.taskDTO.updateTaskDueDate(this.dueDate);
        }


        public void updateAssigne()
        {
            this.TaskDTO.updateTaskAssigne(assigne);
        }



        public void updateColumnOridinal()
        {
            this.TaskDTO.updateTaskOridinal(columnId);
        }


        public Task (TaskDTO task)
        {
            this.taskDTO = task;
            this.description = task.description;
            this.dueDate = task.dueDate;
            this.title=task.title;
            this.creationTime = task.creationTime;
            this.boardId = task.BoardId;
            this.columnId = task.ColumnId;  
            this.Id = task.Id;
            this.assigne = task.asignee;
        }
    }
}
