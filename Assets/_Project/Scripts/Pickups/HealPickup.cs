using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : PickupEffects
{
    [SerializeField] private int _healAmount;

    public override void OnPick(GameObject picker)
    {
        var LifeController = picker.GetComponent<LifeController>();

        if (LifeController != null )
        {
            LifeController.AddHp(_healAmount);
        }
    }
}
