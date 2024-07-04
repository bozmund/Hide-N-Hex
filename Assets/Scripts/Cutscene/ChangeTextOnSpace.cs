using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeTextOnSpace : MonoBehaviour
{
    private Text uiText;
    private int pressCount = 0;

    void Start()
    {
        uiText = GetComponent<Text>();
        if (uiText == null)
        {
            Debug.LogError("No Text component found on this GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressCount++;
            switch (pressCount)
            {
                case 1:
                    uiText.text = "This is the second text.";
                    break;
                case 2:
                    uiText.text = "This is the third text.";
                    break;
                case 3:
                    uiText.text = "This is the fourth text.";
                    break;
                case 4:
                    SceneManager.LoadScene("SecondCutscene");
                    break;
                default:
                    break;
            }
        }
    }
}
