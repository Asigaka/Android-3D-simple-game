using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemsInHands", fileName = "ItemsInHands")]
public class ItemsInHandsInfo : ScriptableObject
{
    public List<ItemInHandEntity> Items;

    [System.Serializable]
    public class ItemInHandEntity
    {
        public Side ItemSide;
        public ItemInfo ItemInfo;
        public Vector3 SceneItemPosition;
        public Quaternion SceneItemRotation;

        //[HideInInspector]
        public GameObject SpawnedItemModel;
    }
}
