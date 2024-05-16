using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonsBehaviour : MonoBehaviour, IDataPersistance
{
    [SerializeField] GameObject ItemButton;
    [SerializeField] Sprite exitSprite;
    [SerializeField] Sprite startSprite;

    [HideInInspector]
    public List<fatherRoom> rooms = new List<fatherRoom>();
    fatherRoom startRoom;
    fatherRoom endRoom;

    GetAllPrefabs getAllPrefabs;
    GameObject Mask;
    RoomBehaviour roomBehaviour;

    TextMeshProUGUI warning;

    private void Start()
    {
        startRoom = new fatherRoom();
        endRoom = new fatherRoom();

        getAllPrefabs = GameObject.Find("PrefabManager").GetComponent<GetAllPrefabs>();
        roomBehaviour = GameObject.Find("RoomBehaviour").GetComponent<RoomBehaviour>();
        warning = GameObject.Find("Warning").GetComponent<TextMeshProUGUI>();
        Mask = GameObject.Find("Content");

        warning.gameObject.SetActive(false);
        
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            StartCoroutine(loadRooms());
        }
        else
        {
            InstanciateItemButtons();
        }
        SetExtraRooms();
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
            if (getAllPrefabs.allPrefabs[i].name != "SnailKnightPrefab")
            {
                go = Instantiate(ItemButton, Mask.transform);
                go.GetComponent<ItemButton>().item = getAllPrefabs.allPrefabs[i];
                go.name = getAllPrefabs.allPrefabs[i].name;
            }
        }
    }

    void InstanciateRoomButtons()
    {
        GameObject go;

        for (int i = 0; i < rooms.Count; i++)
        {
            go = Instantiate(ItemButton, Mask.transform);
            if (rooms[i].name == "StartRoom") go.GetComponent<ItemButton>().SetSprite(startSprite);
            if (rooms[i].name == "EndRoom") go.GetComponent<ItemButton>().SetSprite(exitSprite);
            go.GetComponent<ItemButton>().room = rooms[i];
            roomBehaviour.SetCurrentRoomToPlace(go.GetComponent<ItemButton>().room);
        }
    }

    void SetExtraRooms() 
    {
        startRoom.children = new List<roomTiles>();
        startRoom.width = 16;
        startRoom.height = 16;
        startRoom.name = "StartRoom";
        roomTiles room = new roomTiles();
        room.tilePos = new Vector2Int(9, 9);
        room.tileState = 4;
        startRoom.children.Add(room);
        room.tilePos = new Vector2Int(9, 10);
        room.tileState = 408;
        room._innerTiles.nameID = "15727028-b4ae-48f9-b969-24289dc03b45";
        startRoom.children.Add(room);

        endRoom.children = new List<roomTiles>();
        endRoom.width = 16;
        endRoom.height = 16;
        endRoom.name = "EndRoom";
        room = new roomTiles();
        room.tilePos = new Vector2Int(9, 9);
        room.tileState = 4;
        room._innerTiles.nameID = "";
        endRoom.children.Add(room);
        room.tilePos = new Vector2Int(9, 10);
        room.tileState = 408;
        endRoom.children.Add(room);

        rooms.Add(endRoom);
        rooms.Add(startRoom);
    }

    public void StartSimulation()
    {
        roomBehaviour.gridManager.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public void SetSelectedItem(ItemButton itemButton)
    {
        StartCoroutine(PopWarning(itemButton));
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

    public void DisableFinder()
    {
        GameObject.Find("Plane Finder").SetActive(false);
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

    IEnumerator PopWarning(ItemButton item)
    {
        warning.gameObject.SetActive(true);
        warning.text = item == null ? "floor" : item.name;
        yield return new WaitForSeconds(0.5f);
        warning.gameObject.SetActive(false);
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

    public void Exit()
    {
        SceneManager.LoadScene("SelectGamemode");
    }
}
