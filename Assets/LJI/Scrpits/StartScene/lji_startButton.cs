using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lji_startButton : MonoBehaviour
{
    public string nextScene="Scene1";

    public void LoadSceneButton()
    {
        Debug.Log("pressButton");
        StartCoroutine(LoadScene());
    }

    public void TestButton()
    {
        Debug.Log("pressButton");
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(nextScene);
        while (!asyncOper.isDone)
        {
            yield return null;
            Debug.Log(asyncOper.progress);
        }
    }

    public void ExitProgram()
    {
        Application.Quit();
    }
}
