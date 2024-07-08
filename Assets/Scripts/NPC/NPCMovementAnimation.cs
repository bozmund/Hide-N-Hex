using UnityEngine;

public class NPCMovementAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;
    private Vector2 moving;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moving = rb2d.velocity;
        Animate();
    }

    private void Animate()
    {
        if (moving != Vector2.zero)
        {
            animator.SetFloat("Horizontal", moving.x);
            animator.SetFloat("Vertical", moving.y);
        }
        animator.SetFloat("Speed", moving.sqrMagnitude);
    }
}
