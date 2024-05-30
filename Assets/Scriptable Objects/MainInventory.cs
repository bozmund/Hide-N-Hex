using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainInventory", menuName = "ScriptableObjects/MainInventory", order = 1)]
public class MainInventory : ScriptableObject
{
    public List<InventorySlot> slots;

    [System.Serializable]
    public class InventorySlot
    {
        public string itemNumber;
        public string itemName;
        public int count;
    }

    private void OnEnable()
    {
        if (slots == null || slots.Count == 0)
        {
            slots = new List<InventorySlot>
            {
                new InventorySlot { itemNumber = "firstItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "secondItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirdItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fourthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fifthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "sixthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "seventhItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "eighthItem", itemName = "", count = 0 }
            };
        }
    }

    public Dictionary<string, InventorySlot> GetMainInventory()
    {
        Dictionary<string, InventorySlot> inventory = new Dictionary<string, InventorySlot>();
        foreach (var slot in slots)
        {
            if (inventory.ContainsKey(slot.itemNumber))
            {
                Debug.LogError($"Duplicate itemNumber found: {slot.itemNumber}");
                continue; // Skip adding this duplicate entry
            }
            inventory.Add(slot.itemNumber, slot);
        }
        return inventory;
    }

    public void UpdateMainInventory(string itemNumber, string newItemName, int newCount)
    {
        foreach (var slot in slots)
        {
            if (slot.itemNumber == itemNumber)
            {
                slot.itemName = newItemName;
                slot.count = newCount;
                break;
            }
        }
    }

    public int GetSlotAndCountForItem(string itemName, out string itemNumber)
    {
        itemNumber = null;
        foreach (var slot in slots)
        {
            if (slot.itemName == itemName)
            {
                itemNumber = slot.itemNumber;
                return slot.count;
            }
        }

        // Find the first empty slot
        foreach (var slot in slots)
        {
            if (string.IsNullOrEmpty(slot.itemName))
            {
                itemNumber = slot.itemNumber;
                return 0; // Return 0 if the item is not found
            }
        }

        return 0; // Return 0 if the item is not found and no empty slots are available
    }
}
