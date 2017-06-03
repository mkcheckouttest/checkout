using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Checkout.OfficeShoppingList.WebApi.Exceptions;

namespace Checkout.OfficeShoppingList.WebApi.Attributes
{
    public class AuthorizeByKeyAttribute : FilterAttribute, IAuthorizationFilter
    {
        public override bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            var authorization = actionContext.Request.Headers.Authorization;
            //this should contain a call to a data store to retrieve the API key and validate it properly
            if (authorization == null || !authorization.Scheme.Equals("pk_test_2997d616-471e-48a5-ba86-c775ed3ac38a"))
            {
                throw new NotAuthorizedException();
            }

            return continuation();
        }
    }
}