using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Items;
using Unity.VisualScripting;
using System.Linq;

public class UIDragHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private float longPressDuration = 0.1f;
    private bool isPointerDown = false;
    private float pointerDownTimer = 0f;
    private Sprite spriteToUse;
    private Image imageComponent;
    public TextMeshProUGUI textMeshPro;
    private GameObject draggedItem; // Reference to the currently dragged item GameObject
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    public MainInventory MainInventoryData;
    public InventoryUIManager inventoryUIManager;

    void Start()
    {
        imageComponent = GetComponent<Image>();

        // Attempt to get the TextMeshProUGUI component
        if (imageComponent != null)
        {
            textMeshPro = imageComponent.GetComponentInChildren<TextMeshProUGUI>();
        }

        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isPointerDown && !isDragging)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= longPressDuration)
            {
                if (spriteToUse != null)
                {
                    CreateDraggedItem();
                    isDragging = true;
                }
                ResetPointer();
            }
        }

        // Handle dragging of the draggedItem
        if (isDragging && draggedItem != null)
        {
            draggedItem.transform.position = GetMouseWorldPosition() + offset;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (imageComponent.sprite == null)
        {
            Debug.LogWarning("Cannot drag, Image component does not have a sprite assigned.");
            return;
        }

        isPointerDown = true;
        LoadSprite();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging && draggedItem != null)
        {
            CheckUIElementCollision(); // Check for collision when releasing the pointer
        }

        ResetPointer();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && draggedItem != null)
        {
            draggedItem.transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void ResetPointer()
    {
        isPointerDown = false;
        pointerDownTimer = 0f;
    }

    private void LoadSprite()
    {
        if (imageComponent == null)
        {
            Debug.LogError("Image component not found.");
            return;
        }

        string spritePath = $"plants/{imageComponent.sprite.name}";
        spriteToUse = Resources.Load<Sprite>(spritePath);

        if (spriteToUse == null)
        {
            spritePath = $"Potions/{imageComponent.sprite.name}";
            spriteToUse = Resources.Load<Sprite>(spritePath);
        }

        if (spriteToUse == null)
        {
            Debug.LogError($"Sprite not found for {imageComponent.sprite.name} in Resources folders 'plants' or 'Potions'.");
        }
    }

    private void CreateDraggedItem()
    {
        if (spriteToUse == null)
        {
            Debug.LogError("Cannot create dragged item because spriteToUse is null.");
            return;
        }

        // Ensure only one dragged item is created at a time
        if (draggedItem != null)
        {
            Destroy(draggedItem);
        }

        // Instantiate a new GameObject for the dragged item
        draggedItem = new GameObject("DraggedItem");

        // Add a SpriteRenderer component to it
        SpriteRenderer spriteRenderer = draggedItem.AddComponent<SpriteRenderer>();

        // Assign the sprite to the SpriteRenderer
        spriteRenderer.sprite = spriteToUse;

        // Add a BoxCollider2D component to it
        BoxCollider2D boxCollider = draggedItem.AddComponent<BoxCollider2D>();

        // Set the sorting order
        spriteRenderer.sortingOrder = 5;

        // Set the position of the new object
        draggedItem.transform.position = GetMouseWorldPosition();

        // Create a child GameObject named "ItemCount"
        GameObject itemCountObject = new GameObject("ItemCount");

        // Attach the new "ItemCount" GameObject to the dragged item as a child
        itemCountObject.transform.parent = draggedItem.transform;

        // Add a TextMeshProUGUI component to display count
        TextMeshProUGUI itemCountText = itemCountObject.AddComponent<TextMeshProUGUI>();

        // Set the text from the TextMeshProUGUI component of imageComponent
        if (textMeshPro != null)
        {
            itemCountText.text = textMeshPro.text;
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI component not found.");
            itemCountText.text = "DefaultText";
        }

        // Set the position of the "ItemCount" relative to its parent
        itemCountObject.transform.localPosition = new Vector3(0f, 1f, 0f);

        // Add Draggable script component to the draggedItem GameObject
        draggedItem.AddComponent<UIDragHandler>(); // Add this script to handle further dragging
    }

    private void CheckUIElementCollision()
    {
        if (draggedItem == null)
        {
            Debug.Log("draggedItem is null, returning.");
            return;
        }

        // Convert the world position of the game object to screen position
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(draggedItem.transform.position);
        Debug.Log("Screen position of draggedItem: " + screenPosition);

        // Get all UI elements in the scene
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        Debug.Log("Found " + canvases.Length + " canvases in the scene.");

        bool collided = false;

        var aceptAll = new string[]
        {
        "firstItem", "secondItem", "thirdItem", "fourthItem", "fifthItem", "sixthItem", "seventhItem", "eighthItem"
        };

        var common = new string[]
        {
        "ninthItem", "tenthItem", "eleventhItem", "twelfthItem", "thirteenthItem", "fourteenthItem", "fifteenthItem",
        "sixteenthItem", "seventeenthItem", "eighteenthItem", "nineteenthItem", "twentiethItem", "twentyFirstItem",
        "twentySecondItem", "twentyThirdItem", "twentyFourthItem", "twentyFifthItem", "twentySixthItem",
        "twentySeventhItem", "twentyEighthItem", "twentyNinthItem", "thirtiethItem", "thirtyFirstItem",
        "thirtySecondItem", "thirtyThirdItem", "thirtyFourthItem", "thirtyFifthItem", "thirtySixthItem"
        };

        var rare = new string[]
        {
        "thirtySeventhItem", "thirtyEighthItem", "thirtyNinthItem", "fortiethItem", "fortyFirstItem", "fortySecondItem",
        "fortyThirdItem", "fortyFourthItem", "fortyFifthItem", "fortySixthItem", "fortySeventhItem", "fortyEighthItem",
        "fortyNinthItem", "fiftiethItem", "fiftyFirstItem", "fiftySecondItem", "fiftyThirdItem", "fiftyFourthItem",
        "fiftyFifthItem", "fiftySixthItem"
        };

        var commonSprite = new string[]
        {
        "blindweed", "dewcatcher", "earthroot", "fadeleaf", "firebloom", "icecap", "mageroyal", "rotberry", "sorrowmoss",
        "sungrass", "swiftthistle", "ConfusionPotion", "WeaknessPotion", "Potion", "Potion", "Potion", "Potion", "Potion",
        "PurificationPotion", "LevitationPotion", "RecallPotion", "UsefulnessPotion", "ClothingPotion", "HealingPotion",
        "StrengthPotion", "InvisibilityPotion", "MindVisionPotion", "LowerSusPotion", "ToxicGasPotion",
        "LiquidFlamePotion", "FrostPotion"
        };

        var rareSprite = new string[]
        {
        "starflower", "goldenLotus", "blandfruit", "stormvine", "bearPoop", "ParalyticGasPotion", "UselessnessPotion",
        "CatcallingPotion", "SwiftnessPotion", "MightPotion", "RepairPotion", "HolyGrailPotion"
        };

        foreach (Canvas canvas in canvases)
        {
            Image[] images = canvas.GetComponentsInChildren<Image>();
            Debug.Log("Found " + images.Length + " images in canvas: " + canvas.name);

            foreach (Image image in images)
            {
                // Check if the UI element's name contains "Item"
                if (image.name.Contains("Item") && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, screenPosition, mainCamera))
                {
                    Debug.Log("Collision detected with UI element: " + image.name);

                    bool collision = false;
                    Debug.Log("OVO" + image.sprite);

                    // Check if the image.name is in aceptAll
                    if (aceptAll.Contains(image.name) && aceptAll.Contains(imageComponent.name))
                    {
                        collision = true;
                    }

                    else if (aceptAll.Contains(image.name) && image.sprite == null)
                    {
                        collision = true;
                    }

                    else if (aceptAll.Contains(image.name) && commonSprite.Contains(image.sprite.name) && commonSprite.Contains(imageComponent.sprite.name))
                    {
                        collision = true;
                    }

                    else if (aceptAll.Contains(image.name) && rareSprite.Contains(image.sprite.name) && rareSprite.Contains(imageComponent.sprite.name))
                    {
                        collision = true;
                    }

                    // Check if the image.name is in common and imageComponent.sprite.name is in commonSprite
                    else if (common.Contains(image.name) && commonSprite.Contains(imageComponent.sprite.name))
                    {
                        collision = true;
                    }
                    // Check if the image.name is in rare and imageComponent.sprite.name is in rareSprite
                    else if (rare.Contains(image.name) && rareSprite.Contains(imageComponent.sprite.name))
                    {
                        collision = true;
                    }

                    if (collision)
                    {
                        // Find the TextMeshPro child object
                        TextMeshProUGUI textMeshProChild = image.GetComponentInChildren<TextMeshProUGUI>();
                        Debug.Log("TextMeshPro child object found: " + (textMeshProChild != null));

                        var SecondNull = false;

                        if (textMeshProChild != null)
                        {
                            Debug.Log("TextMeshPro child object name: " + textMeshProChild.gameObject.name);
                        }

                        if (image.sprite == null)
                        {
                            Debug.Log("Image sprite is null, updating inventory with empty data.");
                            MainInventoryData.UpdateMainInventory(imageComponent.name, "", 0);
                            SecondNull = true;
                        }
                        else if (int.TryParse(textMeshProChild.text, out int itemCountSecond))
                        {
                            Debug.Log("Parsed item count from TextMeshPro child: " + itemCountSecond);
                            MainInventoryData.UpdateMainInventory(imageComponent.name, image.sprite.name, itemCountSecond);
                        }

                        if (int.TryParse(textMeshPro.text, out int itemCount))
                        {
                            Debug.Log("Parsed item count from TextMeshPro: " + itemCount);
                            MainInventoryData.UpdateMainInventory(image.name, imageComponent.sprite.name, itemCount);
                        }

                        inventoryUIManager.LoadInventorySprites();

                        if (SecondNull == true)
                        {
                            Debug.Log("SecondNull is true, setting imageComponent sprite to null.");
                            imageComponent.sprite = null;
                        }

                        collided = true;
                        break;
                    }
                    else
                    {
                        Debug.Log("No collision: names or sprites are not in the appropriate arrays.");
                    }
                }
            }

            if (collided)
            {
                break;
            }
        }

        // Destroy the draggedItem if collision detected or not
        Destroy(draggedItem);
        Debug.Log("Dragged item destroyed.");
        draggedItem = null; // Clear draggedItem reference
        isDragging = false; // Reset dragging state
    }




    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(draggedItem.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
