using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource playSFX;

    public void PlayGame()
    {
        StartCoroutine(LoadGameScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadGameScene()
    {
        playSFX.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
