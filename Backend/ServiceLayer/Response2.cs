using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class Response2
    {
        public object ReturnValue { get; set; }

        public string ErrorMessage { get; set; }

        public bool ErrorOccured()
        {
            return ErrorMessage != null;
        }


        public Response2(object ReturnValue, string ErrorMessage) //changed no longer extands response
        {
            this.ReturnValue = ReturnValue;
            this.ErrorMessage = ErrorMessage;
        }

        internal static Response2 FromValue(object value)
        {
            return new Response2(value, null);
        }

        /*        public object Select(Func<object, object, MessageModel> p)
                {
                    throw new NotImplementedException();
                }
        */
        internal static Response2 FromError(string msg)
        {
            return new Response2(default(object), msg);
        }
    }
}
