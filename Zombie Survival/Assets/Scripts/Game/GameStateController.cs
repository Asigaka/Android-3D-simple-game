using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Left, Right }
public enum GameState { Playing, Menu, Pause, InteractiveUI}
public class GameStateController : MonoBehaviour
{
    [SerializeField] private GameState startState;

    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                Time.timeScale = 1;
                break;
            case GameState.Menu:
                Time.timeScale = 0;
                break;
            case GameState.Pause:
                Time.timeScale = 0.2f;
                break;
        }
    }
}
