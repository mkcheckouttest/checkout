using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Checkout.OfficeShoppingList.WebApi.Exceptions
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public static class Factory
        {
            public static GlobalExceptionHandler Create(IHttpErrorResponseFactory responseFactory)
            {
                return new GlobalExceptionHandler(responseFactory);
            }
        }

        public IHttpErrorResponseFactory ResponseFactory { get; }

        public GlobalExceptionHandler(IHttpErrorResponseFactory responseFactory)
        {
            this.ResponseFactory = responseFactory;
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new CoreExceptionResult(ResponseFactory, context);
            base.Handle(context);
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            // When CORS is enabled, CorsMessageHandler catches and handles all the exceptions.
            // By returning true, we make sure that this custom exception handler is run before CorsMessageHandler,
            // so that custom logic can be applied.
            return true;
        }
    }
}