using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePauseUI : MonoBehaviour
{
    public static InGamePauseUI Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void TurnOnPauseUI()
    {
        UIInGameManager.Instance.ToogleUI(UIInGameObjectType.Pause);
    }

    public void TurnOffPauseUI()
    {
        UIInGameManager.Instance.ToogleMainUI();
    }

    public void OnMainMenuBtnClick()
    {
        SceneManager.LoadScene(0);
    }
}
