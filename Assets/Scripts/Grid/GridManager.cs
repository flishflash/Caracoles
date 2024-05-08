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
                tiles[x, y] = Instantiate(tilePref, new Vector3(x, 0, y), Quaternion.identity).GetComponent<Tile>();
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
        int pow = 0;
        tile.tileState = 0;

        for (int y = tile.tilePos.y - 1; y <= tile.tilePos.y + 1; y++)
        {
            for (int x = tile.tilePos.y - 1; x <= tile.tilePos.x + 1; x++)
            {
                if (tile.tilePos != new Vector2Int(x, y))
                {
                    pow++;
                    if (((x < width && x >= 0 ) && (y < height && y >= 0)))
                    {
                        if (tiles[x, y].tileState != -1) tile.tileState += (int)Mathf.Pow(2, pow);
                    }
                }
            }
        }

        tile.SetTileState(tile.tileState);
    }

    public void SetAllTilesInOtherGrid(GridManager gridManager, Tile tileStart)
    {

    }

}
