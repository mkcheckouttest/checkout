using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Owin;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Checkout.OfficeShoppingList.WebApi.Attributes;
using Checkout.OfficeShoppingList.WebApi.Exceptions;

namespace Checkout.OfficeShoppingList.WebApi.Configuration
{
    internal partial class Bootstrap
    {
        public Bootstrap UseWebApi()
        {
            HttpConfiguration.MapHttpAttributeRoutes();

            HttpConfiguration.Services.Replace(typeof(IExceptionHandler),
                GlobalExceptionHandler.Factory.Create(new HttpErrorResponseFactory()));
            HttpConfiguration.Filters.Add(new AuthorizeByKeyAttribute());
            HttpConfiguration.EnsureInitialized();
            AppBuilder.UseWebApi(HttpConfiguration);

            return this;
        }
    }
}