using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float movementSpeed;
    public Rigidbody2D rb2d;
    private Vector2 movementDirection;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Movement();
    }
    
    void Update()
    {
        ProcessInputs();
    }

    void Movement()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(movementDirection.x * movementSpeed, movementDirection.y * movementSpeed);
    }

    void ProcessInputs()
    {

        float moveY = Input.GetAxisRaw("Horizontal");
        float moveX = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(moveY, moveX).normalized;
    }
}
