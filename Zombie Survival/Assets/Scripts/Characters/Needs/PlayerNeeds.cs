using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNeeds : MonoBehaviour
{
    public enum NeedType { Health, Satiety, Thirst}
    public Need Health = new Need();
    public Need Satiety = new Need();
    public Need Thirst = new Need();

    [System.Serializable]
    public class Need
    {
        public string Name;
        public string Description;
        public NeedType Type;
        public float MaxAmount;
        public float CurrentAmount;
        public float ChangePerMinute;
    }
}
