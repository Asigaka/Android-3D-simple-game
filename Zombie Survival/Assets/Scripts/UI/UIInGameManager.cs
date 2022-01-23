using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIInGameObjectType { HUD, Inventory, Terminal, Craft, Pause}
public class UIInGameManager : MonoBehaviour
{
    [SerializeField] private List<UIInGameObject> UIObjects;
    [SerializeField] private UIInGameObjectType startType;
    [SerializeField] private GameObject interactiveBtn;

    private PlayerInventoryUI inventoryUI;

    public static UIInGameManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        inventoryUI = PlayerInventoryUI.Instance;

        ToogleUI(startType);
    }

    private void Update()
    {
        interactiveBtn.SetActive(PlayerInteractionController.Instance.Interactive != null);
    }

    public void ToogleUI(UIInGameObjectType type)
    {
        switch (type)
        {
            case UIInGameObjectType.HUD:
                GameStateController.Instance.ChangeGameState(GameState.Playing);
                break;
            default:
                GameStateController.Instance.ChangeGameState(GameState.Pause);
                break;
        }

        foreach (UIInGameObject ui in UIObjects)
            ui.gameObject.SetActive(ui.Type == type);
    }

    public void ToogleMainUI()
    {
        ToogleUI(startType);
    }

    public void OnBackIntoMainMenuBtnClick()
    {
        SceneManager.LoadScene(1);
    }
}
