using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public VolumeValue volumeValue;

    private void Start()
    {
        // Initialize the slider value based on the scriptable object's value
        UpdateSliderValue();

        // Add listener to update the scriptable object when the slider value changes
        volumeSlider.onValueChanged.AddListener(UpdateVolumeValue);
    }

    private void UpdateSliderValue()
    {
        volumeSlider.value = volumeValue.fillAmount;
    }

    private void UpdateVolumeValue(float value)
    {
        volumeValue.fillAmount = value;
    }
}
