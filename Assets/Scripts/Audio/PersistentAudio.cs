using UnityEngine;

public class PersistentAudioManager : MonoBehaviour
{
    public static PersistentAudioManager Instance;
    private void Awake()
    {
        // Check for duplicate instances with the same name in the current scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
            else if (Instance != this && GameObject.Find(gameObject.name) != null)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        
    }
}
