using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private Slider loadingSlider;

    public void LoadScene(string sceneToLoad)
    {
        loadingScreen.SetActive(true);

        StartCoroutine(LoadSceneASync(sceneToLoad));
    }

    IEnumerator LoadSceneASync(string sceneToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / .9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
