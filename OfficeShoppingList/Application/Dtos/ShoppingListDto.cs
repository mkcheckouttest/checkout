using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.OfficeShoppingList.Domain;

namespace Checkout.OfficeShoppingList.Application.Dtos
{
    public class ShoppingListDto
    {
        public HashSet<ItemDto> Items { get; set; }
        public Guid Id { get; set; }

        public static implicit operator ShoppingListDto(ShoppingList list)
        {
            return new ShoppingListDto()
            {
                Id = list.Id,
                Items = new HashSet<ItemDto>(list.Items.Select(item => (ItemDto)item))
            };
        }
    }
}