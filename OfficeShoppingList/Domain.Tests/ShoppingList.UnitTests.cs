using System.Collections.Generic;
using FluentAssertions;
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

        [TestCase("foo", 1)]
        [TestCase("bar", 10)]
        public void GivenShoppingListWithExistingItem_WhenAddSameItem_ShoppingListShouldIncrementQuantityOfExistingItem(string name, int originalValue)
        {
            var sut = new ShoppingList(new Dictionary<string, Item>() {{name, new Item(name, (uint)originalValue)}});

            sut.AddItem(name);

            sut.Items.Should().Contain(x => x.Name == name && x.Quanity == originalValue + 1);
        }
    }
}
