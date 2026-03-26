using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KillReward 
{
    [SerializeField] private Pickup _rewardItemPrefab;
    [SerializeField] private int _killRequired;

    private bool _isGiven;

    public Pickup RewardItemPrefab => _rewardItemPrefab;

    public int KillRequired => _killRequired;

    public bool IsGiven { get => _isGiven; set => _isGiven = value; }
}
