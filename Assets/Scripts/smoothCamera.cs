using UnityEngine;

public class smoothCamera : MonoBehaviour
{

    [SerializeField] private Transform target;
    public float smoothTime = 0.25f;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private Vector3 velociy = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velociy, smoothTime);
    }
}
