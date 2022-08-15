using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using log4net;

namespace IntroSE.Kanban.Backend.BusinessLayer

{
    
    public class BoardController
    {
        const int MAX_COLUMN_NUMBER = 2;
        const int MIN_COLUMN_NUMBER = 0;
        private Dictionary<string, LinkedList<Board>> boardsOfMembers; // email to boards that i member in them
        private Dictionary<int,Board> boardIds; // 
        private UserController userController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DBoardController dbController = new DBoardController("Boards");
        private DColumnController dcController = new DColumnController("Columns");
        private DTaskController dtController = new DTaskController("tasks");
        private DMembersController dmController = new DMembersController("MembersInBoard");
        private int boardCounter=0;
        public BoardController(UserController uc)
        {
            this.boardsOfMembers = new Dictionary<string, LinkedList<Board>>();
            this.userController = uc;
            this.boardIds = new Dictionary<int, Board>();
        }
        /// <summary>
        /// This method returns a board by its name and the email
        /// </summary>
        /// <param name="boardName">The board's name</param>
        /// <param name="email">The user email.must be logged in</param>
        /// <returns>return the board, unless an error occurs </returns>
        public Board getBoardByName(string boardName, string email) 
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email is invalid");
            }
            email = email.ToLower();
            checkEmail(email);
            if(boardName == null)
            {
                log.Error($"board name is null");
                throw new Exception("board name is null");
            }
       
