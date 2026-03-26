using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPickup : PickupEffects
{
    public override void OnPick(GameObject picker)
    {
        var player = picker.GetComponent<PlayerController>();
        var weaponHolder = picker.GetComponentInChildren<WeaponHolder>();

        if (player != null)
        {
            player.SetLevel();

            if (weaponHolder.EquippedWeapon != null)
            {
                Destroy(weaponHolder.EquippedWeapon.gameObject);
                weaponHolder.HasWeapon(false);
            }

            Debug.Log("Sei aumentato di livello, la tua arma si è distrutta!");
        }
    }
}
