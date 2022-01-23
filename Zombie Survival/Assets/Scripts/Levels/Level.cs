using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Transform spawnPoint;

    public GameObject LevelPrefab { get => levelPrefab; }
    public Transform SpawnPoint { get => spawnPoint; }
}
