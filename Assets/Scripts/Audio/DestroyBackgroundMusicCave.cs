using UnityEngine;

public class DestroyBackgroundMusicCave : MonoBehaviour
{
    void Start()
    {
        // Find the GameObject named "BackgroundMusicCutscene"
        GameObject musicObject = GameObject.Find("BackgroundMusicForCave");

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
