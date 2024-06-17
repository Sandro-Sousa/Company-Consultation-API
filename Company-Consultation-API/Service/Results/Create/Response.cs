
using Flunt.Notifications;

namespace Service.Results.Create
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

        public Response(string message, ResponseData data)
        {
            Message = message;
            Status = 201;
            Notifications = null;
            Data = data;
        }

        public ResponseData? Data { get; private set; }
    }

    public record ResponseData(int Id, string Name, string Email);
}
