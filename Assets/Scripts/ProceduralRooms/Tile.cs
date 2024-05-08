using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int tileState = -1;
    public bool isAccesible = true;
    public Vector2Int tilePos = new Vector2Int();
    public innerTiles _innerTiles = new innerTiles();
    GameObject currentFloor;

    [SerializeField] SpriteRenderer spriteRenderer;
    
    AllBuildingPrefabs allBuildingPrefabs;

    private void Start()
    {
        tileState = -1;
        currentFloor = null;
        allBuildingPrefabs = GameObject.Find("AllBuildingPrefabs").GetComponent<AllBuildingPrefabs>();
    }

    private void Update()
    {
        if (isAccesible)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.grey;
        }
    }

    public void SetTilePos(Vector2Int pos)
    {
        tilePos = pos;
    }

    public void SetTileState(int state)
    {
        if (currentFloor != null) Destroy(currentFloor);
        tileState = state;
        currentFloor = (tileState == -1) ? null : Instantiate(allBuildingPrefabs.GetBuildingByByte(state), transform);
    }

    public void SetInnerTile(PrefabID objectID)
    {
        _innerTiles.nameID = objectID.GUID;
        Instantiate(objectID.gameObject, transform);
    }
}
