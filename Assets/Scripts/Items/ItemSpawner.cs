using UnityEngine;
using System.Collections.Generic;

public class MultiPositionSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PrefabPosition
    {
        public GameObject prefab; // Prefab to spawn
        public List<Vector3> positions; // List of positions to spawn the prefab
    }

    public List<PrefabPosition> prefabPositions; // List of prefabs with their spawn positions
    public List<GameObject> objectsToDisable; // List of GameObjects to disable or destroy

    void Start()
    {
        // Spawn prefabs at specified positions
        foreach (var prefabPosition in prefabPositions)
        {
            foreach (var position in prefabPosition.positions)
            {
                Instantiate(prefabPosition.prefab, position, Quaternion.identity);
            }
        }

        // Disable or destroy multiple specified GameObjects
        if (objectsToDisable != null)
        {
            foreach (var obj in objectsToDisable)
            {
                if (obj != null) // Check if the GameObject is not null
                {
                    obj.SetActive(false); // Disables the GameObject
                    // Or
                    // Destroy(obj); // Destroys the GameObject
                }
            }
        }
    }
}
