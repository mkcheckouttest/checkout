using Checkout.OfficeShoppingList.Domain;

namespace Checkout.OfficeShoppingList.Application.Dtos
{
    public class ItemDto
    {
        public string Name { get; set; }
        public uint Quantity { get; set; }

        public static implicit operator ItemDto(Item item)
        {
            return new ItemDto
            {
                Name = item.Name,
                Quantity = item.Quanity
            };
        }
    }
}