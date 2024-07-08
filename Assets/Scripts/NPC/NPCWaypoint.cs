using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class NPCWaypoint : MonoBehaviour
{
    [Header("References")]
    public NPCWaypoints waypoints;
    public Rigidbody2D rb2d;

    [Header("Variables")]
    public float moveSpeed = 1f;
    [SerializeField]private float waitTime = 2f;
    [SerializeField]private int currentWaypointIndex = 0;
    private int arraySize;
    public bool isWaiting = false;
    private bool FirstInList = true, LastInList = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        arraySize = waypoints.waypoints.Length;
    }

    public IEnumerator MoveToNextWaypoint()
    {
        if (!isWaiting)
        {
            Vector2 targetPosition = waypoints.waypoints[currentWaypointIndex];
            Vector2 direction = (targetPosition - rb2d.position).normalized;
            rb2d.velocity = direction * moveSpeed;
                
            if (Vector2.Distance(rb2d.position, targetPosition) < 0.1f)
            {
                isWaiting = true;
                rb2d.velocity = Vector2.zero;
                yield return new WaitForSeconds(waitTime);

                if (FirstInList)
                {
                    Forward();
                }
                else Backward();
                isWaiting = false;
            }
        }
    }

    private void Forward()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex == arraySize)
        {
            LastInList = true;
            FirstInList = false;
        }
        
    }

    private void Backward()
    {
        currentWaypointIndex--;
        if (currentWaypointIndex == 0)
        {
            FirstInList = false;
            LastInList = true;
        }
    }
}


