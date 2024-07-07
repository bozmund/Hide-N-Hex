using UnityEngine;

public class PopUpSoundHandler : MonoBehaviour
{
    private AudioSource popUpSoundAudio;

    void Start()
    {
        // Find the "MinorSounds" GameObject
        GameObject minorSounds = GameObject.Find("MinorSounds");

        if (minorSounds != null)
        {
            // Find the "PopUpSound" GameObject inside "MinorSounds"
            GameObject popUpSound = minorSounds.transform.Find("PopUpSound").gameObject;

            if (popUpSound != null)
            {
                // Get the AudioSource component
                popUpSoundAudio = popUpSound.GetComponent<AudioSource>();

                if (popUpSoundAudio == null)
                {
                    Debug.LogWarning("PopUpSound GameObject does not have an AudioSource component.");
                }
            }
            else
            {
                Debug.LogWarning("Could not find GameObject named 'PopUpSound' inside 'MinorSounds'.");
            }
        }
        else
        {
            Debug.LogWarning("Could not find GameObject named 'MinorSounds'.");
        }
    }

    void Update()
    {
        // Check for Esc or I key presses
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I))
        {
            if (popUpSoundAudio != null)
            {
                popUpSoundAudio.Play();
            }
        }
    }
}
