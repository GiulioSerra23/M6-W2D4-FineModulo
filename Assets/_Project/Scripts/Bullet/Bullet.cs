
using UnityEngine;

public class Bullet : PoolableObject
{
    [Header ("Sound ID")]
    [SerializeField] protected SoundID _collisionSound;

    [Header("Bullet Settings")]
    [SerializeField] protected int _damage;

    protected Rigidbody _rb;

    public virtual void SetUp(Vector3 direction, float speed)
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();

        _rb.velocity = direction * speed;
        transform.up = direction;
    }

    public override void OnSpawned()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (TryGetComponent<SpawnTrapOnCollision>(out var trapSpawner))
        {
            trapSpawner.TryToSpawn(collision);
        }

        if (collision.collider.TryGetComponent<LifeController>(out var lifeController))
        {
            lifeController.TakeDamage(_damage);
        }

        AudioManager.Instance.Play(_collisionSound);
        Release();
    }
}
