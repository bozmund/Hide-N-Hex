using UnityEngine;
using UnityEngine.UI;

public class ButtonCheckmarksStatus : MonoBehaviour
{
    // Public references to the UI Images
    public Image image1;
    public Image image2;
    public Image image3;

    // The button to control
    private Button button;

    void Start()
    {
        // Get the Button component attached to the same GameObject
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("No Button component found on this GameObject.");
            return;
        }
    }

    void Update()
    {
        // Continuously check the sprites' names and update the button's status
        UpdateButtonStatus();
    }

    // Function to check the sprites and update the button's status
    public void UpdateButtonStatus()
    {
        if (AreAllSpritesCheckSymbol())
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    // Function to check if all image sprites are named "checksymbol"
    private bool AreAllSpritesCheckSymbol()
    {
        return image1.sprite.name == "checksymbol" &&
               image2.sprite.name == "checksymbol" &&
               image3.sprite.name == "checksymbol";
    }
}
