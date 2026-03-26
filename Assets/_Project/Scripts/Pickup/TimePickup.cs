
using UnityEngine;

public class TimePickup : Pickup
{
    [Header ("Time Settings")]
    [SerializeField] private float _timeAmount = 10f;

    public override void OnPick(GameObject player)
    {
        base.OnPick(player);

        TimerManager.Instance.AddTime(_timeAmount);
    }
}
