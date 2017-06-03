using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.OfficeShoppingList.Application.Dtos;
using Checkout.OfficeShoppingList.Domain;

namespace Checkout.OfficeShoppingList.Application
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IShoppingListRepository repository;
        private readonly IShoppingListCommands commands;

        public ShoppingListService(IShoppingListRepository repository, IShoppingListCommands commands)
        {
            this.repository = repository;
            this.commands = commands;
        }

        public ShoppingListDto GetShoppingList(Guid id)
        {
            return GetDomainShoppingList(id);
        }

        public ItemDto GetShoppingListItem(Guid id, string name)
        {
            var list = GetDomainShoppingList(id);

            var item = list.Items.FirstOrDefault(x => x.Name == name);

            if (item == null)
            {
                throw new KeyNotFoundException($"ShoppingList({id}): Unable to find item, {name}, in shopping list");
            }

            return item;
        }

        public void AddItemToShoppingList(Guid id, ItemDto item)
        {
            var list = GetDomainShoppingList(id);
            list.AddItem(item.Name, item.Quantity);
            commands.Save(list);
        }

        public void UpdateItemInShoppingList(Guid id, ItemDto update)
        {
            var list = GetDomainShoppingList(id);
            list.UpdateItem(update.Name, update.Quantity);
            commands.Save(list);
        }

        public void RemoveItemFromShoppingList(Guid id, string name)
        {
            var list = GetDomainShoppingList(id);
            list.RemoveItem(name);
            commands.Save(list);
        }

        private ShoppingList GetDomainShoppingList(Guid id)
        {
            var list = repository.Get(id);

            if (list == null)
            {
                throw new KeyNotFoundException($"Unable to find ShoppingList({id})");
            }

            return list;
        }
    }
}