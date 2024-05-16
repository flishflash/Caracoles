using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectMode : MonoBehaviour
{
    //Change me to change the touch me stepbro.
    TouchPhase touchPhase = TouchPhase.Began;

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            mousePick(ray);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            mousePick(ray);
        }
    }

    void mousePick(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            switch (hit.collider.gameObject.name)
            {
                case "Exit":
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

                    break;
                case "BuildRoom":
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                    break;
                case "BuildDungeon":
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

                    break;
            }
        }
    }
}