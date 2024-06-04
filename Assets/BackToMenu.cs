using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BackToMenu : MonoBehaviour
{
    public void Back()
    {
        StartCoroutine(LoadSceneMenu());
    }
    public IEnumerator LoadSceneMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainScene");
    }
}
