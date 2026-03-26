
using UnityEngine;

public class AltitudeBullet : Bullet
{
    [Header("Bullet Settings")]
    [SerializeField] private float _altitude = 10f;

    public override void SetUp(Vector3 direction, float speed)
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        _rb.velocity = direction * speed + Vector3.up * _altitude;
    }
}
