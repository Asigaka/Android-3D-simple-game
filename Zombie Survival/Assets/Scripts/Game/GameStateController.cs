using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Playing, Menu, Pause, InteractiveUI}
public class GameStateController : MonoBehaviour
{
    [SerializeField] private GameState startState;

    public static GameStateController Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                //PlayerCamera.Instance.CanRotate = true;
                break;
            case GameState.Menu:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                //PlayerCamera.Instance.CanRotate = false;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                //PlayerCamera.Instance.CanRotate = false;
                break;
            case GameState.InteractiveUI:
                Time.timeScale = 0.2f;
                Cursor.lockState = CursorLockMode.None;
                //PlayerCamera.Instance.CanRotate = false;
                break;
        }
    }
}
