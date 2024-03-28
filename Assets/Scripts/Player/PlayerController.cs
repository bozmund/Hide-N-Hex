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
                playerController.PlayerMove();
            }
            else if (playerState == PlayerState.Interacting)
            {
                
            }
        }
    }
}