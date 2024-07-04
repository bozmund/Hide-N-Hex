using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckGameOver: MonoBehaviour
{
    public HealthValue healthValue;
    public SuspicionValue suspicionValue;

    void Update()
    {
        if (healthValue.fillAmount == 0)
        {
            SceneManager.LoadScene("GameOverHealth");
        }
        else if (suspicionValue.fillAmount == 1)
        {
            SceneManager.LoadScene("GameOverSuspicion");
        }
    }
}
