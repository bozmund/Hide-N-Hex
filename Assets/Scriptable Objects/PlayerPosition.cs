using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPosition", menuName = "ScriptableObjects/PlayerPosition", order = 1)]
public class PlayerPosition : ScriptableObject
{
    public string sceneName;
    public Vector3 position;

    // Method to save player state
    public void SaveState(string scene, Vector3 pos)
    {
        sceneName = scene;
        position = pos;
    }

    // Method to load player state
    public void LoadState(out string scene, out Vector3 pos)
    {
        scene = sceneName;
        pos = position;
    }
}
