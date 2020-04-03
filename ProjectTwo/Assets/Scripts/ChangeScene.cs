using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void changeWaveMenu(string sceneName)
    {
          Global.mode = GameMode.wave;
        SceneManager.LoadScene(sceneName);
    }

    public void changeEndlessMenu(string sceneName)
    {
        Global.mode = GameMode.endless;
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Default changeMenu so I don't have to change any other references to this script
    /// </summary>
    /// <param name="sceneName"></param>
    public void changeMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
