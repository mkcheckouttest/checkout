using System;

namespace Checkout.OfficeShoppingList.WebApi.Exceptions
{
    public interface IHttpErrorResponseFactory
    {
        HttpErrorResponse Create(Exception exception);
    }
}