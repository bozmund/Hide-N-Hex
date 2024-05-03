using UnityEngine;
using System.Collections.Generic; // This is needed for List<T>

public class ItemSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab; // Prefab for the collectible item
    public List<Vector3> spawnPositions; // Positions to spawn the collectibles
    public GameObject objectToDisable; // Reference to the GameObject to disable or destroy

    void Start()
    {
        // Spawn collectible prefabs
        foreach (var position in spawnPositions)
        {
            Instantiate(collectiblePrefab, position, Quaternion.identity);
        }

        if (objectToDisable != null)
        {
            // Disable or destroy the specified GameObject
            objectToDisable.SetActive(false); // Disables the GameObject
            // Or
            // Destroy(objectToDisable); // Destroys the GameObject
        }
    }
}
