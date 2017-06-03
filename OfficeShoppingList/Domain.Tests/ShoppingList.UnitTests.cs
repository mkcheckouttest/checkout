using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Common;
using Moq;
using NUnit.Framework;

namespace Checkout.OfficeShoppingList.Domain.Tests
{
    [TestFixture]
    public class ShoppingListUnitTests
    {
        [Test]
        public void GivenAnEmptyShoppingList_WhenDoNothing_ShoppingListShouldBeEmpty()
        {
            var sut = new ShoppingList();

            sut.Items.Should().BeEmpty();
        }

        [Test]
        public void GivenEmptyShoppingList_WhenAddAnItem_ShoppingListShouldContainNewItem()
        {
            var sut = new ShoppingList();

            sut.AddItem("foo");

            sut.Items.Should().Contain(x => x.Name == "foo" && x.Quanity == 1);
        }

        [TestCase("foo")]
        [TestCase("bar")]
        public void GivenShoppingListWithExistingItem_WhenAddSameItem_ShouldNotBeAllowedToAddItemAgain(string name)
        {
            var sut = new ShoppingList(new Dictionary<string, Item>() {{name, new Item(name)}});

            TestDelegate action = () => sut.AddItem(name);

            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void GivenShoppingList_WhenAddAnItemwithQuantity_ShoppingListShouldContainNewItemWithQuantity()
        {
            var random = new Random();
            var quantity = random.Next(int.MaxValue);

            var sut = new ShoppingList();

            sut.AddItem("foo", (uint)quantity);

            sut.Items.Should().Contain(x => x.Name == "foo" && x.Quanity == quantity);
        }

        [TestCase("foo")]
        [TestCase("bar")]
        public void GivenShoppingList_WhenUpdatingQuantityOfAnItem_ShoppingListShouldContainItemWithNewQuantity(string name)
        {
            var random = new Random();
            var sut = new ShoppingList(new Dictionary<string, Item>() { { name, new Item(name, (uint)random.Next(int.MaxValue)) } });

            var expectedQuantity = random.Next(int.MaxValue);

            sut.UpdateItem(name, (uint)expectedQuantity);

            sut.Items.Should().Contain(x => x.Name == name && x.Quanity == expectedQuantity);
        }

        [TestCase("foo", "bar", "baz")]
        [TestCase("foobar", "barbar")]
        public void GivenShoppingList_WhenMultipleItemsAdded_ShoppingListShouldContainAllItems(params string[] itemNames)
        {
            var sut = new ShoppingList();

            foreach (var name in itemNames)
            {
                sut.AddItem(name);
            }

            sut.Items.Count.IsSameOrEqualTo(itemNames.Length);
        }

        [Test]
        public void GivenEmptyShoppingList_WhenAttemptToRemoveUnexpectedItem_ShouldNotDoAnything()
        {
            var sut = new ShoppingList();

            sut.RemoveItem("foo");

            sut.Items.Count.Should().Be(0);
        }

        [TestCase("foo")]
        [TestCase("bar")]
        public void GivenShoppingList_WhenAttemptToRemoveItem_ShoppingListShouldNotContainItem(string name)
        {
            var sut = new ShoppingList(new Dictionary<string, Item>() { { name, new Item(name) } });

            sut.RemoveItem(name);

            sut.Items.Should().NotContain(x => x.Name == name);
        }

        [TestCase("foo")]
        [TestCase("bar")]
        public void GivenShoppingList_WhenAttemptToUpdateQuantityToZero_ShoppingListShouldNotContainItem(string name)
        {
            var sut = new ShoppingList(new Dictionary<string, Item>() { { name, new Item(name) } });

            sut.UpdateItem(name, 0);

            sut.Items.Should().NotContain(x => x.Name == name);
        }
    }
}
