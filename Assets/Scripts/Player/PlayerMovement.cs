using Animation;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private float _movementSpeed = 2.5f;
        private Vector3 _crosshairPosition;
        private const float _crosshairMinDistance = 1f;
        [SerializeField] private float _crosshairMaxDistance = 5f;
        private const float _smoothing = 1f;
        [SerializeField] private const float _potionHeight = 5f;
        [SerializeField] private const float _potionSpeed = 2.5f;

        private float _throwStrenghtPosition;
        private float _throwStrenghtDestination;
        private float _throwStrenghtSpeed;
        private Vector3 _startThrowPosition;
        private Vector3 _endThrowPosition;
        private float fireLerp = 1;

        [Space(5)]
        [SerializeField] Transform _potion;
        [SerializeField] Transform _throwStrenghtMinDestination;
        [SerializeField] Transform _throwStrenghtMaxDestination;

        [Space(5)]
        [Header("Statistics")]
        private Vector2 _movementDirection;
        private Vector2 _lastMoveDirection;

        [Space(5)]
        [Header("References")]
        public Rigidbody2D rb2d;
        public Animator Animator;
        public GameObject Crosshair;
        public GameObject Potion;

        private void Awake()
        {
            //Cursor.visible = false;
        }

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            rb2d.velocity = new Vector2(_movementDirection.x * _movementSpeed, _movementDirection.y * _movementSpeed);
        }

        private void Update()
        {
            ProcessInputs();
            Animate();

            Vector3 newProjectilePos = ThrowTrajectory(_startThrowPosition, _endThrowPosition, fireLerp);
            transform.position = newProjectilePos;

            fireLerp += _potionSpeed * Time.deltaTime;
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

        void Animate()
        {
            if (_movementDirection != Vector2.zero)
            {
                Animator.SetFloat("Horizontal", _movementDirection.x);
                Animator.SetFloat("Vertical", _movementDirection.x);
            }

            Animator.SetFloat("Speed", _movementSpeed);
        }

        void Aim()
        {
            _crosshairPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            _crosshairPosition.x = Mathf.Clamp(_crosshairPosition.x, _crosshairMinDistance, _crosshairMaxDistance);
            _crosshairPosition.y = Mathf.Clamp(_crosshairPosition.y, _crosshairMinDistance, _crosshairMaxDistance);

            transform.position = _crosshairPosition;

            _throwStrenghtDestination = Mathf.Clamp01(_throwStrenghtDestination);
            _throwStrenghtPosition = Mathf.SmoothDamp(_throwStrenghtPosition, _throwStrenghtDestination, ref _throwStrenghtSpeed, _smoothing);
            _potion.position = Vector2.Lerp(_throwStrenghtMinDestination.position, _throwStrenghtMaxDestination.position, _throwStrenghtPosition);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ThrowPotion(transform.position, _crosshairPosition);
            }
        }

        void ThrowPotion(Vector3 firePosition, Vector3 targetPosition)
        {
            _startThrowPosition = firePosition;
            _endThrowPosition = targetPosition;
        }

        Vector3 ThrowTrajectory(Vector3 firePosition, Vector3 targetPosition, float t)
        {
            Vector3 linearProgress = Vector3.Lerp(firePosition, targetPosition, t);
            float perspectiveOffset = Mathf.Sin(t * Mathf.PI) * _potionHeight;

            Vector3 trajectoryPosition = linearProgress + (Vector3.up * perspectiveOffset);
            return trajectoryPosition;
        }
    }
}