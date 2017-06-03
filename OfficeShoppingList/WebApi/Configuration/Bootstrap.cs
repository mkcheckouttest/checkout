using System.Web.Http;
using Owin;

namespace Checkout.OfficeShoppingList.WebApi.Configuration
{
    internal partial class Bootstrap
    {
        public readonly IAppBuilder AppBuilder;
        public readonly HttpConfiguration HttpConfiguration;

        public Bootstrap(IAppBuilder appBuilder, HttpConfiguration httpConfiguration)
        {
            AppBuilder = appBuilder;
            HttpConfiguration = httpConfiguration;
        }
    }
}