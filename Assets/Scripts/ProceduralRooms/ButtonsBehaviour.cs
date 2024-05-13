using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonsBehaviour : MonoBehaviour, IDataPersistance
{
    [SerializeField] GameObject ItemButton;
    [HideInInspector]
    public fatherRoom rooms = new fatherRoom();
    GetAllPrefabs getAllPrefabs;
    GameObject Mask;
    RoomBehaviour roomBehaviour;

    private void Start()
    {
        getAllPrefabs = GameObject.Find("PrefabManager").GetComponent<GetAllPrefabs>();
        roomBehaviour = GameObject.Find("RoomBehaviour").GetComponent<RoomBehaviour>();
        Mask = GameObject.Find("Content");
        InstanciateItemButtons();
    }

    public void SaveObj()
    {
        GridManager go = GameObject.Find("GridManager").GetComponent<GridManager>();
        string name = go.name.ToString();
        go.name = System.Guid.NewGuid().ToString();
        rooms.children = new List<roomTiles>();

        rooms.name = go.name;

        //for (int i = 0; i < go.tiles.Count; i++)
        //{
        //    roomTiles childObj = new roomTiles();

        //    childObj.nameID = go.tiles[i].transform.name;
        //    childObj.tilePos = go.tiles[i].tilePos;
        //    childObj.tileState = go.tiles[i].tileState;

        //    childObj._innerTiles = go.tiles[i].innerTilesInfo;

        //    rooms.children.Add(childObj);
        //}

        DataPersistanceManager.instance.SaveGame();

        go.name = name;
    }

    void InstanciateItemButtons()
    {
        GameObject go;
        Instantiate(ItemButton, Mask.transform);

        for (int i = 0; i < getAllPrefabs.allPrefabs.Count; i++)
        {
            go = Instantiate(ItemButton, Mask.transform);
            go.GetComponent<ItemButton>().item = getAllPrefabs.allPrefabs[i];
        }
    }

    public void SetSelectedItem(ItemButton itemButton)
    {
        roomBehaviour.SetCurrentObjectToPlace(itemButton.item);
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
