using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D idleCursor;
    public Texture2D pointerCursor;
    private static CustomCursor instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Set the idle cursor as the default cursor with the hotspot at the center
        Vector2 idleHotspot = new Vector2(idleCursor.width / 2, idleCursor.height / 2);
        Cursor.SetCursor(idleCursor, idleHotspot, CursorMode.Auto);
        Cursor.visible = true; // Ensure the cursor is visible
    }

    void Update()
    {
        // Example condition to change the cursor
        if (Input.GetMouseButton(0)) // Left mouse button pressed
        {
            Vector2 pointerHotspot = new Vector2(pointerCursor.width / 2, pointerCursor.height / 2);
            Cursor.SetCursor(pointerCursor, pointerHotspot, CursorMode.Auto);
        }
        else
        {
            Vector2 idleHotspot = new Vector2(idleCursor.width / 2, idleCursor.height / 2);
            Cursor.SetCursor(idleCursor, idleHotspot, CursorMode.Auto);
        }

        // Ensure the cursor is visible
        if (!Cursor.visible)
        {
            Cursor.visible = true;
        }
    }
}
