using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesContainer : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;

    public void WakeUpEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.SetPlayerTarget();
        }
    }

    [ContextMenu("Fill Array")]
    public void FillArray()
    {
        enemies = new Enemy[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            enemies[i] = transform.GetChild(i).GetComponent<Enemy>();
        }
    }
}
