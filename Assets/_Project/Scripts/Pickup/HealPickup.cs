
using UnityEngine;

public class HealPickup : Pickup
{
    [Header ("Heal Settings")]
    [SerializeField] private int _healAmount;

    public override void OnPick(GameObject picker)
    {
        base.OnPick(picker);

        if (!picker.TryGetComponent<LifeController>(out var lifeController)) return;

        lifeController.AddHp(_healAmount);
    }
}
