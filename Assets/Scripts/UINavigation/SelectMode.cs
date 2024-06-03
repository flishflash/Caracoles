using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectMode : MonoBehaviour
{
    //Change me to change the touch me stepbro.
    TouchPhase touchPhase = TouchPhase.Began;

    private ASyncLoader aSyncLoader;
    public GameObject aSyncObject;

    // Update is called once per frame
    private void Start()
    {
        aSyncLoader = aSyncObject.GetComponent<ASyncLoader>();
    }
    void Update()
    {

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Debug.Log("");
            mousePick(ray);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("");
            mousePick(ray);
        }
    }

    void mousePick(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            switch (hit.collider.gameObject.name)
            {
                case "Exit":
                    aSyncLoader.LoadScene("MainScene");

                    break;
                case "BuildRoom":
                    aSyncLoader.LoadScene("BuildRoomScene");

                    break;
                case "BuildDungeon":
                    aSyncLoader.LoadScene("GameScene");

                    break;
            }
        }
    }
}