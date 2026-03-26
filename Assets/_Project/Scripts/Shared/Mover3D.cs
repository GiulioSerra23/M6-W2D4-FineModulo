
using UnityEngine;

public class Mover3D : MonoBehaviour
{
    [Header ("Movement Settings")]    
    [SerializeField] protected float _baseSpeed = 4f;
    [SerializeField] private CameraOrbit _cameraOrbit;

    [Header ("Jump Settings")]
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] protected float _jumpForce = 4f;
    [SerializeField] private int _maxJumps = 2;

    protected AnimationParamHandler _animHandler;
    protected Rigidbody _rb;
    protected Vector3 _input;

    private float _speedMultipier = 1f;
    private int _jumpCount;
    protected bool _isJumping;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animHandler = GetComponent<AnimationParamHandler>();
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        _speedMultipier = multiplier;
    }

    public void ResetSpeedMultiplier()
    {
        SetSpeedMultiplier(1f);
    }

    public void SetMovementInput(Vector3 input)
    {
        _input = _cameraOrbit.ConvertInputToCameraDirection(input);
    }

    public void Jump()
    {
        if (_groundCheck.IsGrounded)
        {
            _jumpCount = 0;
            _isJumping = false;
        }

        if (!_groundCheck.IsGrounded && _jumpCount == 0) return;

        if (_jumpCount >= _maxJumps) return;

        _isJumping = true;
        _jumpCount++;
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);

        if (!_isJumping) _animHandler.OnJump();
    }

    private void Move()
    {
        float currentSpeed = _baseSpeed * _speedMultipier;
        Vector3 velocity = _input * currentSpeed;
        _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
    }

    private void Rotate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_input);
        Quaternion rotation = Quaternion.Slerp(_rb.rotation, targetRotation, _baseSpeed * Time.deltaTime);
        _rb.MoveRotation(rotation);
    }    

    public void ResetMoveAndRotateKeepingY()
    {
        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        _rb.angularVelocity = Vector3.zero;
    }

    public void ResetMoveAndRotate()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    private void MoveAndRotate()
    {
        if (_input != Vector3.zero)
        {
            Move();
            Rotate();
        }
        else
        {
            ResetMoveAndRotateKeepingY();
        }
    }

    protected virtual void FixedUpdate()
    {
        MoveAndRotate();
    }
}
