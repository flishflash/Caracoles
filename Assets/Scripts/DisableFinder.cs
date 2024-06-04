using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFinder : MonoBehaviour
{
    public void _DisableFinder()
    {
        GameObject.Find("Plane Finder").SetActive(false);
    }
}
