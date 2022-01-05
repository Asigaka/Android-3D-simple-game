using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIObjectType { HUD, Inventory}
public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIObject> UIObjects;
    [SerializeField] private UIObjectType startType;
    [SerializeField] private GameObject interactiveBtn;

    private PlayerInventoryUI inventoryUI;

    public static UIManager Instance;

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

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToogleMainUI();

        if (Input.GetKeyDown(KeyCode.I))
            inventoryUI.TurnOnInventory();
    }*/

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        if (Input.GetKeyDown(KeyCode.F))
            PlayerInteractionController.Instance.Interactive.OnInteractive();
        interactiveBtn.SetActive(PlayerInteractionController.Instance.Interactive != null);
    }

    public void ToogleUI(UIObjectType type)
    {
        switch (type)
        {
            case UIObjectType.HUD:
                GameStateController.Instance.ChangeGameState(GameState.Playing);
                break;
            case UIObjectType.Inventory:
                GameStateController.Instance.ChangeGameState(GameState.InteractiveUI);
                break;
        }

        foreach (UIObject ui in UIObjects)
            ui.gameObject.SetActive(ui.Type == type);
    }

    public void ToogleMainUI()
    {
        ToogleUI(startType);
    }
}
