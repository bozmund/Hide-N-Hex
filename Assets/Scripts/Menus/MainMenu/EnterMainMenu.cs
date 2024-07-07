using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterMainMenu : MonoBehaviour
{
    public EnableStartButtonStatus enableStartButtonStatus; // Reference to the EnableStartButtonStatus ScriptableObject

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (enableStartButtonStatus != null)
            {
                enableStartButtonStatus.StartButtonStatus = "disabled"; // Set StartButtonStatus to "disabled"
            }
            SceneManager.LoadScene("MainMenu");
        }
    }
}
