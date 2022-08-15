using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using IntroSE.Kanban.Backend.BusinessLayer;

//namespace IntroSE.Kanban.Backend.ServiceLayer.boardService
namespace BackendTests
{
    public class ServiceTest
    {
        private static UserController uc = new UserController();
        private static BoardController bc = new BoardController(uc);
        private BoardService boardService = new BoardService(uc, bc);
        private UserService userSer = new UserService(uc);
        private TaskService taskService = new TaskService(bc);
        private JsonDeserializer jsonDeserializer = new JsonDeserializer();


        /// <summary>
        ///this function try to registers users and return the result of the program try.
        /// This function tests Requirement 7
        /// </summary>

        public void testRegister()
        {

            string resultRegister1 = userSer.register("adirelad@gmail.com", "Nir1234");
            Console.WriteLine(resultRegister1);
            ResponseT<string>? jsonResultRegister1 = JsonSerializer.Deserialize<ResponseT<string>>(resultRegister1);

            if (!jsonResultRegister1.ErrorOccured)
                Console.WriteLine("OK, return ReturnValue is :" + jsonResultRegister1.ReturnValue);
            else
                Console.WriteLine(jsonResultRegister1.ErrorOccured);

            string resultRegister2 = userSer.register("adirelad@gmail.com", "Nir1234");
            ResponseT<string>? jsonResultRegister2 = JsonSerializer.Deserialize<ResponseT<string>>(resultRegister2);

            if (jsonResultRegister2.ErrorOccured)
                Console.WriteLine("OK, good error: " + jsonResultRegister2.ErrorMessage);
            else
                Console.WriteLine("should have faild, this email is already registered");

            string resultRegister3 = userSer.register("niryaron678@gmail.com", "Nir1234");
            Console.WriteLine(resultRegister3);
            ResponseT<string>? jsonResultRegister3 = JsonSerializer.Deserialize<ResponseT<string>>(resultRegister3);

            if (!jsonResultRegister3.ErrorOccured)
                Console.WriteLine("OK, return ReturnValue is :" + jsonResultRegister3.ReturnValue);
            else
                Console.WriteLine(jsonResultRegister1.ErrorOccured);



            string resultRegister5 = userSer.register("AdIrELAD@gmail.com", "Nir1234");
            ResponseT<string>? jsonResultRegister5 = JsonSerializer.Deserialize<ResponseT<string>>(resultRegister5);

            if (jsonResultRegister5.ErrorOccured)
                Console.WriteLine("OK, good error: " + jsonResultRegister5.ErrorMessage);
            else
                Console.WriteLine("should have faild");

            string resultRegister6 = userSer.register("niryar@gmailcom", "Nir1234");
            ResponseT<string>? jsonResultRegister6 = JsonSerializer.Deserialize<ResponseT<string>>(resultRegister6);

            if (jsonResultRegister6.ErrorOccured)
                Console.WriteLine("OK, good error: " + jsonResultRegister6.ErrorMessage);
            else
                Console.WriteLine("not valid email,should be failed");



            string resultRegister7 = userSer.register("niryargmail.com", "Nir1234");
            ResponseT<string>? jsonResultRegister7 = JsonSerializer.Deserialize<ResponseT<string>>(resultRegister7);

            if (jsonResultRegister7.ErrorOccured)
                Console.WriteLine("OK, good error: " + jsonResultRegister7.ErrorMessage);
            else
                Console.WriteLine("not valid email,should be failed");










        }
        /// <summary>
        ///this function try to login users and return the result of the program try. (login check if the user exists and if the pass
        /// word fit)
        /// This function tests Requirement 8
        /// </summary>
        public void testLogin()
        {

            // mazav : users register and login : adirelad@gmail.com,niryaron678@gmail.com

            userSer.logout("adirelad@gmail.com");
            userSer.logout("niryaron678@gmail.com");
            string resultTest0 = userSer.login("adirelad@gmail.com", "Nir1234134");
            ResponseT<string>? jsonResultLoginTest0 = JsonSerializer.Deserialize<ResponseT<string>>(resultTest0);
            if (!jsonResultLoginTest0.ErrorOccured)
                Console.WriteLine("should be failed, login with wrong password");
            else
                Console.WriteLine("OK, good error: " + jsonResultLoginTest0.ErrorMessage);

            string resultTest3 = userSer.login("aaa@gmail.com", "Nir11111");
            ResponseT<string>? jsonResultLoginTest3 = JsonSerializer.Deserialize<ResponseT<string>>(resultTest3);
            if (!jsonResultLoginTest3.ErrorOccured)
                Console.WriteLine("Error : login work on not exist email");
            else
                Console.WriteLine("OK, good error: " + jsonResultLoginTest3.ErrorMessage);

            string resultTest1 = userSer.login("adirelad@gmail.com", "Nir1234");
            ResponseT<string>? jsonResultLoginTest1 = JsonSerializer.Deserialize<ResponseT<string>>(resultTest1);
            if (!jsonResultLoginTest1.ErrorOccured)
                Console.WriteLine("OK, return ReturnValue is: " + jsonResultLoginTest1.ReturnValue);
            else
                Console.WriteLine(jsonResultLoginTest1.ErrorMessage);

            string resultTest2 = userSer.login("adirelad@gmail.com", "Nir1234");
            ResponseT<string>? jsonResultLoginTest2 = JsonSerializer.Deserialize<ResponseT<string>>(resultTest2);
            if (!jsonResultLoginTest2.ErrorOccured)
                Console.WriteLine("this let to login to a loggedin user");
            else
                Console.WriteLine("OK, good error: " + jsonResultLoginTest2.ErrorMessage);

            string resultTest4 = userSer.login("niryaron678@gmail.com", "Nir1234");
            ResponseT<string>? jsonResultLoginTest4 = JsonSerializer.Deserialize<ResponseT<string>>(resultTest4);
            if (!jsonResultLoginTest4.ErrorOccured)
                Console.WriteLine("OK, return ReturnValue is: " + jsonResultLoginTest4.ReturnValue);
            else
                Console.WriteLine(jsonResultLoginTest4.ErrorMessage);



        }

