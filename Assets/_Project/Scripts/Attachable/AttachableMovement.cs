
using UnityEngine;

public class AttachableMovement : MonoBehaviour
{
    [Header ("Attach Settings")]
    [SerializeField] private float _attachCooldown = 0.5f;

    [Header ("Attach Button")]
    [SerializeField] private string _attachButton = Inputs.E;

    private IAttachable _currentAttachable;
    private IAttachable _pendingAttachable;
    private Rigidbody _rb;
    private AnimationParamHandler _animHandler;

    private float _attachLockTimer;

    public Rigidbody Rb => _rb;
    public bool IsAttached => _currentAttachable != null;
    public bool CanAttach => _attachLockTimer <= 0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animHandler = GetComponent<AnimationParamHandler>();
    }

    private void Attach(IAttachable attachable)
    {
        _currentAttachable = attachable;
        attachable.OnAttach(this);
        _animHandler.OnIsAttachedChanged(IsAttached);

        GetComponent<Mover3D>().enabled = false;
    }

    private void Detach(bool isForced)
    {
        _currentAttachable.OnDetach(this, isForced);
        _currentAttachable = null;
        _animHandler.OnIsAttachedChanged(IsAttached);

        GetComponent<Mover3D>().enabled = true;
    }

    public void LockAttach()
    {
        _attachLockTimer = _attachCooldown;
    }

    public void ForceDetach()
    {
        if (!IsAttached) return;

        Detach(true);
    }

    private void Update()
    {
        if (_attachLockTimer > 0f) _attachLockTimer -= Time.deltaTime;

        if (!IsAttached && _pendingAttachable != null && CanAttach)
        {
            if (Input.GetButtonDown(_attachButton))
            {
                Attach(_pendingAttachable);
            }
        }

        if (!IsAttached) return;        

        _currentAttachable.HandleAttachedInput(this);

        if (Input.GetButtonDown(Inputs.Space))
        {
            Detach(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAttached) return;

        if (!CanAttach) return; 

        if (!other.TryGetComponent<IAttachable>(out var attachable)) return;

        if (attachable.RequiresInputToAttach)
        {
            _pendingAttachable = attachable;
        }
        else
        {
            Attach(attachable);
        }            
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<IAttachable>(out var attachable)) return;

        if (_pendingAttachable == attachable)
        {
            _pendingAttachable = null;
        }
    }
}
