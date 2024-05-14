using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HammerBehaviour : TrapBehaviour
{
    public bool clockwise = false;

    public override void DoAnimation()
    {
        if (clockwise)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z + (animationSpeed * Time.deltaTime)
                );
        }
        else
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x, 
                transform.rotation.eulerAngles.y, 
                transform.rotation.eulerAngles.z - (animationSpeed * Time.deltaTime)
                );
        }
    }
}
