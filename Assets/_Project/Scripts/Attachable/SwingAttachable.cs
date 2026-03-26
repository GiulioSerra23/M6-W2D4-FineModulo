
using UnityEngine;

public class SwingAttachable : MonoBehaviour, IAttachable
{
    [Header("Attach Points")]
    [SerializeField] private Transform _pivotPoint;

    [Header("Swing Settings")]
    [SerializeField] private float _maxSwingAngle = 45f;
    [SerializeField] private float _safeDetachAngle = 40f;
    [SerializeField] private float _swingForce = 5f;
    [SerializeField] private float _jumpOffForce = 5f;
    [SerializeField] private float _jumpUpForce = 5f;

    private Rigidbody _rb;
    private Rigidbody _playerRb;
    private HingeJoint _hingeJoint;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    public bool RequiresInputToAttach => false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _initialPosition = transform.position;
        _initialRotation =  transform.rotation;
    }

    public void OnAttach(AttachableMovement player)
    {
        _playerRb = player.Rb;

        _hingeJoint = _playerRb.gameObject.AddComponent<HingeJoint>();
        _hingeJoint.connectedBody = GetComponent<Rigidbody>();
        _hingeJoint.axis = Vector3.right;
        _hingeJoint.useLimits = true;        
        _hingeJoint.limits = new JointLimits { min = -_maxSwingAngle, max = _maxSwingAngle };
        _hingeJoint.useSpring = false;

        _playerRb.AddForce(_pivotPoint.forward * 1f, ForceMode.VelocityChange);
    }

    public void HandleAttachedInput(AttachableMovement player)
    {
        if (!_playerRb) return;

        float input = Input.GetAxis(Inputs.Vertical);

        if (Mathf.Abs(input) < 0.05f) return;

        Vector3 toPlayer = (_playerRb.position - _pivotPoint.position).normalized;

        Vector3 swingTangent = Vector3.Cross(Vector3.right, toPlayer).normalized;

        Vector3 zForce = Vector3.Project(swingTangent, _pivotPoint.forward);

        _playerRb.AddForce(zForce * input * _swingForce, ForceMode.Force);
    }   

    private bool IsWithinSafeAngle()
    {
        Vector3 toPlayer = (_playerRb.position - _pivotPoint.position).normalized;

        float angle = Vector3.Angle(Vector3.down, toPlayer);

        return angle < _safeDetachAngle;
    }

    private void JumpOff()
    {
        Vector3 velocity = _playerRb.velocity;
        velocity.y = 0f;

        if (velocity.magnitude < 0.1f) velocity = _pivotPoint.forward;

        Vector3 jumpDir = velocity.normalized * _jumpOffForce + Vector3.up * _jumpUpForce;

        _playerRb.AddForce(jumpDir, ForceMode.VelocityChange);
    }

    private void ResetPosition()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.MovePosition(_initialPosition);
        _rb.MoveRotation(_initialRotation);
    }

    public void OnDetach(AttachableMovement player, bool isForced)
    {
        player.LockAttach();

        bool canJumpOff = !isForced && IsWithinSafeAngle();

        if (_hingeJoint != null)
        {
            Destroy(_hingeJoint);
            _hingeJoint = null;
        }

        if (canJumpOff) JumpOff();

        _playerRb = null;

        Invoke(nameof(ResetPosition), 1f);
    }
}
