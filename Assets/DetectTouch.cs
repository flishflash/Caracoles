using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectTouch : MonoBehaviour
{
    TouchPhase touchPhase = TouchPhase.Began;
    void Update()
    {
        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase) || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("SelectGamemode");
        }
    }
}
