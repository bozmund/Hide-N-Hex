using UnityEngine;

public class MouseClickSound: MonoBehaviour
{
    private AudioSource mouseSoundAudio;

    void Start()
    {
        // Find the "MinorSounds" GameObject
        GameObject minorSounds = GameObject.Find("MinorSounds");

        if (minorSounds != null)
        {
            // Find the "MouseSound" GameObject inside "MinorSounds"
            GameObject mouseSound = minorSounds.transform.Find("MouseSound").gameObject;

            if (mouseSound != null)
            {
                // Get the AudioSource component
                mouseSoundAudio = mouseSound.GetComponent<AudioSource>();

                if (mouseSoundAudio == null)
                {
                    Debug.LogWarning("MouseSound GameObject does not have an AudioSource component.");
                }
            }
            else
            {
                Debug.LogWarning("Could not find GameObject named 'MouseSound' inside 'MinorSounds'.");
            }
        }
        else
        {
            Debug.LogWarning("Could not find GameObject named 'MinorSounds'.");
        }
    }

    void Update()
    {
        // Check for left or right mouse button clicks
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (mouseSoundAudio != null)
            {
                mouseSoundAudio.Play();
            }
        }
    }
}
