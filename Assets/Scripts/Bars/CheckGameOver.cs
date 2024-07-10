using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckGameOver: MonoBehaviour
{
    public HealthValue healthValue;
    public SuspicionValue suspicionValue;
    public EnableStartButtonStatus enableStartButtonStatus;

    void Update()
    {
        if (healthValue.fillAmount < 0.01)
        {
            enableStartButtonStatus.StartButtonStatus = "disabled";
            SceneManager.LoadScene("GameOverHealth");
        }
        else if (suspicionValue.fillAmount > 0.98)
        {
            enableStartButtonStatus.StartButtonStatus = "disabled";
            SceneManager.LoadScene("GameOverSuspicion");
        }
    }
}
