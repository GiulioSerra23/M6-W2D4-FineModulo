using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : Creature
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _minDistance = 2;
    [SerializeField] private float _maxDistance = 4;
    [SerializeField] private float _speed = 4;

    private Vector2 _lastPos;
    private bool _playerInTrigger;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (_target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(Tags.Player);

            if (player != null) _target = player.transform;
        }
    }

    private void Move()
    {
        Vector2 currentPos = transform.position;
        Vector2 targetPos = _target.position;

        Vector2 direction = (targetPos - currentPos).normalized;
        float distance = Vector2.Distance(transform.position, _target.position);;

        if (distance >= _minDistance && distance <= _maxDistance)
        {
            float distanceFactor = Mathf.InverseLerp(_minDistance, _maxDistance, distance);
            float currentSpeed = _speed * distanceFactor;

            transform.position = Vector2.MoveTowards(currentPos, targetPos, currentSpeed * Time.fixedDeltaTime);
        }

        Vector2 velocity = ((Vector2)transform.position - _lastPos) / Time.fixedDeltaTime;
        _lastPos = transform.position;

        if (_animHandler != null) _animHandler.SetDirectionAndSetMoving(velocity, 0.05f);
    }

    private void FixedUpdate()
    {
        if (_playerInTrigger && _target != null)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out var player))
        {
            if (player != null)
            {
                _playerInTrigger = true;
                player.Damage += _damage;
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
