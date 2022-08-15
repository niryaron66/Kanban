using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Frontend.Model;
using Frontend.ModelView;

namespace Frontend.ModelView
{
    internal class BoardViewModel : NotifiableObject
    {
        private BackendController Control;
        private UserModel User;
  
        public string Title { get; set; }

        /*private ObservableCollection<ColumnModel> columns;*/
        private ObservableCollection<TaskModel> backlogColumn;
        private ObservableCollection<TaskModel> inProgressColumn;
        private ObservableCollection<TaskModel> doneColumn;
        private string boardName;
        public string BoardName { get=>boardName; set => boardName = value; }   

        public ObservableCollection<TaskModel> BacklogColumn
        {
            get => backlogColumn; set
            {
                backlogColumn = value;
            }
        }
        public ObservableCollection<TaskModel> InProgressColumn
        {
            get => inProgressColumn; set
            {
                inProgressColumn = value;
            }
        }

        public ObservableCollection<TaskModel> DoneColumn
        {
            get => doneColumn; set
            {
                doneColumn = value;
            }
        }


        /*public ObservableCollection<ColumnModel> Columns { get => columns;
            set
            {
                columns = value;
          *//*      RaisePropertyChanged("Column1");
                RaisePropertyChanged("Column2");
                RaisePropertyChanged("Column3");*//*
            } }*/

        public BoardViewModel(UserModel user,string boardName)
        {
            this.User = user;
            this.Control = user.Controller;
            BoardName = boardName;
            getColumns(boardName);
        }
        public void getColumns(string boardName)
        {
/*            columns = new ObservableCollection<ColumnModel>();
*/            List<FTask> response1 = Control.getColumns(User.Email, boardName, 0);
            List<FTask> response2 = Control.getColumns(User.Email, boardName, 1);
            List<FTask> response3 = Control.getColumns(User.Email, boardName, 2);
            BacklogColumn= new ObservableCollection<TaskModel>();
            InProgressColumn= new ObservableCollection<TaskModel>();    
            DoneColumn= new ObservableCollection<TaskModel>();
            ColumnModel c1= new ColumnModel("Backlog", response1);
            foreach (TaskModel t in c1.Tasks)
                BacklogColumn.Add(t);
            /*            BacklogColumn = new ColumnModel("Backlog", response1);
            */
            ColumnModel c2 = new ColumnModel("InProgress", response2);
            foreach (TaskModel t in c2.Tasks)
                InProgressColumn.Add(t);

            ColumnModel c3 = new ColumnModel("Done", response3);
            foreach (TaskModel t in c3.Tasks)
                DoneColumn.Add(t);

            /*    Columns.Add(c1);
                Columns.Add(c2);
                Columns.Add(c3);*/


        }
    }
}

