using System;
using System.Collections.Generic;
using System.Net;

namespace Checkout.OfficeShoppingList.WebApi.Exceptions
{
    public class HttpErrorResponseFactory : IHttpErrorResponseFactory
    {
        public HttpErrorResponse Create(Exception exception)
        {
            if (exception.GetType() == typeof(KeyNotFoundException))
            {
                return new HttpErrorResponse()
                {
                    InternalErrorCode = HttpStatusCode.NotFound.ToString(),
                    StatusCode = HttpStatusCode.NotFound,
                    Message = exception.Message
                };
            }

            if (exception.GetType() == typeof(InvalidOperationException))
            {
                return new HttpErrorResponse()
                {
                    InternalErrorCode = HttpStatusCode.BadRequest.ToString(),
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = exception.Message
                };
            }

            return new HttpErrorResponse
            {
                Message = "Unexpected error occured.",
                InternalErrorCode = "UnexpectedError",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}