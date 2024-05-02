using UnityEngine;

public class ToggleMultipleImages : MonoBehaviour
{
    public GameObject[] imagesToShow; // References to the Images to display
    public GameObject imageToHide; // Reference to the Image to hide

    public void ToggleDisplay()
    {
        if (imageToHide != null && imagesToShow != null)
        {
            // Check the current visibility of one of the images to show
            bool showImages = !imagesToShow[0].activeSelf;

            // Show or hide the images to display
            foreach (GameObject img in imagesToShow)
            {
                if (img != null)
                {
                    img.SetActive(showImages);
                }
            }

            // Hide or show the image to hide, based on the inverse condition
            imageToHide.SetActive(!showImages);
        }
    }
}
