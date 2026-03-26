using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDropTable : MonoBehaviour
{
    [SerializeField] private List<DropItem> _dropItems;

    public void RandomDrop(Vector2 dropPosition)
    {
        float randomNum = Random.Range(0f, 100f);
        float accumulatedChance = 0f;
        
        foreach (DropItem dropItem in _dropItems)
        {
            accumulatedChance += dropItem.DropChance;

            if (randomNum <= accumulatedChance)
            {
                Instantiate(dropItem.DropItemPrefab, dropPosition, Quaternion.identity);
                return;
            }
        }
    }
}
