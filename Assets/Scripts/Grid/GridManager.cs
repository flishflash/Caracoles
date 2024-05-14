using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public int width, height;
    [SerializeField] private GameObject tilePref;
    public Tile[,] tiles;

    private void Start()
    {
        tiles = new Tile[width, height];
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = Instantiate(tilePref, new Vector3(x, 0, y) + transform.position, Quaternion.identity).GetComponent<Tile>();
                tiles[x, y].transform.forward = Vector3.up;
                tiles[x, y].transform.parent = gameObject.transform;
                tiles[x, y].name = $"Tile {x}{y}";
                tiles[x, y].SetTilePos(new Vector2Int(x, y));
            }
        }
    }

    public void SetAllToNotAccesible()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y].isAccesible = false;
            }
        }
        CalculateAllTileState();
    }

    public void ErraseRoom(string groupID)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y].groupID == groupID && groupID != "") tiles[x, y].ClearTile();
            }
        }
    }

    void SetAllToAccesible()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y].isAccesible = true;
            }
        }
    }

    void CalculateAllTileState()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y].tileState != -1) TileSorrounding(tiles[x, y]);
            }
        }
    }

    void TileSorrounding(Tile tile)
    {
        if (tile.tileState < 400)
        {
            int pow = 0;
            tile.tileState = 0;

            for (int y = tile.tilePos.y - 1; y <= tile.tilePos.y + 1; y++)
            {
                for (int x = tile.tilePos.x - 1; x <= tile.tilePos.x + 1; x++)
                {
                    pow++;
                    if (((x < width && x >= 0) && (y < height && y >= 0)) &&
                        tiles[x, y].tileState != -1 && pow % 2 == 0
                        && tile.tilePos != new Vector2Int(x, y))
                    {
                        tile.tileState += (int)Mathf.Pow(2, pow);
                    }
                }
            }
        }

        tile.SetTileState(tile.tileState);
    }

    public bool CheckIfDoorPossible(Tile tile)
    {
        int pow = 0;

        for (int y = tile.tilePos.y - 1; y <= tile.tilePos.y + 1; y++)
        {
            for (int x = tile.tilePos.x - 1; x <= tile.tilePos.x + 1; x++)
            {
                if (((x < width && x >= 0) && (y < height && y >= 0)) &&
                    tiles[x, y].tileState != -1 && pow % 2 == 0
                    && tile.tilePos != new Vector2Int(x, y))
                {
                    pow++;
                }
                if (tiles[x, y].isDoor) return false;
            }
        }

        if (pow > 1 || pow == 0) return false;
        else return true;
    }

    public int CheckDoorOrientation(Tile tile)
    {
        int i = -1;
        int pow = 400;

        for (int y = tile.tilePos.y - 1; y <= tile.tilePos.y + 1; y++)
        {
            for (int x = tile.tilePos.x - 1; x <= tile.tilePos.x + 1; x++)
            {
                pow++;
                if (((x < width && x >= 0) && (y < height && y >= 0)) &&
                    tiles[x, y].tileState != -1 && pow % 2 == 0
                    && tile.tilePos != new Vector2Int(x, y))
                {
                    i = pow;
                }
            }
        }

        return i;
    }

    public void  CheckIfBoardEmpty()
    {
        bool ret = true;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y].tileState != -1) ret = false;
            }
        }

        if (ret) SetAllToAccesible();
    }

    public void SetAllTilesInOtherGrid(fatherRoom room, Tile tileStart)
    {
        string GUID = System.Guid.NewGuid().ToString();
        Vector2Int roomSize = new Vector2Int(room.width/2, room.height/2);
        Vector2Int pos;

        for (int i = 0; i < room.children.Count; i++)
        {
            pos = tileStart.tilePos + room.children[i].tilePos - roomSize;
            if (pos.x >= width || pos.y >= height || tiles[pos.x, pos.y].tileState != -1)
            {
                StartCoroutine(RoomOutOfBounds(GUID));
                break;
            }

            if (room.children[i].tileState != -1)
                tiles[pos.x, pos.y].SetFullTile(room.children[i], GUID);
        }
    }

    IEnumerator RoomOutOfBounds(string groupID)
    {
        //Aviso UI
        Debug.Log("OUT OF BOUNDS");

        ErraseRoom(groupID);
        yield return null;

        //Apagar Aviso UI
    }
}
