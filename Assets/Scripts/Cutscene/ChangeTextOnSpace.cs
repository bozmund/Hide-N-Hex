using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeTextOnSpace : MonoBehaviour
{
    private Text uiText;
    private int pressCount = 0;
    public Text speakerName;

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
                    speakerName.text = "Granny Willow";
                    uiText.text = "You have given me many years of joy... I'm so sorry. ";
                    break;
                case 2:
                    uiText.text = "My time draws near and I must leave you.";
                    break;
                case 3:
                    uiText.text = "There is one more thing I must tell you...";
                    break;
                case 4:
                    uiText.text = "We come from a long line of witches, stretching back generations. You must stay here and fulfill your duty. ";
                    break;
                case 5:
                    uiText.text = "You must save our reputation... for your sake.";
                    break;
                case 6:
                    uiText.text = "Don't let them find out what you are, they will end you.";
                    break;
                case 7:
                    uiText.text = ".... Farewell ....";
                    break;
                case 8:
                    speakerName.text = "Luna the cat";
                    uiText.text = "<i>purrs</i>";
                    break;
                case 9:
                    speakerName.text = "Misty";
                    uiText.text = "What... just... happened?";
                    break;
                case 10:
                    SceneManager.LoadScene("SecondCutscene");
                    break;
                default:
                    break;
            }
        }
    }
}
