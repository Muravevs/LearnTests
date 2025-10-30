using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
    public interface IInventory
    {
        public int MaxSlots { get; }
        public IReadOnlyList<Item> Items { get; }
        public List<Item> NearbyItems { get; }
        public void TryPickupClosestItem();
        public void AddItem(Item item);
        public void RemoveItem();
        public void GetAllItems();
        public void ContainsItem(Item item);
    }
}
