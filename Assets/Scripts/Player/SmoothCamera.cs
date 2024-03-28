using UnityEngine;

namespace Player
{
    public class SmoothCamera : MonoBehaviour
    {

        [SerializeField] private Transform target;
        public float smoothTime = 0.1f;
        private readonly Vector3 _offset = new(0f, 0f, -2f);
        private Vector3 _velocity = Vector3.zero;

        private void FixedUpdate()
        {
            var targetPosition = target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }
    }
}
