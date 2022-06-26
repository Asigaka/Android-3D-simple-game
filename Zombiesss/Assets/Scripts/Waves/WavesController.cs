using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    [SerializeField] private WavesPreset preset;
    [SerializeField] private float waveTimeStep = 3;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float minSpawnTime = 0.1f;
    [SerializeField] private float maxSpawnTime = 0.2f;
    [SerializeField] private Transform enemiesContent;

    [SerializeField] private Wave currentWave;
    [SerializeField] private float currentTime;

    public void StartWaves()
    {
        currentWave = preset.Waves[0];

        StartCoroutine(EStartCurrentWave());
    }

    private IEnumerator EStartCurrentWave()
    {
        currentTime = GetTimeToWave();

        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
        }

        ESpawnWaveEnemies();

        int nextWaveIndex = preset.Waves.IndexOf(currentWave) + 1;

        if (nextWaveIndex < preset.Waves.Count)
        {
            currentWave = preset.Waves[nextWaveIndex];
            StartCoroutine(EStartCurrentWave());
        }
    }

    private IEnumerator ESpawnWaveEnemies()
    {
        foreach (WaveEnemy waveEnemy in currentWave.WaveEnemies)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            for (int i = 0; i < waveEnemy.EnemiesAmount; i++)
            {
                Enemy newEnemy = Instantiate(waveEnemy.EnemyPrefab);
                newEnemy.transform.SetParent(enemiesContent);
            }
        }
    }

    private float GetTimeToWave()
    {
        return (preset.Waves.IndexOf(currentWave) + 1) * waveTimeStep;
    }
}
