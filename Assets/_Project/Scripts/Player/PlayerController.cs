
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _sprintMultiplier = 1.8f;

    private Mover3D _mover;
    private AnimationParamHandler _animHandler;
    private Vector3 _direction;

    private float _horizontal;
    private float _vertical;
    private bool  _isSprinting;

    private void Awake()
    {
        _mover = GetComponent<Mover3D>();
        _animHandler = GetComponent<AnimationParamHandler>();
    }    

    private void HandleJump()
    {
        if (Input.GetButtonDown(Inputs.Space))
        {
            _mover.Jump();
        }
    }

    private void HandleSprint()
    {
        _isSprinting = Input.GetKey(KeyCode.LeftShift) && _direction.magnitude > 0.1f;

        if (_isSprinting)
        {
            _mover.SetSpeedMultiplier(_sprintMultiplier);
        }
        else
        {
            _mover.ResetSpeedMultiplier();
        }
    }

    private void Update()
    {
        if (UI_State.IsUIOpen) return;

        _horizontal = Input.GetAxis(Inputs.Horizontal);
        _vertical = Input.GetAxis(Inputs.Vertical);

        _direction = new Vector3(_horizontal, 0f, _vertical);

        HandleJump();

        HandleSprint();

        float animSpeed = _direction.magnitude * (_isSprinting ? 1.5f : 1f);

        _animHandler.SetForward(animSpeed);
    }

    private void FixedUpdate()
    {
        _mover.SetMovementInput(_direction);        
    }
}
