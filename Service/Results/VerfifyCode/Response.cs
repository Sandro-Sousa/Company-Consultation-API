
using Flunt.Notifications;

namespace Service.Results.VerfifyCode
{
    public class Response : SharedContext.Response
    {
        protected Response()
        {
        }

        public Response(string message, int status, IEnumerable<Notification>? notifications = null)
        {
            Message = message;
            Status = status;
            Notifications = notifications;
        }

        public Response(string message, bool valid)
        {
            Message = message;
            Status = 201;
            Notifications = null;
        }
    }
}
