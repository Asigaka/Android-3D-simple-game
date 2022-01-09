using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Monolog", fileName ="MonologInfo")]
public class MonologInfo : ScriptableObject
{
    public List<MonologEntity> monologEntities;
}
