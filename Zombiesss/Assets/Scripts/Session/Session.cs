using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    [SerializeField] private Player player;

    public static Session Instance;

    public Player Player { get => player; }

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

    }
}
