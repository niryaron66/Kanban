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
   
    public class Board
    {
        const int MAX_COLUMN_NUMBER = 2;
        const int MIN_COLUMN_NUMBER  = 0;
        private Column[] columns;
        private string name;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int taskCounter;
        private int boardId;
        public string boardOwner;
        public string creatorEmail;
        private BoardDTO boardDTO;
        public Column[] Columns { get => columns; set => columns = value; }
        public string Name { get => name; set => name = value; }
        public int Id { get => boardId;set => boardId = value;}
        public string owner { get => boardOwner; set => boardOwner = value; }
        public string CreatorEmail { get => creatorEmail; set => creatorEmail = value; }
        public int counter { get => taskCounter; set { taskCounter = value;  boardDTO.TaskCounter = value; } }
        public Board( string boardOwner, string name,int id)
        {
            this.Columns = new Column[3];
            for(int i=MIN_COLUMN_NUMBER;i<=MAX_COLUMN_NUMBER;i++)
                columns[i]=new Column(i,id);
            this.name = name;
            this.taskCounter = 0;
            this.boardId = id;
            this.boardOwner = boardOwner;
            this.creatorEmail = boardOwner;
            boardDTO = new BoardDTO(name, id, 0, boardOwner);
        }
        
      /// <summary>
        /// This method gets the name of a specific column
        /// </summary>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>string with the column Name, unless an error occurs </returns>
        public string getColumnName(int columOrdinal) {
            if (columOrdinal.Equals(null) || columOrdinal < MIN_COLUMN_NUMBER || columOrdinal >MAX_COLUMN_NUMBER)
            {
                log.Info($"user tried get column name with invalid column number on board {this.name}");
                throw new Exception("column ordinal were wrong");
            }
            return columns[columOrdinal].getName(); 
        }
        /// <summary>
        /// This method gets the limit of a specific column.
        /// </summary>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>int with the limit value, unless an error occurs </returns>
        public int getColumnLimit(int columOrdinal) { 
            if(columOrdinal.Equals(null)||columOrdinal<MIN_COLUMN_NUMBER || columOrdinal >MAX_COLUMN_NUMBER)
            {
                log.Info($"user tried get column limint with invalid column number on board {this.name}");
                throw new Exception("column ordinal were wrong");
            }
            return columns[columOrdinal].getLimit();
        }
                /// <summary>
        /// This method returns a column given by columnOrdinal
        /// </summary>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>the column, unless an error occurs </returns>
        public Column getColumn(int colunmOrdinal)
        {
            if(colunmOrdinal<MIN_COLUMN_NUMBER || colunmOrdinal > MAX_COLUMN_NUMBER)
            {
                log.Error("user tried to get column but column number is invalid");
                throw new Exception("column ordinal were wrong");
            }
            return columns[colunmOrdinal];
        }
                /// <summary>
        /// This method change a column identified by columnordinal to given column
        /// </summary>
        /// <param name="c">The given column to put</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        

        public void setColumn (int columnOrdinal, Column c)
        {
            this.columns[columnOrdinal] = c;
        }
        /// <summary>
        /// This method returns a list of tasks of column given it's columnordinal
        /// </summary>
        
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns> list of the column's tasks, unless an error occurs </returns>
        public List<Task> getColumnTasks(int colunmOrdinal)
        {
            List<Task> tasks = new List<Task>();
            if (colunmOrdinal < MIN_COLUMN_NUMBER || colunmOrdinal > MAX_COLUMN_NUMBER)
            {
                log.Error("user tried to get column tasks but column number is invalid");
                throw new Exception("column ordinal were wrong");
            }
            foreach(Task task in columns[colunmOrdinal].getTasks())
                tasks.Add(task);
            return tasks;
        }
        //public task endTask(string title, string description, string dueDate) {  }

       /* public Task getColumtask(int columOrdinal,int taskId) {
            if (columOrdinal < MIN_COLUMN_NUMBER || columOrdinal > MAX_COLUMN_NUMBER)
            {
                log.Info($"user tried get column limint with invalid column number on board {this.name} successfuly");
                throw new Exception("column ordinal were wrong");
            }
            return columns[columOrdinal].getTask(taskId);
        } */

        /// <summary>
        /// This method returns all inProgressTask of the board
        /// </summary>
        /// <returns> list of the inProgressTasks, unless an error occurs </returns>
        public LinkedList<Task> inProgressTasks() { return columns[1].getTasks(); }
        /// <summary>
        /// This method limits the number of tasks in a specific column.
        /// </summary>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        public void limitColumn(int columOrdinal, int limit) {
           if(columOrdinal.Equals(null)||limit.Equals(null)||columOrdinal<MIN_COLUMN_NUMBER || columOrdinal > MAX_COLUMN_NUMBER)
            {
                log.Error("user tried to get column number which is invalid");
                throw new Exception("column ordinal was invalid");
            }
           columns[columOrdinal].setLimit(limit); 
        }
        /// <summary>
        /// This method remove all tasks in the board
        /// </summary>
        public void removeAllTask()
        {
            foreach(Column c in columns)
            {
                c.removeAllTasks();
            }
        }
         /// <summary>
        /// This method advances a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        public void advancedTask(int columnOrdinal, int taskId,string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email is invalid");
            }
            email = email.ToLower();
            if(columnOrdinal.Equals(null)||taskId.Equals(null)||columnOrdinal<MIN_COLUMN_NUMBER | columnOrdinal >= MAX_COLUMN_NUMBER)
            {
                log.Error($" user {creatorEmail} commit advanced task to undefined column");
                throw new Exception("task is already in done column or column ordinal was invalid");
            }
            try
            {
                Task t = columns[columnOrdinal].getTask(taskId);
                if(!t.assigne.Equals(email))
                {
                    log.Error($" user {email} is not the task assinge so he cant advance task");
                throw new Exception("this user is not the task assigne so he cant advance task");

                }   
                columns[columnOrdinal + 1].addtask(t);
                t.ColumnOridinal++;
                columns[columnOrdinal].removeTask(taskId);
                t.updateColumnOridinal();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
          
        }



        public BoardDTO GetDTO()
        {
            return boardDTO;
        }



        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in and the assigne</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        public void updateTaskTitle(string email,int columnOrdinal, int taskId, string title)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email is invalid");
            }
            email = email.ToLower();
            if (columnOrdinal<MIN_COLUMN_NUMBER || columnOrdinal>MAX_COLUMN_NUMBER)
            {
                 log.Error("user tried to get column number which is invalid");
                throw new Exception("column ordinal was invalid");
            }
            else if(columnOrdinal ==MAX_COLUMN_NUMBER)
            {
                 log.Error("user tried to get column number where the tasks is done");
                throw new Exception("column ordinal is the done column");
                
            }
               try
            {
                this.getColumn(columnOrdinal).getTask(taskId).updateTaskTitle(title,email);
             
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
                
            

        }
         /// <summary>
        /// This method updates task description.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in and the assigne</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
         public void updateTaskDescription(string email,int columnOrdinal, int taskId, string description)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email is invalid");
            }
            email = email.ToLower();
            if (columnOrdinal<MIN_COLUMN_NUMBER || columnOrdinal>MAX_COLUMN_NUMBER)
            {
                 log.Error("user tried to get column number which is invalid");
                throw new Exception("column ordinal was invalid");
            }
            else if(columnOrdinal ==MAX_COLUMN_NUMBER)
            {
                 log.Error("user tried to get column number where the tasks is done");
                throw new Exception("column ordinal is the done column");
                
            }
               try
            {
                this.getColumn(columnOrdinal).getTask(taskId).updateTaskDescription(description,email);
             
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
                
            

        }
        /// <summary>
        /// This method updates task dueDate.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in and the assigne</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">New dueDate for the task</param>
         public void updateTaskDueDate(string email,int columnOrdinal, int taskId, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email is invalid");
            }
            email = email.ToLower();
            if (columnOrdinal<MIN_COLUMN_NUMBER || columnOrdinal>MAX_COLUMN_NUMBER)
            {
                 log.Error("user tried to get column number which is invalid");
                throw new Exception("column ordinal was invalid");
            }
            else if(columnOrdinal ==MAX_COLUMN_NUMBER)
            {
                 log.Error("user tried to get column number where the tasks is done");
                throw new Exception("column ordinal is the done column");
                
            }
               try
            {
                this.getColumn(columnOrdinal).getTask(taskId).updateTaskDueDate(dueDate,email);
             
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
                
            

        }
        /// <summary>
        /// This method remove a task from this board at columnOrdinal identifier by taskId
        /// </summary>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        public void removeTask(int columnOrdinal,int taskId)
        {
            if(columnOrdinal < MIN_COLUMN_NUMBER || columnOrdinal>MAX_COLUMN_NUMBER)
            {
                log.Error("user tried to get column number which is invalid");
                throw new Exception("column ordinal was invalid");
            }
            try {
            this.getColumn(columnOrdinal).removeTask(taskId);
            }
            catch(Exception e)
            { throw new Exception(e.Message);}
            
        }
        /// <summary>
        /// This method adds a new task to this board at backlog column.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in and be member of the board.</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        public void addTask(string title,string description,DateTime dueDate,string email)
        {
            try
            {
                this.getColumn(0).addTask(this.taskCounter,title,description,dueDate);
            }
            catch (Exception e)
            {
                  throw new Exception (e.Message);  
            }
                
        }
        /// <summary>
        /// This method should return all tasks of the Board in the column
        /// </summary>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>list of the task in the column of the board, unless an error occurs </returns>
         public LinkedList<Task> getTasks(int columnOrdinal)
        {
            if(columnOrdinal < MIN_COLUMN_NUMBER || columnOrdinal>MAX_COLUMN_NUMBER)
            {
                log.Error("user tried to get column number which is invalid");
                throw new Exception("column ordinal was invalid");
            }
            
           return this.getColumn(columnOrdinal).getTasks();
            
            
            
        }

        /// <summary>
        /// This method should return all tasks of the Board that not done.
        /// </summary>
        /// <returns>list of the tasks that not done in the board, unless an error occurs </returns>
        public LinkedList<Task> getNotDone()
        {
            LinkedList<Task> tasks=new LinkedList<Task> ();
            foreach (Task t in columns[0].getTasks())
                tasks.AddLast(t);
            foreach (Task t in columns[1].getTasks())
                tasks.AddLast(t);

           return tasks;
        }
         /// <summary>
        /// This method get the number of tasks
        /// </summary>
        /// <returns>list of the tasks that not done in the board, unless an error occurs </returns>
        public int getCountTask()
        {
            int count = taskCounter;
            return count;
        }
      
        
    }
}
