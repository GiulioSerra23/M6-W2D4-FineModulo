using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Creature
{
    private float _horizontal;
    private float _vertical;
    private float _level;    

    public float Level { get => _level; set => _level = value; }

    public int Damage { get => _damage; set => _damage = value; }

    public Vector2 Direction { get; private set; }

    public void SetLevel()
    {
        _level++;
        _animHandler.SetLevel(_level);
    }

    private void Update()
    {
        if (!CanMove) return;

        _horizontal = Input.GetAxisRaw(Inputs.Horizontal);
        _vertical = Input.GetAxisRaw(Inputs.Vertical);

        Direction = new Vector2(_horizontal, _vertical);

        _mover2D.SetAndNormalizeInput(Direction);

        if (_animHandler != null) _animHandler.SetDirectionAndSetMoving(Direction);
    }


    private void FixedUpdate()
    {
        if (!CanMove)
        {
            _mover2D.Stop();
            return;
        }

        _mover2D.Move();
    }
}
