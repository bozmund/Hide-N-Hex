using UnityEngine;

public class CatAlertAudio : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AudioSource catSoundAudioSource;

    void Start()
    {
        // Find the "MinorSounds" GameObject
        GameObject minorSoundsObject = GameObject.Find("MinorSounds");
        if (minorSoundsObject == null)
        {
            Debug.LogError("MinorSounds GameObject not found in the scene.");
            return;
        }

        // Find the "CatSound" child GameObject
        Transform catSoundTransform = minorSoundsObject.transform.Find("CatSound");
        if (catSoundTransform == null)
        {
            Debug.LogError("CatSound child GameObject not found under MinorSounds.");
            return;
        }

        // Get the AudioSource component from the "CatSound" GameObject
        catSoundAudioSource = catSoundTransform.GetComponent<AudioSource>();
        if (catSoundAudioSource == null)
        {
            Debug.LogError("AudioSource component not found on CatSound GameObject.");
            return;
        }

        // Get the SpriteRenderer component from the GameObject this script is attached to
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
            return;
        }
    }

    void Update()
    {
        // Continuously check the sprite name in the SpriteRenderer
        if (spriteRenderer.sprite != null)
        {
            string spriteName = spriteRenderer.sprite.name;
            if (spriteName == "buć_alert_0" || spriteName == "buć_alert_1")
            {
                if (!catSoundAudioSource.isPlaying)
                {
                    catSoundAudioSource.Play();
                }
            }
            else
            {
                if (catSoundAudioSource.isPlaying)
                {
                    catSoundAudioSource.Stop();
                }
            }
        }
    }
}
