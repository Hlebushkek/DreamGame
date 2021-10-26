using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMoonScript : MonoBehaviour
{
    [SerializeField]
    GameObject levelloader;
    [SerializeField]
    int levelIndex;
    private void OnTriggerStay2D(Collider2D BrigeToNextLevel)
    {
        if (BrigeToNextLevel.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            levelloader.GetComponent<LevelLoaderScript>().LoadLevel(levelIndex);
        }
    }
}
