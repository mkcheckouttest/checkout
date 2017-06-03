using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using Ploeh.AutoFixture;

namespace Checkout.OfficeShoppingList.WebApi.Tests
{
    public sealed class WebApiContextCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<HttpConfiguration>(c => c
                .OmitAutoProperties());

            fixture.Customize<HttpRequestMessage>(c => c
                .Do(x =>
                {
                    var configuration = fixture.Create<HttpConfiguration>();
                    var context = new HttpRequestContext { Configuration = configuration };
                    x.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, configuration);
                    x.Properties.Add(HttpPropertyKeys.RequestContextKey, context);
                }));
        }
    }
}