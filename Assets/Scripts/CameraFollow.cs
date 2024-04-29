using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3 (0, 0, -10f);
    public float smoothTime = 0.25f;
    [SerializeField] private ParticleSystem staryNight;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Awake()
    {
        staryNight.Play();
    }

    private void FixedUpdate()
    {


        Vector3 targetPostition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPostition, ref velocity, smoothTime);
        
        Vector3 targetPosition = transform.position;

        staryNight.transform.position = Vector3.SmoothDamp(staryNight.transform.position, targetPosition, ref velocity, smoothTime);
    }
}
