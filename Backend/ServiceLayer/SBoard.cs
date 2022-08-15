using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class SBoard
    {
        private string boardName;
        private string creationEmail;
        private Column[] columns;

        public SBoard( string name, string email)
        {
            boardName = name;
            creationEmail = email;
            columns = new Column[3];
        }

        public SBoard(Board b)
        {
            if(b == null)
                throw new Exception("user does not exist");
            boardName = b.Name;
            creationEmail = b.CreatorEmail;
            columns = b.Columns;
        }
        
    }
}
