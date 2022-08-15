using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class ResponseT<T>
    {

        public T? ReturnValue { get; set; }
        public string? ErrorMessage { get; set; }
        public bool ErrorOccured { get => ErrorMessage != null; }
    }
}
