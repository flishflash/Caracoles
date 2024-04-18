using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccuped = false;
    public bool isAccesible = true;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (isAccesible)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.grey;
        }
    }
}
