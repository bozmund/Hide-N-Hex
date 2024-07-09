using Items;
using UnityEngine;

public class BearShit : MonoBehaviour
{
    public MainInventory MainInventoryData;
    public InventoryUIManager inventoryUIManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var poopCount = MainInventoryData.GetSlotAndCountForItem("bearPoop", out var itemNumber);
        poopCount += 1;
        MainInventoryData.UpdateMainInventory(itemNumber, "bearPoop", poopCount);
        inventoryUIManager.LoadInventorySprites();
        gameObject.SetActive(false);
    }
}
