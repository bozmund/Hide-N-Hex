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

    private readonly string[] common = {
        "blindweed", "dewcatcher", "earthroot", "fadeleaf", "firebloom", "icecap",
        "mageroyal", "rotberry", "sorrowmoss", "sungrass", "swiftthistle"
    };

    private readonly string[] rare = {
        "starflower", "goldenLotus", "blandfruit", "stormvine"
    };

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
                new InventorySlot { itemNumber = "eighthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "ninthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "tenthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "eleventhItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twelfthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirteenthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fourteenthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fifteenthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "sixteenthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "seventeenthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "eighteenthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "nineteenthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twentiethItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twentyFirstItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twentySecondItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twentyThirdItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twentyFourthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twentyFifthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twentySixthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twentySeventhItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twentyEighthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "twentyNinthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirtiethItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirtyFirstItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirtySecondItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirtyThirdItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirtyFourthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirtyFifthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirtySixthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirtySeventhItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirtyEighthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "thirtyNinthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fortiethItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fortyFirstItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fortySecondItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fortyThirdItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fortyFourthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fortyFifthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fortySixthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fortySeventhItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fortyEighthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fortyNinthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fiftiethItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fiftyFirstItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fiftySecondItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fiftyThirdItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fiftyFourthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fiftyFifthItem", itemName = "", count = 0 },
                new InventorySlot { itemNumber = "fiftySixthItem", itemName = "", count = 0 }
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

        // Determine which category the item belongs to
        bool isCommon = System.Array.Exists(common, element => element == itemName);
        bool isRare = System.Array.Exists(rare, element => element == itemName);

        // Check the first eight universal slots
        for (int i = 0; i < 8; i++)
        {
            if (string.IsNullOrEmpty(slots[i].itemName))
            {
                itemNumber = slots[i].itemNumber;
                return 0;
            }
        }

        // Find the first empty slot based on the item category
        if (isCommon)
        {
            for (int i = 8; i < 36; i++)
            {
                if (string.IsNullOrEmpty(slots[i].itemName))
                {
                    itemNumber = slots[i].itemNumber;
                    return 0;
                }
            }
        }
        else if (isRare)
        {
            for (int i = 36; i < slots.Count; i++)
            {
                if (string.IsNullOrEmpty(slots[i].itemName))
                {
                    itemNumber = slots[i].itemNumber;
                    return 0;
                }
            }
        }
        else
        {
            for (int i = 8; i < slots.Count; i++)
            {
                if (string.IsNullOrEmpty(slots[i].itemName))
                {
                    itemNumber = slots[i].itemNumber;
                    return 0;
                }
            }
        }

        return 0; // Return 0 if no empty slots are available
    }
}
