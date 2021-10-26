using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void SettingButtonScript()
    {
        Debug.Log("Setting menu");
    }
    public void ExitButtonScript()
    {
        Debug.Log("Application quit");
        Application.Quit();
    }
   
}
