using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBuildingPrefabs : MonoBehaviour 
{
    [SerializeField] List<GameObject> floors;

    public GameObject GetBuildingByByte(int tileState)
    {
        switch (tileState)
        {
            case 0://solo
                return floors[0];
            case 336://arriba
                return floors[1];
            case 324://izquierda
                return floors[2];
            case 276://derecha
                return floors[8];
            case 84://abajo
                return floors[4];
            case 320://arriba + izquierda
                return floors[3];
            case 80://arriba + abajo
                return floors[5];
            case 68://izquierda + abajo
                return floors[6];
            case 64://arriba + izquierda + abajo
                return floors[7];
            case 272://arriba + derecha
                return floors[9];
            case 260://derecha + izquierda
                return floors[10];
            case 256://derecha + arriba + izquierda
                return floors[11];
            case 20://abajo + derecha
                return floors[12];
            case 16://arriba + abajo + derecha
                return floors[13];
            case 4://derecha + abajo + izquierda
                return floors[14];
            case 408://puerta vacio arriba
                return floors[15];
            case 406://puerta vacio izquierda
                return floors[16];
            case 404://puerta vacio derecha
                return floors[17];
            case 402://puerta vacio abajo
                return floors[18];
            default:
                return floors[0];
        }
    }
}
