using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class WindowGenerator : MonoBehaviour
{
    public GameObject WindowPrefab;

    [Range(0, 100)]
    public float WindowSpawnRate;

    private List<GameObject> _childsList = new List<GameObject>();

    private void Awake()
    {
        gameObject.GetChildGameObjects(_childsList);

        foreach (GameObject go in _childsList)
        {
            if (go.name == "Wall_Single" && UnityEngine.Random.Range(0, 101) <= WindowSpawnRate)
            {
                GameObject win = Instantiate(WindowPrefab, go.transform.position, go.transform.rotation);
                win.transform.parent = transform;
                win.transform.localScale = Vector3.one;
                Destroy(go);
            }
        }
    }
}
