using System.Collections;
using UnityEngine;

public class NPCFreeRoam : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb2d;

    [Header("Variables")]
    public float moveSpeed = 1f;
    [SerializeField] private float minX = -22f;
    [SerializeField] private float maxX = 22f;
    [SerializeField] private float minY = -15f;
    [SerializeField] private float maxY = 20f;
    public bool isMoving = true;
    [SerializeField]private float waitTime = 2f;

    [SerializeField]private Vector2 targetPosition;
    [SerializeField]private Vector2 _villagePosition;



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _villagePosition = new Vector2(71, 8);
        SetRandomTargetPosition();
    }

    public IEnumerator MoveAndWait()
    {
        if (isMoving)
        {
            Vector2 direction = (targetPosition - rb2d.position).normalized;
            rb2d.velocity = direction * moveSpeed;

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                rb2d.velocity = Vector2.zero;
                yield return new WaitForSeconds(waitTime);
                SetRandomTargetPosition();
                isMoving = true;
            }
        }        
    }

    public void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(_villagePosition.x + randomX, _villagePosition.y + randomY);
    }
}
