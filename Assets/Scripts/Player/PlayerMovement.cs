using System.Collections.Generic;
using UnityEngine;
using PotionSystem;
using System.Collections;
using System;
using PotionSystem;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(LineRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] public float movementSpeed = 2.5f;
        [SerializeField] public int strength;
        [SerializeField] private float potionHeight = 1f;
        [SerializeField] private float throwDuration = 1f;
        [SerializeField] private float throwCd = 1f;
        [SerializeField] private float throwRadius = 2f;
        private float _lastThrowTime;
        private Vector3 _startThrowPosition;
        private Vector3 _endThrowPosition;
        public Vector2 movementDirection;
        private Vector2 _lastMoveDirection;
        private bool _isThrowing = false;
        private bool _isDrinking = false;
        public bool _invertControls;

        [Space(5)]
        [Header("References")]
        public GameObject Potion;
        public LineRenderer trajectoryRenderer;
        private PotionEffectHandler _potionEffectHandler;
        private Vector3 _crosshairPosition;
        public Animator animator;
        public Rigidbody2D rb2d;
        public PotionEffects potionEffects;
        public SpriteRenderer spriteRenderer;
        public Sprite[] potionDrinkSprites;
        public Sprite[] potionThrowSprites;
        public Sprite[] walkSprites;

        public List<Effect> ActiveEffects = new();
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsThrow = Animator.StringToHash("isThrow");
        private static readonly int IsDrink = Animator.StringToHash("isDrink");

        private void Awake()
        {
            Cursor.visible = false;
        }

        private void Start()
        {
            _potionEffectHandler = PotionEffectHandler.Instance;
            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            SetupTrajectoryRenderer();
        }

        private void FixedUpdate()
        {
            rb2d.velocity = new Vector2(movementDirection.x * movementSpeed, movementDirection.y * movementSpeed);
        }

        private void Update()
        {
            Movement();
            Animate();
            Aim();
            UpdateTrajectoryLine();
            DrinkPotion();
            HandleActiveEffects();
            CheckActiveSprite();
        }

        private void CheckActiveSprite()
        {

            Sprite currentSprite = spriteRenderer.sprite;

            // Check if the current sprite is in the potionDrinkSprites array
            foreach (Sprite sprite in potionDrinkSprites)
            {
                if (currentSprite == sprite)
                {
                    GameObject napitak = GameObject.Find("Potion");
                    napitak.GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
                else
                {
                    GameObject napitak = GameObject.Find("Potion");
                    napitak.GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
            }

            // Check if the current sprite is in the potionThrowSprites array
            foreach (Sprite sprite in potionThrowSprites)
            {
                if (currentSprite == sprite)
                {
                    GameObject napitak = GameObject.Find("Potion");
                    napitak.GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
                else
                {
                    GameObject napitak = GameObject.Find("Potion");
                    napitak.GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
            }

            // Check if the current sprite is in the walkSprites array
            foreach (Sprite sprite in walkSprites)
            {
                if (currentSprite == sprite)
                {
                    GameObject napitak = GameObject.Find("Potion");
                    napitak.GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
                else
                {
                    GameObject napitak = GameObject.Find("Potion");
                    napitak.GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
            }
        }



        // ReSharper disable Unity.PerformanceAnalysis
        private void DrinkPotion()
        {
            if (Input.GetKeyDown(KeyCode.Q) && Potion is not null)
            {
                _potionEffectHandler.Handle(Potion.GetComponent<SpriteRenderer>().sprite.name);
                _isDrinking = true;
                animator.SetBool(IsDrink, true);
                StartCoroutine(ResetAnim(0.3f));
            }
        }

        public void Movement()
        {
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveY = Input.GetAxisRaw("Vertical");

            if (_invertControls)
            {
                moveX = Input.GetAxisRaw("Vertical");
                moveY = Input.GetAxisRaw("Horizontal");
            }
            
            if (moveX == 0 && moveY == 0 && (movementDirection.x != 0 || movementDirection.y != 0))
                _lastMoveDirection = movementDirection;

            movementDirection = new Vector2(moveX, moveY).normalized;
        }

        void Animate()
        {
            if (!potionEffects.flying)
            {
                if (!_isThrowing || !_isDrinking)
                {
                    if (movementDirection != Vector2.zero)
                    {
                        animator.SetFloat(Horizontal, movementDirection.x);
                        animator.SetFloat(Vertical, movementDirection.y);
                    }
                    animator.SetFloat(Speed, movementDirection.sqrMagnitude);
                }
            }
            else
            {
                animator.SetBool("isFly", true);
                if (movementDirection != Vector2.zero)
                {
                    animator.SetFloat(Horizontal, movementDirection.x);
                    animator.SetFloat(Vertical, movementDirection.y);
                }
                animator.SetFloat(Speed, movementDirection.sqrMagnitude);
            }
        }

        private void Aim()
        {
            _crosshairPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _crosshairPosition.z = 0f;

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= _lastThrowTime + throwCd)
            {
                Vector3 throwTargetPosition = ClampThrowPosition(_crosshairPosition);
                ThrowPotion(transform.position, throwTargetPosition);
                _lastThrowTime = Time.time;
                animator.SetBool(IsThrow, true);
                _isThrowing = true;
                FaceCrosshair();
                StartCoroutine(ResetAnim(0.5f));
            }
        }

        void FaceCrosshair()
        {
            Vector2 direction = (_crosshairPosition - transform.position).normalized;
            animator.SetFloat(Horizontal, direction.x);
            animator.SetFloat(Vertical, direction.y);
        }
        void ThrowPotion(Vector3 firePosition, Vector3 targetPosition)
        {
            _startThrowPosition = firePosition;
            _endThrowPosition = targetPosition;

            GameObject potion = Instantiate(Potion, firePosition, Quaternion.identity) as GameObject;
            potion.transform.localScale = new Vector2(0.4f, 0.4f);
            StartCoroutine(AnimateThrow(potion, firePosition, targetPosition));
        }

        private IEnumerator AnimateThrow(GameObject potion, Vector3 firePosition, Vector3 targetPosition)
        {
            float elapsedTime = 0f;

            while (elapsedTime < throwDuration)
            {
                float t = elapsedTime / throwDuration;
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
            _isThrowing = false;
            _isDrinking = false;
            animator.SetBool(IsThrow, false);
            animator.SetBool(IsDrink, false);
        }

        public void AddEffect(Effect effect)
        {
            ActiveEffects.Add(effect);
            effect.Apply(this);
        }

        public void RemoveEffect(Effect effect)
        {
            effect.End(this);
            ActiveEffects.Remove(effect);
        }

        private void HandleActiveEffects()
        {
            for (var i = ActiveEffects.Count - 1; i >= 0; i--)
            {
                ActiveEffects[i].duration -= Time.deltaTime;
                if (ActiveEffects[i].duration <= 0)
                {
                    RemoveEffect(ActiveEffects[i]);
                }
            }
        }

        void SetupTrajectoryRenderer()
        {
            trajectoryRenderer.positionCount = 30;
            trajectoryRenderer.startWidth = 0.05f;
            trajectoryRenderer.endWidth = 0.05f;
        }

        Vector3 ThrowTrajectory(Vector3 firePosition, Vector3 targetPosition, float t)
        {
            Vector3 linearProgress = Vector3.Lerp(firePosition, targetPosition, t);
            float perspectiveOffset = Mathf.Sin(t * Mathf.PI) * potionHeight;

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
            if (clampedPosition.magnitude > throwRadius)
            {
                clampedPosition = clampedPosition.normalized * throwRadius;
                targetPosition = transform.position + clampedPosition;
            }
            return targetPosition;
        }
    }
}
