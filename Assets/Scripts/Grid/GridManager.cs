using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private GameObject tilePref;
    public List<Tile> tiles = new List<Tile>();

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = -(width / 2); x <= (width / 2); x++)
        {
            for (int y = -(height / 2); y <= (height / 2); y++)
            {
                var spawnedTile = Instantiate(tilePref, new Vector3(x, 0, y), Quaternion.identity);
                spawnedTile.transform.forward = Vector3.up;
                spawnedTile.transform.parent = gameObject.transform;
                spawnedTile.name = $"Tile {x}{y}";
                tiles.Add(spawnedTile.GetComponent<Tile>());
            }
        }
    }

    public void SetAllToNotAccesible()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].isAccesible = false;
        }
    }
}
