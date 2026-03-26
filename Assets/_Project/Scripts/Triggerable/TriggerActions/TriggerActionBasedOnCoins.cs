
using UnityEngine;

public class TriggerActionBasedOnCoins : TriggerAction
{
    [Header ("Coin Settings")]
    [SerializeField] private int _requiredCoins;

    protected override void OnTriggerEnter(Collider other)
    {
        if (!CoinsManager.Instance.HasReachedCoins(_requiredCoins)) return;

        base.OnTriggerEnter(other);
    }
}
