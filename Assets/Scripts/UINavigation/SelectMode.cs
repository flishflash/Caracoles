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

    public AudioSource exitSFX;
    public AudioSource buildRoomSFX;
    public AudioSource buildDungeonSFX;

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
                    StartCoroutine(LoadWithSound(exitSFX, "MainScene"));
                    break;
                case "BuildRoom":
                    StartCoroutine(LoadWithSound(buildRoomSFX, "BuildRoomScene"));
                    break;
                case "BuildDungeon":
                    StartCoroutine(LoadWithSound(buildDungeonSFX, "GameScene", 1));
                    break;
            }
        }
    }
    //Esta función es para que no se pare el audio al cargar la siguiente escena
    public IEnumerator LoadWithSound(AudioSource audio, string scenename, float time = 0.5f) 
    {
        audio.Play();
        yield return new WaitForSeconds(time);
        aSyncLoader.LoadScene(scenename);
    }
}