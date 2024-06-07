using UnityEngine;

[CreateAssetMenu(fileName = "HealthValue", menuName = "ScriptableObjects/HealthValue", order = 1)]
public class HealthValue : ScriptableObject
{
    [Range(0, 1)]
    public float fillAmount;
}

