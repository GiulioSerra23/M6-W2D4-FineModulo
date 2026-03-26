using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMover2D : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;

    private Rigidbody2D _rb;
    private Vector2 _input;

    public float Speed => _speed;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetInput(Vector2 input)
    {
        _input = input;
    }

    public void SetAndNormalizeInput(Vector2 input)
    {
        float sqrMagnitude = input.sqrMagnitude;
        if (sqrMagnitude > 1)
        {
            input = input.normalized;
        }
        SetInput(input);
    }

    public void Stop()
    {
        _input = Vector2.zero;
        _rb.velocity = Vector2.zero;
    }

    public void Move()
    {
        if (_input != Vector2.zero)
        {
            _rb.MovePosition(_rb.position + _input * (_speed * Time.fixedDeltaTime));
        }
    }
}
