
using UnityEngine;
using UnityEngine.Events;

public class CoinsManager : GenericSingleton<CoinsManager>
{
    [Header ("Events")]
    [SerializeField] private UnityEvent<int> _onCoinsChanged;

    [Header ("Coins")]
    [SerializeField] private int _coins;

    private void SetCoins(int coins)
    {
        coins = Mathf.Max(coins, 0);
        _coins = coins;
        _onCoinsChanged.Invoke(_coins);
    }

    public void AddCoins(int amount)
    {
        SetCoins(_coins + amount);        
    }

    public bool HasReachedCoins(int requiredCoins) => _coins >= requiredCoins;
}
