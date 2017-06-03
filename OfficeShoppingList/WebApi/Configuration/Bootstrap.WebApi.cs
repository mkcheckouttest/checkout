using Owin;
using System.Web.Http;

namespace Checkout.OfficeShoppingList.WebApi.Configuration
{
    internal partial class Bootstrap
    {
        public Bootstrap UseWebApi()
        {
            HttpConfiguration.MapHttpAttributeRoutes();
            //HttpConfiguration.Filters.Add(...);
            HttpConfiguration.EnsureInitialized();
            AppBuilder.UseWebApi(HttpConfiguration);

            return this;
        }
    }
}