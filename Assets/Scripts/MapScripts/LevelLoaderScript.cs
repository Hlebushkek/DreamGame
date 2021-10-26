using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoaderScript : MonoBehaviour
{
    [SerializeField]
    private Animator transition;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject LoadingScreen;
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadPlayLevel(sceneIndex));
    }
    IEnumerator LoadPlayLevel(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;

            yield return null;
        }
    }
}
