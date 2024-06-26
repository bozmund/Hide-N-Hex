using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCaveMainRoom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Load the scene
            SceneManager.LoadScene("BearCaveMainRoom");
        }
    }
}
