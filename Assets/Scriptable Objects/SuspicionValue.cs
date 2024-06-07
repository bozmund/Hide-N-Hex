using UnityEngine;

[CreateAssetMenu(fileName = "SuspicionValue", menuName = "ScriptableObjects/SuspicionValue", order = 1)]
public class SuspicionValue : ScriptableObject
{
    [Range(0, 1)]
    public float fillAmount;
}
