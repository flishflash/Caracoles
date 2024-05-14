using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBehaviour : TrapBehaviour
{
    public bool clockwise = false;

    public override void DoAnimation()
    {
        if (clockwise)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + (animationSpeed * Time.deltaTime),
                transform.rotation.eulerAngles.z
                );
        }
        else
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y - (animationSpeed * Time.deltaTime),
                transform.rotation.eulerAngles.z
                );
        }
    }
}