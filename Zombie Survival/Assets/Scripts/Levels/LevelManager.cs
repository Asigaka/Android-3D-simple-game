using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //[SerializeField] private List<LevelPrefab> allLevels;
    [SerializeField] private int currentLevelIndex;

    public static LevelManager Instance;

    public int CurrentLevelIndex { get => currentLevelIndex; private set => currentLevelIndex = value; }

    //public List<LevelPrefab> AllLevels { get => allLevels; private set => allLevels = value; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void TurnOnLevel(int index = 1)
    {
        currentLevelIndex = index;
        SaveSystem.SaveData(SaveType.Levels);
        SceneManager.LoadScene(currentLevelIndex);
    }

    public void TurnOnSavedLevel()
    {
        LevelsSaveData saveData = (LevelsSaveData)SaveSystem.LoadData(SaveType.Levels);
        TurnOnLevel(saveData.IndexCurrentScene);
    }

    public void TurnOnNextLevel()
    {
        int index = currentLevelIndex + 1;
        if (SceneManager.sceneCount >= index)
            TurnOnLevel(index);
    }
}
