using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StatsScript : MonoBehaviour
{
    public int hp = 2;
    public int dmg = 1;
    public float spe = 1f; // tiempo que tarda en atacar, en milisegundos

    public GameObject dieSFX;

    void Start()
    {
        
    }

    void Update()
    {
        if (hp <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                Debug.Log("Defeat...");
                //LoadScene
                SceneManager.LoadScene("LoseScene");
            }
            Instantiate(dieSFX);
            Destroy(gameObject);
        }
    }
}
