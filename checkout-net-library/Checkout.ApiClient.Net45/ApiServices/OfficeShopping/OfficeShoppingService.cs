using System;
using Checkout.ApiServices.OfficeShopping.RequestModels;
using Checkout.ApiServices.OfficeShopping.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.OfficeShopping
{
    public class OfficeShoppingService
    {
        public HttpResponse<ShoppingList> GetShoppingList(Guid id)
        {
            var getShoppingListUri = string.Format(ApiUrls.ShoppingListUri, id);
            return new ApiHttpClient().GetRequest<ShoppingList>(getShoppingListUri, AppSettings.PublicKey);
        }

        public HttpResponse<ShoppingListItem> AddShoppingListItem(Guid id, ShoppingListItem item)
        {
            var addShoppingListItemUri = string.Format(ApiUrls.ShoppingListItemsUri, id);
            return new ApiHttpClient().PostRequest<ShoppingListItem>(addShoppingListItemUri, AppSettings.PublicKey, item);
        }

        public HttpResponse<ShoppingListItem> UpdateShoppingListItem(Guid id, string name, UpdateShoppingListItem update)
        {
            var updateShoppingListItemUri = string.Format(ApiUrls.ShoppingListItemResourceUri, id, name);
            return new ApiHttpClient().PutRequest<ShoppingListItem>(updateShoppingListItemUri, AppSettings.PublicKey, update);
        }

        public HttpResponse<ShoppingListItem> GetShoppingListItem(Guid id, string name)
        {
            var getShoppingListItemUri = string.Format(ApiUrls.ShoppingListItemResourceUri, id, name);
            return new ApiHttpClient().GetRequest<ShoppingListItem>(getShoppingListItemUri, AppSettings.PublicKey);
        }

        public HttpResponse<ShoppingListItem> DeleteShoppingListItem(Guid id, string name)
        {
            var deleteShoppingListItemUri = string.Format(ApiUrls.ShoppingListItemResourceUri, id, name);
            return new ApiHttpClient().DeleteRequest<ShoppingListItem>(deleteShoppingListItemUri, AppSettings.PublicKey);
        }
    }
}
