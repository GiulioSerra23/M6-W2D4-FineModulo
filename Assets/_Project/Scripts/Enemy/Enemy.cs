using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    [SerializeField] private float _speed;

    private EnemyManager _enemyManager;
    private PlayerController _player;
    private Rigidbody2D _rb;

    private bool _playerInTrigger;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();
        _enemyManager = FindObjectOfType<EnemyManager>();
        _enemyManager.AddEnemy(this);
    }

    private void EnemyMovement()
    {
        if (_player == null) return;

        Vector2 currentPos = _rb.position;
        Vector2 targetPos = _player.transform.position;

        Vector2 direction = (targetPos - currentPos).normalized;

        Vector2 newPos = Vector2.MoveTowards(currentPos, targetPos, _speed * Time.fixedDeltaTime);

        _rb.MovePosition(newPos);

        if (_animHandler != null) _animHandler.SetDirectionAndSetMoving(direction);
    }

    public override void Die()
    {
        base.Die();
        _enemyManager.RemoveEnemy(this);
    }

    private void FixedUpdate()
    {
        if (_playerInTrigger)
        {
            if (!CanMove) return;

            EnemyMovement();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PlayerController>(out var player))
        { 
            var lifeController = collision.collider.GetComponent<LifeController>();
        
            player.Hit(_damage);

            if (lifeController.Hp <= 0)
            {
                player.Die();
            }

            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out var player))
        {
            _playerInTrigger = true;
        }
    }
}
