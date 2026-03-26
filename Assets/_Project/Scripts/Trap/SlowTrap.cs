
using System.Collections;
using UnityEngine;

public class SlowTrap : PoolableObject
{
    [Header ("Slow Settings")]
    [SerializeField] private float _slowMultiplier = 0.5f;
    [SerializeField] private float _slowDuration = 5;
    [SerializeField] private float _lifeSpan = 5;

    private Coroutine _slowRoutine;
    private Mover3D _mover;

    private float _lifeStartTime;
    private bool IsLifeFinished() => Time.time - _lifeStartTime >= _lifeSpan;

    public override void OnSpawned()
    {
        _mover = null;
        _lifeStartTime = Time.time;
    }

    public override void OnDespawned()
    {
        if (_slowRoutine != null)
        {
            StopCoroutine(_slowRoutine);
            _slowRoutine = null;
        }
        if (_mover != null)
        {
            _mover.ResetSpeedMultiplier();
            _mover = null;
        }
    }

    private IEnumerator SlowRoutine()
    {
        _mover.SetSpeedMultiplier(_slowMultiplier);

        yield return new WaitForSeconds(_slowDuration);

        if (_mover != null )
        {
            _mover.ResetSpeedMultiplier();
            _mover = null;
        }

        _slowRoutine = null;
    }


    private void Update()
    {
        if (IsLifeFinished())
        {
            Release();
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Mover3D>(out var mover)) return;
        if (_slowRoutine != null) return;

        _mover = mover;
        _slowRoutine = StartCoroutine(SlowRoutine());
    }
}