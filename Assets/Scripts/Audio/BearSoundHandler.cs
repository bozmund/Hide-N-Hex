using UnityEngine;

public class BearSoundHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AudioSource bearAudioSource;
    private string[] stopWords = new string[] { "dead", "sleep" };

    private void Awake()
    {
        // Find the MinoSounds GameObject in the scene
        GameObject minoSounds = GameObject.Find("MinorSounds");

        if (minoSounds != null)
        {
            // Find the BearSound child object
            Transform bearSoundTransform = minoSounds.transform.Find("BearSound");

            if (bearSoundTransform != null)
            {
                // Get the AudioSource component from the BearSound GameObject
                bearAudioSource = bearSoundTransform.GetComponent<AudioSource>();
            }
            else
            {
                Debug.LogError("BearSound child object not found!");
            }
        }
        else
        {
            Debug.LogError("MinoSounds GameObject not found!");
        }

        // Get the SpriteRenderer component from the GameObject this script is attached to
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found!");
        }
    }

    private void Update()
    {
        // Ensure the spriteRenderer and bearAudioSource are not null
        if (spriteRenderer != null && bearAudioSource != null)
        {
            // Get the name of the current sprite
            string spriteName = spriteRenderer.sprite.name;

            // Check if the sprite name contains any of the stop words
            bool shouldStop = false;
            foreach (string stopWord in stopWords)
            {
                if (spriteName.Contains(stopWord))
                {
                    shouldStop = true;
                    break;
                }
            }

            // Play or stop the audio based on the sprite name
            if (shouldStop)
            {
                if (bearAudioSource.isPlaying)
                {
                    bearAudioSource.Stop();
                }
            }
            else
            {
                if (!bearAudioSource.isPlaying)
                {
                    bearAudioSource.Play();
                }
            }
        }
    }
}
