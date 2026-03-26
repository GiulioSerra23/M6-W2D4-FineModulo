using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestWeapon : Chest
{
    [SerializeField] private Pickup _weaponPrefab;

    protected override void Drop()
    {
        Instantiate(_weaponPrefab, _newPos, Quaternion.identity);
    }
}
