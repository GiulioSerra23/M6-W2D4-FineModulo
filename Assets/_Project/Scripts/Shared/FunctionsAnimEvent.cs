using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FunctionsAnimEvent : MonoBehaviour
{
    [SerializeField] private string _phrase;

    private Destroyable _destroyable;
    private WeaponHolder _weaponHolder;
    private Creature _creature;

    private void Awake()
    {
        _weaponHolder = GetComponentInParent<WeaponHolder>();
        _creature = GetComponentInParent<Creature>();
        _destroyable = GetComponentInParent<Destroyable>();
    }

    public void Shoot()
    {
        _weaponHolder.EquippedWeapon.ShootFromAnimation();
    }

    public void EndAttack()
    {
        _weaponHolder.IsAttacking = false;
    }

    public void MoveAfterHit()
    {
        _creature.EndHitFromAnimation();
    }

    public void ShowText()
    {
        Debug.Log($"{_phrase}");
    }

    public void Destroy()
    {
        if (_destroyable != null)
        {
            _destroyable.DestroySelf();
        }
    }

    public void DestroyColliders()
    {
        foreach (var collider2D in GetComponentsInParent<Collider2D>())
        {
            Destroy(collider2D);
        }
    }
}
