
using UnityEngine;

public class DamageOnCollision : PoolableObject
{
    [Header("Sound ID")]
    [SerializeField] private SoundID _collisionSound;

    [Header ("Trap Settings")]
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeSpan = 10f;

    private Rigidbody _rb;

    private float _lifeStartTime;
    private bool IsLifeFinished() => Time.time - _lifeStartTime >= _lifeSpan;

    public override void OnSpawned()
    {
        _lifeStartTime = Time.time;
    }

    public override void OnDespawned()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    private void Update()
    {
        if (IsLifeFinished())
        {
            Release();
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.Play(_collisionSound);

        if (!collision.collider.CompareTag(Tags.Player)) return;

        if (!collision.collider.TryGetComponent<LifeController>(out var lifeController)) return;

        lifeController.TakeDamage(_damage);
    }
}
