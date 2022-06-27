using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIInMenuObjectType { MainMenu, NewGame, Settings }
public class UIInMenuManager : MonoBehaviour
{
    [SerializeField] private List<UIInMenuObject> UIObjects;
    [SerializeField] private UIInMenuObjectType startType;

    [Space(7)]
    [SerializeField] private GameObject continueBtn;

    public static UIInMenuManager Instance;

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

    private void OnEnable()
    {
        continueBtn.SetActive(SaveSystem.SavesIsExists(SaveType.Levels)
           || SaveSystem.SavesIsExists(SaveType.Inventory));
    }

    public void ToogleUI(UIInMenuObjectType type)
    {
        foreach (UIInMenuObject ui in UIObjects)
            ui.gameObject.SetActive(ui.Type == type);
    }

    public void ToogleMainUI()
    {
        ToogleUI(startType);
    }

    public void OnContinueBtnClick()
    {
        SaveSystem.LoadAllData();
        Session.Instance.Level.TurnOnSavedLevel();
    }

    public void OnNewGameBtnClick()
    {
        SaveSystem.ClearAllData();
        Session.Instance.Level.TurnOnLevel();
    }

    public void OnExitBtnClick()
    {
        Application.Quit();
    }
}
