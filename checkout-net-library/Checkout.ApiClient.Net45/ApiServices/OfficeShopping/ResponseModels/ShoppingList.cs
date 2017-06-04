using System;
using System.Collections.Generic;

namespace Checkout.ApiServices.OfficeShopping.ResponseModels
{
    public class ShoppingList
    {
        public Guid Id { get; set; }
        public HashSet<ShoppingListItem> Items { get; set; }
    }
}
