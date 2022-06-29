using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIScreen> screens;
    [SerializeField] private UIType startType;

    [Space]
    [SerializeField] private HUDScreen hud;
    [SerializeField] private InventoryScreen inventory;
    [SerializeField] private CraftScreen craft;
    [SerializeField] private TerminalScreen terminal;

    public static UIManager Instance;

    public HUDScreen HUD { get => hud; }
    public InventoryScreen Inventory { get => inventory; }
    public CraftScreen Craft { get => craft; }
    public TerminalScreen Terminal { get => terminal; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        ToogleUI(startType);
    }

    public void ToogleUI(UIType type)
    {
        switch (type)
        {
            case UIType.HUD:
                Session.Instance.GameState.ChangeGameState(GameState.Playing);
                break;
            default:
                Session.Instance.GameState.ChangeGameState(GameState.Pause);
                break;
        }

        foreach (UIScreen ui in screens)
            ui.gameObject.SetActive(ui.Type == type);
    }

    public void ClearAllChilds(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
            Destroy(parent.GetChild(i).gameObject);
    }

    public void BackInMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
