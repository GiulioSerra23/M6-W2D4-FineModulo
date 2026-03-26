
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TurretDetection _detection;

    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSpeed = 5f;

    private void RotateHeadToTarget()
    {
        if (_detection.Target == null) return;

        Vector3 direction = _detection.Target.position - transform.position;
        direction.y = 0f;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        RotateHeadToTarget();
    }
}
