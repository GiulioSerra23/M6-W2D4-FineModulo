
using UnityEngine;

public class RollingPlatform : MovingPlatform
{
    [Header ("Rotation Settings")]    
    [SerializeField] private float _rotateSpeed;

    [Header ("OverlapSphere Settings")]
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    private Collider[] _hits;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected override void Move()
    {
        RotateSpeed();
        PushPlayer();
    }

    private float RotateSpeed()
    {
        float deltaAngle = _rotateSpeed;
        transform.Rotate(Vector3.forward, deltaAngle);

        float tangentialVel = Mathf.Deg2Rad * deltaAngle * _radius;
        return tangentialVel;
    }

    private void PushPlayer()
    {
        _hits = Physics.OverlapSphere(_rb.position, _radius, _layerMask);
        foreach (Collider hit in _hits)
        {
            if (!hit.TryGetComponent<Rigidbody>(out var playerRb)) return;

            Vector3 pushDir = Vector3.forward * _speed;

            float forceStrenght = RotateSpeed();
            playerRb.AddForce(pushDir * forceStrenght, ForceMode.VelocityChange);
        }
    }
}
