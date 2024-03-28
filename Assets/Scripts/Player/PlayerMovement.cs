using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _movementSpeed = 2.5f;
        public Rigidbody2D rb2d;
        private Vector2 _movementDirection;
        private Animator _animator;

        private void Awake()
        {
        }

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rb2d.velocity = new Vector2(_movementDirection.x * _movementSpeed, _movementDirection.y * _movementSpeed);
        }

        private void Update()
        {
            PlayerMove();
            if (Input.GetKeyDown(KeyCode.F)) Interact();
        }

        public void PlayerMove()
        {
            _movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        public void Interact()
        {
            var facingDirection = new Vector3(_movementDirection.x, _movementDirection.y);
            var interactPosition = transform.position + facingDirection;
            
            Debug.DrawLine(transform.position, interactPosition, Color.red, 1f);
        }
    }
}
