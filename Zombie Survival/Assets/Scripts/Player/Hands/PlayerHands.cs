using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private ItemsInHandsInfo itemsInHands;
    [SerializeField] private Transform rightItemTransform;
    [SerializeField] private Transform leftItemTransform;

    [Header("Weapons")]
    [SerializeField] private List<ItemModel> models;

    public static PlayerHands Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void EquipItem(ItemInfo item)
    {
        ItemModel model = null;

        if (ItemModelExist(item, ref model))
        {
            UnactiveHandsItems();
            model.gameObject.SetActive(true);
            model.transform.parent.gameObject.SetActive(true);
            PlayerCombat.Instance.EquipWeapon(model);
        }
    }

    public bool ItemModelExist(ItemInfo item)
    {
        foreach (ItemModel model in models)
        {
            if (model.Info == item)
            {
                return true;
            }
        }

        return false;
    }

    public bool ItemModelExist(ItemInfo item, ref ItemModel refModel )
    {
        foreach (ItemModel model in models)
        {
            if (model.Info == item)
            {
                refModel = model;
                return true;
            }
        }

        return false;
    }

    private void UnactiveHandsItems()
    {
        PlayerCombat.Instance.OnTakeOffWeapon();
        rightItemTransform.gameObject.SetActive(false);
        leftItemTransform.gameObject.SetActive(false);

        for (int i = 0; i < rightItemTransform.childCount; i++)
            rightItemTransform.GetChild(i).gameObject.SetActive(false);

        for (int i = 0; i < leftItemTransform.childCount; i++)
            leftItemTransform.GetChild(i).gameObject.SetActive(false);
    }
}
