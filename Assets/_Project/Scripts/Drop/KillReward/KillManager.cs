using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillManager : MonoBehaviour
{
    [SerializeField] private List<KillReward> _killRewards;

    private int _killCount;

    public bool HasReachedKills(int requireKills)
    {
        return _killCount >= requireKills;
    }

    private void CheckRewards(Vector2 dropPosition)
    {
        foreach (var reward in _killRewards)
        {
            if (!reward.IsGiven && HasReachedKills(reward.KillRequired))
            {
                Instantiate(reward.RewardItemPrefab, dropPosition, Quaternion.identity);
                reward.IsGiven = true;
            }
        }
    }

    public void OnKill(Vector2 dropPosition)
    {
        _killCount++;
        Debug.Log($"Uccisioni: {_killCount}");

        CheckRewards(dropPosition);        
    }
}
