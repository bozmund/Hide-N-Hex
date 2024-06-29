using UnityEngine;
using UnityEngine.UI;

namespace Bars
{
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

        public void DecreaseHealth(float amount = 0.1f)
        {
            fillAmountData.fillAmount -= amount;
        }

        public void IncreaseHealth(float amount = 0.1f)
        {
            fillAmountData.fillAmount += amount;
        }
    }
}