        /// <summary>
        ///this function try to logout users and return the result of the program try. (login check if the user exists and if the pass
        /// word fit)
        /// This function tests Requirement 8
        /// </summary>
        public void testLogout()
        {
            // mazav : users register and login : adirelad@gmail.com,niryaron678@gmail.com

            string resultTest1 = userSer.logout("adirelad@gmail.com");
            ResponseT<string>? jsonResultLogoutTest1 = JsonSerializer.Deserialize<ResponseT<string>>(resultTest1);
            if (!jsonResultLogoutTest1.ErrorOccured)
                Console.WriteLine("OK, return ReturnValue is: " + jsonResultLogoutTest1.ReturnValue);
            else
                Console.WriteLine(jsonResultLogoutTest1.ErrorMessage);

            string resultTest2 = userSer.logout("adirelad@gmail.com");
            ResponseT<string>? jsonResultLogoutTest2 = JsonSerializer.Deserialize<ResponseT<string>>(resultTest2);
            if (!jsonResultLogoutTest2.ErrorOccured)
                Console.WriteLine("this let to logout from logged out user");
            else
                Console.WriteLine("OK, good error: " + jsonResultLogoutTest2.ErrorMessage);

            string resultTest3 = userSer.logout("aaa@gmail.com");
            ResponseT<string>? jsonResultLogoutTest3 = JsonSerializer.Deserialize<ResponseT<string>>(resultTest3);
            if (!jsonResultLogoutTest3.ErrorOccured)
                Console.WriteLine("Error : logout work on not exist email");
            else
                Console.WriteLine("OK, good error: " + jsonResultLogoutTest3.ErrorMessage);

            userSer.login("adirelad@gmail.com", "Nir1234");
        }


        /// <summary>
        /// this function try to get board name by board id, get program result and print it
        /// this function tests Requirement 5 
        /// </summary>

