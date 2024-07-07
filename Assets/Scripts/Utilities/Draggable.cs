using UnityEngine;
using UnityEngine.UI;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private bool checkCollision = false; // Flag to check collision after dragging stops
    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    void OnMouseUp()
    {
        isDragging = false;
        checkCollision = true; // Set flag to check collision after dragging stops
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }

        if (checkCollision && !isDragging)
        {
            CheckUIElementCollision();
            checkCollision = false; // Reset flag after checking collision
        }
    }

    private void CheckUIElementCollision()
    {
        // Convert the world position of the game object to screen position
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

        // Get all UI elements in the scene
        Canvas[] canvases = FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in canvases)
        {
            Image[] images = canvas.GetComponentsInChildren<Image>();

            foreach (Image image in images)
            {
                // Check if the UI element's name contains "Item"
                if (image.name.Contains("Item") && RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, screenPosition, mainCamera))
                {
                    Debug.Log("Collision detected with UI element: " + image.name);
                    Destroy(gameObject); // Destroy this GameObject when collision is detected
                    return; // Exit the method after destroying the GameObject
                }
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
