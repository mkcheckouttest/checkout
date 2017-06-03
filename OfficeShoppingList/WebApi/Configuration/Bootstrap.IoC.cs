using System.Web.Http;
using Checkout.OfficeShoppingList.Application;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace Checkout.OfficeShoppingList.WebApi.Configuration
{
    internal partial class Bootstrap
    {
        public Bootstrap SetupDependencies()
        {
            // Create the container as usual.
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            var inMemoryShoppingListRepo = new InMemoryShoppingListRepository();
            Container.Register<IShoppingListService, ShoppingListService>();
            Container.RegisterSingleton<IShoppingListRepository>(inMemoryShoppingListRepo);
            Container.RegisterSingleton<IShoppingListCommands>(inMemoryShoppingListRepo);

            // This is an extension method from the integration package.
            Container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            Container.Verify();

            HttpConfiguration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(Container);

            return this;
        }
    }
}