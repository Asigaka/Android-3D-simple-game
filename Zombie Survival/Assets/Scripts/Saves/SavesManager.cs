using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    public string[] results;
    public static SavesManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    public bool SavesIsHs()
    {
        return SaveSystem.LoadInventory() != null;
    }

    private void Start()
    {
        LoadInventory();
    }

    private void LoadInventory()
    {
        InventorySaveData inventorySave =  SaveSystem.LoadInventory();
        PlayerInventory.Instance.ItemsInInventory = inventorySave.GetItems();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveInventory();
    }
}
