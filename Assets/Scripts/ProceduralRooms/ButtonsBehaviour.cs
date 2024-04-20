using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonsBehaviour : MonoBehaviour, IDataPersistance
{
    [HideInInspector]
    public fatherRoom rooms = new fatherRoom();
    GetAllPrefabs getAllPrefabs;

    private void Start()
    {
        getAllPrefabs = GameObject.Find("PrefabManager").GetComponent<GetAllPrefabs>();
    }

    public void SaveObj()
    {
        GameObject go = GameObject.Find("RoomBehaviour");
        string name = go.name.ToString();
        go.name = System.Guid.NewGuid().ToString();
        rooms.children = new List<childObjRoom>();

        rooms.name = go.name;

        for (int i = 0; i < go.transform.childCount; i++)
        {
            childObjRoom childObj = new childObjRoom();
            Transform child = go.transform.GetChild(i);
            childObj.p = child.position;
            childObj.r = child.rotation.eulerAngles;
            childObj.s = child.localScale;
            childObj.nameID = child.GetComponent<PrefabID>().GUID;
            rooms.children.Add(childObj);
        }

        DataPersistanceManager.instance.SaveGame();

        go.name = name;
    }

    public void LoadPrefabFromFile()
    {
        //spawn object
        GameObject objToSpawn = new GameObject(rooms.name);
        //Add children
        foreach (var item in rooms.children)
        {
            Quaternion rot = Quaternion.Euler(item.r.x, item.r.y, item.r.z);
            GameObject go = Instantiate(getAllPrefabs.checkAllPrefabs(item.nameID), item.p, rot);
            go.transform.localScale = item.s;
            go.transform.parent = objToSpawn.transform;
        }
    }

    public void LoadNewScene()
    {
        DataPersistanceManager.instance.SaveGame();
        StartCoroutine(loadScene());
    }
    IEnumerator loadScene()
    {
        yield return new WaitForFixedUpdate();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadData(GameData gameData)
    {
        foreach (var item in gameData.allCraftedRooms)
        {
            rooms = item;
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.allCraftedRooms.Add(rooms);
    }
}
