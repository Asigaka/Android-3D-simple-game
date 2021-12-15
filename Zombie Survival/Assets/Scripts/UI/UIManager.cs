using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIObjectType { HUD, Inventory}
public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIObject> UIObjects;
    [SerializeField] private UIObjectType startType;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToogleMainUI();

        if (Input.GetKeyDown(KeyCode.I))
            inventoryUI.OnInventorySwitch();
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
                if (ContainerInventory.Instance.SelectedContainer != null)
                {
                    ContainerInventoryUI.Instance.TurnOnInventory();
                }
                else
                {
                    ContainerInventoryUI.Instance.TurnOffIventory();
                }
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
