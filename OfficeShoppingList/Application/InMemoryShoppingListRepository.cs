using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Checkout.OfficeShoppingList.Domain;

namespace Checkout.OfficeShoppingList.Application
{
    public class InMemoryShoppingListRepository: IShoppingListCommands, IShoppingListRepository
    {
        // starts with a single existing list that we can then use to find.
        public static ConcurrentDictionary<Guid, ShoppingList> Lists =
            new ConcurrentDictionary<Guid, ShoppingList>(new List<KeyValuePair<Guid, ShoppingList>>()
            {
                new KeyValuePair<Guid, ShoppingList>(new Guid("DD366592-C35C-48B3-A80C-87D1D04AF824"),
                    new ShoppingList(new Guid("DD366592-C35C-48B3-A80C-87D1D04AF824"), new Dictionary<string, Item>()))
            });

        public ShoppingList Create()
        {
            var list = new ShoppingList();
            Lists[list.Id] = list;
            return list;
        }

        public void Save(ShoppingList list)
        {
            Lists[list.Id] = list;
        }

        public ShoppingList Get(Guid id)
        {
            return Lists[id];
        }
    }
}
