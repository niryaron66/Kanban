namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Response
    {
        public readonly string ErrorMessage;
        public bool ErrorOccured { get => ErrorMessage != null; }
        public Response() 
        {
            ErrorMessage = null;
        }
        public Response(string msg)
        {
            this.ErrorMessage = msg;
        }
    }
}