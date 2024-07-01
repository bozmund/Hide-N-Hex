using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class InventoryUIManager : MonoBehaviour
    {
        public MainInventory mainInventoryData; // Reference to the ScriptableObject
        public List<Image> itemImages; // List of UI Image components
        public List<TextMeshProUGUI> itemCounts; // List of TextMeshProUGUI components for item counts
        public string resourceSubfolder = "plants"; // Default subfolder in the Resources folder

        public static InventoryUIManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep this object when loading new scenes
            }
            else
            {
                Destroy(gameObject); // Ensure only one instance exists
            }
        }

        private void Start()
        {
            LoadInventorySprites();
        }

        public void LoadInventorySprites()
        {
            Dictionary<string, MainInventory.InventorySlot> inventory = mainInventoryData.GetMainInventory();

            foreach (var slot in inventory)
            {
                string itemNumber = slot.Key;
                string itemName = slot.Value.itemName;
                int count = slot.Value.count;

                if (!string.IsNullOrEmpty(itemName))
                {
                    // Check if the itemName contains "Potion" and adjust the resourceSubfolder accordingly
                    string resourcePath;
                    if (itemName.Contains("Potion", System.StringComparison.OrdinalIgnoreCase))
                    {
                        resourcePath = $"Potions/{itemName}";
                    }
                    else
                    {
                        resourcePath = $"{resourceSubfolder}/{itemName}";
                    }

                    Sprite itemSprite = Resources.Load<Sprite>(resourcePath);

                    if (itemSprite != null)
                    {
                        // Find the corresponding UI Image component and set the sprite
                        Image itemImage = GetImageBySlotName(itemNumber);
                        if (itemImage != null)
                        {
                            itemImage.sprite = itemSprite;
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Sprite with name '{resourcePath}' not found in Resources.");
                    }
                }

                // Find the corresponding TextMeshProUGUI component and set the count
                TextMeshProUGUI itemCountText = GetTextBySlotName(itemNumber);
                if (itemCountText != null)
                {
                    itemCountText.text = count > 0 ? count.ToString() : "";
                }
            }
        }

        public void ClearUIElement(string itemNumber)
        {
            Image itemImage = GetImageBySlotName(itemNumber);
            if (itemImage != null)
            {
                itemImage.sprite = null;
            }

            TextMeshProUGUI itemCountText = GetTextBySlotName(itemNumber);
            if (itemCountText != null)
            {
                itemCountText.text = "";
            }
        }

        private Image GetImageBySlotName(string itemNumber)
        {
            foreach (Image img in itemImages)
            {
                //Debug.Log($"Checking image: {img.gameObject.name} against slot: {itemNumber}");
                if (img.gameObject.name.Equals(itemNumber, System.StringComparison.OrdinalIgnoreCase))
                {
                    return img;
                }
            }
            Debug.LogWarning($"UI Image for slot '{itemNumber}' not found.");
            return null;
        }

        public TextMeshProUGUI GetTextBySlotName(string itemNumber)
        {
            // Adjust itemNumber to match the expected naming convention for TextMeshProUGUI components
            string countName = $"count{char.ToUpper(itemNumber[0])}{itemNumber.Substring(1)}";

            foreach (TextMeshProUGUI text in itemCounts)
            {
                //Debug.Log($"Checking text: {text.gameObject.name} against slot: {countName}");
                if (text.gameObject.name.Equals(countName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return text;
                }
            }
            Debug.LogWarning($"TextMeshProUGUI for slot '{countName}' not found.");
            return null;
        }
    }
}
