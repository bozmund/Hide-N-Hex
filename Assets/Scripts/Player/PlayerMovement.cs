using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(LineRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private float _movementSpeed = 2.5f;
        private const float _smoothing = 1f;
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

        [Space(5)]
        [Header("References")]
        [SerializeField] public GameObject crosshair;
        [SerializeField] private GameObject Potion;
        [SerializeField] Transform _potion;
/*        [SerializeField] private LineRenderer throwLimitRenderer;*/
        [SerializeField] private LineRenderer trajectoryRenderer;
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
/*            SetupThrowLimitRenderer();*/
            SetupTrajectoryRenderer();
        }

        private void FixedUpdate()
        {
            rb2d.velocity = new Vector2(_movementDirection.x * _movementSpeed, _movementDirection.y * _movementSpeed);
        }

        private void Update()
        {
            ProcessInputs();
            //Animate();
            //Aim();
/*            UpdateThrowLimit();*/
            //UpdateTrajectoryLine();
        }

        public void ProcessInputs()
        {
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveY = Input.GetAxisRaw("Vertical");

            if (moveX == 0 && moveY == 0 && (_movementDirection.x != 0 || _movementDirection.y != 0))
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
            if (!isThrowing)
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
                //StartCoroutine(ResetIsThrow(0.3f));
            }
        }

        private IEnumerator ResetIsThrow(float delay)
        {
            yield return new WaitForSeconds(delay);
            isThrowing = false;
            _animator.SetBool("isThrow", false);
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
            if (potion.transform.position == targetPosition)
            {
                Destroy(potion);
            }
        }
        void FaceCrosshair()
        {
            Vector3 direction = (_crosshairPosition - transform.position).normalized;
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.y);
        }

/*        void SetupThrowLimitRenderer()
        {
            throwLimitRenderer.positionCount = 100;
            throwLimitRenderer.loop = true;
            throwLimitRenderer.startWidth = 0.05f;
            throwLimitRenderer.endWidth = 0.05f;
        }*/

        void SetupTrajectoryRenderer()
        {
            trajectoryRenderer.positionCount = 31;
            trajectoryRenderer.startWidth = 0.05f;
            trajectoryRenderer.endWidth = 0.05f;
        }

        /*        void UpdateThrowLimit()
                {
                    Vector3[] positions = new Vector3[throwLimitRenderer.positionCount];
                    for (int i = 0; i < throwLimitRenderer.positionCount; i++)
                    {
                        float angle = i * Mathf.PI * 2 / throwLimitRenderer.positionCount;
                        float x = Mathf.Cos(angle) * _throwRadius;
                        float y = Mathf.Sin(angle) * _throwRadius;
                        positions[i] = new Vector3(x, y, 0) + transform.position;
                    }
                    throwLimitRenderer.SetPositions(positions);
                }*/

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

