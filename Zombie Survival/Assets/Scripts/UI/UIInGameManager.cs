using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIInGameObjectType { HUD, Inventory, Terminal, Craft, Pause, Settings}
public class UIInGameManager : MonoBehaviour
{
    [SerializeField] private List<UIInGameObject> UIObjects;
    [SerializeField] private UIInGameObjectType startType;
    [SerializeField] private GameObject interactiveBtn;

    public static UIInGameManager Instance;

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

    private void Update()
    {
        interactiveBtn.SetActive(PlayerInteraction.Instance.Interactive != null);
    }

    public void ToogleUI(UIInGameObjectType type)
    {
        switch (type)
        {
            case UIInGameObjectType.HUD:
                Session.Instance.GameState.ChangeGameState(GameState.Playing);
                break;
            default:
                Session.Instance.GameState.ChangeGameState(GameState.Pause);
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
