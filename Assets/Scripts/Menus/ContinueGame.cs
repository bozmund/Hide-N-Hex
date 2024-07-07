using UnityEngine;
using UnityEngine.UI;

public class ContinueGame: MonoBehaviour
{
    public GameObject gameObjectToToggle;

    // Attach this method to a Unity UI Button's OnClick event in the Inspector
    public void ToggleGameObjectActivation()
    {
        if (gameObjectToToggle != null)
        {
            gameObjectToToggle.SetActive(!gameObjectToToggle.activeSelf);
        }
        else
        {
            Debug.LogWarning("GameObject to toggle is not assigned!");
        }
    }
}
