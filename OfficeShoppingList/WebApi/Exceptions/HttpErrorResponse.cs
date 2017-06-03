using System.Net;

namespace Checkout.OfficeShoppingList.WebApi.Exceptions
{
    public class HttpErrorResponse
    {
        public string Message { get; set; }
        public string InternalErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}