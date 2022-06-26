using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private WaveEnemy[] waveEnemies;

    public WaveEnemy[] WaveEnemies { get => waveEnemies; set => waveEnemies = value; }
}
