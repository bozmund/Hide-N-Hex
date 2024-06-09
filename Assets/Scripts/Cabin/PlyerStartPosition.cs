using UnityEngine;
using Cinemachine;

public class PlayerStartPosition: MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public Vector2 startPosition; // Desired start position for the player
    public CinemachineBrain cinemachineBrain; // Reference to the CinemachineBrain

    void Start()
    {
        // Disable CinemachineBrain
        cinemachineBrain.enabled = false;

        // Set the player's start position
        player.transform.position = startPosition;

        // Re-enable CinemachineBrain
        cinemachineBrain.enabled = true;
    }
}
