using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _maxHp;
    [SerializeField] private int _currentHp;

    public int MaxHp { get => _maxHp; set => SetMaxHp(value); }

    public int Hp { get => _currentHp; set => SetHp(value); }

    public void SetMaxHp(int maxHp) => _maxHp = maxHp;

    private void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHp);

        if (hp != _currentHp)
        {
            _currentHp = hp;
        }
    }

    public void AddHp(int amount) => SetHp(_currentHp + amount);

    public void TakeDamage(int amount) => SetHp(_currentHp - amount);
}
