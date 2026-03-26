using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;

    public List<Enemy> Enemies => _enemies;

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy); 
    }

    public void RemoveEnemy(Enemy enemy) 
    {
        _enemies.Remove(enemy);
    }
}
