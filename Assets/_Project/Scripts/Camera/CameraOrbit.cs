
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [Header ("Camera Settings")]
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _rotationSpeed = 2f;

    [Header ("Pitch Settings")]
    [SerializeField] private float _minYAngle = 0f;
    [SerializeField] private float _maxYAngle = 50f;

    [Header("Zoom Settings")]
    [SerializeField] private float _distance = 5f;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _minZoom = 2f;
    [SerializeField] private float _maxZoom = 8f;

    private float _pitch;
    private float _yaw;

    private void Start()
    {
        LockMouse();
    }
    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis(Inputs.ScrollWheel);

        _distance -= scroll * _zoomSpeed;
        _distance = Mathf.Clamp(_distance, _minZoom, _maxZoom );
    }

    public Vector3 ConvertInputToCameraDirection(Vector3 input)
    {
        Vector3 cameraForward = transform.forward;
        Vector3 cameraRight = transform.right;

        Vector3 moveDir = cameraForward * input.z + cameraRight * input.x;
        moveDir.y = 0;

        if (moveDir.magnitude > 0.01f) moveDir.Normalize();

        return moveDir;
    }

    private void HandleRotation()
    {
        _yaw += Input.GetAxis(Inputs.MouseX) * _rotationSpeed;
        _pitch -= Input.GetAxis(Inputs.MouseY) * _rotationSpeed;
        _pitch = Mathf.Clamp(_pitch, _minYAngle, _maxYAngle);
    }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);

        Vector3 direction = rotation * Vector3.back;
        Vector3 desideredPosition = _target.position + direction * _distance;

        Vector3 lookAt = _target.position + _offset;
        Quaternion lookRotation = Quaternion.LookRotation(lookAt - desideredPosition);
        transform.SetPositionAndRotation(desideredPosition, lookRotation);
    }

    private void LateUpdate()
    {
        if (UI_State.IsUIOpen) return;
        if (_target == null) return;

        HandleRotation();
        HandleZoom();
        UpdateCameraPosition();
    }
}
