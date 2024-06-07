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
            itemImage.sprite = null;
        }

        InventoryUIManager uiManager = FindObjectOfType<InventoryUIManager>();
        if (uiManager != null)
        {
            TextMeshProUGUI itemCountText = uiManager.GetTextBySlotName(itemNumber);
            if (itemCountText != null)
            {
                itemCountText.text = "";
            }
        }
    }
}
