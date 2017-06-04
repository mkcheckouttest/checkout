using System;
using System.Net;
using Checkout.ApiServices.OfficeShopping.RequestModels;
using Checkout.ApiServices.OfficeShopping.ResponseModels;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.OfficeShoppingService
{
    [TestFixture(Category = "OfficeShoppingApi")]
    public class OfficeShoppingListServiceTests : BaseServiceTests
    {
        //probably would want this to be something randomly generated, however for simplicity lets say we always use this as we have an in memory version already
        //and we havent exposed the creation of a new shopping list on the API.
        private readonly Guid testGuid = new Guid("DD366592-C35C-48B3-A80C-87D1D04AF824");

        [Test]
        public void GetShoppingList_ReturnsFullShoppingList()
        {
            var response = CheckoutClient.OfficeShoppingService.GetShoppingList(testGuid);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Should().NotBeNull();
            response.Model.Id.Should().Be(testGuid);
        }

        [TestCase("DD366592-C35C-48B3-A80C-87D1D04AF825", HttpStatusCode.NotFound)]
        public void GetShoppingList_AssertErrorStatusCode(string guidString, HttpStatusCode code)
        {
            var id = new Guid(guidString);
            var response = CheckoutClient.OfficeShoppingService.GetShoppingList(id);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(code);
        }

        [Test]
        public void AddShoppingListItem_ReturnCreatedResponse()
        {
            var random = new Random();
            var shoppingListItem = new ShoppingListItem { Name = Guid.NewGuid().ToString(),  Quantity = (uint)random.Next(int.MaxValue)};
            var response = CheckoutClient.OfficeShoppingService.AddShoppingListItem(testGuid, shoppingListItem);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);
            //because the CreateHttpResponse is always expecting OK, it means that we never get a body with any other status code, even though this is perfectly valid.
        }

        [Test]
        public void GetShoppingListItem_ReturnShoppingListItem()
        {
            var random = new Random();
            var shoppingListItem = new ShoppingListItem { Name = Guid.NewGuid().ToString(), Quantity = (uint)random.Next(int.MaxValue) };
            CheckoutClient.OfficeShoppingService.AddShoppingListItem(testGuid, shoppingListItem);

            var response = CheckoutClient.OfficeShoppingService.GetShoppingListItem(testGuid, shoppingListItem.Name);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.ShouldBeEquivalentTo(shoppingListItem);
        }

        [Test]
        public void UpdateShoppingListItem_ReturnUpdatedShoppingListItem()
        {
            var random = new Random();
            var shoppingListItem = new ShoppingListItem { Name = Guid.NewGuid().ToString(), Quantity = (uint)random.Next(int.MaxValue) };
            var updatedItem = new UpdateShoppingListItem() { Quantity = (uint)random.Next(int.MaxValue) };
            CheckoutClient.OfficeShoppingService.AddShoppingListItem(testGuid, shoppingListItem);

            var response = CheckoutClient.OfficeShoppingService.UpdateShoppingListItem(testGuid, shoppingListItem.Name, updatedItem);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Name.Should().Be(shoppingListItem.Name);
            response.Model.Quantity.Should().Be(updatedItem.Quantity);
        }

        [Test]
        public void DeleteShoppingListItem_ReturnOkResponse()
        {
            var random = new Random();
            var shoppingListItem = new ShoppingListItem { Name = Guid.NewGuid().ToString(), Quantity = (uint)random.Next(int.MaxValue) };
            CheckoutClient.OfficeShoppingService.AddShoppingListItem(testGuid, shoppingListItem);

            var response = CheckoutClient.OfficeShoppingService.DeleteShoppingListItem(testGuid, shoppingListItem.Name);

            response.Should().NotBeNull();
            //originally i wanted to return no content for deletion, however this did not work for similar reasons as above with the CreateHttpResponse.
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
