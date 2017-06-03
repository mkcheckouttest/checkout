using Checkout.OfficeShoppingList.WebApi;
using Checkout.OfficeShoppingList.WebApi.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Checkout.OfficeShoppingList.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Bootstrap()
                .SetupDependencies()
                .WithoutDefaultContentTypes()
                .WithJsonContentType()
                .UseWebApi();
        }
    }
}
