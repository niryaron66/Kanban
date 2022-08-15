using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// A class for grading your work <b>ONLY</b>. The methods are not using good SE practices and you should <b>NOT</b> infer any insight on how to write the service layer/business layer. 
    /// <para>
    /// Each of the class' methods should return a JSON string with the following structure (see <see cref="System.Text.Json"/>):
    /// <code>
    /// {
    ///     "ErrorMessage": &lt;string&gt;,
    ///     "ReturnValue": &lt;object&gt;
    /// }
    /// </code>
    /// Where:
    /// <list type="bullet">
    ///     <item>
    ///         <term>ReturnValue</term>
    ///         <description>
    ///             The return value of the function.
    ///             <para>
    ///                 The value may be either a <paramref name="primitive"/>, a <paramref name="Task"/>, or an array of of them. See below for the definition of <paramref name="Task"/>.
    ///             </para>
    ///             <para>If the function does not return a value or an exception has occorred, then the field is undefined.</para>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>ErrorMessage</term>
    ///         <description>If an exception has occorred, then this field will contain a string of the error message.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>
    /// The structure of the JSON of a Task, is:
    /// <code>
    /// {
    ///     "Id": &lt;int&gt;,
    ///     "CreationTime": &lt;DateTime&gt;,
    ///     "Title": &lt;string&gt;,
    ///     "Description": &lt;string&gt;,
    ///     "DueDate": &lt;DateTime&gt;
    /// }
    /// </code>
    /// </para>
    /// </summary>
    public class GradingService
    {
        ServiceFactory serviceFactory;
        public GradingService()
        {
            serviceFactory = new ServiceFactory();
        }
         private static string jsonConvertor<T>(string json)
        {
            ResponseT<T> result = JsonSerializer.Deserialize<ResponseT<T>>(json);
            if (result.ErrorMessage != null)
            {
                string res = JsonSerializer.Serialize(result, new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                return res;
                /*string res = JsonSerializer.Serialize(result.ErrorMessage);
                return res;*/
            }
            return JsonSerializer.Serialize(result, new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }

        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string Register(string email, string password)
        {
            string result = serviceFactory._userService.register(email, password);
            result = jsonConvertor<Object>(result);
            return result;
        }


        /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>Response with user email, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string Login(string email, string password)
        {
            string result = serviceFactory._userService.login(email, password);
            result = jsonConvertor<Object>(result);
            return result;
        }


        /// <summary>
        /// This method logs out a logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string Logout(string email)
        {
            string result = serviceFactory._userService.logout(email);
            result = jsonConvertor<Object>(result);
            return result;
        }

        /// <summary>
        /// This method limits the number of tasks in a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {          
            string result = serviceFactory._boardService.limitColumn(email, boardName, columnOrdinal, limit);
            result = jsonConvertor<Object>(result);
            return result;
        }

        /// <summary>
        /// This method gets the limit of a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column limit value, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            string result =  serviceFactory._boardService.getColumnLimit(email, boardName, columnOrdinal);
            result = jsonConvertor<object>(result);
            return result;
        }
        /// <summary>
        /// This method gets the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column name value, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {   
            string result = serviceFactory._boardService.getColumnName(email, boardName, columnOrdinal);
            result = jsonConvertor<string>(result);
            return result;
        }


        /// <summary>
        /// This method adds a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>Response with user-email, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            string result =  serviceFactory._taskService.addTask(email, boardName, title, description, dueDate);
            result = jsonConvertor<string>(result);
            return result;
        }


        /// <summary>
        /// This method updates the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            string result = serviceFactory._taskService.updateTaskDueDate(email, boardName, columnOrdinal, taskId, dueDate);
            result = jsonConvertor<Object>(result);
            return result;
        }


        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            string result = serviceFactory._taskService.updateTaskTitle(email, boardName, columnOrdinal, taskId, title);
            result = jsonConvertor<Object>(result);
            return result;
        }


        /// <summary>
        /// This method updates the description of a task.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            string result = serviceFactory._taskService.updateTaskDescription(email, boardName, columnOrdinal, taskId, description);
            result = jsonConvertor<Object>(result);
            return result;
        }


        /// <summary>
        /// This method advances a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            string result = serviceFactory._taskService.advancedTask(email, boardName, columnOrdinal, taskId);
            result = jsonConvertor<Object>(result);
            return result;

        }


        /// <summary>
        /// This method returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with  a list of the column's tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            string result = serviceFactory._boardService.getColumn(email, boardName, columnOrdinal);
            result = jsonConvertor<List<STask>>(result);
            return result;
        }


        /// <summary>
        /// This method adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AddBoard(string email, string name)
        {
            string result = serviceFactory._boardService.addBoard(email, name);
            result = jsonConvertor<Object>(result);
            return result;
        }


        /// <summary>
        /// This method removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string RemoveBoard(string email, string name)
        {
            string result = serviceFactory._boardService.removeBoard(email, name);
            result = jsonConvertor<Object>(result);
            return result;
        }


        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>Response with  a list of the in progress tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string InProgressTasks(string email)
        {
            string result = serviceFactory._taskService.getInProgress(email);
            result = jsonConvertor<List<STask>>(result);
            return result;
        }
        /// <summary>
        /// this method aloow a user to change his password
        /// </summary>
        /// <param name="email">the email of the user
        /// <param name="oldPass">the old password of the user - jus to athenticate
        /// <param name="newPass">the new password to change
        /// <returns></returns>
        public string changePass(string email,string oldPass,string newPass)
        {
            string result = serviceFactory._userService.updatePassword(email, oldPass, newPass);
            result = jsonConvertor<string>(result);
            return result;
        }
        /// <summary>
        /// This method returns a list of IDs of all user's boards.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A response with a list of IDs of all user's boards, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetUserBoards(string email)
        {
            string result = serviceFactory._boardService.getUserBoards(email);
            result = jsonConvertor<List<int>>(result);
            return result;
            
        }
        /// <summary>
        /// This method returns a board's name
        /// </summary>
        /// <param name="boardId">The board's ID</param>
        /// <returns>A response with the board's name, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetBoardName(int boardId)
        {
            string result = serviceFactory._boardService.getBoardName(boardId);
            result = jsonConvertor<string>(result);
            return result;
        }
        /// <summary>
        /// This method adds a user as member to an existing board.
        /// </summary>
        /// <param name="email">The email of the user that joins the board. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string JoinBoard(string email, int boardID)
        {
            string result = serviceFactory._boardService.joinBoard(email, boardID);
            result = jsonConvertor<string>(result);
            return result;
        }
        /// <summary>
        /// This method removes a user from the members list of a board.
        /// </summary>
        /// <param name="email">The email of the user. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string LeaveBoard(string email, int boardID)
        { 
            string result = serviceFactory._boardService.leaveBoard(email, boardID);
            result = jsonConvertor<string>(result);
            return result;
        }
        /// <summary>
        /// This method assigns a task to a user
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column number. The first column is 0, the number increases by 1 for each column</param>
        /// <param name="taskID">The task to be updated identified a task ID</param>
        /// <param name="emailAssignee">Email of the asignee user</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            string result = serviceFactory._taskService.assignTask(email, boardName,columnOrdinal,taskID,emailAssignee);
            result = jsonConvertor<string>(result);
            return result;
        }

        ///<summary>This method loads all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b> When starting the system via the GradingService - do not load the data automatically, only through this method.
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
        ///</para>
        /// </summary>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string LoadData()
        {
            string res1 = serviceFactory._userService.loadData();
            string res2 = serviceFactory._boardService.loadData();
            string result = "";
            if (res2.Length > 2)
            {
                result = res2;
            }
            else
            {
                result = res1;
            }
            result = jsonConvertor<string>(result);
            return result;
        }

        ///<summary>This method deletes all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b>
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
        ///</para>
        /// </summary>
        ///<returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string DeleteData()
        {
            string res1 = serviceFactory._boardService.deleteData();
            string result = jsonConvertor<string>(res1);
            return result;
        }

        /// <summary>
        /// This method transfers a board ownership.
        /// </summary>
        /// <param name="currentOwnerEmail">Email of the current owner. Must be logged in</param>
        /// <param name="newOwnerEmail">Email of the new owner</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            string result = serviceFactory._boardService.TransferOwnership(currentOwnerEmail,boardName,newOwnerEmail);
            result = jsonConvertor<string>(result);
            return result;
        }
    }
}

