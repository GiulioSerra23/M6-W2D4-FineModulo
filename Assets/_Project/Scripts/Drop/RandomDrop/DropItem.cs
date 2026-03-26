using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    [SerializeField] private Pickup _dropItemPrefab;

    [SerializeField] private float _dropChance;

    public Pickup DropItemPrefab => _dropItemPrefab;

    public float DropChance => _dropChance;
}
