using UnityEngine;
using UnityEngine.UI;

public class SuspicionBar : MonoBehaviour
{
    public Image uiImage;
    public SuspicionValue fillAmountData;

    void Update()
    {
        if (uiImage != null && fillAmountData != null)
        {
            uiImage.fillAmount = fillAmountData.fillAmount;
        }
    }
}
