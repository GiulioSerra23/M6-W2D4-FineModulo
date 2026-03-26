
using UnityEngine;

public class TurretSingleShot : TurretRayShooter
{
    [Header ("New Target Settings")]
    [SerializeField] bool _shootOnNewDetection = false;
    [SerializeField] private Transform _targetNotDetection;

    protected override void Fire()
    {
        Vector3 direction = CalculateDirection();
        ShootBasedOnUseRayCast(direction);
    }

    private Vector3 CalculateDirection()
    {
        if (_shootOnNewDetection)
        {
            return (_targetNotDetection.position - _firePoint.position).normalized;
        }
        else
        {            
            return (_detection.Target.position - _firePoint.position).normalized;
        }
    }
}