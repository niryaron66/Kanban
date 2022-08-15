using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class DTO
    {
        protected DalController controller;
        protected DTO(DalController _controller) { controller = _controller; }
     
    }

    
}
