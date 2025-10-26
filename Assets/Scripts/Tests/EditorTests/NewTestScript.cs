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
    public void AddItem_WhenCalledNotUnicaledId_LogErrorNotUnicaledId()
    {
        inventory.AddItem(item);
        inventory.AddItem(item);

        LogAssert.Expect(LogType.Error, $"предмет с id = {item.itemId} уже находится в инвентаре");
    }

    [Test]
    public void RemoveItem_WhenCalled_RemoveCountItemsInList()
    {
        inventory.AddItem(item);
        inventory.RemoveItem();

        Assert.AreEqual(0, inventory.Items.Count);
    }

    [Test]
    public void AddItem_WhenAddNullItemToInventory_notAddsInCountItemsInList()
    {
        inventory.AddItem(null);

        Assert.AreEqual(0, inventory.Items.Count);
    }

    [Test]
    public void ContaintsItem_whenCalled_LogContaintsItemInList()
    {
        inventory.AddItem(item);
        inventory.ContainsItem(item);

        LogAssert.Expect(LogType.Log, $"В инвентаре есть предмет с id:{item.itemId}");
    }

    [Test]
    public void GetAllItems_whenCalled_LogAllItemInList()
    {
        inventory.AddItem(item);
        inventory.GetAllItems();

        LogAssert.Expect(LogType.Log, "=== ПОЛНЫЙ СПИСОК ПРЕДМЕТОВ В ИНВЕНТАРЕ ===");
        LogAssert.Expect(LogType.Log, $"Всего предметов: {inventory.Items.Count}/{inventory.maxSlots}");
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            LogAssert.Expect(LogType.Log, $"{i + 1}. {inventory.Items[i].itemName}");
        }
        LogAssert.Expect(LogType.Log, "=========================================");
    }
}
