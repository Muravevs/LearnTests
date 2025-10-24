using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int maxSlots = 10;
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;

    [Header("Events")]
    public UnityEvent<Item> onItemAdded;
    public UnityEvent<Item> onItemRemoved;
    public UnityEvent onInventoryFull;

    private List<Item> items = new List<Item>();
    public IReadOnlyList<Item> Items => items;
    private List<Item> nearbyItems = new List<Item>();

    void Update()
    {
        if (Input.GetKeyDown(pickupKey) && nearbyItems.Count > 0)
        {
            TryPickupClosestItem();
        }

        if (Input.GetKeyDown(dropKey) && items.Count > 0)
        {
            RemoveItem();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && !nearbyItems.Contains(item))
        {
            nearbyItems.Add(item);
            Debug.Log("Рядом предмет: " + item.itemName);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && nearbyItems.Contains(item))
        {
            nearbyItems.Remove(item);
        }
    }

    void TryPickupClosestItem()
    {
        if (nearbyItems.Count == 0) return;

        Item itemToPickup = nearbyItems[0];

        if (items.Count < maxSlots)
        {
            AddItem(itemToPickup);
            nearbyItems.Remove(itemToPickup);
        }
        else
        {
            onInventoryFull?.Invoke();
            Debug.Log("Инвентарь полон!");
        }
    }

    public void AddItem(Item item)
    {
        if (item != null)
        {
            items.Add(item);
            item.Pickup();
            onItemAdded?.Invoke(item);
        }
    }

    public void RemoveItem()
    {
        if (items.Count > 0)
        {
            Item itemToRemove = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            itemToRemove.Restore();

            itemToRemove.transform.position = transform.position + transform.forward * 2f;
        }
        else
        {
            Debug.Log("Инвентарь пуст");
        }
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
        Debug.Log($"Всего предметов: {items.Count}/{maxSlots}");

        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log($"{i + 1}. {items[i].itemName}");
        }
        Debug.Log("=========================================");
    }
}
