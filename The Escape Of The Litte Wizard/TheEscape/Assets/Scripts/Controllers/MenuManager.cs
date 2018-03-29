using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    //loads the scene
    public void LoadScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }

    //closes the application
    public void doquit()
    {
        Debug.Log("Application quit");
        Application.Quit();
    }
}
