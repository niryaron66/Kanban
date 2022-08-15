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
  
    public class Column
    {
        const int TITLE_MAX_LENGTH=50;
        const int TITLE_MIN_LENGTH=1;
        const int DESCRIPTION_MAX_LENGTH=300;
        const int MIN_LIMIT=-1;
        Dictionary<int, string> map =new Dictionary<int, string>();
        private int boardId;
        private string name;
        private int columnOrdinal;
        private int TaskLimit;
        private LinkedList<Task> tasks;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private ColumnDTO ColumnDTO;
        public int ColumnOrdinal { get => columnOrdinal; set => columnOrdinal = value; }
        public int Id { get => boardId; set => boardId = value; }
     
        public int taskLimit
        {
            get => TaskLimit;
            set {
                ColumnDTO.TaskLimit = value;
                TaskLimit = value;

                }     
        }

        public LinkedList<Task> Tasks { get => tasks; set => tasks = value; }
        public Column(int columnOrdinal, int boardId)
        {
            map.Add(0, "backlog");
            map.Add(1, "in progress");
            map.Add(2, "done");
            this.ColumnOrdinal = columnOrdinal;
            this.TaskLimit = -1;
            this.tasks = new LinkedList<Task>();
            this.name = map[columnOrdinal];
            this.boardId = boardId;
            this.ColumnDTO = new ColumnDTO(boardId,columnOrdinal,this.TaskLimit,this.name);
        }

        /// <summary>
        /// This method adds a new task to this column
        /// </summary>
        /// <param name="taskId">the taskid of the new task.</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        public void addTask(int taskId,string title, string description,DateTime dueDate) {
            
             DateTime creationTime= DateTime.Now;
        /*    if (dueDate.Equals(null)||dueDate.AddDays(1) <= creationTime)
            {
                log.Error($"dueDate cannot be before creation time ");
                throw new Exception("dueDate cannot be before creation time ");
            }*/
   
            if (tasks.Count == TaskLimit)
            {
                log.Error($"column {columnOrdinal} has arrived full capacity");
                throw new Exception("column" + columnOrdinal + "has arrived full capacity");
            }
            if (title==null||title.Length > TITLE_MAX_LENGTH | title.Length<TITLE_MIN_LENGTH)
            {
                log.Error($"title length must be between {TITLE_MIN_LENGTH} to {TITLE_MAX_LENGTH}");
                throw new Exception("title length must be between " + TITLE_MIN_LENGTH + " to " + TITLE_MAX_LENGTH);
            }
            if (description==null||description.Length > DESCRIPTION_MAX_LENGTH)
            {
                log.Error($"description length must be under {DESCRIPTION_MAX_LENGTH}");
                throw new Exception("description length must be under " + DESCRIPTION_MAX_LENGTH);
            }
            tasks.AddLast(new Task(taskId,title, description, dueDate, creationTime, this.boardId,0));
        }
        /// <summary>
        /// This method adds a new given task to this column
        /// </summary>
        /// <param name="t">the given task to add.</param>
        public void addtask(Task t)
        {
            if (tasks.Count == TaskLimit)
            {
                log.Error($"column {columnOrdinal} has arrived full capacity");
                throw new Exception("column" + columnOrdinal + "has arrived full capacity");
            }
            else
            {
                tasks.AddLast(t);
            }
        }

        public ColumnDTO GetDTO()
        {
            return this.ColumnDTO;
        }


        /// <summary>
        /// This method set a new limit for this column
        /// </summary>
        /// <param name="limit">the new limit of the column</param>
        public void setLimit(int limit)
        {
            
            if (limit < MIN_LIMIT) {
                throw new Exception("limit cannot be under " + MIN_LIMIT);
            }
            if(limit!=-1)
            {
                if (limit <= tasks.Count)
                {
                    throw new Exception("limit cannot be less then actual amout");
                }
            }
            
            taskLimit = limit;
        }
        /// <summary>
        /// This method gets the limit of this column
        /// </summary>
        public int getLimit()
        {
            return TaskLimit;
        }
        /// <summary>
        /// This method gets all the tasks of this column
        /// </summary>
        /// <returns>list of the task of this column, unless an error occurs </returns>
        public LinkedList<Task> getTasks() { return tasks; }
        
        /// <summary>
        /// This method return the task by taskId
        /// /// <param name="id">the id of the wanted task</param>
        /// </summary>
        /// <returns>list of the task of this column, unless an error occurs </returns>
        public Task getTask(int id) 
        {
            if (id < 0)
            {
                log.Error($"taskId {id} cannot be under zero");
                throw new Exception("id cannot be under 0");
            }
            for(int i = 0; i < tasks.Count; i++)
            {
                if (tasks.ElementAt(i).id == id)
                {
                    return tasks.ElementAt(i);
                }
            }
            log.Error($"user commit get task with id that does not exits");
            throw new Exception("task was not found");
        }
          /// <summary>
        /// This method removes all the tasks of this column
        /// </summary>
        public void removeAllTasks()
        {
            this.tasks.Clear();
        }

        public string getName()
        {
            return name.ToString();
        }
          /// <summary>
        /// This method remove a task from the column identify by its id
        /// </summary>
       /// <param name="taskId">the taskid of the wanted task to remove.</param>
       
        public void removeTask(int taskId)
        {
            Task t = null;
            try
            {
                t = getTask(taskId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (t != null)
            {
                tasks.Remove(t);

            }
        }
    }
}