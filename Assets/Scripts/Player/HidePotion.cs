using UnityEngine;

public class HidePotion: MonoBehaviour
{
    public GameObject potion; // Public reference to the Potion GameObject
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer potionSpriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component attached to the player
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        if (playerSpriteRenderer == null)
        {
            Debug.LogError("Player does not have a SpriteRenderer component.");
            return;
        }

        if (potion != null)
        {
            // Get the SpriteRenderer component attached to the potion
            potionSpriteRenderer = potion.GetComponent<SpriteRenderer>();

            if (potionSpriteRenderer == null)
            {
                Debug.LogError("Potion does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError("Potion GameObject reference is not set.");
        }
    }

    void Update()
    {
        if (playerSpriteRenderer != null && potionSpriteRenderer != null)
        {
            string playerSpriteName = playerSpriteRenderer.sprite.name;

            // Check if the sprite name contains "back"
            bool containsBack = playerSpriteName.ToLower().Contains("back");

            Color potionColor = potionSpriteRenderer.color;

            if (containsBack)
            {
                // Set alpha to 0
                potionColor.a = 0;
            }
            else
            {
                // Set alpha to max (1)
                potionColor.a = 1;
            }

            potionSpriteRenderer.color = potionColor;
        }
    }
}
