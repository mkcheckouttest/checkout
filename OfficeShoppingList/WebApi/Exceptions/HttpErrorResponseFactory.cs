using System;
using System.Net;

namespace Checkout.OfficeShoppingList.WebApi.Exceptions
{
    public class HttpErrorResponseFactory : IHttpErrorResponseFactory
    {
        public HttpErrorResponse Create(Exception exception)
        {
            return new HttpErrorResponse
            {
                Message = "Unexpected error occured.",
                InternalErrorCode = "UnexpectedError",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}