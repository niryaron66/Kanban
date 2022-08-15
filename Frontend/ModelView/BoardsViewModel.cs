using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ModelView
{
    internal class BoardsViewModel : NotifiableObject
    {
        BackendController Control;
        public UserModel User { get => user; set => user = value; }
        private ObservableCollection<BoardModel> boards;
        private UserModel user;

        public ObservableCollection<BoardModel> Boards
        {
            get => boards;
            set
            {
                boards = value;
                /*                RaisePropertyChanged("Boards");
                */
            }
        }




        // i succced to login, need to show all boards
        public BoardsViewModel(UserModel user)
        {
            this.User = user;
            this.Control = user.Controller;
            boards = new ObservableCollection<BoardModel>();
            foreach (string name in Control.GetAllBoardNames(user.Email))
            {
                boards.Add(new BoardModel(user, name));
            }

        }

        public BoardModel getBoard(string boardName)
        {

            try
            {
                return Control.getBoard(User, boardName);
            }


            catch (Exception e)
            {

                return null;
            }

        }
        /*  public void getBoards()*/


    }
}
