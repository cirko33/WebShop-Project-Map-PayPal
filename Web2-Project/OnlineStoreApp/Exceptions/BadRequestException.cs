using System.Net;

namespace OnlineStoreApp.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message, null, HttpStatusCode.BadRequest)
        {
        }
    }
}
