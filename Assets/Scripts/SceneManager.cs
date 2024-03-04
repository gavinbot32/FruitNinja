using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

 
    public void LoadScene(string name) {

        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void LoadScene(int buildIndex)
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
