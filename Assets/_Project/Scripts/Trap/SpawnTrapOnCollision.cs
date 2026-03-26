
using UnityEngine;

public class SpawnTrapOnCollision : MonoBehaviour
{
    [SerializeField] private PoolType _poolType = PoolType.POOL_TRAP_SLOW;
    [SerializeField] private string _tag = Tags.Ground;

    public void TryToSpawn(Collision collision)
    {        
        if(!collision.collider.CompareTag(_tag)) return;

        ObjectPool trapPool = PoolManager.Instance.GetPool(_poolType);
        PoolableObject obj = trapPool.GetObject();

        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;
    }
}