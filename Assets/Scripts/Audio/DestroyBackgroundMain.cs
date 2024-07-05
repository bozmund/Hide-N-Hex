using UnityEngine;

public class DestroyBackgroundMusicMain : MonoBehaviour
{
    void Start()
    {
        // Find the GameObject named "BackgroundMusicCutscene"
        GameObject musicObject = GameObject.Find("BackgroundMusicMain");

        // Check if the object exists
        if (musicObject != null)
        {
            // Destroy the GameObject
            Destroy(musicObject);
            Debug.Log("BackgroundMusicCutscene has been destroyed.");
        }
        else
        {
            Debug.Log("BackgroundMusicCutscene not found.");
        }
    }
}
