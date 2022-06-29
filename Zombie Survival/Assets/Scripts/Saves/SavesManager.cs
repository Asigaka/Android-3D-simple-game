using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    public ItemInfo[] results;

    private void Start()
    {
        //LoadInventory();
        results = SaveSystem.GetItemsInfo();
    }

    public void LoadInventory()
    {
        if (SaveSystem.SavesIsExists(SaveType.Inventory))
        {
            InventorySaveData inventorySave = (InventorySaveData)SaveSystem.LoadData(SaveType.Inventory);
            PlayerInventory.Instance.ItemsInInventory = inventorySave.GetItems();
        }
    }

    public void LoadLevels()
    {

    }
}
