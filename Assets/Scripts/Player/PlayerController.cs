using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerController;

        private PlayerState _playerState;

        private void Update()
        {
            switch (_playerState)
            {
                case PlayerState.Walking:
                    playerController.ProcessInputs();
                    break;
                case PlayerState.Interacting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}