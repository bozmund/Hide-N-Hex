using Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems; // Required for event handling
using UnityEngine.UI;

public class InventoryItemClickHandler : MonoBehaviour, IPointerClickHandler
{
    public MainInventory mainInventoryData; // Reference to the ScriptableObject

    // This function is called when the user clicks on the UI element
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the click was a right-click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            string itemNumber = gameObject.name;
            // Find the inventory slot by item number and update it
            mainInventoryData.UpdateMainInventory(itemNumber, "", 0);

            // Clear the sprite and text of the UI element
            ClearUIElement(itemNumber);
        }
    }

    private void ClearUIElement(string itemNumber)
    {
        Image itemImage = GetComponent<Image>();
        if (itemImage != null)
        {
            InventoryUIManager uiManager = FindObjectOfType<InventoryUIManager>();
            TextMeshProUGUI itemCountText = uiManager?.GetTextBySlotName(itemNumber);
            int itemCount = 1; // Default to 1 if itemCountText is null or empty
            if (itemCountText != null && int.TryParse(itemCountText.text, out int parsedCount))
            {
                itemCount = parsedCount;
            }

            // Find the DroppedItems GameObject in the current scene
            GameObject droppedItemsParent = GameObject.Find("DroppedItems");
            if (droppedItemsParent == null)
            {
                Debug.LogError("DroppedItems GameObject not found in the scene.");
                return;
            }

            // Find the Player GameObject in the current scene
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player GameObject not found in the scene.");
                return;
            }

            Vector3 playerPosition = player.transform.position;
            Vector3 dropPosition = new Vector3(playerPosition.x + 0.8f, playerPosition.y, playerPosition.z); // Adjust this value to set how far to the right of the player the items will be dropped

            for (int i = 0; i < itemCount; i++)
            {
                // Create a new GameObject with the name of the sprite
                string newObjectName = itemImage.sprite.name;
                GameObject newObject = new GameObject(newObjectName);

                // Set the position to a little bit to the right of the player
                newObject.transform.position = dropPosition;

                // Set the parent of the new object to DroppedItems
                newObject.transform.SetParent(droppedItemsParent.transform);

                // Add a SpriteRenderer to see the sprite and set the sprite
                SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = itemImage.sprite;

                // Set the order in layer
                spriteRenderer.sortingOrder = 1;

                // Set the tag to "DisappearObject"
                newObject.tag = "DisappearObject";

                // Add a BoxCollider2D with size 0.5 by 0.5
                BoxCollider2D boxCollider = newObject.AddComponent<BoxCollider2D>();
                boxCollider.size = new Vector2(0.5f, 0.5f);
            }

            // Clear the sprite of the UI element
            itemImage.sprite = null;

            // Clear the text of the UI element
            if (itemCountText != null)
            {
                itemCountText.text = "";
            }
        }
    }
}