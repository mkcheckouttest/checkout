using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PartialResponse.Net.Http.Formatting;

namespace Checkout.OfficeShoppingList.WebApi.Configuration
{
    internal partial class Bootstrap
    {
        public Bootstrap WithoutDefaultContentTypes()
        {
            HttpConfiguration.Formatters.Clear();
            return this;
        }

        public Bootstrap WithJsonContentType()
        {
            //allows for behaviour whereby you can select partial responses by specifying the fields you want returned.
            var formatter = new PartialJsonMediaTypeFormatter
            {
                IgnoreCase = true,
                Indent = true,
                SerializerSettings =
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateParseHandling = DateParseHandling.None
                }
            };

            //default to always use string enums.
            formatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            HttpConfiguration.Formatters.Add(formatter);
            JsonConvert.DefaultSettings = () => formatter.SerializerSettings;

            return this;
        }
    }
}