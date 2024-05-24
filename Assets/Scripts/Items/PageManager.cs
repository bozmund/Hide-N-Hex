using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PageManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject firstPage;
    public GameObject secondPage;
    public GameObject thirdPage;
    public GameObject fourthPage;

    // This function will be called when the user clicks on the UI element this script is attached to
    public void OnPointerClick(PointerEventData eventData)
    {
        // Make the first and second pages invisible
        firstPage.SetActive(false);
        secondPage.SetActive(false);

        // Make the third and fourth pages visible
        thirdPage.SetActive(true);
        fourthPage.SetActive(true);
    }
}
