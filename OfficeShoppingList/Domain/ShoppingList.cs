using System.Collections.Generic;
using System.Linq;

namespace Checkout.OfficeShoppingList.Domain
{
    public class ShoppingList
    {
        private readonly Dictionary<string, Item> itemDictionary;
        public IReadOnlyList<Item> Items => itemDictionary.Values.ToList() as IReadOnlyList<Item>;

        internal ShoppingList(Dictionary<string, Item> items)
        {
            this.itemDictionary = items;
        }

        public ShoppingList() : this(new Dictionary<string, Item>())
        {
        }

        public void AddItem(string name)
        {
            itemDictionary[name] = itemDictionary.ContainsKey(name)
                ? new Item(name, itemDictionary[name].Quanity + 1)
                : new Item(name);
        }
    }
}
