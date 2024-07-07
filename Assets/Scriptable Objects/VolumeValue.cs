using UnityEngine;

[CreateAssetMenu(fileName = "VolumeValue", menuName = "ScriptableObjects/VolumeValue", order = 1)]
public class VolumeValue : ScriptableObject
{
    [Range(0, 1)]
    public float fillAmount;
}