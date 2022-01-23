using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public enum SaveType { Inventory, Levels}
public static class SaveSystem
{
    public static string pathOfInventorySave = Application.persistentDataPath + "/inventory.save";
    public static string pathOfLevelsSave = Application.persistentDataPath + "/levels.save";

    public static ItemInfo[] GetItemsInfo()
    {
        ItemInfo[] items;
        items = Resources.FindObjectsOfTypeAll(typeof(ItemInfo)) as ItemInfo[];
        return items;
    }

    public static ItemInfo GetItemsInfoByAssetLabel(string itemName)
    {
        ItemInfo[] items;
        items = Resources.FindObjectsOfTypeAll(typeof(ItemInfo)) as ItemInfo[];
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Name == itemName)
            {
                return items[i];
            }
        }
        return null;
    }

    public static void SaveData(SaveType type)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string targetPath = "";
        FileStream stream = null;
        ISaveData saveData = null;
        switch (type)
        {
            case SaveType.Inventory:
                targetPath = pathOfInventorySave;
                saveData = new InventorySaveData(PlayerInventory.Instance.ItemsInInventory);
                Debug.Log("Инвентарь сохранён");
                break;
            case SaveType.Levels:
                targetPath = pathOfLevelsSave;
                saveData = new LevelsSaveData(LevelManager.Instance.CurrentLevelIndex);
                Debug.Log("Уровни сохранены");
                break;
        }

        stream = new FileStream(targetPath, FileMode.Create);
        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static ISaveData LoadData(SaveType type)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string targetPath = "";
        FileStream stream = null;
        ISaveData saveData = null;
        switch (type)
        {
            case SaveType.Inventory:
                targetPath = pathOfInventorySave;
                stream = new FileStream(targetPath, FileMode.Open);
                saveData = formatter.Deserialize(stream) as InventorySaveData;
                break;
            case SaveType.Levels:
                targetPath = pathOfLevelsSave;
                stream = new FileStream(targetPath, FileMode.Open);
                saveData = formatter.Deserialize(stream) as LevelsSaveData;
                break;
        }

        if (SavesIsExists(type))
        {
            stream.Close();
            return saveData;
        }

        Debug.Log("Не найден сохранённый файл " + targetPath);
        return null;
    }

    public static bool SavesIsExists(SaveType type)
    {
        string targetPath = "";

        switch (type)
        {
            case SaveType.Inventory: targetPath = pathOfInventorySave; break;
            case SaveType.Levels: targetPath = pathOfLevelsSave; break;
        }

        return File.Exists(targetPath);
    }

    public static void ClearAllData()
    {
        if (SavesIsExists(SaveType.Inventory))
            File.Delete(pathOfInventorySave);
        if (SavesIsExists(SaveType.Levels))
            File.Delete(pathOfLevelsSave);
    }

    public static void LoadAllData()
    {
        if (SavesIsExists(SaveType.Inventory))
            LoadData(SaveType.Inventory);
        if (SavesIsExists(SaveType.Levels))
            LoadData(SaveType.Levels);
    }
}
