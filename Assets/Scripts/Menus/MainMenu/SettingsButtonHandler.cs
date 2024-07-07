using UnityEngine;
using UnityEngine.UIElements;

public class SettingsButtonHandler : MonoBehaviour
{
    public GameObject settingsPanel; // Public reference to the GameObject

    void Start()
    {
        // Get the root visual element from the UIDocument component
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Find the button with the name "Settings"
        var settingsGameButton = root.Q<Button>("Settings");

        // Register a callback to the button's click event
        if (settingsGameButton != null)
        {
            settingsGameButton.clicked += OnSettingsButtonClick;
        }
        else
        {
            Debug.LogWarning("Settings button not found in the UI document.");
        }
    }

    void OnSettingsButtonClick()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true); // Set the referenced GameObject active
        }
        else
        {
            Debug.LogWarning("Settings panel GameObject reference is not assigned.");
        }
    }
}
