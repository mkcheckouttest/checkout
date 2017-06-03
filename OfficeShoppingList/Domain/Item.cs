namespace Checkout.OfficeShoppingList.Domain
{
    public class Item
    {
        public readonly string Name;
        public readonly uint Quanity;

        public Item(string name, uint quanity = 1)
        {
            Name = name;
            Quanity = quanity;
        }
    }
}