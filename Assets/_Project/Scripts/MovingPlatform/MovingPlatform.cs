
using UnityEngine;

public abstract class MovingPlatform : MonoBehaviour, ITriggerable
{
    public enum PlatformMoveType { OneWay, OneWayReturn, Loop }

    [Header ("Platform Settings")]
    [SerializeField] protected bool _startOnTrigger = false;
    [SerializeField] protected bool _canRetrigger = false;
    [SerializeField] protected bool _setParent;
    [SerializeField] protected float _speed;

    [Header("Platform Move Type")]
    [SerializeField] protected PlatformMoveType _movingMode = PlatformMoveType.Loop;

    protected bool _hasPlayer;
    protected bool _isActive;
    protected bool _isMoving;

    protected void Start()
    {
        _isActive = !_startOnTrigger;
        _isMoving = _isActive;
    }

    protected abstract void Move();

    protected virtual void FixedUpdate() 
    {
        if (!_isActive) return;
        
        Move();
    }

    public virtual void TriggerEnter(Collider other)
    {
        if (_isActive && !_isMoving && !_canRetrigger) return;

        _isActive = true;
        _isMoving = true;
    }

    public virtual void TriggerExit(Collider other) { }

    private void OnTriggerEnter(Collider other)
    {
        if (!_setParent) return;
        if (!other.gameObject.CompareTag(Tags.Player)) return;

        _hasPlayer = true;
        other.gameObject.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_setParent) return;
        if (!other.gameObject.CompareTag(Tags.Player)) return;

        _hasPlayer = false;
        other.gameObject.transform.SetParent(null);
    }
}
