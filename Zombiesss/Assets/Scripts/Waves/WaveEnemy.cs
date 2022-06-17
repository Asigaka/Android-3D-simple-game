using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveEnemy
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int enemiesAmount;

    public Enemy EnemyPrefab { get => enemyPrefab; }
    public int EnemiesAmount { get => enemiesAmount; }
}
