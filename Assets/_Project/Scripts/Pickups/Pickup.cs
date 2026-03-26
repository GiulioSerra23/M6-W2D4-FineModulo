using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float _altitude = 0.1f;
    [SerializeField] private float _speed = 2f;

    private PickupEffects _effect;
    private Vector3 _startPosition;

    private void Awake()
    {
        _effect = GetComponent<PickupEffects>();
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        float offSetY = Mathf.Sin(Time.time * _speed) * _altitude;
        transform.position = _startPosition + Vector3.up * offSetY;
    }

    private void OnTriggerEnter2D(Collider2D collsion)
    {
        if (!collsion.CompareTag(Tags.Player)) return;

        if (_effect != null)
        {
            _effect.OnPick(collsion.gameObject);
        }

        Destroy(gameObject);
    }
}
