using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image uiImage;
    public HealthValue fillAmountData;

    void Update()
    {
        if (uiImage != null && fillAmountData != null)
        {
            uiImage.fillAmount = fillAmountData.fillAmount;
        }
    }
}
