using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.OfficeShoppingList.Domain
{
    public class ShoppingList
    {
        private readonly Dictionary<string, Item> itemDictionary;
        public readonly Guid Id;
        public IReadOnlyList<Item> Items => itemDictionary.Values.ToList() as IReadOnlyList<Item>;

        internal ShoppingList(Guid id, Dictionary<string, Item> items)
        {
            Id = id;
            itemDictionary = items;
        }

        public ShoppingList() : this(Guid.NewGuid(), new Dictionary<string, Item>())
        {
        }

        public void AddItem(string name, uint quantity = 1)
        {
            if (itemDictionary.ContainsKey(name))
            {
                throw new InvalidOperationException("Cannot add existing item to Shopping List");
            }

            UpdateItem(name, quantity);
        }

        public void UpdateItem(string name, uint quantity)
        {
            if (quantity == 0)
            {
                RemoveItem(name);
            }
            else
            {
                itemDictionary[name] = new Item(name, quantity);
            }
        }

        public void RemoveItem(string name)
        {
            if (itemDictionary.ContainsKey(name))
            {
                itemDictionary.Remove(name);
            }   
        }
    }
}
