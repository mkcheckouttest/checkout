using System;
using Checkout.OfficeShoppingList.Application.Dtos;

namespace Checkout.OfficeShoppingList.Application
{
    public interface IShoppingListService
    {
        ShoppingListDto GetShoppingList(Guid id);
        ItemDto GetShoppingListItem(Guid id, string name);

        //todo: this probably could be merged with the commands so that we have actual CQRS
        void UpdateItemInShoppingList(Guid id, ItemDto update);
        void AddItemToShoppingList(Guid id, ItemDto item);
        void RemoveItemFromShoppingList(Guid id, string name);
    }
}
