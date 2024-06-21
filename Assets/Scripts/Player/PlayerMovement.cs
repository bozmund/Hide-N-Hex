using System.Collections.Generic;
using UnityEngine;
using PotionSystem;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(LineRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] public float _movementSpeed = 2.5f;
        [SerializeField] public int strength;
        [SerializeField] private float _potionHeight = 1f;
        [SerializeField] private float _throwDuration = 1f;
        [SerializeField] private float _throwCD = 1f;
        [SerializeField] private float _throwRadius = 2f;
        private const float _smoothing = 1f;
        private float lastThrowTime;
        private Vector3 _startThrowPosition;
        private Vector3 _endThrowPosition;
        private Vector2 _movementDirection;
        private Vector2 _lastMoveDirection;
        private bool isThrowing = false;

        [Space(5)]
        [Header("References")]
        [SerializeField] public GameObject crosshair;
        [SerializeField] private GameObject Potion;
        [SerializeField] Transform _potion;
        [SerializeField] private LineRenderer trajectoryRenderer;
        private readonly PotionEffectHandler _potionEffectHandler = PotionEffectHandler.Instance;
        private Vector3 _crosshairPosition;
        public Animator _animator;
        public Rigidbody2D rb2d;

        private List<Effect> _activeEffects = new List<Effect>();

        private void Awake()
        {
            Cursor.visible = false;
        }

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            SetupTrajectoryRenderer();
        }

        private void FixedUpdate()
        {
            rb2d.velocity = new Vector2(_movementDirection.x * _movementSpeed, _movementDirection.y * _movementSpeed);
        }

        private void Update()
        {
            Movement();
            DrinkPotion();
            HandleActiveEffects();
        }

        private void DrinkPotion()
        {
            if (Input.GetKeyDown(KeyCode.Q) && Potion is not null) 
                _potionEffectHandler.Handle(Potion.name);
        }

        public void Movement()
        {
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveY = Input.GetAxisRaw("Vertical");

            if (moveX == 0 && moveY == 0 && (_movementDirection.x != 0 || _movementDirection.y != 0))
                _lastMoveDirection = _movementDirection;

            _movementDirection = new Vector2(moveX, moveY).normalized;
        }

        public void AddEffect(Effect effect)
        {
            _activeEffects.Add(effect);
            effect.Apply(this);
        }

        public void RemoveEffect(Effect effect)
        {
            effect.End(this);
            _activeEffects.Remove(effect);
        }

        private void HandleActiveEffects()
        {
            for (int i = _activeEffects.Count - 1; i >= 0; i--)
            {
                _activeEffects[i].duration -= Time.deltaTime;
                if (_activeEffects[i].duration <= 0)
                {
                    RemoveEffect(_activeEffects[i]);
                }
            }
        }

        // Other existing methods...

        void SetupTrajectoryRenderer()
        {
            trajectoryRenderer.positionCount = 31;
            trajectoryRenderer.startWidth = 0.05f;
            trajectoryRenderer.endWidth = 0.05f;
        }

        Vector3 ThrowTrajectory(Vector3 firePosition, Vector3 targetPosition, float t)
        {
            Vector3 linearProgress = Vector3.Lerp(firePosition, targetPosition, t);
            float perspectiveOffset = Mathf.Sin(t * Mathf.PI) * _potionHeight;

            Vector3 trajectoryPosition = linearProgress + (Vector3.up * perspectiveOffset);
            return trajectoryPosition;
        }
        void UpdateTrajectoryLine()
        {
            Vector3 firePosition = transform.position;
            Vector3 targetPosition = ClampThrowPosition(_crosshairPosition);

            Vector3[] positions = new Vector3[trajectoryRenderer.positionCount];
            for (int i = 0; i < trajectoryRenderer.positionCount; i++)
            {
                float t = (float)i / (trajectoryRenderer.positionCount - 1);
                positions[i] = ThrowTrajectory(firePosition, targetPosition, t);
            }
            trajectoryRenderer.SetPositions(positions);
        }

        Vector3 ClampThrowPosition(Vector3 targetPosition)
        {
            Vector3 clampedPosition = targetPosition - transform.position;
            if (clampedPosition.magnitude > _throwRadius)
            {
                clampedPosition = clampedPosition.normalized * _throwRadius;
                targetPosition = transform.position + clampedPosition;
            }
            return targetPosition;
        }
    }
}
