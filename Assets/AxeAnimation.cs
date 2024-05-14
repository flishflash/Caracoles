using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AxeAnimation : MonoBehaviour
{
    public float speed;
    public float maxAngle;

    private bool _direction = false; // false = left, true = right
    private float _currentAngle;

    // Update is called once per frame
    void Update()
    {
        if (_direction == false)
        {
            if (transform.rotation.eulerAngles.z <= maxAngle || transform.rotation.eulerAngles.z >= 360f - maxAngle)
            {
                transform.rotation = Quaternion.Euler(
                    transform.rotation.eulerAngles.x, 
                    transform.rotation.eulerAngles.y, 
                    transform.rotation.eulerAngles.z - (speed * Time.deltaTime)
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
                    transform.rotation.eulerAngles.z + (speed * Time.deltaTime)
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