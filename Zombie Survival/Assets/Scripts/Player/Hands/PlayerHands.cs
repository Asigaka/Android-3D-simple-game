using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private ItemsInHandsInfo itemsInHands;
    [SerializeField] private Transform rightItemTransform;
    [SerializeField] private Transform leftItemTransform;

    [Header("Weapons")]
    [SerializeField] private WeaponModel pistolModel;

    public static PlayerHands Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public void EquipWeapon(ItemWeaponInfo item)
    {
        UnactiveHandsItems();

        switch (item.WeaponID)
        {
            case 1:
                EquipWeapon(pistolModel);
                break;
        }
    }

    private void EquipWeapon(WeaponModel model)
    {
        model.gameObject.SetActive(true);
        PlayerCombatController.Instance.EquipWeapon(model);
    }

   /* public void SpawnItemsInHands()
    {
        int i = 0;
        foreach (ItemsInHandsInfo.ItemInHandEntity item in itemsInHands.Items)
        {
            GameObject itemModel = null;

            switch (item.ItemSide)
            {
                case Side.Left:
                    itemModel = Instantiate(item.ItemInfo.ItemModel, leftItemTransform);
                    break;
                case Side.Right:
                    itemModel = Instantiate(item.ItemInfo.ItemModel, rightItemTransform);
                    break;
            }

            if (itemModel.GetComponent<Collider>())
                Destroy(itemModel.GetComponent<Collider>());
            if (itemModel.GetComponent<Rigidbody>())
                Destroy(itemModel.GetComponent<Rigidbody>());

            itemModel.transform.localPosition = item.SceneItemPosition;
            itemModel.transform.localRotation = Quaternion.Euler(item.SceneItemRotation.x, item.SceneItemRotation.y, item.SceneItemRotation.z);
            itemModel.SetActive(false);
            itemInHandEntitiesSpawned.Add(item);
            itemInHandEntitiesSpawned[i].SpawnedItemModel = itemModel;
            i++;
        }
    }*/

    private void UnactiveHandsItems()
    {
        PlayerCombatController.Instance.OnTakeOffWeapon();
        
        for (int i = 0; i < rightItemTransform.childCount; i++)
            rightItemTransform.GetChild(i).gameObject.SetActive(false);

        for (int i = 0; i < leftItemTransform.childCount; i++)
            leftItemTransform.GetChild(i).gameObject.SetActive(false);
    }

    public bool ExistWeapon(ItemWeaponInfo item)
    {
        switch (item.WeaponID)
        {
            case 1: return true;
            default: return false;
        }
    }
}
