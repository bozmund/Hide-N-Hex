using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCabin : MonoBehaviour
{
    public GameObject objectToToggle; // Public reference to the game object to be activated/deactivated
    private bool playerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(true); // Set the object to active when the player is in range
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (objectToToggle != null)
            {
                objectToToggle.SetActive(false); // Set the object to inactive when the player is not in range
            }
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            //Debug.Log("Kliknuto");
            SceneManager.LoadScene("Cabin");
        }
    }
}
