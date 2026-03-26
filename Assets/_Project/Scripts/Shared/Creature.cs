using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Creature : MonoBehaviour
{ 
    [SerializeField] protected string _name;
    [SerializeField] protected int _damage;

    protected LifeController _lifeController;
    protected TopDownMover2D _mover2D;
    protected AnimationParamHandler _animHandler;

    protected int _hitStopCount;
    protected bool _isDead;

    public bool CanMove => _hitStopCount == 0 && !_isDead;

    public bool IsDead { get => _isDead; set => _isDead = value; }

    protected virtual void Awake()
    {
        _lifeController = GetComponent<LifeController>();
        _mover2D = GetComponent<TopDownMover2D>();
        _animHandler = GetComponent<AnimationParamHandler>();
    }      

    public virtual void Hit(int damage)
    {
        if (_isDead) return;   

        _lifeController.TakeDamage(damage);
        _hitStopCount++;
        _animHandler.SetIsHit();
    }

    public void EndHitFromAnimation()
    {
        _hitStopCount--;
        if (_hitStopCount < 0)
        {
            _hitStopCount = 0;
        }        
    }

    public virtual void Die()
    {
        _isDead = true;
        _animHandler.SetIsDead();
        GetComponent<Collider2D>().enabled = false;
    }
}