using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIInMenuObjectType { MainMenu, NewGame, Settings }
public class UIInMenuManager : MonoBehaviour
{
    [SerializeField] private List<UIInMenuObject> UIObjects;
    [SerializeField] private UIInMenuObjectType startType;

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
        OnStartCheckSavedGame();
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

    private void OnStartCheckSavedGame()
    {
       
    }

    public void OnNewGameBtnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitBtnClick()
    {
        Application.Quit();
    }
}
