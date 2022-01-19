using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private ItemsInHandsInfo itemsInHands;
    [SerializeField] private Transform rightItemTransform;
    [SerializeField] private Transform leftItemTransform;

    [Space(7)]
    [SerializeField] private ItemInfo rightItem;
    [SerializeField] private ItemInfo leftItem;
    [SerializeField] private List<ItemsInHandsInfo.ItemInHandEntity> itemInHandEntitiesSpawned;

    public static PlayerHands Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        SpawnItemsInHands();
    }

    public void SpawnItemsInHands()
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
    }

    public void TakeInHand(ItemInfo takedItem)
    {
        switch (GetItemByInfo(itemsInHands.Items, takedItem).ItemSide)
        {
            case Side.Left:
                foreach (ItemsInHandsInfo.ItemInHandEntity item in itemsInHands.Items)
                {
                    if (takedItem == item.ItemInfo)
                        leftItem = item.ItemInfo;
                }
                break;
            case Side.Right:
                foreach (ItemsInHandsInfo.ItemInHandEntity item in itemsInHands.Items)
                {
                    if (takedItem == item.ItemInfo)
                        rightItem = item.ItemInfo;
                }
                break;
        }

        UpdateItemsInHands();
    }

    private void UpdateItemsInHands()
    {
        UnactiveHandsItems();
        if (GetItemByInfo(itemInHandEntitiesSpawned, rightItem) != null)
        {
            if (GetItemByInfo(itemInHandEntitiesSpawned, rightItem).ItemInfo.Type == ItemType.Weapon)
                PlayerCombatController.Instance.OnEquipWeapon((ItemWeaponInfo)rightItem);

            GetItemByInfo(itemInHandEntitiesSpawned, rightItem).SpawnedItemModel.SetActive(true);
        }

        if (GetItemByInfo(itemInHandEntitiesSpawned, leftItem) != null)
        {
            if (GetItemByInfo(itemInHandEntitiesSpawned, leftItem).ItemInfo.Type == ItemType.Weapon)
                PlayerCombatController.Instance.OnEquipWeapon((ItemWeaponInfo)leftItem);

            GetItemByInfo(itemInHandEntitiesSpawned, leftItem).SpawnedItemModel.SetActive(true);
        }
    }

    private void UnactiveHandsItems()
    {
        PlayerCombatController.Instance.OnTakeOffWeapon();

        for (int i = 0; i < rightItemTransform.childCount; i++)
            rightItemTransform.GetChild(i).gameObject.SetActive(false);

        for (int i = 0; i < leftItemTransform.childCount; i++)
            leftItemTransform.GetChild(i).gameObject.SetActive(false);
    }

    public void RemoveItemInHandIfTransferHis(ItemInfo transferedItem)
    {
        if (transferedItem == rightItem)
            rightItem = null;

        if (transferedItem == leftItem)
            leftItem = null;

        UpdateItemsInHands();
    }

    public ItemsInHandsInfo.ItemInHandEntity GetItemByInfo(List<ItemsInHandsInfo.ItemInHandEntity> list, ItemInfo info)
    {
        for (int i = 0; i < list.Count; i++)
            if (list[i].ItemInfo == info)
                return list[i];

        return null;
    }
}
