using System;
using System.Collections.Generic;
using System.Windows;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Frontend.Model
{
    public class BackendController
    {
        private UserService Service { get; set; }
        private BoardService BoardService { get; set; }
        private ServiceFactory serviceFactory;
        public ServiceFactory ServiceFactory { get => serviceFactory; set => serviceFactory = value; }
        public BackendController(UserService service)
        {
            this.Service = service;
            serviceFactory = new ServiceFactory();
        }

        public BackendController()
        {
            serviceFactory = new ServiceFactory();
            this.Service = serviceFactory._userService;
            this.BoardService = serviceFactory._boardService;
            Service.loadData();
            BoardService.loadData();    
        }


        public UserModel Login(string username, string password)
        {
            string res = Service.login(username, password);
            ResponseT<string>? responseRes = JsonSerializer.Deserialize<ResponseT<string>>(res);
            if (responseRes.ErrorOccured)
            {
                throw new Exception(responseRes.ErrorMessage);
            }
            return new UserModel(this, username);
        }

        internal List<string> GetAllBoardNames(string email)
        {
            string res = serviceFactory._boardService.get3BoardsName(email);
            if(res == null)
            {
                return new List<string>();
            }
            ResponseT<List<string>>? responseRes = JsonSerializer.Deserialize<ResponseT<List<string>>>(res);
            if (responseRes.ErrorOccured)
            {
                throw new Exception(responseRes.ErrorMessage);
            }
            return responseRes.ReturnValue;
        }

        internal UserModel Register(string username, string password)
        {
            string res = Service.register(username, password);
            ResponseT<string>? responseRes = JsonSerializer.Deserialize<ResponseT<string>>(res);
            if(responseRes != null)
            {
                if (responseRes.ErrorOccured)
                {
                    throw new Exception(responseRes.ErrorMessage);
                }
            }
            return new UserModel(this, username);
        }
        public BoardModel getBoard(UserModel user,string boardName)
        {
          /*  ResponseT<List<STask>> res = new ResponseT<List<STask>>(tasks.Select(task => new STask(task)).ToList(), null);
            ResponseT<FBoard> res= new ResponseT<FBoard>(serviceFactory._boardService.getBoard(user.Email,boardName) => new FBoard())*/
/*            ResponseT<FBoard>? responseRes = JsonSerializer.Deserialize<ResponseT<FBoard>>(serviceFactory._boardService.getBoard(user.Email, boardName));
*/
            string res = serviceFactory._boardService.getBoard(user.Email,boardName);
            ResponseT<FBoard>? responseRes = JsonSerializer.Deserialize<ResponseT<FBoard>>(res);
            if (responseRes != null)
            { 
           /* string res = serviceFactory._boardService.getBoard(user.Email, boardName);
            ResponseT<string>? responseRes = JsonSerializer.Deserialize<ResponseT<string>>(res);*/
            if (responseRes.ErrorOccured)
            {
                throw new Exception(responseRes.ErrorMessage);
            }
            }
            return new BoardModel(user, boardName);
        }
        public List<string> boardsNames(string username)
        {
            string res = serviceFactory._boardService.get3BoardsName(username);
            ResponseT<List<string>>? responseRes = JsonSerializer.Deserialize<ResponseT<List<string>>>(res);
            if (responseRes.ErrorOccured)
            {
                throw new Exception(responseRes.ErrorMessage);
            }
            return responseRes.ReturnValue;
        }
        internal List<FTask> getColumns(string email, string boardName, int columnOrdinal)
        {
            string res =  serviceFactory._boardService.getColumn(email, boardName, columnOrdinal);
            ResponseT<List<FTask>>? responseRes = JsonSerializer.Deserialize<ResponseT<List<FTask>>>(res);
            if (responseRes.ErrorOccured)
            {
                throw new Exception(responseRes.ErrorMessage);
            }
            if(responseRes.ReturnValue != null) 
                 return responseRes.ReturnValue;
            return null;
           
        }


    }
}
