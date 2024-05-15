using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonsBehaviour : MonoBehaviour, IDataPersistance
{
    [SerializeField] GameObject ItemButton;

    [HideInInspector]
    public List<fatherRoom> rooms = new List<fatherRoom>();
    
    GetAllPrefabs getAllPrefabs;
    GameObject Mask;
    RoomBehaviour roomBehaviour;

    private void Start()
    {
        getAllPrefabs = GameObject.Find("PrefabManager").GetComponent<GetAllPrefabs>();
        roomBehaviour = GameObject.Find("RoomBehaviour").GetComponent<RoomBehaviour>();
        Mask = GameObject.Find("Content");

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            StartCoroutine(loadRooms());
        }
        else
        {
            InstanciateItemButtons();
        }
    }

    public void SaveObj()
    {
        fatherRoom room = new fatherRoom();
        room.children = new List<roomTiles>();
        room.name = System.Guid.NewGuid().ToString();
        room.width = roomBehaviour.gridManager.width;
        room.height = roomBehaviour.gridManager.height;


        for (int x = 0; x < room.width; x++)
        {
            for (int y = 0; y < room.height; y++)
            {
                roomTiles childObj = new roomTiles();

                childObj.nameID = roomBehaviour.gridManager.tiles[x, y].transform.name;
                childObj.tilePos = roomBehaviour.gridManager.tiles[x, y].tilePos;
                childObj.tileState = roomBehaviour.gridManager.tiles[x, y].tileState;

                childObj._innerTiles = roomBehaviour.gridManager.tiles[x, y]._innerTiles;

                room.children.Add(childObj);
            }
        }

        if (room.children.Count > 0) rooms.Add(room);

        DataPersistanceManager.instance.SaveGame();
    }

    public void SetErraser()
    {
        roomBehaviour.errase = !roomBehaviour.errase;
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

    void InstanciateRoomButtons()
    {
        GameObject go;

        for (int i = 0; i < rooms.Count; i++)
        {
            go = Instantiate(ItemButton, Mask.transform);
            go.GetComponent<ItemButton>().room = rooms[i];
            roomBehaviour.SetCurrentRoomToPlace(go.GetComponent<ItemButton>().room);
        }
    }

    public void StartSimulation()
    {
        roomBehaviour.gridManager.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public void SetSelectedItem(ItemButton itemButton)
    {
        roomBehaviour.SetCurrentObjectToPlace(itemButton.item);
    }

    public void SetSelectedRoom(ItemButton itemButton)
    {
        roomBehaviour.SetCurrentRoomToPlace(itemButton.room);
    }

    public void LoadNewScene()
    {
        StartCoroutine(loadScene());
    }
    IEnumerator loadScene()
    {
        yield return new WaitForFixedUpdate();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator loadRooms()
    {
        yield return new WaitForFixedUpdate();
        InstanciateRoomButtons();
    }

    public void LoadData(GameData gameData)
    {
        foreach (var item in gameData.allCraftedRooms)
        {
            rooms.Add(item);
        }
    }

    public void SaveData(ref GameData gameData)
    {
        foreach (var room in rooms)
        {
            gameData.allCraftedRooms.Add(room);
        }
    }
}
