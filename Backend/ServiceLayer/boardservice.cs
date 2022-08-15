using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        private BoardController bc;
        private UserController uc;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static int boardsCounter;
        public BoardService(UserController userController, BoardController boardController)
        {
            uc = userController;
            bc = boardController;

        }
        /// <summary>
        /// This method limits the number of tasks in a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>response with The string "{}", unless an error occurs </returns>

        public string limitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                Board board = bc.getBoardByName(boardName, email);
                board.limitColumn(columnOrdinal, limit);
                ResponseT<string> ret = new ResponseT<string>(null, null);
                log.Info($"user {email} limit column {columnOrdinal} to limit {limit} on board {boardName} successfuly");
                return JsonSerializer.Serialize(ret);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "limit column failed!" + e.Message));
            }
        }
        /// <summary>
        /// This method returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with  a list of the column's tasks, unless an error occurs </returns>

        public string getColumn(string email, string boardName, int columnOrdinal)
        {
            try
            {
                Board board = bc.getBoardByName(boardName, email);
                List<Task> tasks = board.getColumnTasks(columnOrdinal);
                ResponseT<List<STask>> res = new ResponseT<List<STask>>(tasks.Select(task => new STask(task)).ToList(), null);
                log.Info($"user {email} tried get column {columnOrdinal} from board {boardName} successfuly");
                var Options = new JsonSerializerOptions();
                return JsonSerializer.Serialize(res, res.GetType(), Options);
            }
            catch (Exception e)
            {
                List<Task> tasks = new List<Task>();
                return JsonSerializer.Serialize(new ResponseT<List<Task>>(null, " getColumn column failed!!! " + e.Message));
            }
        }
        /// <summary>
        /// This method adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>response with The string "{}", unless an error occurs </returns>

        public string addBoard(string email, string name)
        {
            try
            {
                bc.addBoard(email, name);
                ResponseT<string> res = new ResponseT<string>(null, null);
                log.Info($"user {email} added board {name} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "board add failed!!" + e.Message));
            }

        }
        public string TransferOwnership(string email, string boardName, string newName)
        {
            try
            {
                bc.setBoardOwner(email, boardName, newName);
                ResponseT<string> res = new ResponseT<string>(null, null);
                log.Info($"user {email} setBoardOwner on board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "board removed failed!!" + e.Message));
            }
        }
        /// <summary>
        /// This method removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>response with The string "{}", unless an error occurs </returns>

        public string removeBoard(string email, string name)
        {
            try
            {
                bc.removeBoard(email, name);
                ResponseT<string> res = new ResponseT<string>(null, null);
                log.Info($"user {email} removed board {name} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "board removed failed!!" + e.Message));
            }
        }

        public string getBoardName(int boardId)
        {
            try
            {
                string name =bc.getBoardName(boardId);
                ResponseT<string> res = new ResponseT<string>(name, null);
                log.Info($" user tried getting board name: {name} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "board removed failed!!" + e.Message));
            }
        }

        /// <summary>
        /// This method returns the board with name like board name from the username.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>response with The boardname string, unless an error occurs </returns>
        public string getBoard(string email, string boardName)
        {
            try
            {
                Board board = bc.getBoardByName(boardName, email);
                ResponseT<Board> res = new ResponseT<Board>(board, null);
                log.Info($"user {email}tried get board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "get board failed!!" + e.Message));
            }
        }
        /// <summary>
        /// This method gets the limit of a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column limit value, unless an error occurs </returns>

        public string getColumnLimit(string email, string boardName, int columnOrdinal)
        {
            try
            {
                
                Board board = bc.getBoardByName(boardName, email); //TODO check if its ok 
                int limit = board.getColumnLimit(columnOrdinal);
                Response2 res = new Response2(limit, null);
                log.Info($"user {email}tried get limit of column {columnOrdinal} on board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "get board failed!!" + e.Message));
            }
        }
        /// <summary>
        /// This method gets the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column name value, unless an error occurs </returns>

        public string getColumnName(string email, string boardName, int columnOrdinal)
        {
            try
            {
                Board board = bc.getBoardByName(boardName, email); 
                string name = board.getColumnName(columnOrdinal);
                Response2 res = new Response2(name, null);
                log.Info($"user {email} tried get the name of column {columnOrdinal} on board {boardName} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "get board failed!!" + e.Message));
            }
        }
        /// <summary>
        /// This method adds a user as member to an existing board.
        /// </summary>
        /// <param name="email">The email of the user that joins the board. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs </returns>

        public string joinBoard(string email, int boardId)
        {
            try
            {
                bc.addMember(email, boardId);
                Response2 res = new Response2(null, null);
                log.Info($"user {email} tried to add himself to board {boardId} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "add board member failed!!" + e.Message));
            }
        }

       
        /// <summary>
        /// This method removes a user from the members list of a board.
        /// </summary>
        /// <param name="email">The email of the user. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string leaveBoard(string email, int boardID)
        {
            try
            {
                bc.removeMember(email, boardID);
                Response2 res = new Response2(null, null);
                log.Info($"user {email} tried to remove himself to board {boardID} successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "remove board member failed!!" + e.Message));
            }
        }

     /*   public string transferOwnerShip(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            try { 
            Board board=bc.getBoardByName(boardName,currentOwnerEmail);
            board.transferOwnerShip(currentOwnerEmail,newOwnerEmail);

                    }
            
        } */

        /// <summary>
        /// This method returns a list of IDs of all user's boards.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A response with a list of IDs of all user's boards, unless an error occurs </returns>
        public string getUserBoards(string email)
        {
            try
            {
                List<int> ids = bc.getUserBoards(email);
                Response2 res = new Response2(ids, null);
                log.Info($"user tried to get all {email} boards by id uccessfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "get User boards faild , user does not registered!!" + e.Message));
            }
        }
        ///<summary>This method loads all boards and users persisted data.
        /// </summary>
        /// <returns>An empty response, unless an error occurs</returns>
       public string loadData()
        {
            try
            {
                // add try catch
               // uc.loadData();
                bc.loadData();
                Response2 res = new Response2(null, null);
                log.Info($"user loaded all data uccessfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "load data failed!!" + e.Message));
            }
        }
        ///<summary>This method deletes all persisted data.
        
        /// </summary>
        ///<returns>An empty response, unless an error occurs </returns>
        public string deleteData()
        {
            try
            {
                bc.deleteData();
                uc.deleteData();
                Response2 res = new Response2(null, null);
                log.Info($"user tried to delete all data of boards successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new ResponseT<string>(null, "delete data failed!!" + e.Message));
            }
           
        }
        public string get3BoardsName(string email)
        {
            try
            {
                List<String> boards = bc.get3BoardsName(email);
                Response2 res = new Response2(boards, null);
                log.Info($"user {email} tried to get 3 boards successfuly");
                return JsonSerializer.Serialize(res);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
