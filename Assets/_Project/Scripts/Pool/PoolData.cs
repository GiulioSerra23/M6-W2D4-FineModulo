
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
}

[System.Serializable]
public class PoolEntry
{
    public PoolType PoolType;
    public ObjectPool Pool;
}
