using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class FColumn
    {
        public int ColumnOrdinal { get; set; }  
        public int Id   { get; set; }
        public int taskLimit { get; set; }  

        public List<FTask> Tasks { get; set; }
    }
}

