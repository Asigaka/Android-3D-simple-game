using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public static class SaveSystem
{
    public static List<ItemInfo> GetItemsInfo()
    {
        List<ItemInfo> list = new List<ItemInfo>();
        string[] results = AssetDatabase.FindAssets("t:iteminfo");
        for (int i = 0; i < results.Length; i++)
        {
            list.Add((ItemInfo)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(results[i]), typeof(ItemInfo)));
        }
        return list;
    }

    public static string GetAssetLabelByItemName(string itemName)
    {
        string[] results = AssetDatabase.FindAssets("t:iteminfo");
        for (int i = 0; i < results.Length; i++)
        {
            ItemInfo item = (ItemInfo)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(results[i]), typeof(ItemInfo));
            if (item.Name == itemName)
            {
                return AssetDatabase.GUIDToAssetPath(results[i]);
            }
        }
        return null;
    }

    public static ItemInfo GetItemsInfoByAssetLabel(string path)
    {
        return ((ItemInfo)AssetDatabase.LoadAssetAtPath(path, typeof(ItemInfo)));
    }

    public static void SaveInventory()
    {
        string path = Application.persistentDataPath + "/inventory.save";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        InventorySaveData inventorySave = new InventorySaveData(PlayerInventory.Instance.ItemsInInventory);

        formatter.Serialize(stream, inventorySave);
        stream.Close();
    }

    public static InventorySaveData LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventorySaveData inventorySave = formatter.Deserialize(stream) as InventorySaveData;
            stream.Close();

            return inventorySave;
            //File.Delete(path);
            //return null;
        }
        else
        {
            Debug.Log("Не найден сохранённый файл " + path);
            return null;
        }
    }
}
