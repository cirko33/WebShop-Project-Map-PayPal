using System.Net;

namespace OnlineStoreApp.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
