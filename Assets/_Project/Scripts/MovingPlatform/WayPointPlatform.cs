using UnityEngine;

public class WayPointPlatform : MovingPlatform
{
    [Header ("Path")]
    [SerializeField] private Transform[] _waypoints;

    [Header ("Movement")]
    [SerializeField] private float _rotateSpeed = 2f;

    private int _currentIndex = 0;
    private int _direction = 1;

    protected override void Move()
    {
        if (_waypoints.Length == 0 || !_isMoving) return;

        Movement();
        RotateTowardsNext();
    }

    private void UpdateIndex()
    {
        switch (_movingMode)
        {
            case PlatformMoveType.Loop:
                HandleLoop();
                break;

            case PlatformMoveType.OneWay:
                HandleOneWay();
                break;

            case PlatformMoveType.OneWayReturn:
                HandleOneWayReturn();
                break;
        }
    }

    private void Movement()
    {
        Vector3 targetPosition = _waypoints[_currentIndex].position;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < Mathf.Epsilon)
        {
            UpdateIndex();
        }
    }

    private void HandleLoop()
    {
        _currentIndex = (_currentIndex += _direction) % _waypoints.Length;
    }

    private void HandleOneWay()
    {
        _currentIndex += _direction;
        if (_currentIndex >= _waypoints.Length)
        {
            _currentIndex = _waypoints.Length - 1;
            _isMoving = false;
        }
    }

    private void HandleOneWayReturn()
    {
        _currentIndex += _direction;

        if ( _currentIndex >= _waypoints.Length - 1)
        {
            _direction = -1;
            _currentIndex = _waypoints.Length - 1;
        }
        else if (_currentIndex < 0)
        {
            _direction = 1;
            _currentIndex = 0;
            _isMoving = false;
        }
    }

    private void RotateTowardsNext()
    {
        if (_waypoints.Length == 0) return;

        Vector3 direction = _waypoints[_currentIndex].position - transform.position;

        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.fixedDeltaTime);
    }
}
