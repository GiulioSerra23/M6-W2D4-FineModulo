
using UnityEngine;

public class PetOrbit : MonoBehaviour
{
    [Header("Orbit Setting")]
    [SerializeField] float _orbitRadius = 1.5f;
    [SerializeField] float _orbitSpeed = 90f;
    [SerializeField] float _heightOffset = 0.5f;
    [SerializeField] float _followSmooth = 5f;

    [Header ("Scale Settings")]
    [SerializeField] float _scaleMultiplier = 0.5f;

    private Transform _target;
    float _angle;
    bool _isOrbiting;

    private bool CanOrbit() => _isOrbiting && _target != null;

    private void UpdateOrbitAngle()
    {
        _angle += _orbitSpeed * Time.deltaTime;
    }

    private Vector3 CalculateOrbitPosition()
    {
        Vector3 horizonatlOffset = new Vector3(Mathf.Cos(_angle * Mathf.Deg2Rad) * _orbitRadius, 0f , Mathf.Sin(_angle * Mathf.Deg2Rad) * _orbitRadius);

        Vector3 position = _target.position + horizonatlOffset;
        position.y += _heightOffset;

        return position;
    }

    private void MoveToPosition(Vector3 targetPosition)
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _followSmooth);
    }

    private void FaceTarget()
    {
        transform.LookAt(_target.position);
    }

    private void StartOrbit(Transform newTarget)
    {
        _target = newTarget;
        _isOrbiting = true;
        _angle = Random.Range(0f, 360f);
    }

    private void ChangeScale()
    {
        transform.localScale *= _scaleMultiplier;
    }

    private void Update()
    {
        if (!CanOrbit()) return;

        UpdateOrbitAngle();
        Vector3 orbitPosition = CalculateOrbitPosition();
        MoveToPosition(orbitPosition);
        FaceTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isOrbiting) return;
        if (!other.CompareTag(Tags.Player)) return;

        ChangeScale();
        StartOrbit(other.transform);
    }
}
