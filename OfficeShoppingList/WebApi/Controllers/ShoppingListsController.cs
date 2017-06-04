using System;
using System.Net;
using System.Web.Http;
using Checkout.OfficeShoppingList.Application;
using Checkout.OfficeShoppingList.Application.Dtos;

namespace Checkout.OfficeShoppingList.WebApi.Controllers
{
    public class ShoppingListsController : ApiController
    {
        private readonly IShoppingListService service;

        public ShoppingListsController(IShoppingListService service)
        {
            this.service = service;
        }

        [HttpGet, Route("lists/{id}")]
        public IHttpActionResult GetShoppingList(Guid id)
        {
            return Content(HttpStatusCode.OK, service.GetShoppingList(id)); 
        }

        [HttpPost, Route("lists/{id}/items")]
        public IHttpActionResult AddShoppingListItem(Guid id, [FromBody] ItemDto item)
        {
            service.AddItemToShoppingList(id, item);
            return Created($"lists/{id}/items/{item.Name}", item);
        }

        [HttpPut, Route("lists/{id}/items/{name}")]
        public IHttpActionResult UpdateShoppingListItem(Guid id, string name, [FromBody] UpdateItemDto updatedItem)
        {
            var item = new ItemDto {Name = name, Quantity = updatedItem.Quantity};
            service.UpdateItemInShoppingList(id, item);
            return Ok(item);
        }

        [HttpGet, Route("lists/{id}/items/{name}")]
        public IHttpActionResult GetShoppingListItem(Guid id, string name)
        {
            return Ok(service.GetShoppingListItem(id, name));
        }

        [HttpDelete, Route("lists/{id}/items/{name}")]
        public IHttpActionResult Delete(Guid id, string name)
        {
            var item = service.GetShoppingListItem(id, name);
            service.RemoveItemFromShoppingList(id, name);
            return Ok(item);
        }
    }
}