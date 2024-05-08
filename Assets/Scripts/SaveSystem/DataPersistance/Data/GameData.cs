using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct roomTiles
{
    public string nameID;

    public Vector2Int tilePos;

    public byte tileState;

    public innerTiles _innerTiles;
}

[System.Serializable]
public struct innerTiles
{
    public string nameID;
}

[System.Serializable]
public struct fatherRoom
{
    public List<roomTiles> children;
    public int width, height;
    public string name;
}

[System.Serializable]
public class GameData 
{
    //Aqui se ponen todos los valores que se quieren guardar
    public List<fatherRoom> allCraftedRooms;

    public GameData()
    {
        allCraftedRooms = new List<fatherRoom>();
    }
}
