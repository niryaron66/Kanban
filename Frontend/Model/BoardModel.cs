using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class BoardModel : INotifyPropertyChanged
    {
        private UserModel user;
        private string name;

        public string Name { get => name; }

        public event PropertyChangedEventHandler? PropertyChanged;

/*        public ObservableCollection<ColumnModel> Columns { get; set; }
*/

        public BoardModel( UserModel user, string name)
        {
            this.user = user;
            this.name = name;
        }
        public BackendController Control { get; }
        public UserModel UserModel { get; }
         private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return name;
        }
    }
}
