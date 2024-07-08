using UnityEngine;

[CreateAssetMenu(fileName = "NPCWaypoints", menuName = "ScriptableObjects/NPCWaypoints", order = 1)]
public class NPCWaypoints : ScriptableObject
{
    public Vector2[] waypoints;
}
