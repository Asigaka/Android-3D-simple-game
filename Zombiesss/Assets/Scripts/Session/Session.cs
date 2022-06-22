using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private WavesController waves;

    public static Session Instance;

    public Player Player { get => player; }
    public WavesController Waves { get => waves; }

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        StartSession();
    }

    private void StartSession()
    {
        //waves.StartWaves();
    }
}
