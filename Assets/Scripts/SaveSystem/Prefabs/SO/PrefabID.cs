using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabID : MonoBehaviour
{
    public string GUID;
    [HideInInspector]
    public GameObject prefab;

    [ContextMenu("Generate GUID")]
    private void GenerateGuid()
    {
        GUID = System.Guid.NewGuid().ToString();
    }

    private void Start()
    {
        prefab = gameObject;
    }

    public bool CheckIfThisprefab(string uid)
    {
        return string.Compare(GUID, uid) == 0;
    }
}
