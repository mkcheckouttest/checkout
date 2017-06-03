using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Checkout.OfficeShoppingList.WebApi.Exceptions
{
    public sealed class CoreExceptionResult : IHttpActionResult
    {
        private readonly IHttpErrorResponseFactory httpErrorResponseFactory;
        private readonly ExceptionHandlerContext context;

        public CoreExceptionResult(IHttpErrorResponseFactory httpErrorResponseFactory,
            ExceptionHandlerContext context)
        {
            this.httpErrorResponseFactory = httpErrorResponseFactory;
            this.context = context;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var errorResponse = httpErrorResponseFactory.Create(context.Exception);
            var responseMessage = context.Request.CreateResponse(errorResponse.StatusCode, errorResponse,
                context.RequestContext.Configuration);

            return Task.FromResult(responseMessage);
        }
    }
}