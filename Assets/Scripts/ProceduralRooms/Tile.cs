using UnityEngine;

public class Tile : MonoBehaviour
{
    public int tileState = -1;
    public bool isDoor = false;
    public bool isUsed = false;
    public bool isExit = false;
    public bool isAccesible = true;
    public Vector2Int tilePos = new Vector2Int();
    public innerTiles _innerTiles = new innerTiles();
    GameObject currentFloor;
    GameObject currentInnerTile;

    GetAllPrefabs getAllPrefabs;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem clearParticle;
    
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
        switch (tileState)
        {
            case 402:
            case 404:
            case 406:
            case 408:
                isDoor = true;
                break;
            default:
                isDoor = false;
                break;
        }
    }

    public void SetFullTile(roomTiles roomTile, string groupID)
    {
        isAccesible = false;
        this.groupID = groupID;
        isExit = roomTile.isExit;
        SetTileState(roomTile.tileState);
        if (getAllPrefabs.checkAllPrefabs(roomTile._innerTiles.nameID) != null)
        {
            SetInnerTile(getAllPrefabs.checkAllPrefabs(roomTile._innerTiles.nameID).GetComponent<PrefabID>());
        }
    }

    public void ClearTile(out int doorCountBack, int doorCount)
    {
        doorCountBack = doorCount;
        if (tileState > 400) doorCountBack = doorCount-1;

        tileState = -1;
        isAccesible = true;

        if (currentFloor != null) 
        { 
            Instantiate(clearParticle, transform.position, Quaternion.identity);
            Destroy(currentFloor); 
        }
        currentFloor = null;

        _innerTiles.nameID = "";

        if (currentInnerTile != null)
        {
            Instantiate(clearParticle, transform.position, Quaternion.identity);
            Destroy(currentInnerTile);
            currentInnerTile = null;

            groupID = "";
        }
    }

    public void SetInnerTile(PrefabID objectID)
    {
        _innerTiles.nameID = objectID.GUID;
        currentInnerTile = Instantiate(objectID.gameObject, transform);
        currentInnerTile.transform.localPosition = objectID.transform.position;
        currentInnerTile.transform.rotation = objectID.transform.rotation;
    }
}