            if (boardsOfMembers.ContainsKey(email))
            {
                foreach (Board b in boardsOfMembers[email])
                {
                    if (b.Name == boardName)
                    {
                        return b;
                    }
                }
            }
            log.Error($"sercheang for board {boardName} failed ,board does not exist");
            throw new Exception("board does not exists"); 
        }


        /// <summary>
        /// This method adds a new task to this board at backlog column.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in and be member of the board.</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        public void addTask(string email, string boardName, string title, string decription, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email is invalid");
            }
            email = email.ToLower();
            Board board = getBoardByName(boardName, email);
            if (!this.boardsOfMembers[email].Contains(board))
            {
                log.Error($"user {email} is not member of board {boardName} so he cant add Task");
                throw new Exception("this user is not member of this board so he cant add task");
            }
            board.addTask(title, decription, dueDate, email);
            //dtController.insert(board.getColumn(0).getTasks().Last().TaskDTO);
            LinkedList<string> attributeForUpdate = new LinkedList<string>();
            LinkedList<object> actualKeysForUpdate = new LinkedList<object>();
            attributeForUpdate.AddLast("boardId");
            actualKeysForUpdate.AddLast(board.Id);
            board.counter = board.counter + 1;
            dbController.update(attributeForUpdate, actualKeysForUpdate, "taskCounter", board.counter);
        }



        /// <summary>
        /// This method adds a user as member to this board.
        /// </summary>
        /// <param name="email">The email of the user that joins the board. Must be logged in</param>

        public void addMember(string email, int boardId)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            {
                log.Error("cannot add member");
                throw new Exception("cannot add member email is wrong");
            }
            email = email.ToLower();
            checkEmail(email);
            if (!boardIds.ContainsKey(boardId))
            {
                log.Error($" board id {boardId} does not exits");
                throw new Exception("board id does not exits");
            }
            Board board = boardIds[boardId];
            if(!this.boardsOfMembers.ContainsKey(email))
            {
                boardsOfMembers.Add(email, new LinkedList<Board>());
            }
            if (this.boardsOfMembers[email].Contains(board))
            { 
                log.Error($" user {email} commit addMember to board {board.Name} but is already exist in the members");
                throw new Exception("this user already member in the board");
            }
            foreach(Board b in boardsOfMembers[email])
                {
                if(b.Name==board.Name)
                {
                    log.Error($" user {email} commit addMember to board {board.Name} but  he is already have board with the same name ");
                    throw new Exception("this user already have a board with this name");
                }

            }
            dmController.insert(new MemberDTO(email, boardId));
            boardsOfMembers[email].AddLast(board);
        }


        /// <summary>
        /// This method removes a user from being member at this board
        /// </summary>
        /// <param name="email">The email of the user that joins the board. Must be logged in</param>
        public void removeMember(string email, int boardId)
        {
            email = email.ToLower();
            checkEmail(email);
            if (!boardIds.ContainsKey(boardId))
            {
                log.Error($" board id {boardId} does not exits");
                throw new Exception("board id does not exits");
            }
            Board board = boardIds[boardId];

            if (email == board.boardOwner)
            {
                log.Error($" user {email} commit removeMember to board owner {board.boardOwner} ");
                throw new Exception("cant remove board owner");
            }
            if (!boardsOfMembers[email].Contains(board))
            {
                log.Error($" user {email} commit removeMember to board {board.Name} but is not member in this board");
                throw new Exception("board does not exsits for this user");
            }
            LinkedList<string> attributeForDelete = new LinkedList<string>();
            LinkedList<object> valuesForDelete = new LinkedList<object>();
            attributeForDelete.AddLast("email");
            attributeForDelete.AddLast("boardId");
            valuesForDelete.AddLast(email);
            valuesForDelete.AddLast(boardId);
            dmController.Delete(attributeForDelete, valuesForDelete);
            boardsOfMembers[email].Remove(board);
            LinkedList<string> attributeForUpdate = new LinkedList<string>();
            LinkedList<object> actualKeysForUpdate = new LinkedList<object>();
            attributeForUpdate.AddLast("boardId");
            attributeForUpdate.AddLast("taskId");
            actualKeysForUpdate.AddLast(boardId);
            LinkedList<Task> taskThatNotDone=board.getNotDone();
            foreach (Task t in taskThatNotDone)
            {
                if (t.assigne.Equals(email))
                {
                    actualKeysForUpdate.AddLast(t.id);
                    dtController.update(attributeForUpdate, actualKeysForUpdate,"taskAssigne","");
                    actualKeysForUpdate.RemoveLast();
                    t.assigne = null;
                }
            }
        }


        internal void checkEmail(string email)
        {
            if (email == null)
            {
                log.Error($"user {email} is null");
                throw new Exception("user is null");
            }
            if (!userController.isRegistred(email))
            {
                log.Error($"user {email} commit get board but is not registered");
                throw new Exception("user isnt register");
            }
            if (!userController.isLogedIn(email))
            {
                log.Error($"user {email} commit get board but is not loggin");
                throw new Exception("user isnt logeed in");
            }
        }


        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns> list of the in progress tasks, unless an error occurs </returns>
        public List<Task> getInProgress(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            {
                log.Error("cannot get in progress");
                throw new Exception("cannot get in progress email is wrong");
            }
            email = email.ToLower();
            checkEmail(email);
            List<Task> output = new List<Task>();
            if (boardsOfMembers == null | !boardsOfMembers.ContainsKey(email))
            {
                log.Error($"user {email} tried to get inprogress tasks but dosent has board");
                return output;
            }
            foreach (Board b in boardsOfMembers[email])
            {
                foreach(Task t in b.inProgressTasks())
                {
                    if(t.assigne.Equals(email))
                        output.Add(t);
                }
            }
            return output;
        }
        /// <summary>
        /// This method removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        internal void removeBoard(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
            {
                log.Error("cannot remove board");
                throw new Exception("cannot remove board email is wrong");
            }
            email = email.ToLower();
            if (!userController.isRegistred(email))
            {
                log.Error($"{email} tried to remove board but wassent even registerd");
                throw new Exception("user isnt register");
            }
            if (!userController.isLogedIn(email))
            {
                log.Error($"user {email} tried to remove board but wassent logged in");
                throw new Exception("user isnt logeed in");
            }
       
            try 
            { 
                Board b = getBoardByName(name, email);
                if(!b.owner.Equals(email))
                {
                    log.Error($"user {email} tried to remove board but he is not the owner");
                    throw new Exception("user is not the owner of this board ");
                }
                LinkedList<string> attributeForDelete = new LinkedList<string>();
                LinkedList<object> IdForDelete = new LinkedList<object>();
                attributeForDelete.AddLast("boardId");
                IdForDelete.AddLast(b.Id);
                dtController.Delete(attributeForDelete, IdForDelete);
                dcController.Delete(attributeForDelete, IdForDelete);
                dmController.Delete(attributeForDelete, IdForDelete);
                dbController.Delete(attributeForDelete, IdForDelete);
                b.removeAllTask();
                boardsOfMembers[email].Remove(b);
                boardIds.Remove(b.Id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// This method adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        internal void addBoard(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                {
                    log.Error("cannot add board");
                    throw new Exception("cannot add board email is wrong");
                }
            email = email.ToLower();
            if (!userController.isRegistred(email))
            {
                log.Error($"{email} tried to add board but wassent even registerd");
                throw new Exception("user isnt register");
            }
            if (!userController.isLogedIn(email))
            {
                log.Error($"user {email} tried to add board but wassent logged in");
                throw new Exception("user isnt logeed in");
            }
            try
            {
                Board board = getBoardByName(name, email);
                log.Error($"user {email} tried to add board {name} but this board alredy exist ");
                throw new Exception("board allready exists");
            }
            catch (Exception e)
            {
                try
                {
                    if (e.Message == "board does not exists")
                    {

                        Board b = new Board(email, name, boardCounter);
                        if (!boardsOfMembers.ContainsKey(email))
                        {
                            boardsOfMembers.Add(email, new LinkedList<Board>());
                        }
                        boardsOfMembers[email].AddLast(b);
                        boardIds[boardCounter] = b;
                        dbController.insert(b.GetDTO());
                        dcController.insert(b.getColumn(0).GetDTO());
                        dcController.insert(b.getColumn(1).GetDTO());
                        dcController.insert(b.getColumn(2).GetDTO());
                        dmController.insert(new MemberDTO(email,b.Id));
                        boardCounter++;
                    }
                    else
                    {
                        throw new Exception(e.Message);
                    }
                }catch (Exception e2)
                {
                    log.Error(e2.Message);
                    throw new Exception(e.Message);
                }
            }
        }

        internal List<string> get3BoardsName(string email)
        {
            List<string> names = new List<string>();
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            {
                log.Error("cannot get boards name");
                throw new Exception("cannot get boards name email is wrong");
            }
            email = email.ToLower();
            if (!userController.isRegistred(email))
            {
                log.Error($"{email} tried to get boards name but wassent even registerd");
                throw new Exception("user isnt register");
            }
            if (!userController.isLogedIn(email))
            {
                log.Error($"user {email} tried to get boards name but wassent logged in");
                throw new Exception("user isnt logeed in");
            }
            if (boardsOfMembers == null || !boardsOfMembers.ContainsKey(email))
            {
                log.Error($"user {email} tried to get boards name but dosent has board");
                throw new Exception("user dosent have board");
            }
            foreach (Board b in boardsOfMembers[email])
            {
                if (names.Count < 3)
                {
                    names.Add(b.Name);
                }
            }
            return names;
        }

        /// <summary>
        /// This method transfers a board ownership.
        /// </summary>
        /// <param name="currentOwnerEmail">Email of the current owner. Must be logged in</param>
        /// <param name="newOwnerEmail">Email of the new owner</param>
        /// <param name="boardName">The name of the board</param>
        public void setBoardOwner(string email, string boardName, string newName)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            {
                log.Error("cannot set board owner");
                throw new Exception("cannot set board owner email is wrong");
            }
            if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
            {
                log.Error("cannot set board owner");
                throw new Exception("cannot set board owner email is wrong");
            }
            email = email.ToLower();
            newName = newName.ToLower();
            Board board = getBoardByName(boardName, email);
            if (newName == null || !userController.isRegistred(email))
            {
                log.Error($" user {newName} is null or not registered");
                throw new Exception("user isnt register of email is null");
            }
            if (!board.owner.Equals(email))
            {
                log.Error($"user {email} tried to change board owner on board {boardName} but he is not the boardowner ");
                throw new Exception("member cannot change board owner");
            }
            if(board.owner.Equals(newName))
            {
                log.Error($"user {newName} is allready the board owner");
                throw new Exception("allready board owner");
            }
            if(!boardsOfMembers[newName].Contains(board))
            {
                log.Error($"user {newName} must be member of board to be owner");
                throw new Exception("user must be member of board to be owner");
            }
            LinkedList<string> attributeForUpdate = new LinkedList<string>();
            LinkedList<object> actualKeysForUpdate = new LinkedList<object>();
            attributeForUpdate.AddLast("boardId");
            actualKeysForUpdate.AddLast(board.Id);
            dbController.update(attributeForUpdate, actualKeysForUpdate,"boardOwner",newName);
            board.owner = newName;
        }







        /// <summary>
        /// This method returns a board by its boardId
        /// </summary>
        /// <param name="boardId">The board's id</param>
        /// <returns>return the board, unless an error occurs </returns>
        public Board getBoardbyId(string email,int boardId)
        {
            checkEmail(email);
            if (!boardIds.ContainsKey(boardId))
            {
                log.Error($" boardId {boardId} does not exits");
                throw new Exception("Boardid does not exist");
            }
            return boardIds[boardId];
        }

        public Board getBoardbyIdForLoad(string email, int boardId)
        {
            if (!userController.isRegistred(email))
            {
                log.Error($"user {email} commit get board but is not registered");
                throw new Exception("user isnt register");
            }
            if (!boardIds.ContainsKey(boardId))
            {
                log.Error($" boardId {boardId} does not exits");
                throw new Exception("Boardid does not exist");
            }
            return boardIds[boardId];
        }




        /// <summary>
        /// This method assigns a task to a user
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">The column number. The first column is 0, the number increases by 1 for each column</param>
        /// <param name="taskID">The task to be updated identified a task ID</param>
        /// <param name="emailAssignee">Email of the asignee user</param>
        public void assignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            {
                log.Error("cannot assign Task");
                throw new Exception("cannot assign Task email is wrong");
            }
            if (string.IsNullOrWhiteSpace(emailAssignee) || string.IsNullOrEmpty(emailAssignee))
            {
                log.Error("cannot assign Task");
                throw new Exception("cannot assign Task email is wrong");
            }
            email = email.ToLower();
            emailAssignee = emailAssignee.ToLower();
            checkEmail(email);
            if(emailAssignee == null || !userController.isRegistred(email))
            {
                log.Error($" user {emailAssignee} is null or not registered");
                throw new Exception("user isnt register of email is null");
            }
            Board board = getBoardByName(boardName, email);
            if (columnOrdinal < MIN_COLUMN_NUMBER || columnOrdinal >= MAX_COLUMN_NUMBER)
            {
                log.Error($" user {email} commit assign task to undefined column");
                throw new Exception("task is already in done column or column ordinal was invalid");
            }

            Task t = board.getColumn(columnOrdinal).getTask(taskID);
            if (t.assigne.Equals(""))
            {
                if (!boardsOfMembers[email].Contains(board) || !boardsOfMembers[emailAssignee].Contains(board))
                {
                    log.Error(" this task assigne is null and one of the users in not member");
                    throw new Exception(" this task assigne is null and one of the users in not member");
                }
                t.assigne = emailAssignee;
                t.updateAssigne();
            }
            else
            {
                t.assignTask(email, emailAssignee);
                t.updateAssigne();
            }
        }




        public string getBoardName(int id)
        {
            if(!boardIds.ContainsKey(id))
            {
                throw new Exception("board id does not exits");
            }
            return this.boardIds[id].Name;
        }

        /// <summary>
        /// This method returns a list of IDs of all user's boards.
        /// </summary>
        /// <param name="email">the user email </param>
        /// <returns>list of IDs of all user's boards, unless an error occurs </returns>
        public List<int> getUserBoards(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            {
                log.Error("cannot get user boards");
                throw new Exception("cannot get user boards email is wrong");
            }
            email = email.ToLower();
            if (!userController.isRegistred(email))
            {
                throw new Exception(" error , email not familier");
            }
            List<int> boardIds = new List<int>();
            if (boardsOfMembers.ContainsKey(email))
            {
                foreach(Board b in boardsOfMembers[email])
                {
                    boardIds.Add(b.Id);
                }
            }
            if (boardsOfMembers.Count > 0)
            {
                return boardIds;
            }
            return boardIds;
            // add database
        }

       ///<summary>This method loads all boards persisted data.
        /// </summary>
        public void loadData()
        {
            this.boardsOfMembers = new Dictionary<string, LinkedList<Board>>();
            this.boardIds = new Dictionary<int, Board>();
            try
            {
                List<BoardDTO> bds = dbController.selectAllBoards();
                List<ColumnDTO> columns = dcController.selectAllColumns();
                List<TaskDTO> tasks = dtController.selectAllTasks();
                List<MemberDTO> members = dmController.selectAllMembers();
                LinkedList<User> users = userController.getUsers();
                foreach(User u in users)
                {

                    boardsOfMembers.Add(u.Email, new LinkedList<Board>());
                }
                int i = 0;
                foreach (BoardDTO board in bds)
                {
                    if (board.BoardId >= i)
                        i = board.BoardId;
                }
                boardCounter = i + 1;

                foreach (BoardDTO board in bds)
                {
                    Board newb = new Board(board.owner, board.Name, board.BoardId);
                 
                    foreach (ColumnDTO c in columns)
                    {
                        if (c.id == newb.Id)
                        {
                            Column column = new Column(c.ColumnOrdinal, c.id);
                            foreach (TaskDTO task in tasks)
                            {
                                if (task.BoardId == board.BoardId & task.ColumnId == c.ColumnOrdinal)
                                {
                                    column.addtask(new Task(task));
                                }
                            }
                            newb.setColumn(c.ColumnOrdinal, column);
                        }
                    }

                    newb.counter = board.TaskCounter;
                    /*if (boardsOfMembers.ContainsKey(board.owner))
                    {
                        boardsOfMembers[board.owner].AddLast(newb);
                    }
                    else
                    {
                        boardsOfMembers.Add(board.owner, new LinkedList<Board>());
                        boardsOfMembers[board.owner].AddLast(newb);
                    }*/
                    if (!boardIds.ContainsKey(newb.Id))
                    {
                        boardIds.Add(newb.Id, newb);
                    }
                }
                foreach (MemberDTO m in members)
                {
                    
                    if (this.boardsOfMembers.ContainsKey(m.Email))
                    {
                        this.boardsOfMembers[m.Email].AddLast(getBoardbyIdForLoad(m.Email,m.BoardId));
                    }
                    else
                    {
                        this.boardsOfMembers.Add(m.Email, new LinkedList<Board>());
                        this.boardsOfMembers[m.Email].AddLast(getBoardbyIdForLoad(m.Email, m.BoardId));
                    }
                }
                log.Info("user successfuly apdated data from db");
            }
            catch (Exception e)
            {
                log.Error("user tired load data but failed" + e.Message);
                
            }
        }
          ///<summary>This method deletes all boards persisted data.
        /// </summary>
        public void deleteData()
        {
            dbController.DeleteAll();
            dcController.DeleteAll();
            dmController.DeleteAll();
            dtController.DeleteAll();
            this.boardsOfMembers = new Dictionary<string, LinkedList<Board>>();
            this.boardIds = new Dictionary<int, Board>();
        }
    }
}
