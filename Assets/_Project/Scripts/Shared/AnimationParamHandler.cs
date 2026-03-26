using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParamHandler : MonoBehaviour
{
    [SerializeField] private string _hSpeedName = "hSpeed";
    [SerializeField] private string _vSpeedName = "vSpeed";
    [SerializeField] private string _level = "level";
    [SerializeField] private string _isMoving = "isMoving";
    [SerializeField] private string _isDeadName = "isDead";
    [SerializeField] private string _isHasWeaponName = "hasWeapon";
    [SerializeField] private string _isHitName = "isHit";    
    [SerializeField] private string _isAttackingName = "isAttacking";    
    [SerializeField] private string _isOpenName = "isOpen";    

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    public void SetDirectionAndSetMoving(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            SetDirectionalSpeed(direction);
            SetIsMoving(true);
        }
        else
        {
            SetIsMoving(false);
        }
    }

    public void SetDirectionAndSetMoving(Vector2 direction, float threshold)
    {
        if (direction.sqrMagnitude < threshold * threshold)
        {
            SetIsMoving(false);
        }
        else
        {
            SetDirectionalSpeed(direction);
            SetIsMoving(true);
        }
    }

    public void SetDirectionalSpeed(Vector2 speed)
    {
        SetHorizontalSpeed(speed.x);
        SetVerticalSpeed(speed.y);
    }

    public void SetHorizontalSpeed(float speed)
    {
        _anim.SetFloat(_hSpeedName, speed);
    }

    public void SetVerticalSpeed(float speed)
    {
        _anim.SetFloat(_vSpeedName, speed);
    }

    public void SetLevel(float level)
    {
        _anim.SetFloat(_level, level);
    }

    public void SetIsMoving(bool isMoving)
    {
        _anim.SetBool(_isMoving, isMoving);
    }

    public void SetHasWeapon(bool hasWeapon)
    {
        _anim.SetBool(_isHasWeaponName, hasWeapon);
    }

    public void SetIsDead()
    {
        _anim.SetTrigger(_isDeadName);
    }

    public void SetIsHit()
    {
        _anim.SetTrigger(_isHitName);
    }

    public void SetIsAttacking()
    {
        _anim.SetTrigger(_isAttackingName);
    }

    public void SetIsOpen()
    {
        _anim.SetTrigger(_isOpenName);
    }
}
