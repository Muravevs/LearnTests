using UnityEngine;
using System.Collections.Generic;

namespace GameScene
{
    public sealed class Inventory : IInventory
    {
        [Header("Inventory Settings")]
        public int MaxSlots { get; private set; } = 10;
       

        private List<Item> items = new List<Item>();
        public IReadOnlyList<Item> Items => items;
        private List<Item> nearbyItems = new List<Item>();
        public List<Item> NearbyItems => nearbyItems;

        public void TryPickupClosestItem()
        {
            if (nearbyItems.Count == 0) return;

            Item itemToPickup = nearbyItems[0];

            if (items.Count < MaxSlots)
            {
                AddItem(itemToPickup);
                nearbyItems.Remove(itemToPickup);
            }
            else
            {
                Debug.Log("Инвентарь полон!");
            }
        }

        public void AddItem(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemId == item.itemId)
                {
                    Debug.LogError($"предмет с id = {item.itemId} уже находится в инвентаре");
                    return;
                }
            }
            if (item != null)
            {
                items.Add(item);
                item.Pickup();
            }
        }

        public void RemoveItem()
        {
            if (items == null || items.Count == 0)
            {
                Debug.Log("Инвентарь пуст");
                return;
            }

            // ✅ Удаляем по индексу последнего элемента
            int lastIndex = items.Count - 1;
            Item lastItem = items[lastIndex];

            items.RemoveAt(lastIndex);
            Debug.Log($"Удален последний предмет: {lastItem.itemName}, id: {lastItem.itemId}");
        }

        [ContextMenu("GetAllItems")]
        public void GetAllItems()
        {
            if (items.Count == 0)
            {
                Debug.Log("Инвентарь пуст");
                return;
            }

            Debug.Log("=== ПОЛНЫЙ СПИСОК ПРЕДМЕТОВ В ИНВЕНТАРЕ ===");
            Debug.Log($"Всего предметов: {items.Count}/{MaxSlots}");

            for (int i = 0; i < items.Count; i++)
            {
                Debug.Log($"{i + 1}. {items[i].itemName}");
            }
            Debug.Log("=========================================");
        }

        public void ContainsItem(Item item)
        {
            Debug.Log($"В инвентаре есть предмет с id:{item.itemId}");
        }
    }
}
