using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lji_deadSceneManager : MonoBehaviour
{
    public string nextScene = "Scene1";

    private void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("StatusManager"));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Debug.Log("test");
            //LoadScene();
            SceneManager.LoadScene("Scene1");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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
}