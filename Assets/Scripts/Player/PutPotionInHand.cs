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
        if (potionInHand == null)
        {
            //Debug.LogWarning("PotionInHand is null. Setting sprite to none.");
            spriteRenderer.sprite = null;
        }
        else if (potionInHand.potionName == null)
        {
            //Debug.LogWarning("PotionInHand.potionName is null. Setting sprite to none.");
            spriteRenderer.sprite = null;
        }
        else if (potionInHand.potionName.Contains("Potion"))
        {
            // Construct the path to the sprite in the Resources folder
            string spritePath = spriteSubfolder + "/" + potionInHand.potionName;
            Sprite newSprite = Resources.Load<Sprite>(spritePath);

            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                spriteRenderer.sprite = null;
            }
        }
        else
        {
            // Set the sprite to none if potionInHand does not contain "Potion"
            spriteRenderer.sprite = null;
        }
    }
}
