using UnityEngine;
using UnityEngine.UI;

namespace CraftingSystem
{
    public class CircleFillHandler : MonoBehaviour
    {
        public Image circleFillImage;
        public RectTransform handlerEdgeImage;
        public RectTransform fillHandler;
        public Image Q;
        public Image W;
        public Image E;
        public Image Heat;
        public Image OptimalHeat;

        private float fillSpeed = 0.3f;
        private float targetFillAmount = 1.0f;
        private float decreaseRate = 0.1f;
        private float increaseRate = 0.4f;

        private void Start()
        {
            circleFillImage.fillAmount = 0.0f;
            Heat.fillAmount = 0;

            // Randomly place the OptimalHeat image within 0 to 80% of the HeatBar
            var optimalPositionY = Random.Range(0, 80);
            OptimalHeat.rectTransform.anchoredPosition =
                new Vector2(OptimalHeat.rectTransform.anchoredPosition.x, optimalPositionY);
        }

        private void Update()
        {
            circleFillImage.fillAmount += fillSpeed * Time.deltaTime;

            if (circleFillImage.fillAmount >= targetFillAmount)
            {
                circleFillImage.fillAmount = 0;
            }

            if (Input.GetKeyDown(KeyCode.Q) && circleFillImage.fillAmount >= 0.1f &&
                circleFillImage.fillAmount <= 0.25f)
            {
                Q.color = Color.green;
            }

            if (Input.GetKeyDown(KeyCode.W) && circleFillImage.fillAmount >= 0.4f &&
                circleFillImage.fillAmount <= 0.6f)
            {
                W.color = Color.green;
            }

            if (Input.GetKeyDown(KeyCode.E) && circleFillImage.fillAmount >= 0.75f &&
                circleFillImage.fillAmount <= 0.9f)
            {
                E.color = Color.green;
            }

            Heat.fillAmount -= decreaseRate * Time.deltaTime;

            if (Input.GetKey(KeyCode.Space))
            {
                Heat.fillAmount += increaseRate * Time.deltaTime;
            }

            Heat.fillAmount = Mathf.Clamp(Heat.fillAmount, 0, 1);
        }
    }
}