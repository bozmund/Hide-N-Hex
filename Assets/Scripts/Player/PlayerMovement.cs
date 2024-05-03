using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float _movementSpeed = 2.5f;
        public Rigidbody2D rb2d;
        private Vector2 _movementDirection;
        private Vector2 _lastMoveDirection;
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
            ProcessInputs();
            if (Input.GetKeyDown(KeyCode.F)) Interact();
        }

        public void ProcessInputs()
        {
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveY = Input.GetAxisRaw("Vertical");

            if (moveX == 0 && moveY == 0 && _movementDirection.x != 0 || _movementDirection.y != 0)
                _lastMoveDirection = _movementDirection;

            _movementDirection = new Vector2(moveX, moveY).normalized;
        }

        public void Interact()
        {
            var facingDirection = new Vector3(_movementDirection.x, _movementDirection.y);
            var interactPosition = transform.position + facingDirection;

            Debug.DrawLine(transform.position, interactPosition, Color.red, 1f);
        }
    }
}