using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/ContainerInfo", fileName = "ContainerInfo")]
public class ContainerInfo : ScriptableObject
{
    public string Name;
    public List<ItemInContainer> ItemsInContainer;

    [System.Serializable]
    public class ItemInContainer
    {
        public ItemInfo Info;
        public int MinCount;
        public int MaxCount;

        public int GetCount() => Random.Range(MinCount, MaxCount);
    }
}
