using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    
    public class ResponseT<T>
    {
        public T ReturnValue { get; set; }

        public string ErrorMessage { get; set; }

        public bool ErrorOccured() 
        { 
            return ErrorMessage != null; 
        }


        public ResponseT(T ReturnValue, string ErrorMessage) //changed no longer extands response
        {
            this.ReturnValue = ReturnValue;
            this.ErrorMessage = ErrorMessage;
        }

        internal static ResponseT<T> FromValue(T value)
        {
            return new ResponseT<T>(value, null);
        }

        /*        public object Select(Func<object, object, MessageModel> p)
                {
                    throw new NotImplementedException();
                }
        */
        internal static ResponseT<T> FromError(string msg)
        {
            return new ResponseT<T>(default(T), msg);
        }
    }

    //      "Id": &lt;int&gt;,
    ///     "CreationTime": &lt;DateTime&gt;,
    ///     "Title": &lt;string&gt;,
    ///     "Description": &lt;string&gt;,
    ///     "DueDate": &lt;DateTime&gt;
    ///     
    //      "ErrorMessage": &lt;string&gt;,
    ///     "ReturnValue": &lt;object&gt;
}