        public void testGetBoardName() // *****
        {

            // adirelad@gmail.com login with Board boardboard (id=0) (bobobo=1)
            //niryaron678@gmail.com logout
            boardService.addBoard("adirelad@gmail.com", "bobobo");
            ResponseT<string>? resultGetBoardName = JsonSerializer.Deserialize<ResponseT<string>>(boardService.getBoardName(1));
            if (resultGetBoardName != null)
            {
                if (!resultGetBoardName.ErrorOccured)
                {
                    if (resultGetBoardName.ReturnValue.Equals("bobobo"))
                        Console.WriteLine("OK, return ReturnValue is: " + resultGetBoardName.ReturnValue);
                    else
                        Console.WriteLine("failed, this is not the fit boardName to the id");
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultGetBoardName.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultGetBoardName = JsonSerializer.Deserialize<ResponseT<string>>(boardService.getBoardName(1000));
            if (resultGetBoardName != null)
            {
                if (!resultGetBoardName.ErrorOccured)
                {
                    Console.WriteLine("failed, there is no board with this boardid");
                }
                else
                {
                    Console.WriteLine("OK :predicterd faild with error: " + resultGetBoardName.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");



        }
        /// <summary>
        ///this function test the add Board function, it try to add board and get
        /// program result and print it . 
        /// This function tests Requirement 9
        /// </summary>
        public void testAddBoard()
        {
            // mazav : users register and login : adirelad@gmail.com,niryaron678@gmail.com
            userSer.logout("niryaron678@gmail.com");
            ResponseT<string>? resultTestAddBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.addBoard("adirelad@gmail.com", "boardboard"));


            if (resultTestAddBoard != null)
            {
                if (!resultTestAddBoard.ErrorOccured)
                    Console.WriteLine("OK, return ReturnValue is: " + resultTestAddBoard.ReturnValue);
                else
                    Console.WriteLine(resultTestAddBoard.ErrorMessage);


            }
            else
                Console.WriteLine("json problem");

            resultTestAddBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.addBoard("adirelad@gmail.com", "boardboard"));
            if (resultTestAddBoard != null)
            {
                if (resultTestAddBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAddBoard.ErrorMessage);
                }
                else
                    Console.WriteLine("board name should be uninqe");
            }
            else
                Console.WriteLine("json problem");



            resultTestAddBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.addBoard("niryaron678@gmail.com", "board1"));
            if (resultTestAddBoard != null)
            {
                if (resultTestAddBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAddBoard.ErrorMessage);
                }
                else
                    Console.WriteLine("this user isnt login so cant add boards.");
            }
            else
                Console.WriteLine("json problem");
        }
        /// <summary>
        ///this function test the get Board function, it try to get board by user and name from the 
        /// program  and print result . 
        /// </summary>
        public void testGetBoard()
        {

            // adirelad@gmail.com login with Board boardboard
            //niryaron678@gmail.com logout
            //string resultTest = boardService.getBoard("adirelad@gmail.com", "boardboard");
            ResponseT<string>? resultTestGetBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.getBoard("adirelad@gmail.com", "boardboard"));
            if (resultTestGetBoard != null)
            {
                if (!resultTestGetBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultTestGetBoard.ReturnValue);
                }
                else
                {
                    Console.WriteLine("board should be found: " + resultTestGetBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");
            //string resultTest2 = boardService.getBoard("adirelad@gmail", "boardboard");
            resultTestGetBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.getBoard("adirelad@gmail", "boardboard"));
            if (resultTestGetBoard != null)
            {
                if (resultTestGetBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestGetBoard.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("error should be displayed because mail does not exists");
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTest3 = boardService.getBoard("adirelad@gmail.com", "board");
            resultTestGetBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.getBoard("adirelad@gmail.com", "board"));
            if (resultTestGetBoard != null)
            {
                if (resultTestGetBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestGetBoard.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("error should be displayed because board name does not exists");
                }
            }
            else
                Console.WriteLine("json problem");

            resultTestGetBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.getBoard("niryaron678@gmail.com", "boardboard"));
            if (resultTestGetBoard != null)
            {
                if (resultTestGetBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestGetBoard.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("error should be failed,this user logout");
                }
            }
            else
                Console.WriteLine("json problem");




        }
        /// <summary>
        ///this function try to get column of some board , get
        /// program result and print it . 
        /// </summary>
        public void testGetColumn()
        {

            // adirelad@gmail.com login with Board boardboard (id=0) (bobobo=1)
            //niryaron678@gmail.com logout


            DateTime d = DateTime.Parse("22/2/2023");
            /* taskService.addTask("adirelad@gmail.com", "b", "work 2", " build engine in exa", d);
             taskService.addTask("adirelad@gmail.com", "b", "work 3", " build engine", d);*/
            //string res = boardService.getColumn("adirelad@gmail.com", "b", 0);
            //Console.WriteLine(res);
            ResponseT<List<Task>>? resultTestGetColumn = JsonSerializer.Deserialize<ResponseT<List<Task>>>(boardService.getColumn("adirelad@gmail.com", "boardboard", 0));
            Console.WriteLine(resultTestGetColumn);
            if (resultTestGetColumn != null)
            {
                if (!resultTestGetColumn.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is:" + resultTestGetColumn.ReturnValue);
                }
                else
                {
                    Console.WriteLine("fail,board exist but didnt found column number 0: " + resultTestGetColumn.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultTestGetColumn = JsonSerializer.Deserialize<ResponseT<List<Task>>>(boardService.getColumn("adirelad@gmail.com", "boardboard", 3));
            if (resultTestGetColumn != null)
            {
                if (resultTestGetColumn.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestGetColumn.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("fail, there is no such column");
                }
            }


            resultTestGetColumn = JsonSerializer.Deserialize<ResponseT<List<Task>>>(boardService.getColumn("adirelad@gmail.com", "c", 2));
            if (resultTestGetColumn != null)
            {
                if (!resultTestGetColumn.ErrorOccured)
                {
                    Console.WriteLine("fail, there is no such board");
                }
                else
                {
                    Console.WriteLine("OK, good error: " + resultTestGetColumn.ErrorMessage);
                }
            }
            userSer.logout("adirelad@gmail.com");

            resultTestGetColumn = JsonSerializer.Deserialize<ResponseT<List<Task>>>(boardService.getColumn("adirelad@gmail.com", "boardboard", 2));
            if (resultTestGetColumn != null)
            {
                if (!resultTestGetColumn.ErrorOccured)
                {
                    Console.WriteLine("should be fail, the user isnt log in");
                }
                else
                {
                    Console.WriteLine("OK, good error: " + resultTestGetColumn.ErrorMessage);
                }
            }
            userSer.login("adirelad@gmail.com", "Nir1234");

        }
        /// <summary>
        ///this function try to limit column,  get
        /// program result and print it . 
        /// This function tests Requirement 10,11 
        /// </summary>
        public void testLimitColumn()
        {
            // adirelad@gmail.com login with Board boardboard (id=0) (bobobo=1)
            //niryaron678@gmail.com logout
            ResponseT<string>? resultTestLimitColumn = JsonSerializer.Deserialize<ResponseT<string>>(boardService.limitColumn("adirelad@gmail.com", "boardboard", 0, 2));
            if (resultTestLimitColumn != null)
            {
                if (!resultTestLimitColumn.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue: " + resultTestLimitColumn.ReturnValue);
                }
                else
                {
                    Console.WriteLine("limit faild with error: " + resultTestLimitColumn.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultTestLimitColumn = JsonSerializer.Deserialize<ResponseT<string>>(boardService.limitColumn("adirelad@gmail.", "boardboard", 0, 2));
            //string resultTest1 = boardService.limitColumn("adirelad@gmail.", "b", 0, 5);
            if (resultTestLimitColumn != null)
            {
                if (resultTestLimitColumn.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestLimitColumn.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("email does not exists should have faild");
                }
            }
            else
                Console.WriteLine("json problem");

            resultTestLimitColumn = JsonSerializer.Deserialize<ResponseT<string>>(boardService.limitColumn("adirelad@gmail.com", "c", 0, 2));
            //string resultTest2 = boardService.limitColumn("adirelad@gmail.com", "c", 0, 5);
            if (resultTestLimitColumn != null)
            {
                if (resultTestLimitColumn.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestLimitColumn.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("board name does not exists should have faild");
                }
            }
            else
                Console.WriteLine("json problem");


            resultTestLimitColumn = JsonSerializer.Deserialize<ResponseT<string>>(boardService.limitColumn("adirelad@gmail.", "boardboard", 0, -5));
            //string resultTest3 = boardService.limitColumn("adirelad@gmail.", "b", 0, -5);
            if (resultTestLimitColumn != null)
            {
                if (resultTestLimitColumn.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestLimitColumn.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("limit cannot be negative should have faild");
                }
            }
            else
                Console.WriteLine("json problem");


            ResponseT<int>? resultTestGetLimitColumn = JsonSerializer.Deserialize<ResponseT<int>>(boardService.getColumnLimit("adirelad@gmail.com", "boardboard", 0));//change to string
            if (resultTestGetLimitColumn != null)
            {
                if (!resultTestGetLimitColumn.ErrorOccured && resultTestGetLimitColumn.ReturnValue == 2)
                {
                    Console.WriteLine("OK, return ReturnValue: " + resultTestGetLimitColumn.ReturnValue);
                }
                else
                {
                    Console.WriteLine("get limit faild with error: " + resultTestLimitColumn.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");



        }



        /// <summary>
        /// this function try to join a user to board by the email and boardid  and print the program result 
        /// this function tests Requirement 12 
        /// </summary>

        public void testJoinBoard()
        {
            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2) (bobobo=1) 
            //niryaron678@gmail.com logout
            userSer.login("niryaron678@gmail.com", "Nir1234");
            ResponseT<string>? resultJoinBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.joinBoard("niryaron678@gmail.com", 1));
            if (resultJoinBoard != null)
            {
                if (!resultJoinBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultJoinBoard.ReturnValue);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultJoinBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");



            //string resultTest1 = boardService.removeBoard("adirelad@gmail.com", "b");
            resultJoinBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.joinBoard("niryaron678@gmail.com", 1));
            if (resultJoinBoard != null)
            {
                if (!resultJoinBoard.ErrorOccured)
                {
                    Console.WriteLine("failed, this user already a member in the board");
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultJoinBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            resultJoinBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.joinBoard("niryaron678@gmail.com", 2));
            if (resultJoinBoard != null)
            {
                if (!resultJoinBoard.ErrorOccured)
                {
                    Console.WriteLine("should be failed, this board id not exists");
                }
                else
                {
                    Console.WriteLine("OK :addition faild with error: " + resultJoinBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultJoinBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.joinBoard("niryaron@gmail.com", 1));
            if (resultJoinBoard != null)
            {
                if (!resultJoinBoard.ErrorOccured)
                {
                    Console.WriteLine("should be failed, this email not exists");
                }
                else
                {
                    Console.WriteLine("OK :addition faild with error: " + resultJoinBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            userSer.register("zivcohen@gmail.com", "Nir1234");
            userSer.logout("zivcohen@gmail.com");


            resultJoinBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.joinBoard("zivcohen@gmail.com", 1));
            if (resultJoinBoard != null)
            {
                if (!resultJoinBoard.ErrorOccured)
                {
                    Console.WriteLine("should be failed, this email is logout");
                }
                else
                {
                    Console.WriteLine("OK :addition faild with error: " + resultJoinBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");








        }
        /// <summary>
        /// this function try to leave a user to board by the email and boardid  and print the program result 
        /// this function tests Requirement 12 
        /// </summary>
        public void testLeaveBoard()
        {


            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2) (bobobo=1 niryaron678 is member) 
            //niryaron678@gmail.com login
            //zivcohen@gmail.com logout


            ResponseT<string>? resultLeaveBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.leaveBoard("niryaron678@gmail.com", 1));
            if (resultLeaveBoard != null)
            {
                if (!resultLeaveBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultLeaveBoard.ReturnValue);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultLeaveBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");



            //string resultTest1 = boardService.removeBoard("adirelad@gmail.com", "b");
            resultLeaveBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.leaveBoard("niryaron678@gmail.com", 1));
            if (resultLeaveBoard != null)
            {
                if (!resultLeaveBoard.ErrorOccured)
                {
                    Console.WriteLine("failed, this user is not a member in this board");
                }
                else
                {
                    Console.WriteLine("OK : addition faild with error: " + resultLeaveBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultLeaveBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.leaveBoard("niryaron678@gmail.com", 100));
            if (resultLeaveBoard != null)
            {
                if (!resultLeaveBoard.ErrorOccured)
                {
                    Console.WriteLine("should be failed, this boardid dont exists");
                }
                else
                {
                    Console.WriteLine("OK : addition faild with error: " + resultLeaveBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultLeaveBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.leaveBoard("adirelad@gmail.com", 1));
            if (resultLeaveBoard != null)
            {
                if (!resultLeaveBoard.ErrorOccured)
                {
                    Console.WriteLine("should be failed, this user is the owner");
                }
                else
                {
                    Console.WriteLine("OK : addition faild with error: " + resultLeaveBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            boardService.joinBoard("niryaron678@gmail.com", 1);


        }




        /// <summary>
        /// in this function , user try to  transferownership of board for another user and print the result of the program  
        /// this function tests Requirement 13 
        /// </summary>


        public void testTransfterOwnership()
        {
            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2) (bobobo=1 niryaron678 member) 
            //niryaron678@gmail.com login
            //zivcohen@gmail.com logout


            ResponseT<string> resultTransfer = JsonSerializer.Deserialize<ResponseT<string>>(boardService.TransferOwnership("adirelad@gmail.com", "bobobo", "niryaron678@gmail.com"));
            if (resultTransfer != null)
            {
                if (!resultTransfer.ErrorOccured)
                {
                    Console.WriteLine("Ok: return ReturnValue" + resultTransfer.ReturnValue);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultTransfer.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            userSer.login("zivcohen@gmail.com", "Nir1234");



            boardService.joinBoard("zivcohen@gmail.com", 1);

            userSer.register("yonatan@gmail.com", "Nir1234");


            resultTransfer = JsonSerializer.Deserialize<ResponseT<string>>(boardService.TransferOwnership("adirelad@gmail.com", "bobobo", "zivcohen@gmail.com"));
            if (resultTransfer != null)
            {
                if (!resultTransfer.ErrorOccured)
                {
                    Console.WriteLine("should be failed,this user is not a ownership");
                }
                else
                {
                    Console.WriteLine("OK: addition faild with error: " + resultTransfer.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");



            resultTransfer = JsonSerializer.Deserialize<ResponseT<string>>(boardService.TransferOwnership("niryaron678@gmail.com", "bobobo", "yonatan@gmail.com"));
            if (resultTransfer != null)
            {
                if (!resultTransfer.ErrorOccured)
                {
                    Console.WriteLine("should be failed,this email is not member");
                }
                else
                {
                    Console.WriteLine("OK: addition faild with error: " + resultTransfer.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultTransfer = JsonSerializer.Deserialize<ResponseT<string>>(boardService.TransferOwnership("niryaron678@gmail.com", "bobobo", "yoet@gmail.com"));
            if (resultTransfer != null)
            {
                if (!resultTransfer.ErrorOccured)
                {
                    Console.WriteLine("should be failed,this email is not exist");
                }
                else
                {
                    Console.WriteLine("OK: addition faild with error: " + resultTransfer.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultTransfer = JsonSerializer.Deserialize<ResponseT<string>>(boardService.TransferOwnership("niryaron678@gmail.com", "bas", "yoet@gmail.com"));
            if (resultTransfer != null)
            {
                if (!resultTransfer.ErrorOccured)
                {
                    Console.WriteLine("should be failed,this board not exist");
                }
                else
                {
                    Console.WriteLine("OK: addition faild with error: " + resultTransfer.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");







        }


        /// <summary>
        ///this function try to remove board from some user and get
        /// program result and print it . 
        /// This function tests Requirement 9
        /// </summary>
        public void testRemoveBoard()
        {
            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2) 
            //niryaron678@gmail.com login (bobobo=1 adirelad,zivco member) 
            //zivcohen@gmail.com login
            //yonatan@gmail.com login
            userSer.register("niryaron@gmail.com", "nir123");
            userSer.login("niryaron@gmail.com", "nir123");
            ResponseT<string>? resultRemoveBoard1 = JsonSerializer.Deserialize<ResponseT<string>>(boardService.removeBoard("adirelad@gmail.com", "bobobo"));
            if (resultRemoveBoard1 != null)
            {
                if (!resultRemoveBoard1.ErrorOccured)
                {
                    Console.WriteLine("should be failed, this user is not the owner so he cant removeBoard");
                }
                else
                {
                    Console.WriteLine("OK :addition faild with error: " + resultRemoveBoard1.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            userSer.logout("niryaron678@gmail.com");
            ResponseT<string>? resultRemoveBoard2 = JsonSerializer.Deserialize<ResponseT<string>>(boardService.removeBoard("niryaron678@gmail.com", "bobobo"));
            if (resultRemoveBoard2 != null)
            {
                if (!resultRemoveBoard2.ErrorOccured)
                {
                    Console.WriteLine("should be failed,this user isnt log in");
                }
                else
                {
                    Console.WriteLine("OK : addition faild with error: " + resultRemoveBoard2.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            userSer.login("niryaron678@gmail.com", "Nir1234");

            ResponseT<string>? resultRemoveBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.removeBoard("niryaron678@gmail.com", "bobobo"));
            if (resultRemoveBoard != null)
            {
                if (!resultRemoveBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultRemoveBoard.ReturnValue);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultRemoveBoard.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTest1 = boardService.removeBoard("adirelad@gmail.com", "b");
            resultRemoveBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.removeBoard("niryaron678@gmail.com", "bobobo"));
            if (resultRemoveBoard != null)
            {
                if (resultRemoveBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultRemoveBoard.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("board does not exists because it was allready removed, should have faild");
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTest2 = boardService.removeBoard("adirelad@gmail", "b");
            resultRemoveBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.removeBoard("adirelad@gmail", "b"));
            if (resultRemoveBoard != null)
            {
                if (resultRemoveBoard.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultRemoveBoard.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("email does not exists should have faild");
                }
            }
            else
                Console.WriteLine("json problem");
        }




        /// <summary>
        /// in this function user try to get all of his boards(by id ) and print the program result. 
        /// </summary>

        public void testGetUserBoards()
        {

            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2) 
            //niryaron678@gmail.com login 
            //zivcohen@gmail.com login
            //yonatan@gmail.com login



            ResponseT<string>? resultTestGetUserBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.getUserBoards("adirelad@gmail.com"));

            if (resultTestGetUserBoard != null)
            {


                if (!resultTestGetUserBoard.ErrorOccured)
                {

                    if (resultTestGetUserBoard.ReturnValue.Length == 1)
                    {
                        Console.WriteLine("Ok: return ReturnValue" + resultTestGetUserBoard.ReturnValue);
                    }
                    Console.WriteLine("not the result expected");
                }
                else
                    Console.WriteLine(resultTestGetUserBoard.ErrorMessage);
            }
            else
                Console.WriteLine("json problem");

            resultTestGetUserBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.getUserBoards("niryaron678@gmail.com"));

            if (resultTestGetUserBoard != null)
            {


                if (!resultTestGetUserBoard.ErrorOccured)
                {

                    if (resultTestGetUserBoard.ReturnValue.Length == 0)
                    {
                        Console.WriteLine("Ok: return ReturnValue" + resultTestGetUserBoard.ReturnValue);
                    }
                    Console.WriteLine("not the result expected");
                }
                else
                    Console.WriteLine(resultTestGetUserBoard.ErrorMessage);
            }
            else
                Console.WriteLine("json problem");




            resultTestGetUserBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.getUserBoards("niryaron678@gmacom"));

            if (resultTestGetUserBoard != null)
            {


                if (!resultTestGetUserBoard.ErrorOccured)
                {

                    Console.WriteLine("should be failed email not good");
                }
                else
                    Console.WriteLine("OK: addition faild with error: " + resultTestGetUserBoard.ErrorMessage);
            }
            else
                Console.WriteLine("json problem");

            userSer.logout("yonatan@gmail.com");
            resultTestGetUserBoard = JsonSerializer.Deserialize<ResponseT<string>>(boardService.getUserBoards("yonatan@gmail.com"));

            if (resultTestGetUserBoard != null)
            {


                if (!resultTestGetUserBoard.ErrorOccured)
                {

                    Console.WriteLine("should be failed user not login");
                }
                else
                    Console.WriteLine("OK: addition faild with error: " + resultTestGetUserBoard.ErrorMessage);
            }
            else
                Console.WriteLine("json problem");



        }




        /// <summary>
        ///this function try to add task to specific board, get
        /// program result and print it . 
        /// This function tests Requirement 12
        /// </summary>
        public void testAddTask()
        {
            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2 have 2 TASKS without assigne) 
            //niryaron678@gmail.com login 
            //zivcohen@gmail.com login
            //yonatan@gmail.com login

            DateTime d = DateTime.Parse("22/2/2023");
            DateTime e = DateTime.Parse("22/2/2002");

            ResponseT<string>? resultTestAddTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.addTask("adirelad@gmail.com", "boardboard", "work 1", " build search engine in binary", d));
            if (resultTestAddTask != null)
            {
                if (!resultTestAddTask.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultTestAddTask.ReturnValue);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultTestAddTask.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTest1 = boardService.addTask("adirelad@gmail.", "b", "work 1", " build search engine in binary", d);
            resultTestAddTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.addTask("niryaron678@gmail.com", "boardboard", "work 1", " build search engine in binary", d));
            if (resultTestAddTask != null)
            {
                if (resultTestAddTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAddTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("should be failed this user is not member");
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTest2 = boardService.addTask("adirelad@gmail.com", "c", "work 1", " build search engine in binary", d);
            resultTestAddTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.addTask("adirelad@gmail.com", "c", "work 1", " build search engine in binary", d));
            if (resultTestAddTask != null)
            {
                if (resultTestAddTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAddTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("board does not exists should have faild");
                }
            }
            else
                Console.WriteLine("json problem");



            //string resultTest3 = boardService.addTask("adirelad@gmail.com", "b", "work 1", " build search engine in binary", e);
            resultTestAddTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.addTask("adirelad@gmail.com", "boardboard", "work 1", " build search engine in binary", e));
            if (resultTestAddTask != null)
            {
                if (resultTestAddTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAddTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("date cannot be in the past should have faild");
                }
            }
            else
                Console.WriteLine("json problem");

            userSer.logout("adirelad@gmail.com");
            resultTestAddTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.addTask("adirelad@gmail.com", "boardboard", "work 1", " build search engine in binary", d));
            if (resultTestAddTask != null)
            {
                if (resultTestAddTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAddTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("should be failed,this user logout");
                }
            }
            else
                Console.WriteLine("json problem");

            userSer.login("adirelad@gmail.com", "Nir1234");

            resultTestAddTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.addTask("adirelad@gmail.com", "boardboard", "", " build search engine in binary", d));
            if (resultTestAddTask != null)
            {
                if (resultTestAddTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAddTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("title is empty,should be failed");
                }
            }
            else
                Console.WriteLine("json problem");

            taskService.addTask("adirelad@gmail.com", "boardboard", "ttt", "ddd", d);

            resultTestAddTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.addTask("adirelad@gmail.com", "boardboard", "scsdc", " build search engine in binary", d));
            if (resultTestAddTask != null)
            {
                if (resultTestAddTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAddTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("should be failed, arrived to task limit ");
                }
            }
            else
                Console.WriteLine("json problem");

        }



        // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2 have 2 TASKS without assigne) 
        //niryaron678@gmail.com login 
        //zivcohen@gmail.com login
        //yonatan@gmail.com login


        /// <summary>
        /// in this function an user (by his email) try to assign task(by boardname,columnordinal and task id ) to another user(by his email) and print program result
        /// this function tests Requirement 23
        /// </summary>

        public void testAssignTask()
        {
            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2 have 2 TASKS without assigne) 
            //niryaron678@gmail.com login 
            //zivcohen@gmail.com login
            //yonatan@gmail.com login



            ResponseT<string> resultAssignTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.assignTask("adirelad@gmail.com", "boardboard", 0, 0, "niryaron678@gmail.com"));
            if (resultAssignTask != null)
            {
                if (!resultAssignTask.ErrorOccured)
                {
                    Console.WriteLine("failed, this user is not a member in this board");
                }
                else
                {
                    Console.WriteLine("OK : addition faild with error: " + resultAssignTask.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            boardService.joinBoard("niryaron678@gmail.com", 0);



            resultAssignTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.assignTask("adirelad@gmail.com", "boardboard", 0, 0, "niryaron678@gmail.com"));
            if (resultAssignTask != null)
            {
                if (!resultAssignTask.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultAssignTask.ReturnValue);
                }
                else
                {
                    Console.WriteLine(" addition faild with error: " + resultAssignTask.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            resultAssignTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.assignTask("adirelad@gmail.com", "boardboard", 0, 0, "niryaron678@gmail.com"));
            if (resultAssignTask != null)
            {
                if (!resultAssignTask.ErrorOccured)
                {
                    Console.WriteLine("should be failed,this user is not the task assigne");
                }
                else
                {
                    Console.WriteLine("OK:  addition faild with error: " + resultAssignTask.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            resultAssignTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.assignTask("niryaron678@gmail.com", "boardboard", 0, 0, "adirelad@gmail.com"));
            if (resultAssignTask != null)
            {
                if (!resultAssignTask.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultAssignTask.ReturnValue);
                }
                else
                {
                    Console.WriteLine(" addition faild with error: " + resultAssignTask.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultAssignTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.assignTask("niryaron678@gmail.com", "c", 0, 0, "adirelad@gmail.com"));
            if (resultAssignTask != null)
            {
                if (!resultAssignTask.ErrorOccured)
                {
                    Console.WriteLine("should be failed this board no exist");
                }
                else
                {
                    Console.WriteLine(" OK:addition faild with error: " + resultAssignTask.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");







        }


        /// <summary>
        ///this function try to advance task from some board  and get
        /// program result and print it . 
        /// This function tests Requirement 13
        /// </summary>
        public void testAdvancedTask()
        {

            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2 have 2 TASKS without assigne,nir member and adirelad taskassigne of 0(column2) 
            //niryaron678@gmail.com login 
            //zivcohen@gmail.com login
            //yonatan@gmail.com login


            ResponseT<string>? resultTestAdvancedTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.advancedTask("adirelad@gmail.com", "boardboard", 0, 0));
            if (resultTestAdvancedTask != null)
            {
                if (!resultTestAdvancedTask.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultTestAdvancedTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultTestAdvancedTask.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultTestAdvancedTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.advancedTask("niryaron678@gmail.com", "boardboard", 1, 0));
            if (resultTestAdvancedTask != null)
            {
                if (resultTestAdvancedTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAdvancedTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("should be failed its not the task assinge ");
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTest1 = boardService.advancedTask("adirelad@gmail.com", "b", 1, 0);
            resultTestAdvancedTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.advancedTask("adirelad@gmail.com", "boardboard", 1, 0));
            if (resultTestAdvancedTask != null)
            {
                if (!resultTestAdvancedTask.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultTestAdvancedTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultTestAdvancedTask.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTest2 = boardService.advancedTask("adirelad@gmail.com", "b", 0, 0);
            resultTestAdvancedTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.advancedTask("adirelad@gmail.com", "boardboard", 0, 0));
            if (resultTestAdvancedTask != null)
            {
                if (resultTestAdvancedTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAdvancedTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("the task is not in that column should have faild");
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTest3 = boardService.advancedTask("adirelad@gmail.com", "c", 0, 0);
            resultTestAdvancedTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.advancedTask("adirelad@gmail.com", "c", 0, 0));
            if (resultTestAdvancedTask != null)
            {
                if (resultTestAdvancedTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAdvancedTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("board does not exists should have faild");
                }
            }
            else
                Console.WriteLine("json problem");


            resultTestAdvancedTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.advancedTask("adirelad@gmail.com", "boardboard", 2, 0));
            if (resultTestAdvancedTask != null)
            {
                if (resultTestAdvancedTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAdvancedTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("the task is in done column cant advanced");
                }
            }
            else
                Console.WriteLine("json problem");

            resultTestAdvancedTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.advancedTask("adirelad@gmail.com", "boardboard", 0, 4));
            if (resultTestAdvancedTask != null)
            {
                if (resultTestAdvancedTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTestAdvancedTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("there is no task with this id ");
                }
            }
            else
                Console.WriteLine("json problem");









        }
        /// <summary>
        ///this function try to update the description of task and get
        /// program result and print it . 
        /// This function tests Requirement 14,15
        /// </summary>
        public void testUpdateTaskDescription()
        {


            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2 have 2 TASKS without assigne
            // ,nir member and taskassigneof1 and adirelad taskassigne of 0(column2) 
            //niryaron678@gmail.com login 
            //zivcohen@gmail.com login
            //yonatan@gmail.com login
            taskService.assignTask("adirelad@gmail.com", "boardboard", 0, 1, "niryaron678@gmail.com");
            ResponseT<string>? resultTesttUpdateTaskDescription = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskDescription("niryaron678@gmail.com", "boardboard", 0, 1, "new des"));
            if (resultTesttUpdateTaskDescription != null)
            {
                if (!resultTesttUpdateTaskDescription.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultTesttUpdateTaskDescription.ReturnValue);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultTesttUpdateTaskDescription.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTes2 = boardService.updateTaskDescription("adirelad@gmail", "b", 1, 0, "new des");
            resultTesttUpdateTaskDescription = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskDescription("adirelad@gmail.com", "boardboard", 0, 1, "new des"));
            if (resultTesttUpdateTaskDescription != null)
            {
                if (resultTesttUpdateTaskDescription.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTesttUpdateTaskDescription.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("should be failed,this user is not the assigne of task");
                }
            }
            else
                Console.WriteLine("json problem");

            resultTesttUpdateTaskDescription = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskDescription("adirelad@gmail.com", "boardboard", 2, 0, "new des"));
            if (resultTesttUpdateTaskDescription != null)
            {
                if (resultTesttUpdateTaskDescription.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultTesttUpdateTaskDescription.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("should be failed,this task is done");
                }
            }
            else
                Console.WriteLine("json problem");





        }
        /// <summary>
        ///this function try to update the title of task and get
        /// program result and print it . 
        /// This function tests Requirement 14,15
        /// </summary>
        public void testUpdateTaskTitle()
        {
            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2 have 2 TASKS without assigne
            // ,nir member and taskassigneof1 and adirelad taskassigne of 0(column2) 
            //niryaron678@gmail.com login 
            //zivcohen@gmail.com login
            //yonatan@gmail.com login
            //string resultTest = boardService.updateTaskTitle("adirelad@gmail.com", "b", 1, 0, "new title");
            ResponseT<string>? resultUpdateTaskTitle = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskTitle("niryaron678@gmail.com", "boardboard", 0, 1, "new title"));
            if (resultUpdateTaskTitle != null)
            {
                if (!resultUpdateTaskTitle.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is:" + resultUpdateTaskTitle.ReturnValue);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultUpdateTaskTitle.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTes2 = boardService.updateTaskTitle("adirelad@gmail", "b", 1, 0, "new title");
            resultUpdateTaskTitle = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskTitle("adirelad@gmail", "boardboard", 0, 1, "new title"));
            if (resultUpdateTaskTitle != null)
            {
                if (resultUpdateTaskTitle.ErrorOccured)
                {
                    Console.WriteLine("OK, good error:" + resultUpdateTaskTitle.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("email does not exists should have faild");
                }
            }
            else
                Console.WriteLine("json problem");

            resultUpdateTaskTitle = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskTitle("niryaron678@gmail.com", "boardboard", 0, 1, "nedsvdssdvsdvdsvsdvsdvcsdvscvsvdvsdvdsvdsvsvsvsdvdsvsdvdsvdsvsdvsdvsdvdsvsdvsddsw title"));
            if (resultUpdateTaskTitle != null)
            {
                if (resultUpdateTaskTitle.ErrorOccured)
                {
                    Console.WriteLine("OK, good error:" + resultUpdateTaskTitle.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("should be failed,title is too high");
                }
            }
            else
                Console.WriteLine("json problem");

            resultUpdateTaskTitle = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskTitle("adirelad@gmail.com", "boardboard", 0, 1, "casca title"));
            if (resultUpdateTaskTitle != null)
            {
                if (resultUpdateTaskTitle.ErrorOccured)
                {
                    Console.WriteLine("OK, good error:" + resultUpdateTaskTitle.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("should be failed,not the task assigne");
                }
            }
            else
                Console.WriteLine("json problem");

            resultUpdateTaskTitle = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskTitle("adirelad@gmail", "boardboard", 2, 0, "casca title"));
            if (resultUpdateTaskTitle != null)
            {
                if (resultUpdateTaskTitle.ErrorOccured)
                {
                    Console.WriteLine("OK, good error:" + resultUpdateTaskTitle.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("should be failed,task is done");
                }
            }
            else
                Console.WriteLine("json problem");

        }
        /// <summary>
        ///this function try to update the due time of task and get
        /// program result and print it . 
        /// This function tests Requirement 14,15
        /// </summary>
        public void testUpdateTaskDueDate()
        {
            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2 have 2 TASKS without assigne
            // ,nir member and taskassigneof1 and adirelad taskassigne of 0(column2) 
            //niryaron678@gmail.com login 
            //zivcohen@gmail.com login
            //yonatan@gmail.com login
            DateTime s = DateTime.Parse("22/8/2030");
            ResponseT<string>? resultUpdateTaskDueDate = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskDueDate("niryaron678@gmail.com", "boardboard", 0, 1, s));
            if (resultUpdateTaskDueDate != null)
            {
                if (!resultUpdateTaskDueDate.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue: " + resultUpdateTaskDueDate.ReturnValue);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultUpdateTaskDueDate.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            DateTime e = DateTime.Parse("22/2/2002");
            //string resultTes2 = boardService.updateTaskDueDate("adirelad@gmail", "b", 1, 0,e);
            resultUpdateTaskDueDate = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskDueDate("niryaron678@gmail", "boardboard", 0, 1, e));
            if (resultUpdateTaskDueDate != null)
            {
                if (resultUpdateTaskDueDate.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultUpdateTaskDueDate.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("email does not exists should have faild");
                }
            }
            else
                Console.WriteLine("json problem");

            resultUpdateTaskDueDate = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskDueDate("niryaron678@gmail.com", "boardboard", 0, 1, e));
            if (resultUpdateTaskDueDate != null)
            {
                if (resultUpdateTaskDueDate.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultUpdateTaskDueDate.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("cant update to dueDate that past");
                }
            }
            else
                Console.WriteLine("json problem");


            resultUpdateTaskDueDate = JsonSerializer.Deserialize<ResponseT<string>>(taskService.updateTaskDueDate("niryaron678@gmail.com", "boardboard", 2, 0, s));
            if (resultUpdateTaskDueDate != null)
            {
                if (resultUpdateTaskDueDate.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultUpdateTaskDueDate.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("cant update ,not the task assigne");
                }
            }
            else
                Console.WriteLine("json problem");





        }

        /// <summary>
        ///this function try to remove task from some board and get
        /// program result and print it . 
        /// </summary>
        public void testRemoveTask()//no need in this stage of the project.
        {

            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2 have 2 TASKS without assigne
            // ,nir member and taskassigneof1 and adirelad taskassigne of 0(column2) 
            //niryaron678@gmail.com login 
            //zivcohen@gmail.com login
            //yonatan@gmail.com login
            ResponseT<string>? resultRemoveTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.removeTask("adirelad@gmail.com", "boardboard", 0, 1));
            if (resultRemoveTask != null)
            {
                if (!resultRemoveTask.ErrorOccured)
                {
                    Console.WriteLine("should be failed , not the task assigne");
                }
                else
                {
                    Console.WriteLine("OK :addition faild with error: " + resultRemoveTask.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultRemoveTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.removeTask("niryaron678@gmail.com", "boardboard", 0, 1));
            if (resultRemoveTask != null)
            {
                if (!resultRemoveTask.ErrorOccured)
                {
                    Console.WriteLine("OK, return ReturnValue is: " + resultRemoveTask.ReturnValue);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultRemoveTask.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultRemoveTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.removeTask("niryaron678@gmail.com", "boardboard", 0, 1));
            if (resultRemoveTask != null)
            {
                if (resultRemoveTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultRemoveTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("this task is not exists anymore");
                }
            }
            else
                Console.WriteLine("json problem");


            //string resultTest1 = boardService.removeTask("adirelad@gmail.com", "b", 1, 0);
            resultRemoveTask = JsonSerializer.Deserialize<ResponseT<string>>(taskService.removeTask("adirelad@gmail.com", "boardboard", 2, 0));
            if (resultRemoveTask != null)
            {
                if (resultRemoveTask.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultRemoveTask.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("this task is already done");
                }
            }
            else
                Console.WriteLine("json problem");
        }

        /*/// <summary>
        ///this function try to get all inProgress task from some board of username and get
        /// program result and print it . 
        /// This function tests Requirement 16
        /// </summary>
        public void testAllInProgressOld()
        {
            DateTime d = DateTime.Now;
            taskService.advancedTask("adirelad@gmail.com", "b", 0, 0);
            taskService.advancedTask("adirelad@gmail.com", "b", 0, 1);
            taskService.advancedTask("adirelad@gmail.com", "b", 0, 2);
            string tasks = taskService.getInProgress("adirelad@gmail.com");
            Console.WriteLine(tasks);
            ResponseT<List<Task>>? resultAllInProgress = JsonSerializer.Deserialize<ResponseT<List<Task>>>(taskService.getInProgress("adirelad@gmail.com"));
            if (resultAllInProgress != null)
            {
                if (!resultAllInProgress.ErrorOccured)
                {
                    Console.WriteLine("OK return ReturnValue is: " + resultAllInProgress.ReturnValue);
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultAllInProgress.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            taskService.removeTask("adirelad@gmail.com", "b", 1, 1);
            taskService.removeTask("adirelad@gmail.com", "b", 1, 2);
            tasks = taskService.getInProgress("adirelad@gmail.com");
            Console.WriteLine(tasks);
            resultAllInProgress = JsonSerializer.Deserialize<ResponseT<List<Task>>>(taskService.getInProgress("adirelad@gmail.com"));
            if (resultAllInProgress != null)
            {
                if (!resultAllInProgress.ErrorOccured)
                {
                    Console.WriteLine("OK return ReturnValue is: " + resultAllInProgress.ReturnValue);
                }
                else
                {
                    Console.WriteLine("faild, there is no task to show");
                }
            }
            else
                Console.WriteLine("json problem");



            //string resultTest2 = boardService.getInProgress("adirelad@gmail");
            resultAllInProgress = JsonSerializer.Deserialize<ResponseT<List<Task>>>(taskService.getInProgress("adirelad@gmail"));
            if (resultAllInProgress != null)
            {
                if (resultAllInProgress.ErrorOccured)
                {
                    Console.WriteLine("OK, good error: " + resultAllInProgress.ErrorMessage);
                }
                else
                {
                    Console.WriteLine("There is no such name, should have faild");
                }
            }
            else
                Console.WriteLine("json problem");


        }
*/


        /// <summary>
        /// this function try to get all in progress tasks list of a user(that he is assigned to them) and print the program result.
        /// this function tests Requirement 22
        /// </summary>
        public void testAllInProgress() // new 
        {

            // adirelad@gmail.com login with Board boardboard (id=0,column 0 limit to 2 have 1 TASKS without assigne
            // ,nir member adirelad taskassigne of 0(column2) 
            //niryaron678@gmail.com login 
            //zivcohen@gmail.com login
            //yonatan@gmail.com login

            DateTime d = DateTime.Parse("20/02/2023");
            boardService.addBoard("niryaron678@gmail.com", "board1");
            boardService.addBoard("niryaron678@gmail.com", "board2");
            boardService.joinBoard("adirelad@gmail.com", 2);
            boardService.joinBoard("adirelad@gmail.com", 3);
            //niryaron, adirelad members
            taskService.addTask("adirelad@gmail.com", "board1", "title1", "do 1111", d);
            taskService.addTask("adirelad@gmail.com", "board2", "title2", "do 1111", d);
            taskService.assignTask("niryaron678@gmail.com", "board1", 0, 0, "adirelad@gmail.com");
            taskService.assignTask("niryaron678@gmail.com", "board2", 0, 0, "adirelad@gmail.com");




            string tasks = taskService.getInProgress("adirelad@gmail.com");
            Console.WriteLine(tasks);
            ResponseT<List<Task>>? resultAllInProgress = JsonSerializer.Deserialize<ResponseT<List<Task>>>(taskService.getInProgress("adirelad@gmail.com"));
            if (resultAllInProgress != null)
            {
                if (!resultAllInProgress.ErrorOccured)
                {
                    if (resultAllInProgress.ReturnValue.Count == 2)
                        Console.WriteLine("OK return ReturnValue is: " + resultAllInProgress.ReturnValue);
                    else
                        Console.WriteLine("couldnt get all of inprogress from all boards");
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultAllInProgress.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");


            resultAllInProgress = JsonSerializer.Deserialize<ResponseT<List<Task>>>(taskService.getInProgress("niryaron678@gmail.com"));
            if (resultAllInProgress != null)
            {
                if (!resultAllInProgress.ErrorOccured)
                {
                    if (resultAllInProgress.ReturnValue.Count == 0)
                        Console.WriteLine("OK return ReturnValue is: " + resultAllInProgress.ReturnValue);
                    else
                        Console.WriteLine("couldnt get all of inprogress from all boards");
                }
                else
                {
                    Console.WriteLine("addition faild with error: " + resultAllInProgress.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

            resultAllInProgress = JsonSerializer.Deserialize<ResponseT<List<Task>>>(taskService.getInProgress("niry78@gmail.com"));
            if (resultAllInProgress != null)
            {
                if (!resultAllInProgress.ErrorOccured)
                {
                    Console.WriteLine("should be failed,email not exist ");
                }
                else
                {
                    Console.WriteLine("OK :addition faild with error: " + resultAllInProgress.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");






        }












        /// <summary>
        /// this function try to loaddata from the database and print the program result 
        /// this function tests Requirement 27 
        /// </summary>

        public void testLoadData()
        {
            ResponseT<string> resultLoadData = JsonSerializer.Deserialize<ResponseT<string>>(boardService.loadData());
            if (resultLoadData != null)
            {
                if (!resultLoadData.ErrorOccured)
                {
                    if (resultLoadData.ReturnValue.Equals("{}"))
                        Console.WriteLine("Ok: return ReturnValue" + resultLoadData.ReturnValue);
                    else
                        Console.WriteLine("not good return value");
                }
                else
                {
                    Console.WriteLine("load faild with error: " + resultLoadData.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");

        }


        /// <summary>
        /// this function try to delete all data  from the database and print the program result 
        /// this function tests Requirement 27 
        /// </summary>
        public void testDeleteData()
        {

            ResponseT<string> resultDeleteData = JsonSerializer.Deserialize<ResponseT<string>>(boardService.deleteData());
            if (resultDeleteData != null)
            {
                if (!resultDeleteData.ErrorOccured)
                {

                    if (resultDeleteData.ReturnValue.Equals("{}"))
                        Console.WriteLine("Ok: return ReturnValue" + resultDeleteData.ReturnValue);
                    else
                        Console.WriteLine("not good return value");
                }
                else
                {
                    Console.WriteLine("delete faild with error: " + resultDeleteData.ErrorMessage);
                }
            }
            else
                Console.WriteLine("json problem");



        }







        public void runTests()
        {
            testRegister();
            testLogin();
            testLogout();
            testAddBoard();
            testGetBoard();
            testGetColumn();
            testLimitColumn();


            testJoinBoard();
            testLeaveBoard();
            testTransfterOwnership();
            testRemoveBoard();
            testGetUserBoards();


            testAddTask();
            testAssignTask();
            testAdvancedTask();
            testUpdateTaskDescription();
            testUpdateTaskTitle();
            testUpdateTaskDueDate();
            testRemoveTask();
            testAllInProgress();



            testLoadData();
            testDeleteData();




        }

    }
}