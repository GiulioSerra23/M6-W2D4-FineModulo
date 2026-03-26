
using UnityEngine;

public class RotatableAttachable : MonoBehaviour, IAttachable
{
    public enum RotationMode { NONE, YONLY, XONLY, XANDY }

    [Header ("Attach Points")]
    [SerializeField] private Transform _attachPoint;

    [Header ("Rotation Mode")]
    [SerializeField] private RotationMode _rotationMode;

    [Header ("Rotation Targets")]
    [SerializeField] private Transform _yawTarget;
    [SerializeField] private Transform _pitchTarget;

    [Header ("Rotation Setting")]
    [SerializeField] private float _yawSpeed = 90f;
    [SerializeField] private float _pitchSpeed = 90f;

    [Header("Pitch Limits")]
    [SerializeField] private float _minPitch = -15f;
    [SerializeField] private float _maxPitch = 35f;

    private float _currentPitch;
    private AttachableMovement _currentPlayer;

    public bool RequiresInputToAttach => true;

    public void OnAttach(AttachableMovement player) 
    {
        _currentPlayer = player;

        if (_attachPoint != null)
        {
            _currentPlayer.transform.position = _attachPoint.position;
            _currentPlayer.transform.rotation = _attachPoint.rotation;
        }

        Transform parent = _yawTarget != null ? _yawTarget : transform;
        _currentPlayer.transform.SetParent(parent);

        _currentPlayer.Rb.velocity = Vector3.zero;
        _currentPlayer.Rb.angularVelocity = Vector3.zero;
    }

    private void HandleYaw()
    {
        if (_yawTarget == null) return;

        float input = Input.GetAxisRaw(Inputs.Horizontal);

        if (Mathf.Abs(input) < 0.01f) return;

        float rotation = input * _yawSpeed * Time.deltaTime;

        _yawTarget.Rotate(-Vector3.up, rotation, Space.World);
    }

    private void HandlePitch()
    {
        if (_pitchTarget == null) return;

        float input = Input.GetAxisRaw(Inputs.Vertical);

        if (Mathf.Abs(input) < 0.01f) return;

        _currentPitch -= input * _pitchSpeed * Time.deltaTime;
        _currentPitch = Mathf.Clamp(_currentPitch, _minPitch, _maxPitch);

        _pitchTarget.localRotation = Quaternion.Euler(_currentPitch, 0f, 0f);

    }

    public void HandleAttachedInput(AttachableMovement player)
    {
        if (_rotationMode == RotationMode.NONE) return;

        if (_rotationMode == RotationMode.YONLY || _rotationMode == RotationMode.XANDY)
        {
            HandleYaw();
        }

        if (_rotationMode == RotationMode.XONLY || _rotationMode == RotationMode.XANDY)
        {
            HandlePitch();
        }
    }

    public void OnDetach(AttachableMovement player, bool isForced) 
    {
        if (_currentPlayer == null) return; 

        _currentPlayer.transform.SetParent(null);

        _currentPlayer = null;
    }
}
