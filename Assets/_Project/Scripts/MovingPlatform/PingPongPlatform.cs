
using UnityEngine;

public class PingPongPlatform : MovingPlatform
{
    [Header("Target Point")]
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    private float _time;
    private bool _hasBeenTriggeredOnce = false;

    protected override void Move()
    {
        if (!_isMoving) return;

        transform.position = Vector3.Lerp(_pointA.position, _pointB.position, CalculateTime());
    }    

    private float CalculateTime()
    {
        _time += _speed * Time.deltaTime;

        switch (_movingMode)
        {
            case PlatformMoveType.OneWay:
                return OneWayTime();
            case PlatformMoveType.OneWayReturn:
                return OneWayReturnTime();
            case PlatformMoveType.Loop:
                return LoopTime();
        }

        return 0f;
    }

    private float OneWayTime()
    {
        float time = Mathf.Clamp01(_time += _speed * Time.deltaTime);

        if (time >= 1f) _isMoving = false;

        return time;
    }

    private float OneWayReturnTime()
    {
        if (_time < 1f) return _time;

        if (_time < 2f) return 2f - _time;

        _isMoving = false;

        return 0f;
    }

    private float LoopTime()
    {
        return Mathf.PingPong(_time, 1f);
    }

    public override void TriggerEnter(Collider other)
    {
        if (_movingMode == PlatformMoveType.Loop)
        {
            base.TriggerEnter(other);
            return;
        }

        if (_hasBeenTriggeredOnce && !_canRetrigger) return;

        if (_isMoving) return;

        _time = 0f;
        _isMoving = true;

        _hasBeenTriggeredOnce = true;
        
        base.TriggerEnter(other);
    }
}