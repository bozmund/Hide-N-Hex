using UnityEngine;
using UnityEngine.UIElements;

public class CheckStartButtonStatus : MonoBehaviour
{
    public EnableStartButtonStatus enableStartButtonStatus; // Reference to the EnableStartButtonStatus ScriptableObject

    void Start()
    {
        if (enableStartButtonStatus == null)
        {
            Debug.LogError("No EnableStartButtonStatus ScriptableObject assigned in the Inspector.");
            return;
        }

        var root = GetComponent<UIDocument>().rootVisualElement;

        var startGameButton = root.Q<Button>("StartGame");

        if (startGameButton == null)
        {
            Debug.LogError("No StartGame button found in the UI Document.");
            return;
        }

        if (enableStartButtonStatus.StartButtonStatus == "enabled")
        {
            startGameButton.visible = true;
        }
        else if (enableStartButtonStatus.StartButtonStatus == "disabled")
        {
            startGameButton.visible = false;
        }
        else
        {
            Debug.LogError("Invalid StartButtonStatus value. It should be either 'enabled' or 'disabled'.");
        }
    }
}
