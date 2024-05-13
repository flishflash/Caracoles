using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Began;

    public bool errase = false;

    [SerializeField] GameObject currentObjToPlace = null;
    [SerializeField] GridManager gridManager;

    [SerializeField] LayerMask layersToIngore;

    private void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layersToIngore))
            {
                GameObject go = null;

                //if (currentObjToPlace != null && (currentObjToPlace.transform.tag != "Floor" &&
                //    CheckIfObjFits(currentObjToPlace.GetComponent<ArtifactDetails>(), hit.point, hit.transform.GetComponent<Tile>().isOccuped)))
                //{
                //    go = Instantiate(currentObjToPlace, hit.transform.position + currentObjToPlace.GetComponent<ArtifactDetails>().spawnPoint,
                //                        Quaternion.Euler(currentObjToPlace.GetComponent<ArtifactDetails>().spawnRot));
                //    dungeonObj.Add(go);
                //    go.transform.parent = transform;
                //}
                //else if (currentObjToPlace == null && !hit.transform.GetComponent<Tile>().isOccuped &&
                //    hit.transform.GetComponent<Tile>().isAccesible)
                //{
                //    go = Instantiate(floors[Random.Range(0, floors.Length)], hit.transform.position, Quaternion.identity);
                //    AddFloorToList(go);
                //    go.transform.parent = transform;
                //    hit.transform.GetComponent<Tile>().isOccuped = true;
                //}
                //else if (currentObjToPlace != null && (currentObjToPlace.transform.tag == "Floor" &&
                //    !hit.transform.GetComponent<Tile>().isOccuped &&
                //    hit.transform.GetComponent<Tile>().isAccesible))
                //{
                //    go = Instantiate(currentObjToPlace, hit.transform.position, Quaternion.identity);
                //    AddFloorToList(go);
                //    go.transform.parent = transform;
                //    hit.transform.GetComponent<Tile>().isOccuped = true;
                //}

                //gridManager.SetAllToNotAccesible();
                //CheckNearTiles();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layersToIngore))
        {
            if (errase)
            {
                hit.transform.GetComponent<Tile>().ClearTile();
            }
            else if (currentObjToPlace != null && currentObjToPlace.GetComponent<ArtifactDetails>() != null &&
                CheckIfObjFits(currentObjToPlace.GetComponent<ArtifactDetails>(), hit.transform.GetComponent<Tile>(), hit.transform.GetComponent<Tile>().tileState != -1))
            {
                hit.transform.GetComponent<Tile>().SetInnerTile(currentObjToPlace.GetComponent<PrefabID>());
            }
            else if (currentObjToPlace == null && hit.transform.GetComponent<Tile>().isAccesible)
            {
                hit.transform.GetComponent<Tile>().tileState = 0;
            }
            else if (currentObjToPlace != null && currentObjToPlace.GetComponent<GridManager>() != null
                && hit.transform.GetComponent<Tile>().isAccesible)
            {
                currentObjToPlace.GetComponent<GridManager>().SetAllTilesInOtherGrid(gridManager, hit.transform.GetComponent<Tile>());
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
                if (gridManager.tiles[x, y]._innerTiles.nameID != null) return false;
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
        if (tile.tileState == -1)
        {
            for (int x = tile.tilePos.x - 1; x <= tile.tilePos.x + 1; x++)
            {
                for (int y = tile.tilePos.y - 1; y <= tile.tilePos.y + 1; y++)
                {
                    if (((x < gridManager.width && x >= 0) && (y < gridManager.height && y >= 0)) &&
                        gridManager.tiles[x, y].tileState != -1) tile.isAccesible = true;
                }
            }
        }
    }

    public void SetCurrentObjectToPlace(GameObject gameObject)
    {
        currentObjToPlace = gameObject == null ? null : gameObject;
    }
}
