using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCabin : MonoBehaviour
{
    private bool playerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Kliknuto");
            SceneManager.LoadScene("Cabin"); // Replace "Cabin" with your scene name
        }
    }
}
