
using UnityEngine;

public class InflictDamageTriggerable : MonoBehaviour, ITriggerable
{
    [SerializeField] private int _damageAmount;
    [SerializeField] private float _interval = 1f;
    [SerializeField] private bool _damageOverTime = true;

    private float _lastDamageTime;
    private LifeController _target;

    private bool CanDamage() => Time.time - _lastDamageTime >= _interval;

    public void TriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<LifeController>(out var lifeController)) return;

        _target = lifeController;

        if (!_damageOverTime)
        {
            _target.TakeDamage(_damageAmount);
        }
        else
        {
            _lastDamageTime = Time.time;
        }
        
    }

    public void TriggerExit(Collider other)
    {
        if (_target != null && other.gameObject == _target.gameObject)
        {
            _target = null;
        }
    }

    private void Update()
    {
        if (!_damageOverTime) return;
        if (_target == null) return;

        if (CanDamage())
        {
            _lastDamageTime = Time.time;
            _target.TakeDamage(_damageAmount);
        }
    }    
}
