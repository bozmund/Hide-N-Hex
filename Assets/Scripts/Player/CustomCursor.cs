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
        // Set the idle cursor as the default cursor and ensure it is visible
        Cursor.SetCursor(idleCursor, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true; // Ensure the cursor is visible
    }

    void Update()
    {
        // Example condition to change the cursor
        if (Input.GetMouseButton(0)) // Left mouse button pressed
        {
            Cursor.SetCursor(pointerCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(idleCursor, Vector2.zero, CursorMode.Auto);
        }

        // Ensure the cursor is visible
        if (!Cursor.visible)
        {
            Cursor.visible = true;
        }
    }
}
