using UnityEngine;
using UnityEngine.UI;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject mainImage; // Reference to the Image that controls the others
    public GameObject[] dependentImages; // References to the Images to toggle

    public void ToggleImage()
    {
        if (mainImage != null)
        {
            // Check current active state of the main Image
            bool isActive = mainImage.activeSelf;

            // Toggle the main Image
            mainImage.SetActive(!isActive);

            // Toggle dependent Images based on the main Image's new state
            foreach (GameObject img in dependentImages)
            {
                if (img != null)
                {
                    img.SetActive(isActive); // Set dependent Images to the opposite state
                }
            }
        }
    }
}
