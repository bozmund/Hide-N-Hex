using System.Collections;
using UnityEngine;

namespace NPC
{
    public class NpcWaypoint : MonoBehaviour
    {
        [Header("References")]
        public NPCWaypoints waypoints;
        public Rigidbody2D rb2d;

        [Header("Variables")]
        public float moveSpeed = 1f;
        [SerializeField]private float waitTime = 2f;
        [SerializeField]private int currentWaypointIndex;
        private int _arraySize;
        public bool isWaiting;
        private bool _firstInList = true, _lastInList = false;

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            _arraySize = waypoints.waypoints.Length;
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

                    if (_firstInList)
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
            if (currentWaypointIndex == _arraySize)
            {
                _lastInList = true;
                _firstInList = false;
            }
        
        }

        private void Backward()
        {
            currentWaypointIndex--;
            if (currentWaypointIndex == 0)
            {
                _firstInList = false;
                _lastInList = true;
            }
        }
    }
}


