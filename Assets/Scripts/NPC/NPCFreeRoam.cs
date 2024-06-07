using System.Collections;
using UnityEngine;

public class NPCFreeRoam : MonoBehaviour
{
    public float moveSpeed = 1f;

    public float minX = -2f;
    public float maxX = 2f;
    public float minY = -2f;
    public float maxY = 2f;

    private Vector2 targetPosition;
    private Vector2 _villagePosition;
    private bool isMoving = true;

    void Start()
    {
        _villagePosition = transform.position;
        SetRandomTargetPosition();
        StartCoroutine(MoveAndWait());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch");
        isMoving = false;
        StartCoroutine(HandleCollision());
    }

    private IEnumerator HandleCollision()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        SetRandomTargetPosition();
        isMoving = true;
    }

    private IEnumerator MoveAndWait()
    {
        while (true)
        {
            if (isMoving)
            {
                MoveTowardsTarget();

                if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
                {
                    isMoving = false;
                    yield return new WaitForSeconds(Random.Range(1f, 3f));
                    SetRandomTargetPosition();
                    isMoving = true;
                }
            }

            yield return null;
        }
    }

    void SetRandomTargetPosition()
    {
        do
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            targetPosition = new Vector2(_villagePosition.x + randomX, _villagePosition.y + randomY);
        } while (!IsWithinBounds(targetPosition));
    }

    bool IsWithinBounds(Vector2 position)
    {
        return position.x >= _villagePosition.x + minX && position.x <= _villagePosition.x + maxX &&
               position.y >= _villagePosition.y + minY && position.y <= _villagePosition.y + maxY;
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
