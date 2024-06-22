using UnityEngine;

public class PotionSpriteManager : MonoBehaviour
{
    public PotionInHand potionInHand; // Reference to the ScriptableObject
    private SpriteRenderer spriteRenderer;
    public string spriteSubfolder = "Potions"; // Subfolder in Resources

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the GameObject.");
            return;
        }

        UpdateSprite();
    }

    void Update()
    {
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (potionInHand != null && potionInHand.potionName.Contains("Potion"))
        {
            string spritePath = spriteSubfolder + "/" + potionInHand.potionName;
            Sprite newSprite = Resources.Load<Sprite>(spritePath);

            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogError("Sprite not found in Resources with the path: " + spritePath);
            }
        }
    }
}
