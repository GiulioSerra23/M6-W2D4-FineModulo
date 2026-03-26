
using UnityEngine;

public class RollingPlatform : MovingPlatform
{
    [Header ("Rotation Settings")]    
    [SerializeField] private float _rotateSpeed;

    [Header ("OverlapSphere Settings")]
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    private Collider[] _hits = new Collider[10];
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected override void Move()
    {
        RotatePlatform();
        PushPlayer();
    }

    private void RotatePlatform()
    {
        _rb.angularVelocity = transform.forward * _rotateSpeed;
    }

    private void PushPlayer()
    {
        int count = Physics.OverlapSphereNonAlloc(_rb.position, _radius, _hits, _layerMask);
        
        for (int i = 0; i < count; i++)
        {
            Collider hit = _hits[i];
            if (!hit.TryGetComponent<Rigidbody>(out var playerRb)) continue;

            Vector3 contactPoint = hit.transform.position;
            Vector3 surfaceVelocity = _rb.GetPointVelocity(contactPoint);

            playerRb.AddForce(surfaceVelocity, ForceMode.VelocityChange);
        }
    }
}