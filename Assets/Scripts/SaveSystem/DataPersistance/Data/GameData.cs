using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct childObjRoom
{
    public string nameID;

    public Vector3 p;
    public Vector3 r;
    public Vector3 s;
}

[System.Serializable]
public struct fatherRoom
{
    public List<childObjRoom> children;
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
