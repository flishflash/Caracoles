using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HammerAnimation : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y, 
            transform.rotation.eulerAngles.z - (speed * Time.deltaTime)
            );
    }
}
