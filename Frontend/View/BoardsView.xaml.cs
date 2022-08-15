using Frontend.Model;
using Frontend.ModelView;
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

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for BoardsView.xaml
    /// </summary>
    public partial class BoardsView : Window
    {
        private BoardsViewModel viewModel;
        private UserModel user;
/*        private List<string> boardsNames;
*/        public BoardsView()
        {
            InitializeComponent();
        }
        public BoardsView(UserModel u)
        {
            
            this.viewModel = new BoardsViewModel(u);
            this.DataContext = viewModel;
            this.user = u;
            InitializeComponent();
        }

       /* private void Button_Click(object sender, RoutedEventArgs e)
        {
            



           
         *//*   BoardModel boardModel = viewModel.getBoard(boardName);
            viewModel.getColumns(boardName);
            Column1.ItemsSource = viewModel.Columns[0].Tasks;
            Column2.ItemsSource = viewModel.Columns[1].Tasks;
            Column3.ItemsSource = viewModel.Columns[2].Tasks;*//*

        }*/

        private void BoardList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string boardName = BoardList.SelectedItem.ToString();
            BoardModel boardModel = viewModel.getBoard(boardName);
            if (boardModel != null)
            {
                BoardView boardView = new BoardView(user, boardModel);
             /*   ColumnBackLog.ItemsSource = boardView.Columns[0].Tasks;
                Column2.ItemsSource = viewModel.Columns[1].Tasks;
                Column3.ItemsSource = viewModel.Columns[2].Tasks;*/
                boardView.Show();
                this.Close();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
