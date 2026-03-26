using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private Weapon _equippedWeapon;
    private AnimationParamHandler _animHandler;
    private bool _hasWeapon;

    public Weapon EquippedWeapon => _equippedWeapon;

    public bool IsAttacking { get => _equippedWeapon.IsAttacking; set => _equippedWeapon.IsAttacking = value; }

    private void Awake()
    {
        _animHandler = GetComponent<AnimationParamHandler>();
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        _equippedWeapon = Instantiate(newWeapon, transform);
        HasWeapon(true);
    }

    public void HasWeapon(bool hasWeapon)
    {
        _hasWeapon = hasWeapon;
        _animHandler.SetHasWeapon(_hasWeapon);
    }

    private void Update()
    {
        _equippedWeapon = GetComponentInChildren<Weapon>();
    }
}
