using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelsSaveData : ISaveData
{
    public int IndexCurrentScene;

    public LevelsSaveData(int indexCurrentLevel)
    {
        IndexCurrentScene = indexCurrentLevel;
    }
}
