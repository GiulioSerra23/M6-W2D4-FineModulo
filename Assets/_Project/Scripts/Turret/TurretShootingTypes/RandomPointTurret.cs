
using UnityEngine;

public class RandomPointTurret : TurretRayShooter
{
    [Header ("Targets")]
    [SerializeField] private Transform[] _targetPoints;

    protected override void Fire()
    {
        int randomIndex = Random.Range(0, _targetPoints.Length);

        Vector3 direction = (_targetPoints[randomIndex].position - _firePoint.position).normalized;
        ShootBasedOnUseRayCast(direction);
    }
}
