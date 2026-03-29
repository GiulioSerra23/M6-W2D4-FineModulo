
using System;
using UnityEngine;
using UnityEngine.Events;

public class CoinsManager : GenericSingleton<CoinsManager>
{
    [Header ("Coins")]
    [SerializeField] private int _coins;

    public event Action<int> OnCoinsChanged;

    private void SetCoins(int coins)
    {
        coins = Mathf.Max(coins, 0);
        _coins = coins;
        OnCoinsChanged?.Invoke(_coins);
    }

    public void AddCoins(int amount)
    {
        SetCoins(_coins + amount);        
    }

    public bool HasReachedCoins(int requiredCoins) => _coins >= requiredCoins;
}
