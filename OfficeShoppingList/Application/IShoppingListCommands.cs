using Checkout.OfficeShoppingList.Domain;

namespace Checkout.OfficeShoppingList.Application
{
    public interface IShoppingListCommands
    {
        ShoppingList Create();
        void Save(ShoppingList list);
    }
}