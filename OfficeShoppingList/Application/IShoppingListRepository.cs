using System;
using Checkout.OfficeShoppingList.Domain;

namespace Checkout.OfficeShoppingList.Application
{
    public interface IShoppingListRepository
    {
        ShoppingList Get(Guid id);
    }
}
