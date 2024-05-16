using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class AxeBehaviour : TrapBehaviour
{
    public float maxAngle;
    private bool _direction = false; // false = left, true = right

    public override void DoAnimation()
    {
        if (_direction == false)
        {
            if (transform.rotation.eulerAngles.z <= maxAngle || transform.rotation.eulerAngles.z >= 360f - maxAngle)
            {
                transform.rotation = Quaternion.Euler(
                    transform.rotation.eulerAngles.x, 
                    transform.rotation.eulerAngles.y, 
                    transform.rotation.eulerAngles.z - (animationSpeed * Time.deltaTime)
                    );
            }
            else
            {
                transform.rotation = Quaternion.Euler(
                    transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y,
                    360f - maxAngle
                    );

                _direction = true;
            }
        }
        else
        {
            if (transform.rotation.eulerAngles.z <= maxAngle || transform.rotation.eulerAngles.z >= 360f - maxAngle)
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
                    maxAngle - 1
                    );

                _direction = false;
            }
        }
    }
}