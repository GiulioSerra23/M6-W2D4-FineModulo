
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _fireRange;

    private EnemyManager _enemyManager;
    private PlayerController _player;
    private AnimationParamHandler _animHandler;
    private Enemy _currentTarget;
    private Enemy _attackTarget;

    private bool _isAttacking = false;
    private bool _hasShot = false;
    private float _lastShot;

    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }   

    private void Awake()
    {
        _enemyManager = FindObjectOfType<EnemyManager>();
        _player = GetComponentInParent<PlayerController>();
        _animHandler = GetComponentInParent<AnimationParamHandler>();
    }

    public Enemy FindNearestEnemy()
    {
        Enemy nearestEnemy = null;

        float minDistance = _fireRange;

        foreach (var enemy in _enemyManager.Enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance <= minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private bool CanShootNow()
    {
        return Time.time - _lastShot >= _fireRate;
    }

    public bool CanStartAttack()
    {
        if (!CanShootNow()) return false;

        if (_currentTarget == null || _currentTarget.IsDead) return false;

        return true;
    }

    public void AttackTarget()
    {
        if (!_player.CanMove)
        {
            _isAttacking = false;
        }

        if (!_isAttacking && _player.CanMove && CanStartAttack())
        {
            _isAttacking = true;
            _hasShot = false;
            _attackTarget = _currentTarget;
            _animHandler.SetIsAttacking();
        }
    }

    public void ShootFromAnimation()
    {
        if (_hasShot) return;
        if (_attackTarget == null || _attackTarget.IsDead) return;

        Vector2 direction = (_attackTarget.transform.position - transform.parent.position).normalized;

        Shoot(direction);
        _hasShot = true;
    }

    private void Shoot(Vector2 direction)
    {
        Bullet clonedBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity, transform);
        clonedBullet.SetUp(direction);

        _lastShot = Time.time;
    }

    private void Update()
    {
        _currentTarget = FindNearestEnemy();
        AttackTarget();
    }
}
