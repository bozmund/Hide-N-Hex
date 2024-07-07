using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Items;

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
            return;
        }

        // Convert the world position of the game object to screen position
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(draggedItem.transform.position);

        // Get all UI elements in the scene
        Canvas[] canvases = FindObjectsOfType<Canvas>();

        bool collided = false;

        foreach (Canvas canvas in canvases)
        {
            Image[] images = canvas.GetComponentsInChildren<Image>();

            foreach (Image image in images)
            {
                // Check if the UI element's name contains "Item"
                if (image.name.Contains("Item") && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, screenPosition, mainCamera))
                {
                    Debug.Log("Collision detected with UI element: " + image.name);

                    // Find the TextMeshPro child object
                    TextMeshProUGUI textMeshProChild = image.GetComponentInChildren<TextMeshProUGUI>();

                    var SecondNull = false;

                    if (textMeshProChild != null)
                    {
                        Debug.Log("TextMeshPro child object name: " + textMeshProChild.gameObject.name);
                    }

                    if (image.sprite == null)
                    {
                        MainInventoryData.UpdateMainInventory(imageComponent.name, "", 0);
                        SecondNull = true;
                    }
                    else if (int.TryParse(textMeshProChild.text, out int itemCountSecond))
                    {
                        MainInventoryData.UpdateMainInventory(imageComponent.name, image.sprite.name, itemCountSecond);
                    }

                    if (int.TryParse(textMeshPro.text, out int itemCount))
                    {
                        MainInventoryData.UpdateMainInventory(image.name, imageComponent.sprite.name, itemCount);
                    }

                    inventoryUIManager.LoadInventorySprites();

                    if (SecondNull == true)
                    {
                        imageComponent.sprite = null;
                    }

                    collided = true;
                    break;
                }
            }

            if (collided)
            {
                break;
            }
        }

        // Destroy the draggedItem if collision detected or not
        Destroy(draggedItem);
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
