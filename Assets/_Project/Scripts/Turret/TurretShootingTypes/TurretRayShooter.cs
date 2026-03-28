
using UnityEngine;

public abstract class TurretRayShooter : MonoBehaviour
{
    [Header("Sound ID")]
    [SerializeField] private SoundID _shootSound;

    [Header ("References")]
    [SerializeField] protected Transform _firePoint;
    [SerializeField] protected PoolType _poolType;

    [Header ("Detection Range Settings")]
    [SerializeField] protected bool _useDetectionRange = true;
    [SerializeField] protected TurretDetection _detection;

    [Header ("Shooting Settings")]
    [SerializeField] protected float _fireRate = 1f;
    [SerializeField] protected float _speed;

    [Header ("RayCast Settings")]
    [SerializeField] protected bool _useRayCast = true;
    [SerializeField] protected float _maxDistance;    
    [SerializeField] protected LayerMask _hitLayer;

    protected float _lastShot;

    protected bool CanShootNow() => Time.time - _lastShot >= _fireRate;

    protected bool RayCastShot(Vector3 direction, out RaycastHit hitInfo)
    {
        Debug.DrawRay(_firePoint.position, direction * _maxDistance, Color.yellow, _fireRate);
        return (Physics.Raycast(_firePoint.position, direction, out hitInfo, _maxDistance, _hitLayer));
    }

    protected void InstantiateBullet(Vector3 direction)
    {
        ObjectPool bulletPool = PoolManager.Instance.GetPool(_poolType);
        PoolableObject obj = bulletPool.GetObject();
        Bullet bullet = obj as Bullet;

        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;
        bullet.SetUp(direction, _speed);
        AudioManager.Instance.Play3DAttached(_shootSound, transform);
    }

    protected void ShootBasedOnUseRayCast(Vector3 direction)
    {
        if (_useRayCast)
        {
            if (RayCastShot(direction, out RaycastHit hitInfo))
            {
                InstantiateBullet(direction);
            }
        }
        else
        {
            InstantiateBullet(direction);
        }
    }

    protected abstract void Fire();

    protected void ShootTarget()
    {
        if (_useDetectionRange && !_detection.Target) return;
        if (!CanShootNow()) return;

        Fire();
        _lastShot = Time.time;
    }

    protected virtual void Update()
    {
        ShootTarget();
    }
}
