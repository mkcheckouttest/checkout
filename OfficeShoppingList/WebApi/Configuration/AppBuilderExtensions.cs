using System.Web.Http;
using Owin;

namespace Checkout.OfficeShoppingList.WebApi.Configuration
{
    internal static class AppBuilderExtensions
    {
        internal static Bootstrap Bootstrap(this IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            return new Bootstrap(appBuilder, httpConfiguration);
        }
    }
}