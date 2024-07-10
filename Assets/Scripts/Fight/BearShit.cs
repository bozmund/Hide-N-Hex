using Items;
using UnityEngine;

public class BearShit : MonoBehaviour
{
    public MainInventory MainInventoryData;
    public InventoryUIManager inventoryUIManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var poopCount = MainInventoryData.GetSlotAndCountForItem("bearPoop", out var itemNumber);
            poopCount += 1;
            MainInventoryData.UpdateMainInventory(itemNumber, "bearPoop", poopCount);
            inventoryUIManager.LoadInventorySprites();
            gameObject.SetActive(false); ;
        }
    }
}
