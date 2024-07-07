using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVolumeController : MonoBehaviour
{
    public VolumeValue volumeValue; // Reference to the scriptable object
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Initialize the AudioSource volume based on the scriptable object's value
        UpdateAudioVolume();

        // Optionally, if you want to continuously monitor the scriptable object's value,
        // you could start a coroutine or update it in the Update method.
    }

    private void UpdateAudioVolume()
    {
        audioSource.volume = volumeValue.fillAmount;
    }

    // If you want to update the volume continuously (e.g., in real-time), you can use Update method.
    private void Update()
    {
        UpdateAudioVolume();
    }
}
