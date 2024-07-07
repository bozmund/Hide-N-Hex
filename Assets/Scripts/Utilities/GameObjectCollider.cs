using UnityEngine;
using UnityEngine.UI;

public class GameObjectCollider : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        CheckUIElementCollision();
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
                }
            }
        }
    }
}
