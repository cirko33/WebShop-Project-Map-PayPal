using System.Net;

namespace OnlineStoreApp.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException(string message) : base(message, null, HttpStatusCode.InternalServerError)
        {
        }
    }
}
