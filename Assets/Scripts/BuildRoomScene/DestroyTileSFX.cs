using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTileSFX : MonoBehaviour
{
    private AudioSource _dSFX;
    private float _lifeTime = 1;
    private float _createTime = 0;

    void Start()
    {
        _dSFX = GetComponent<AudioSource>();
        _createTime = Time.time;
        StartCoroutine(DoSFX());
    }

    IEnumerator DoSFX()
    {
        _dSFX.Play();
        while (_createTime + _lifeTime > Time.time) { yield return null; }
        Destroy(gameObject);
    }
}
