using UnityEngine;
using UnityEngine.UI;

namespace ProgressBar
{
    public class ProgressBar : MonoBehaviour
    {
        public int min;
        public int max;
        public int current;
        public Object fill;
        public Image mask;
        public Color color;

        // Start is called before the first frame update
        void Awake()
        {
            mask = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            GetCurrentFill();
        }

        private void GetCurrentFill()
        {
            float currentOffset = current - min;
            float maximumOffset = max - min;
            var fillAmount = currentOffset / maximumOffset;
            mask.fillAmount = fillAmount;
        }
    }
}
