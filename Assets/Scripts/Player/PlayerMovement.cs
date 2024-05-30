using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private float _movementSpeed = 2.5f;
        private const float _smoothing = 1f;
        [SerializeField] private float _potionHeight = 2.5f;
        [SerializeField] private float _throwDuration = 1.5f;
        [SerializeField] private float _throwCD = 1f;
        private float lastThrowTime;
        private Vector3 _startThrowPosition;
        private Vector3 _endThrowPosition;
        private Vector2 _movementDirection;
        private Vector2 _lastMoveDirection;

        [Space(5)]
        [Header("References")]
        [SerializeField] private GameObject Crosshair;
        [SerializeField] private GameObject Potion;
        [SerializeField] Transform _potion;
        private Vector3 _crosshairPosition;
        public Animator _animator;
        public Rigidbody2D rb2d;

        private void Awake()
        {
            Cursor.visible = false;
        }

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            rb2d.velocity = new Vector2(_movementDirection.x * _movementSpeed, _movementDirection.y * _movementSpeed);
        }

        private void Update()
        {
            ProcessInputs();
            Animate();
            Aim();
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
                _animator.SetFloat("Horizontal", _movementDirection.x);
                _animator.SetFloat("Vertical", _movementDirection.x);
            }

            _animator.SetFloat("Speed", _movementSpeed);
        }

        void Aim()
        {
            _crosshairPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _crosshairPosition.z = 0f;
            Crosshair.transform.position = _crosshairPosition;

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= lastThrowTime + _throwCD)
            {
                ThrowPotion(transform.position, _crosshairPosition);
                lastThrowTime = Time.time;
            }
        }

        void ThrowPotion(Vector3 firePosition, Vector3 targetPosition)
        {
            _startThrowPosition = firePosition;
            _endThrowPosition = targetPosition;

            GameObject potion = Instantiate(Potion, firePosition, Quaternion.identity);
            StartCoroutine(AnimateThrow(potion, firePosition, targetPosition));
        }

        Vector3 ThrowTrajectory(Vector3 firePosition, Vector3 targetPosition, float t)
        {
            Vector3 linearProgress = Vector3.Lerp(firePosition, targetPosition, t);
            float perspectiveOffset = Mathf.Sin(t * Mathf.PI) * _potionHeight;

            Vector3 trajectoryPosition = linearProgress + (Vector3.up * perspectiveOffset);
            return trajectoryPosition;
        }
        private IEnumerator AnimateThrow(GameObject potion, Vector3 firePosition, Vector3 targetPosition)
        {
            float elapsedTime = 0f;

            while (elapsedTime < _throwDuration)
            {
                float t = elapsedTime / _throwDuration;
                Vector3 trajectoryPosition = ThrowTrajectory(firePosition, targetPosition, t);
                potion.transform.position = trajectoryPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            potion.transform.position = targetPosition;
        }
    }
}
