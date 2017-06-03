using System.Web.Http;
using Owin;
using SimpleInjector;

namespace Checkout.OfficeShoppingList.WebApi.Configuration
{
    internal partial class Bootstrap
    {
        public readonly IAppBuilder AppBuilder;
        public readonly HttpConfiguration HttpConfiguration;
        public readonly Container Container;

        public Bootstrap(IAppBuilder appBuilder, HttpConfiguration httpConfiguration, Container container)
        {
            AppBuilder = appBuilder;
            HttpConfiguration = httpConfiguration;
            Container = container;
        }
    }
}