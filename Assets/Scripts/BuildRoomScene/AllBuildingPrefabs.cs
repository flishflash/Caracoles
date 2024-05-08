using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBuildingPrefabs : MonoBehaviour 
{
    [SerializeField] GameObject floor;

    public GameObject GetBuildingByByte(int tileState)
    {
        switch (tileState)
        {
            case 0://solo
                return floor;
            case 4://arriba
                return floor;
            case 16://izquierda
                return floor;
            case 20://arriba + izquierda
                return floor;
            case 128://abajo
                return floor;
            case 132://arriba + abajo
                return floor;
            case 144://izquierda + abajo
                return floor;
            case 148://arriba + izquierda + abajo
                return floor;
            case 32://derecha
                return floor;
            case 36://arriba + derecha
                return floor;
            case 48://derecha + izquierda
                return floor;
            case 52://derecha + arriba + izquierda
                return floor;
            case 160://abajo + derecha
                return floor;
            case 164://arriba + abajo + derecha
                return floor;
            case 176://derecha + abajo + izquierda
                return floor;
            case 180://arriba + derecha + abajo + izquierda
                return floor;
            default:
                return floor;
        }
    }
}
