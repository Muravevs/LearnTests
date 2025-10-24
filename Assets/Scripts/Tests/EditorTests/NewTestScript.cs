using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    Inventory inventory;
    Item item;
    [SetUp]

    public void Setup()
    {
        inventory = new GameObject().AddComponent<Inventory>();
        item = new GameObject().AddComponent<Item>();
    }
    [TearDown]
    public void TearDown()
    {
        inventory = null;
        item = null;
    }

    [Test]
    public void AddItem_WhenCalled_AddCountItemsInList()
    {
        //assert
        inventory.AddItem(item);

        Assert.AreEqual(1, inventory.Items.Count);
    }

    [Test]
    public void RemoveItem_WhenCalled_RemoveCountItemsInList()
    {
        inventory.RemoveItem();

        Assert.AreEqual(0, inventory.Items.Count);
    }

    [Test]
    public void AddItem_WhenAddNullItemToInventory_notAddsInCountItemsInList()
    {
        inventory.AddItem(null);

        Assert.AreEqual(0, inventory.Items.Count);
    }
}
