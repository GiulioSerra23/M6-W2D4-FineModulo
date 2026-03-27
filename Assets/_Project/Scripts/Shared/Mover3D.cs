using UnityEngine;

public class Mover3D : MonoBehaviour
{
    [Header ("Movement Settings")]    
    [SerializeField] private float _baseSpeed = 4f;
    [SerializeField] private CameraOrbit _cameraOrbit;

    [Header ("Jump Settings")]
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private float _jumpForce = 4f;
    [SerializeField] private int _maxJumps = 2;

    private AnimationParamHandler _animHandler;
    private Rigidbody _rb;
    private Vector3 _input;

    private float _speedMultipier = 1f;
    private int _jumpCount;

    private void OnEnable()
    {
        _groundCheck.OnIsGroundedChange += HandleGroundedChanged;
    }

    private void Awake()
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
        if (_jumpCount >= _maxJumps) return;

        _jumpCount++;        

        Vector3 velocity = _rb.velocity;
        velocity.y = _jumpForce;
        _rb.velocity = velocity;

        _animHandler.OnJump();
    }

    public void HandleGroundedChanged(bool isGrounded)
    {
        if (isGrounded)
        {
            _jumpCount = 0;
            _animHandler.ResetOnJump();            
        }

        _animHandler.OnIsGroundedChanged(isGrounded);
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
        if (_input.sqrMagnitude > 0.01f)
        {
            Move();
            Rotate();
        }
        else
        {
            ResetMoveAndRotateKeepingY();
        }
    }

    private void FixedUpdate()
    {
        MoveAndRotate();
    }

    private void OnDisable()
    {
        _groundCheck.OnIsGroundedChange -= HandleGroundedChanged;
    }
}