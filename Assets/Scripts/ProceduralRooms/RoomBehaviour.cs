using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomBehaviour : MonoBehaviour
{
    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Began;

    public bool errase = false;

    public GameObject currentObjToPlace = null;
    fatherRoom currentRoomToPlace;
    public GridManager gridManager;

    [SerializeField] LayerMask layersToIngore;

    bool isRoom;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "BuildRoomScene")
        {
            isRoom = true;
        }
        else 
        {
            isRoom = false;
        }
    }

    private void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if(!isRoom) InputForDungeon(out hit, ray);
            else InputForRoom(out hit, ray);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    OnMouseDown();
        //}
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!isRoom) InputForDungeon(out hit, ray);
        else InputForRoom(out hit, ray);
    }

    void InputForDungeon(out RaycastHit hit, Ray ray)
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layersToIngore))
        {
            if (errase && hit.transform.GetComponent<Tile>() != null)
            {
                gridManager.ErraseRoom(hit.transform.GetComponent<Tile>().groupID);
            }
            else if (currentRoomToPlace.name != null && hit.transform.GetComponent<Tile>() != null 
                && hit.transform.GetComponent<Tile>().isAccesible)
            {
               gridManager.SetAllTilesInOtherGrid(currentRoomToPlace, hit.transform.GetComponent<Tile>());
            }
        }
    }

    void InputForRoom(out RaycastHit hit, Ray ray)
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layersToIngore))
        {
            if (errase && hit.transform.GetComponent<Tile>() != null)
            {
                hit.transform.GetComponent<Tile>().ClearTile();
            }
            else if (currentObjToPlace != null && currentObjToPlace.GetComponent<ArtifactDetails>() != null
                && hit.transform.GetComponent<Tile>() != null &&
                CheckIfObjFits(currentObjToPlace.GetComponent<ArtifactDetails>(), hit.transform.GetComponent<Tile>(), hit.transform.GetComponent<Tile>().tileState != -1))
            {
                hit.transform.GetComponent<Tile>().SetInnerTile(currentObjToPlace.GetComponent<PrefabID>());
            }
            else if (currentObjToPlace == null && hit.transform.GetComponent<Tile>() != null 
                && hit.transform.GetComponent<Tile>().isAccesible)
            {
                hit.transform.GetComponent<Tile>().tileState = 0;
            }
            else if (currentObjToPlace != null && currentObjToPlace.tag == "Door" 
                && hit.transform.GetComponent<Tile>() != null && gridManager.CheckIfDoorPossible(hit.transform.GetComponent<Tile>()))
            {
                hit.transform.GetComponent<Tile>().tileState = gridManager.CheckDoorOrientation(hit.transform.GetComponent<Tile>());
            }

            gridManager.SetAllToNotAccesible();
            CheckNearTiles();
            gridManager.CheckIfBoardEmpty();
        }
    }

    bool CheckIfObjFits(ArtifactDetails artifactDetails, Tile tile, bool occupied)
    {
        bool ret = true;

        if (!occupied) return occupied;

        for (int x = tile.tilePos.x - artifactDetails.objectSize.x; x <= tile.tilePos.x + artifactDetails.objectSize.x; x++)
        {
            for (int y = tile.tilePos.y - artifactDetails.objectSize.y; y <= tile.tilePos.y + artifactDetails.objectSize.y; y++)
            {
                if (gridManager.tiles[x, y].isOccupied()) return false;
            }
        }

        return ret;
    }

    void CheckNearTiles()
    {
        foreach (var currentTile in gridManager.tiles)
        {
            CheckIfEnableTile(currentTile);
        }
    }

    void CheckIfEnableTile(Tile tile)
    {
        int pow = 0;
        if (tile.tileState == -1)
        {
            for (int x = tile.tilePos.x - 1; x <= tile.tilePos.x + 1; x++)
            {
                for (int y = tile.tilePos.y - 1; y <= tile.tilePos.y + 1; y++)
                {
                    pow++;
                    if (((x < gridManager.width && x >= 0) && (y < gridManager.height && y >= 0)) &&
                        gridManager.tiles[x, y].tileState != -1 && pow % 2 == 0) tile.isAccesible = true;
                    if (((x < gridManager.width && x >= 0) && (y < gridManager.height && y >= 0)) &&
                        gridManager.tiles[x, y].isDoor && pow % 2 == 0)
                    {
                        tile.isAccesible = false;
                        break;
                    }
                }
            }
        }
    }

    public void SetCurrentObjectToPlace(GameObject gameObject)
    {
        currentObjToPlace = gameObject == null ? null : gameObject;
    }

    public void SetCurrentRoomToPlace(fatherRoom room)
    {
        currentRoomToPlace = room;
    }
}
