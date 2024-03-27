using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _movementSpeed = 2.5f;
    public Rigidbody2D rb2d;
    private Vector2 _movementDirection;

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
    }

    private void ProcessInputs()
    {
        var moveY = Input.GetAxisRaw("Horizontal");
        var moveX = Input.GetAxisRaw("Vertical");

        _movementDirection = new Vector2(moveY, moveX).normalized;
    }
}
