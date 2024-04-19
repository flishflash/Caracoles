using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAllPrefabs : MonoBehaviour
{
    [SerializeField] List<GameObject> allPrefabs = new List<GameObject>();

    public GameObject checkAllPrefabs(string id)
    {
        foreach (var item in allPrefabs)
        {
            if (item.GetComponent<PrefabID>().CheckIfThisprefab(id)) return item;
        }

        return null;
    }
}
