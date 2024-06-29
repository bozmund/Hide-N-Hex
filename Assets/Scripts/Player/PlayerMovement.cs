using System.Collections.Generic;
using UnityEngine;
using PotionSystem;
using System.Collections;

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
        private float lastThrowTime;
        private Vector3 _startThrowPosition;
        private Vector3 _endThrowPosition;
        private Vector2 _movementDirection;
        private Vector2 _lastMoveDirection;
        private bool isThrowing = false;
        private bool isDrinking = false;

        [Space(5)]
        [Header("References")]
        [SerializeField] public GameObject crosshair;
        [SerializeField] private GameObject Potion;
        [SerializeField] Transform _potion;
        [SerializeField] private LineRenderer trajectoryRenderer;
        private PotionEffectHandler _potionEffectHandler;
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
            _potionEffectHandler = PotionEffectHandler.Instance;
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
            Animate();
            Aim();
            UpdateTrajectoryLine();
            DrinkPotion();
            HandleActiveEffects();
        }

        private void DrinkPotion()
        {
            if (Input.GetKeyDown(KeyCode.Q) && Potion is not null)
            {
                _potionEffectHandler.Handle(Potion.name);
                isDrinking = true;
                _animator.SetBool("isDrink", true);
                StartCoroutine(ResetAnim(0.3f));
            }
        }

        public void Movement()
        {
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveY = Input.GetAxisRaw("Vertical");

            if (moveX == 0 && moveY == 0 && (_movementDirection.x != 0 || _movementDirection.y != 0))
                _lastMoveDirection = _movementDirection;

            _movementDirection = new Vector2(moveX, moveY).normalized;
        }

        void Animate()
        {
            if (!isThrowing || !isDrinking)
            {
                if (_movementDirection != Vector2.zero)
                {
                    _animator.SetFloat("Horizontal", _movementDirection.x);
                    _animator.SetFloat("Vertical", _movementDirection.y);
                }
                _animator.SetFloat("Speed", _movementDirection.sqrMagnitude);
            }
        }

        void Aim()
        {
            _crosshairPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _crosshairPosition.z = 0f;

            crosshair.transform.position = _crosshairPosition;

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= lastThrowTime + _throwCD)
            {
                Vector3 throwTargetPosition = ClampThrowPosition(_crosshairPosition);
                ThrowPotion(transform.position, throwTargetPosition);
                lastThrowTime = Time.time;
                _animator.SetBool("isThrow", true);
                isThrowing = true;
                FaceCrosshair();
                StartCoroutine(ResetAnim(0.5f));
            }
        }

        void FaceCrosshair()
        {
            Vector2 direction = (_crosshairPosition - transform.position).normalized;
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.y);
        }
        void ThrowPotion(Vector3 firePosition, Vector3 targetPosition)
        {
            _startThrowPosition = firePosition;
            _endThrowPosition = targetPosition;

            GameObject potion = Instantiate(Potion, firePosition, Quaternion.identity);
            StartCoroutine(AnimateThrow(potion, firePosition, targetPosition));
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
            if (potion.transform.position == targetPosition)
            {
                Destroy(potion);
            }
        }

        private IEnumerator ResetAnim(float delay)
        {
            yield return new WaitForSeconds(delay);
            isThrowing = false;
            isDrinking = false;
            _animator.SetBool("isThrow", false);
            _animator.SetBool("isDrink", false);
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
            trajectoryRenderer.positionCount = 30;
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
