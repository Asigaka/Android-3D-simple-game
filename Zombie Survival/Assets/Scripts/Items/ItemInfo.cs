using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Default, Food, Weapon}
[CreateAssetMenu(menuName ="Items/Item", fileName ="Item")]
public class ItemInfo : ScriptableObject
{
    public GameObject ItemModel;
    public Sprite Sprite;
    public string Name;
    public string Description;
    public ItemType Type;
}
