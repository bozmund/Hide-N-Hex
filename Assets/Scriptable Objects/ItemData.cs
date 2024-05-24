using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public List<ItemCount> items;

    [System.Serializable]
    public class ItemCount
    {
        public string itemName;
        public int count;
    }

    // Method to initialize the dictionary with default values
    private void OnEnable()
    {
        if (items == null || items.Count == 0)
        {
            items = new List<ItemCount>
            {
                new ItemCount { itemName = "firebloom", count = 0 },
                new ItemCount { itemName = "icecap", count = 0 },
                new ItemCount { itemName = "sorrowmoss", count = 0 },
                new ItemCount { itemName = "blindweed", count = 0 },
                new ItemCount { itemName = "sungrass", count = 0 },
                new ItemCount { itemName = "earthroot", count = 0 },
                new ItemCount { itemName = "fadeleaf", count = 0 },
                new ItemCount { itemName = "rotberry", count = 0 }
            };
        }
    }

    // Method to get the initial item counts
    public Dictionary<string, int> GetItemCounts()
    {
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();
        foreach (var item in items)
        {
            itemCounts.Add(item.itemName, item.count);
        }
        return itemCounts;
    }

    // Method to update the count of a specific item
    public void UpdateItemCount(string itemName, int newCount)
    {
        foreach (var item in items)
        {
            if (item.itemName == itemName)
            {
                item.count = newCount;
                break;
            }
        }
    }
}
