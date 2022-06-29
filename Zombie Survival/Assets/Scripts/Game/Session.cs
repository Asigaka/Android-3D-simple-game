using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    [SerializeField] private SavesManager saves;
    [SerializeField] private LevelManager level;
    [SerializeField] private CraftController crafts;
    [SerializeField] private MonologsController monologs;
    [SerializeField] private GameStateController gameState;
    [SerializeField] private ContainerInventory container;
    [SerializeField] private Player player;

    public static Session Instance;

    public SavesManager Saves { get => saves; }
    public LevelManager Level { get => level; }
    public CraftController Crafts { get => crafts; }
    public MonologsController Monologs { get => monologs; }
    public GameStateController GameState { get => gameState; }
    public Player Player { get => player; }
    public ContainerInventory Container { get => container; }

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
