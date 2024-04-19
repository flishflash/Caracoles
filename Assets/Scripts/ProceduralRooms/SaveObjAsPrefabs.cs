using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public struct newRoomsObjects
{
    public GameObject Room;

}

public class SaveObjAsPrefabs : MonoBehaviour
{
    public List<newRoomsObjects> rooms = new List<newRoomsObjects>();

    public void SaveObj()
    {
        newRoomsObjects newRoom;
        GameObject go = GameObject.Find("RoomBehaviour");
        string name = go.name.ToString();
        go.name = System.Guid.NewGuid().ToString();

        // Create folder Prefabs and set the path as within the Prefabs folder,
        // and name it as the GameObject's name with the .Prefab format
        if (!Directory.Exists("Assets/Prefabs/Rooms"))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
            AssetDatabase.CreateFolder("Prefabs", "Rooms");
        }
        string localPath = "Assets/Prefabs/Rooms/" + go.name + ".prefab";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        // Create the new Prefab and log whether Prefab was saved successfully.
        bool prefabSuccess;
        GameObject pref = PrefabUtility.SaveAsPrefabAsset(go, localPath, out prefabSuccess);
        if (prefabSuccess == true)
        { 
            Debug.Log("Prefab was saved successfully");
            newRoom.Room = pref;

            rooms.Add(newRoom);
        }
        else
            Debug.Log("Prefab failed to save" + prefabSuccess);

        go.name=name;   
    }

    public void LoadPrefabFromFile()
    {
        if(rooms.Count >= 0)
            Instantiate(rooms[0].Room, Vector3.up*2, Quaternion.identity);
    }
}
