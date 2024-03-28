using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerController;

        PlayerState playerState;

        private void Update()
        {
            if (playerState == PlayerState.Walking)
            {
                playerController.ProcessInputs();
            }
            else if (playerState == PlayerState.Interacting)
            {
                
            }
        }
    }
}