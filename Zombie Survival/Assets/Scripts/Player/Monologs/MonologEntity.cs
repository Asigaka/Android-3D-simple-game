using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonologEntity
{
    [TextArea(2, 6)]
    public string MonologText;
    public float MonologTime = 3;
}
