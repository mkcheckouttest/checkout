using System;
using System.Collections.Generic;
using Checkout.OfficeShoppingList.Application.Dtos;
using Checkout.OfficeShoppingList.Domain;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Checkout.OfficeShoppingList.Application.Tests
{
    [TestFixture]
    public class ShoppingListServiceUnitTests
    {
        private IFixture fixture;
        private Mock<IShoppingListCommands> shopppingListCommandMock;
        private Mock<IShoppingListRepository> shoppingListRepositoryMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
            shopppingListCommandMock = fixture.Freeze<Mock<IShoppingListCommands>>();
            shoppingListRepositoryMock = fixture.Freeze<Mock<IShoppingListRepository>>();
        }

        [Test]
        public void GivenAShoppingListExists_WhenAttemptToRetrieveShoppingList_ShouldReturnCorrectShoppingListDto()
        {
            var id = Guid.NewGuid();
            var expectedShoppingList = new ShoppingList(id, new Dictionary<string, Item>() { { "foo", new Item("foo") } });
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => expectedShoppingList);
            var sut = fixture.Create<ShoppingListService>();

            var result = sut.GetShoppingList(id);

            result.Id.Should().Be(id);
        }

        [Test]
        public void GivenAShoppingListDoesNotExist_WhenAttemptToRetrieveShoppingList_ShouldThrowAnError()
        {
            var id = Guid.NewGuid();
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => null);
            var sut = fixture.Create<ShoppingListService>();

            TestDelegate action = () => sut.GetShoppingList(id);

            Assert.Throws<InvalidOperationException>(action);
        }

        [TestCase("foo")]
        public void GivenAShoppingListExists_WhenAttemptToRetrieveShoppingListItem_ShouldReturnCorrectItemDto(string name)
        {
            var random = new Random();
            var id = Guid.NewGuid();
            var quantity = (uint)random.Next(int.MaxValue);
            var expectedShoppingList = new ShoppingList(id, new Dictionary<string, Item>() { { name, new Item(name, quantity) } });
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => expectedShoppingList);
            var sut = fixture.Create<ShoppingListService>();

            var result = sut.GetShoppingListItem(id, name);

            result.Name.Should().Be(name);
            result.Quantity.Should().Be(quantity);
        }

        [Test]
        public void GivenAShoppingListDoesNotExist_WhenAttemptToRetrieveItemFromList_ShouldThrowAnError()
        {
            var id = Guid.NewGuid();
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => null);
            var sut = fixture.Create<ShoppingListService>();

            TestDelegate action = () => sut.GetShoppingListItem(id, "foo");

            Assert.Throws<KeyNotFoundException>(action);
        }

        [Test]
        public void GivenAShoppingListExists_WhenAttemptToRetrieveMissingItem_ShouldThrowAnError()
        {
            var id = Guid.NewGuid();
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => new ShoppingList(id, new Dictionary<string, Item>()));
            var sut = fixture.Create<ShoppingListService>();

            TestDelegate action = () => sut.GetShoppingListItem(id, "foo");

            Assert.Throws<KeyNotFoundException>(action);
        }

        [Test]
        public void GivenAShoppingListExists_WhenAttemptToAddItem_ShouldAddItem()
        {
            var id = Guid.NewGuid();
            var shoppingList = new ShoppingList(id, new Dictionary<string, Item>());
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => shoppingList);
            var sut = fixture.Create<ShoppingListService>();

            sut.AddItemToShoppingList(id, new ItemDto() { Name = "foo", Quantity = 1});

            shoppingList.Items.Should().Contain(x => x.Name == "foo" && x.Quanity == 1);
        }

        [Test]
        public void GivenAShoppingListExists_WhenAttemptToAddItem_ShouldPersistChanges()
        {
            var id = Guid.NewGuid();
            var shoppingList = new ShoppingList(id, new Dictionary<string, Item>());
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => shoppingList);
            var sut = fixture.Create<ShoppingListService>();

            sut.AddItemToShoppingList(id, new ItemDto() { Name = "foo", Quantity = 1 });

            shopppingListCommandMock.Verify(x => x.Save(shoppingList));
        }

        [TestCase("foo")]
        public void GivenAShoppingListExists_WhenAttemptToUpdateItem_ShouldUpdateItem(string name)
        {
            var random = new Random();
            var id = Guid.NewGuid();
            var quantity = (uint)random.Next(int.MaxValue);
            var shoppingList = new ShoppingList(id, new Dictionary<string, Item>() {{name, new Item(name, quantity)}});
            var update = new ItemDto() {Name = "foo", Quantity = (uint) random.Next(int.MaxValue)};
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => shoppingList);
            var sut = fixture.Create<ShoppingListService>();

            sut.UpdateItemInShoppingList(id, update);

            shoppingList.Items.Should().Contain(x => x.Name == name && x.Quanity == update.Quantity);

            shopppingListCommandMock.Verify(x => x.Save(shoppingList));
        }

        [TestCase("foo")]
        public void GivenAShoppingListExists_WhenAttemptToUpdateItem_ShouldPersistChanges(string name)
        {
            var random = new Random();
            var id = Guid.NewGuid();
            var quantity = (uint)random.Next(int.MaxValue);
            var shoppingList = new ShoppingList(id, new Dictionary<string, Item>() { { name, new Item(name, quantity) } });
            var update = new ItemDto() { Name = "foo", Quantity = (uint)random.Next(int.MaxValue) };
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => shoppingList);
            var sut = fixture.Create<ShoppingListService>();

            sut.UpdateItemInShoppingList(id, update);

            shopppingListCommandMock.Verify(x => x.Save(shoppingList));
        }

        [TestCase("foo")]
        public void GivenAShoppingListExists_WhenAttemptToRemoveItem_ShouldRemoveItem(string name)
        {
            var id = Guid.NewGuid();
            var shoppingList = new ShoppingList(id, new Dictionary<string, Item>() { { name, new Item(name) } });
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => shoppingList);

            var sut = fixture.Create<ShoppingListService>();

            sut.RemoveItemFromShoppingList(id, name);

            shoppingList.Items.Should().NotContain(x => x.Name == name);
        }

        [TestCase("foo")]
        public void GivenAShoppingListExists_WhenAttemptToRemoveItem_ShouldPersistChanges(string name)
        {
            var random = new Random();
            var id = Guid.NewGuid();
            var quantity = (uint)random.Next(int.MaxValue);
            var shoppingList = new ShoppingList(id, new Dictionary<string, Item>() { { name, new Item(name, quantity) } });
            shoppingListRepositoryMock.Setup(x => x.Get(id)).Returns(() => shoppingList);
            var sut = fixture.Create<ShoppingListService>();

            sut.RemoveItemFromShoppingList(id, name);

            shopppingListCommandMock.Verify(x => x.Save(shoppingList));
        }
    }
}
