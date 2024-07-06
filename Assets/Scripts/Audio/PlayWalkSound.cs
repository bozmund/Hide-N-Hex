using UnityEngine;

public class PlayWalkSound : MonoBehaviour
{
    private GameObject minorSounds;
    private AudioSource walkFloorAudio;

    void Start()
    {
        // Find the "MinorSounds" GameObject
        minorSounds = GameObject.Find("MinorSounds");

        if (minorSounds != null)
        {
            // Find the "WalkFloorSound" GameObject inside "MinorSounds"
            GameObject walkFloorSound = minorSounds.transform.Find("WalkSound").gameObject;

            if (walkFloorSound != null)
            {
                // Get the AudioSource component
                walkFloorAudio = walkFloorSound.GetComponent<AudioSource>();

                if (walkFloorAudio == null)
                {
                    Debug.LogWarning("WalkFloorSound GameObject does not have an AudioSource component.");
                }
            }
            else
            {
                Debug.LogWarning("Could not find GameObject named 'WalkSound' inside 'MinorSounds'.");
            }
        }
        else
        {
            Debug.LogWarning("Could not find GameObject named 'MinorSounds'.");
        }
    }

    void Update()
    {
        // Check for continuous key presses
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (isWalking)
        {
            if (walkFloorAudio != null && !walkFloorAudio.isPlaying)
            {
                walkFloorAudio.Play();
            }
        }
        else
        {
            if (walkFloorAudio != null && walkFloorAudio.isPlaying)
            {
                walkFloorAudio.Pause();
            }
        }
    }
}
