using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Frontend
{
    public class FBoard
    {
        public string Name { get; set; }

        public int Id { get; set; }
        public string owner { get; set; }
        public int counter { get; set; }
        public string CreatorEmail { get; set; }
        public FColumn[] columns { get; set; }



    }
}

