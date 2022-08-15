﻿using Frontend.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
         
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            
            UserModel u = viewModel.Login();
            if (u != null)
            {
                BoardsView boardsView = new BoardsView(u);
                boardsView.Show();
                this.Close();
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            UserModel u = viewModel.Register();
            if (u != null)
            {
                BoardsView boardView = new BoardsView(u);
                boardView.Show();
                this.Close();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

      
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            viewModel.Password = passwordBox.Password;

        }

      
    }
}
