using System;
using Checkout.OfficeShoppingList.Domain;

namespace Checkout.OfficeShoppingList.Application
{
    public interface IShoppingListQueries
    {
        ShoppingList Get(Guid id);
    }
}
