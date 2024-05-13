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
            case 4://arriba
                return floors[1];
            case 16://izquierda
                return floors[2];
            case 20://arriba + izquierda
                return floors[3];
            case 256://abajo
                return floors[4];
            case 260://arriba + abajo
                return floors[5];
            case 272://izquierda + abajo
                return floors[6];
            case 276://arriba + izquierda + abajo
                return floors[7];
            case 32://derecha
                return floors[8];
            case 36://arriba + derecha
                return floors[9];
            case 48://derecha + izquierda
                return floors[10];
            case 52://derecha + arriba + izquierda
                return floors[11];
            case 288://abajo + derecha
                return floors[12];
            case 292://arriba + abajo + derecha
                return floors[13];
            case 304://derecha + abajo + izquierda
                return floors[14];
            default:
                return floors[0];
        }
    }
}
