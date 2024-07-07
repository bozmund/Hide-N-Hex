using System.Collections;
using UnityEngine;

public class NPCFreeRoam : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float minX = -2f;
    private float maxX = 2f;
    private float minY = -2f;
    private float maxY = 2f;
    public bool isMoving = true;

    private Vector2 targetPosition;
    private Vector2 _villagePosition;

    public Rigidbody2D rb2d;

    void Start()
    {
        _villagePosition = new Vector2(71, 8);
        SetRandomTargetPosition();
    }

    public IEnumerator MoveAndWait()
    {
        while (true)
        {
            if (isMoving)
            {
                rb2d.FreeRoam(targetPosition, moveSpeed);

                if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
                {
                    isMoving = false;
                    rb2d.velocity = Vector2.zero;
                    yield return new WaitForSeconds(Random.Range(1f, 3f));
                    SetRandomTargetPosition();
                    isMoving = true;
                }
            }
            yield return null;
        }
    }

    public void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(_villagePosition.x + randomX, _villagePosition.y + randomY);
    }
}
