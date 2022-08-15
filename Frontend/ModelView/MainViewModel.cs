using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Frontend.Model;
using Frontend.ModelView;


namespace Frontend.ModelView
{
   internal class MainViewModel : INotifyPropertyChanged
    {
        public BackendController Controller { get; private set; }
        private int counter = 0;

        private string _username;
        

        public string Username
        {
            get => _username;
            set
            {
                this._username = value;
                OnPropertyChanged();            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                this._password = value;
                OnPropertyChanged();
            }
        }
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                this._message = value;
                OnPropertyChanged();

            }
        }

        public UserModel Login()
        {
            Message = "";
            try
            {
                return Controller.Login(Username, Password);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }
        public UserModel Register()
        {
            Message = "";
            try
            {
                return Controller.Register(Username, Password);
                Message = "Registered successfully";
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
       }



        public MainViewModel()
        {
            this.Controller = new BackendController();
            this.Username = "";
            this.Password = "";
            if (counter == 0)
            {
                Controller.ServiceFactory._userService.loadData();
                Controller.ServiceFactory._boardService.loadData();
            }
            counter++;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
