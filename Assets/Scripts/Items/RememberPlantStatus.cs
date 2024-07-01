using UnityEngine;

public class RememberPlantState : MonoBehaviour
{
    public GameObject objectToActivate;
    private string playerPrefsKey;

    void Start()
    {
        // Generate a unique key for PlayerPrefs based on the name of the GameObject
        playerPrefsKey = gameObject.name + "_ActiveState";

        // Check PlayerPrefs for saved state
        int savedState = PlayerPrefs.GetInt(playerPrefsKey, 1); // Default to active (1) if key doesn't exist

        // Set the initial state based on savedState
        gameObject.SetActive(savedState == 1); // 1 means active, 0 means inactive
    }

    void OnDisable()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // Always activate the referenced object
        }

        // Save the current active state
        int currentState = gameObject.activeSelf ? 1 : 0; // 1 for active, 0 for inactive
        PlayerPrefs.SetInt(playerPrefsKey, currentState);
        PlayerPrefs.Save();
    }

    void OnDestroy()
    {
        // Ensure PlayerPrefs are saved when the game object is destroyed
        PlayerPrefs.Save();
    }
}
