using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class UISpriteNamePopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text popupText;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        popupText.gameObject.SetActive(false); // Hide popup text initially
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image.sprite != null)
        {
            // Insert a space before each uppercase letter
            string formattedName = Regex.Replace(image.sprite.name, "(\\B[A-Z])", " $1");
            popupText.text = formattedName; // Set the text to the formatted sprite name
            popupText.gameObject.SetActive(true); // Show the popup text
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popupText.gameObject.SetActive(false); // Hide the popup text
    }
}
