
using System;
using UnityEngine;

public enum PoolType
{
    POOL_BULLET_LOG,
    POOL_BULLET_SPIKE,
    POOL_BULLET_COCONUT,
    POOL_BULLET_BANANA,
    POOL_BULLET_BANANANOALT,
    POOL_BULLET_ROCK,

    POOL_TRAP_SLOW,
    POOL_TRAP_LOG,
    POOL_AUDIOSOURCE,
}

[System.Serializable]
public class PoolEntry
{
    [SerializeField] PoolType _poolType;
    [SerializeField] ObjectPool _pool;

    public PoolType PoolType => _poolType;
    public ObjectPool Pool => _pool;
}
