using UnityEngine;
using UnityEngine.UI;

namespace Bars
{
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

        public void DecreaseSuspicion()
        {
            fillAmountData.fillAmount -= 0.1f;
        }
        
        public void IncreaseSuspicion()
        {
            fillAmountData.fillAmount += 0.05f;
        }

    }
}
