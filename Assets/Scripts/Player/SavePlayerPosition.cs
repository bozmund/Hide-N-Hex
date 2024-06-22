using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerPosition : MonoBehaviour
{
    // Singleton instance
    public static SavePlayerPosition instance;

    private void Awake()
    {
        // Ensure only one instance of the script exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to save the player's position and scene
    public void SavePlayerState()
    {
        Vector3 playerPosition = transform.position;
        PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }

    // Method to load the player's position and scene
    public void LoadPlayerState()
    {
        if (PlayerPrefs.HasKey("SceneName"))
        {
            string sceneName = PlayerPrefs.GetString("SceneName");
            if (SceneManager.GetActiveScene().name != sceneName)
            {
                SceneManager.LoadScene(sceneName);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                LoadPlayerPosition();
            }
        }
    }

    // Method to load the player's position after scene has loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadPlayerPosition();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Method to load the player's position
    private void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            transform.position = new Vector3(x, y, z);
        }
    }

    // Call this method to save the player's state when the game is closed
    private void OnApplicationQuit()
    {
        SavePlayerState();
    }

    // Call this method to load the player's state when the game starts
    private void Start()
    {
        LoadPlayerState();
    }

    // Optional: Call this method to save the player's state when they move to a new scene
    private void OnDisable()
    {
        SavePlayerState();
    }
}
