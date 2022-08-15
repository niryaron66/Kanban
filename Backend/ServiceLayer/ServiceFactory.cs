using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class ServiceFactory
    {
        public UserService _userService { get; }
        public BoardService _boardService { get; }
        public TaskService _taskService { get; }
        private UserController userController { get; }
        private BoardController boardController { get; }
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ServiceFactory()
        {
            userController = new UserController();
            boardController = new BoardController(userController);
            _userService = new UserService(userController);
            _boardService = new BoardService(userController, boardController);
            _taskService = new TaskService(boardController);
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("stating log!");
        }
    }
}
