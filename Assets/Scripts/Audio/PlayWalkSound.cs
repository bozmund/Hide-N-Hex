using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class PlayWalkSound : MonoBehaviour
{
    public GameObject player; // Public reference to the Player GameObject
    private GameObject minorSounds;
    private AudioSource walkFloorAudio;
    private AudioSource windAudio;

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

            // Only search for "WindSound" if the current scene is "OutsideTheCabin"
            if (SceneManager.GetActiveScene().name == "OutsideTheCabin")
            {
                // Find the "WindSound" GameObject inside "MinorSounds"
                GameObject windSound = minorSounds.transform.Find("WindSound").gameObject;
                if (windSound != null)
                {
                    // Get the AudioSource component
                    windAudio = windSound.GetComponent<AudioSource>();
                    if (windAudio == null)
                    {
                        Debug.LogWarning("WindSound GameObject does not have an AudioSource component.");
                    }
                }
                else
                {
                    Debug.LogWarning("Could not find GameObject named 'WindSound' inside 'MinorSounds'.");
                }
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

        if (player != null)
        {
            SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                string spriteName = spriteRenderer.sprite.name;

                // Check if the sprite name contains "broom"
                bool containsBroom = spriteName.ToLower().Contains("broom");

                if (containsBroom && isWalking)
                {
                    if (windAudio != null && !windAudio.isPlaying)
                    {
                        windAudio.Play();
                    }

                    if (walkFloorAudio != null && walkFloorAudio.isPlaying)
                    {
                        walkFloorAudio.Pause();
                    }
                }
                else
                {
                    if (windAudio != null && windAudio.isPlaying)
                    {
                        windAudio.Pause();
                    }

                    if (isWalking && !containsBroom)
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
            else
            {
                Debug.LogWarning("Player GameObject does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogWarning("Player GameObject reference is not set.");
        }
    }
}
