using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {
        private BoardController bc;
      
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public TaskService(BoardController boardController)
        {
            bc = boardController;
        }
        /// <summary>
        /// This method advances a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>respone with The string "{}", unless an error occurs </returns>

        public string advancedTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                
                Board board = bc.getBoardByName(boardName, email);
                board.advancedTask(columnOrdinal, taskId,email);
                ResponseT<string> res = new ResponseT<string>(null, null);
                log.Info($"user {email} advanced task {taskId} from column {columnOrdinal} on board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null,"advanced task failed!!" + e.Message));
            }
        }
        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>respone with The string "{}", unless an error occurs</returns>

        public string updateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {

                Board board = bc.getBoardByName(boardName, email);
                board.updateTaskTitle(email,columnOrdinal,taskId,title);
                ResponseT<string> res = new ResponseT<string>(null, null);
                log.Info($"user {email} update Task {taskId} title on column {columnOrdinal} on board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null,"update title failed!!" + e.Message));
            }
        }
        /// <summary>
         /// This method updates the description of a task.
         /// </summary>
         /// <param name="email">Email of user. Must be logged in</param>
         /// <param name="boardName">The name of the board</param>
         /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
         /// <param name="taskId">The task to be updated identified task ID</param>
         /// <param name="description">New description for the task</param>
         /// <returns>respone with The string "{}", unless an error occurs </returns>

        public string updateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            try
            {
               
                Board board = bc.getBoardByName(boardName, email);
                board.updateTaskDescription(email,columnOrdinal,taskId,description);
               // board.getColumn(columnOrdinal).getTask(taskId).updateTaskDescription(description);
                ResponseT<string> res = new ResponseT<string>(null, null);
                log.Info($"user {email} update Task {taskId} decription on column {columnOrdinal} on board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null,"update description failed!!" + e.Message));
            }

        }
        /// <summary>
        /// This method updates the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>respone with The string "{}", unless an error occurs </returns>

        public string updateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                Board board = bc.getBoardByName(boardName, email);
                board.updateTaskDueDate(email,columnOrdinal,taskId,dueDate);   
                ResponseT<string> res = new ResponseT<string>(null, null);
                log.Info($"user {email} update Task {taskId} due date on column {columnOrdinal} on board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null,"update dueDate failed!!" + e.Message));
            }

        }
        /// <summary>
        /// This method removes a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>respone with The string "{}", unless an error occurs </returns>

        public string removeTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                bc.getBoardByName(boardName, email).removeTask(columnOrdinal,taskId);
              //  bc.getBoardByName(boardName, email).getColumn(columnOrdinal).removeTask(taskId);
                ResponseT<string> res = new ResponseT<string>(null, null);
                log.Info($"user {email} remove Task {taskId} on column {columnOrdinal} from board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null,"task removed failed!!" + e.Message));
            }
        }
        /// <summary>
        /// This method should return all tasks of the Board in the column
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>response with list of the task in the column of the board, unless an error occurs </returns>

        public string getTasks(string email, string boardName, int columnOrdinal)
        {
             
            try // TODO: should response list of the task
            {
                bc.getBoardByName(email, boardName).getTasks(columnOrdinal);
                ResponseT<LinkedList<STask>> res = new ResponseT<LinkedList<STask>>(null, null);
                log.Info($"user {email} get tasks  on column {columnOrdinal} on board {boardName} successfuly");
                var Options = new JsonSerializerOptions();
                return JsonSerializer.Serialize(res, res.GetType(), Options);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "task removed failed!!" + e.Message));
            }
        }
        /// <summary>
        /// This method adds a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>Response with user-email, unless an error occurs </returns>

        public string addTask(string email, string boardName, string title, string decription, DateTime dueDate)
        {

            try
            {
                bc.addTask(email, boardName ,title,  decription,  dueDate);
                ResponseT<string> res = new ResponseT<string>(null, null);
                log.Info($"user {email} add Task on board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "task add failed!!" + e.Message));
            }
        }
        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>Response with  a list of the in progress tasks, unless an error occurs </returns>

        public string getInProgress(string email) //OK
        {
            try
            {
                List<Task> tasks = bc.getInProgress(email);
                ResponseT<List<STask>> res = new ResponseT<List<STask>>(tasks.Select(task => new STask(task)).ToList(), null);
                log.Info($"user {email} get tasks which in progress from every board successfuly");
                var Options = new JsonSerializerOptions();
                return JsonSerializer.Serialize(res, res.GetType(), Options);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null,"get in progress failed!!" + e.Message));
            }
        }
        /// <summary>
        /// This method assigns a task to a user
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column number. The first column is 0, the number increases by 1 for each column</param>
        /// <param name="taskID">The task to be updated identified a task ID</param>
        /// <param name="emailAssignee">Email of the asignee user</param>
        /// <returns>An empty response, unless an error occurs </returns>
        public string assignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee) // an Empty Response
        {
            try
            {
                bc.assignTask(email, boardName, columnOrdinal, taskID, emailAssignee);
                ResponseT<string> res = new ResponseT<string>(null, null);
                log.Info($"user {email} assiged Task on board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "task add failed!!" + e.Message));
            }
            
        }
          ///<summary>This method loads all tasks persisted data.
        /// </summary>
        /// <returns>An empty response, unless an error occurs </returns>
        internal void loadData()
        {
            throw new NotImplementedException();
        }
    }
    //public string endTask(string email,string boardName,int columnOrdinal,int taskId) { throw new NotImplementedException(); }
}
