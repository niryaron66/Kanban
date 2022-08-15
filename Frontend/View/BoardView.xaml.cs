using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Frontend.ModelView;

using Frontend.Model;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : Window
    {
        private BoardViewModel viewModel;
        private UserModel user;
        private string board;


        public BoardView(UserModel u,BoardModel b)
        {
            this.viewModel = new BoardViewModel(u,b.Name);
            this.DataContext = viewModel;
            this.user = u;
            InitializeComponent();

        }

      
        private void Board_Changed(object sender, SelectionChangedEventArgs e)
        {
            string boardName = ""; // TODO delete this 
 /*           string boardName = BoardList.SelectedItem.ToString();*/
            board = boardName;
            viewModel.getColumns(boardName);

        }
     
       
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            BoardsView boardsView = new BoardsView(user);
            boardsView.Show();
            this.Close();

        }
    }
}
