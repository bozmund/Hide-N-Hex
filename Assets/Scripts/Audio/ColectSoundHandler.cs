using UnityEngine;
using System.Collections.Generic;

public class ColectSoundHandler : MonoBehaviour
{
    public MainInventory mainInventory; // Reference to the MainInventory ScriptableObject
    private AudioSource colectSoundAudio;
    private List<int> previousCounts;

    void Start()
    {
        // Find the "MinorSounds" GameObject
        GameObject minorSounds = GameObject.Find("MinorSounds");

        if (minorSounds != null)
        {
            // Find the "ColectSound" GameObject inside "MinorSounds"
            GameObject colectSound = minorSounds.transform.Find("ColectSound").gameObject;

            if (colectSound != null)
            {
                // Get the AudioSource component
                colectSoundAudio = colectSound.GetComponent<AudioSource>();

                if (colectSoundAudio == null)
                {
                    Debug.LogWarning("ColectSound GameObject does not have an AudioSource component.");
                }
            }
            else
            {
                Debug.LogWarning("Could not find GameObject named 'ColectSound' inside 'MinorSounds'.");
            }
        }
        else
        {
            Debug.LogWarning("Could not find GameObject named 'MinorSounds'.");
        }

        // Initialize the previous counts list with the current counts
        previousCounts = new List<int>();
        foreach (var slot in mainInventory.slots)
        {
            previousCounts.Add(slot.count);
        }
    }

    void Update()
    {
        CheckInventoryCounts();
    }

    void CheckInventoryCounts()
    {
        for (int i = 0; i < mainInventory.slots.Count; i++)
        {
            int currentCount = mainInventory.slots[i].count;

            if (currentCount > previousCounts[i])
            {
                PlayColectSound();
                previousCounts[i] = currentCount;
            }
        }
    }

    void PlayColectSound()
    {
        if (colectSoundAudio != null)
        {
            colectSoundAudio.Play();
        }
    }
}
