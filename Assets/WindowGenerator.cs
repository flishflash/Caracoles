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

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetChildGameObjects(_childsList);

        foreach (GameObject go in _childsList)
        {
            if (go.name == "Wall_Single" && Random.Range(0, 101) <= WindowSpawnRate)
            {
                Instantiate(WindowPrefab, go.transform.position, go.transform.rotation);
                Destroy(go);
            }
        }
    }
}
