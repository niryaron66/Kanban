using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class JsonDeserializer
    {
        public JsonDeserializer()
        {

        }   
        public bool errorOccured(string s)
        {
            string[] vars = s.Split(':');
            string boolRes = vars[1].Substring(0, vars[1].Length - 1);
            if (boolRes == "true")
                return true;
            else
                return false;
        }
    }
}
