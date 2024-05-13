using UnityEngine;

public class Tile : MonoBehaviour
{
    public int tileState = -1;
    public bool isAccesible = true;
    public Vector2Int tilePos = new Vector2Int();
    public innerTiles _innerTiles = new innerTiles();
    GameObject currentFloor;
    GameObject currentInnerTile;

    GetAllPrefabs getAllPrefabs;

    [SerializeField] SpriteRenderer spriteRenderer;
    
    AllBuildingPrefabs allBuildingPrefabs;

    [HideInInspector]
    public string groupID;

    private void Start()
    {
        groupID = "";
        tileState = -1;
        currentFloor = null;
        currentInnerTile = null;
        _innerTiles.nameID = "";
        getAllPrefabs = GameObject.Find("PrefabManager").GetComponent<GetAllPrefabs>();
        allBuildingPrefabs = GameObject.Find("AllBuildingPrefabs").GetComponent<AllBuildingPrefabs>();
    }

    private void Update()
    {
        spriteRenderer.color = isAccesible ? Color.green : Color.grey;
    }

    public bool isOccupied()
    {
        Debug.Log(currentInnerTile != null);
        return currentInnerTile != null;
    }

    public void SetTilePos(Vector2Int pos)
    {
        tilePos = pos;
    }

    public void SetTileState(int state)
    {
        tileState = state;
        if (currentFloor != null) Destroy(currentFloor);
        currentFloor = (tileState == -1) ? null : Instantiate(allBuildingPrefabs.GetBuildingByByte(state), transform);
    }

    public void SetFullTile(roomTiles roomTile, string groupID)
    {
        isAccesible = false;
        this.groupID = groupID;
        SetTileState(roomTile.tileState);
        if (getAllPrefabs.checkAllPrefabs(roomTile._innerTiles.nameID) != null)
        {
            SetInnerTile(getAllPrefabs.checkAllPrefabs(roomTile._innerTiles.nameID).GetComponent<PrefabID>());
        }
    }

    public void ClearTile()
    {
        tileState = -1;
        isAccesible = true;

        if (currentFloor != null) Destroy(currentFloor);
        currentFloor = null;

        _innerTiles.nameID = "";

        if (currentInnerTile != null) Destroy(currentInnerTile);
        currentInnerTile = null;

        groupID = "";
    }

    public void SetInnerTile(PrefabID objectID)
    {
        _innerTiles.nameID = objectID.GUID;
        currentInnerTile = Instantiate(objectID.gameObject, transform);
        currentInnerTile.transform.localPosition = objectID.transform.position;
        currentInnerTile.transform.rotation = objectID.transform.rotation;
    }
}
